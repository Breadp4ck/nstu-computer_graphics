namespace Lab1.Render
{
    static class ShaderLibrary
    {
        public static ShaderProgram ColorShader(ShaderContext context)
        {
            string vertColorSource = @"#version 330 core
                layout (location = 0) in vec3 aPos;
                layout (location = 1) in vec3 aNormal;
                layout (location = 2) in vec2 aTexCoords;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                void main() {
                    gl_Position = projection * view * model * vec4(aPos, 1.0);
                }
            ";

            string fragColorSource = @"#version 330 core
                out vec4 FragColor;

                struct Material {
                    vec4 color;
                };

                uniform Material material;

                void main() {
                    FragColor = material.color;
                }
            ";

            Shader vert = new Shader(context, ShaderType.VertexShader, vertColorSource);
            Shader frag = new Shader(context, ShaderType.FragmentShader, fragColorSource);

            ShaderProgram program = new ShaderProgram(context);
            program.AttachShader(vert);
            program.AttachShader(frag);

            return program;
        }

        public static ShaderProgram PbrShader(ShaderContext context)
        {
            string vertColorSource = @"#version 330 core
                layout (location = 0) in vec3 aPos;
                layout (location = 1) in vec3 aNormal;
                layout (location = 2) in vec2 aTexCoords;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                out vec3 Normal;
                out vec3 FragPos;
                out vec2 TexCoords;

                void main() {
                    gl_Position = projection * view * model * vec4(aPos, 1.0);

                    FragPos = vec3(model * vec4(aPos, 1.0));
                    Normal = mat3(transpose(inverse(model))) * aNormal;
                    TexCoords = aTexCoords;
                }
            ";

            string fragColorSource = @"#version 330 core
                out vec4 FragColor;

                struct Material {
                    vec3 diffuse;
                    vec3 specular;
                    float shininess;
                };

                struct Env {
                    vec3 ambient;
                };

                struct DirLight {
                    vec3 direction;
                    float strength;

                    vec3 diffuse;
                    vec3 specular;
                };

                struct PointLight {
                    vec3 position;

                    vec3 diffuse;
                    vec3 specular;
                    float strength;

                    float constant;
                    float linear;
                    float quadratic;
                };

                struct SpotLight {
                    vec3 position;
                    vec3 direction;
                    float cutOff;
                    float outerCutOff;

                    vec3 diffuse;
                    vec3 specular;
                    float strength;

                    float constant;
                    float linear;
                    float quadratic;
                };

                struct Camera {
                    vec3 position;
                };

                #define CNT_DIR_LIGHTS 2
                #define CNT_POINT_LIGHTS 16
                #define CNT_SPOT_LIGHTS 8

                uniform Env env;
                uniform Material material;
                uniform DirLight dirLights[CNT_DIR_LIGHTS];
                uniform PointLight pointLights[CNT_POINT_LIGHTS];
                uniform SpotLight spotLights[CNT_SPOT_LIGHTS];
                uniform Camera camera;

                in vec3 Normal;
                in vec3 FragPos;
                in vec2 TexCoords;

                vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir);
                vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir);
                vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir);

                void main() {
                    // properties
                    vec3 normal = normalize(Normal);
                    vec3 viewDir = normalize(camera.position - FragPos);

                    // ambient color
                    vec3 result = env.ambient * material.diffuse;

                    // Directional Lights
                    for (int lightIdx = 0; lightIdx < CNT_DIR_LIGHTS; lightIdx++) {
                        result += CalcDirLight(dirLights[lightIdx], normal, viewDir);
                    }

                    // Point Lights
                    for (int lightIdx = 0; lightIdx < CNT_POINT_LIGHTS; lightIdx++) {
                        result += CalcPointLight(pointLights[lightIdx], normal, FragPos, viewDir);
                    }

                    // Spot Lights
                    for (int lightIdx = 0; lightIdx < CNT_SPOT_LIGHTS; lightIdx++) {
                        result += CalcSpotLight(spotLights[lightIdx], normal, FragPos, viewDir);
                    }
                    
                    FragColor = vec4(result, 1.0);
                }

                vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir) {
                    vec3 lightDir = normalize(-light.direction);

                    // diffuse shading
                    float diff = max(dot(normal, lightDir), 0.0);

                    // specular shading
                    vec3 reflectDir = reflect(-lightDir, normal);
                    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

                    vec3 diffuse = light.diffuse * diff * material.diffuse;
                    vec3 specular = light.specular * spec * material.specular;

                    return light.strength * (diffuse + specular);
                }

                vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir) {
                    vec3 lightDir = normalize(light.position - fragPos);

                    // diffuse shading
                    float diff = max(dot(normal, lightDir), 0.0);

                    // specular shading
                    vec3 reflectDir = reflect(-lightDir, normal);
                    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

                    // attenuation
                    float distance = length(light.position - fragPos);
                    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));
                    
                    // combine results
                    vec3 diffuse = light.diffuse * diff * material.diffuse;
                    vec3 specular = light.specular * spec * material.specular;

                    return light.strength * attenuation * (diffuse + specular);
                }

                vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir) {
                    vec3 lightDir = normalize(light.position - FragPos);

                    float theta = dot(lightDir, normalize(-light.direction));

                    // Return just ambient, if fragment is not in the angle
                    if (theta <= light.outerCutOff) {
                        return vec3(0.0);
                    }

                    float epsilon = light.cutOff - light.outerCutOff;
                    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);  

                    float distance = length(light.position - FragPos);
                    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));
                    
                    // data for diffuse 
                    float diff = max(dot(normal, lightDir), 0.0);
                    
                    // data for specular
                    vec3 reflectDir = reflect(-lightDir, normal);
                    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

                    // combine results
                    vec3 diffuse = light.diffuse * diff * material.diffuse;
                    vec3 specular = light.specular * spec * material.specular;

                    diffuse *= attenuation * intensity;
                    specular *= attenuation * intensity;
                        
                    return light.strength * (diffuse + specular);
                }
            ";

            Shader vert = new Shader(context, ShaderType.VertexShader, vertColorSource);
            Shader frag = new Shader(context, ShaderType.FragmentShader, fragColorSource);

            ShaderProgram program = new ShaderProgram(context);
            program.AttachShader(vert);
            program.AttachShader(frag);

            return program;
        }
    }
}