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

                    float constant;
                    float linear;
                    float quadratic;
                };

                struct Camera {
                    vec3 position;
                };

                #define CNT_POINT_LIGHTS 16

                uniform Env env;
                uniform Material material;
                uniform DirLight dirLight;
                uniform PointLight pointLights[CNT_POINT_LIGHTS];
                uniform Camera camera;

                in vec3 Normal;
                in vec3 FragPos;
                in vec2 TexCoords;

                vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir);
                vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir);

                void main() {
                    // properties
                    vec3 normal = normalize(Normal);
                    vec3 viewDir = normalize(camera.position - FragPos);

                    // ambient color
                    vec3 result = env.ambient * material.diffuse;

                    // Directional Light
                    result += CalcDirLight(dirLight, normal, viewDir);

                    for (int lightID = 0; lightID < CNT_POINT_LIGHTS; lightID++) {
                        result += CalcPointLight(pointLights[lightID], normal, FragPos, viewDir);
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

                    return attenuation * (diffuse + specular);
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