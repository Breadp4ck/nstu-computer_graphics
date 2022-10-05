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

                // out vec3 Normal;
                // out vec3 FragPos;
                // out vec2 TexCoords;

                void main() {
                    gl_Position = projection * view * model * vec4(aPos, 1.0);

                    // FragPos = vec3(model * view * vec4(aPos, 1.0));
                    // Normal = mat3(transpose(inverse(model * view))) * aNormal;
                    // TexCoords = aTexCoords;
                }
            ";

            string fragColorSource = @"#version 330 core
                out vec4 FragColor;

                // struct Material {
                //     sampler2D diffuse;
                //     vec3 specular;
                //     float shininess;
                // };

                // struct DirLight {
                //     vec3 direction;

                //     vec3 ambient;
                //     vec3 diffuse;
                //     vec3 specular;
                // };

                // uniform Material material;
                // uniform DirLight dirLight;

                // in vec3 Normal;
                // in vec3 FragPos;
                // in vec2 TexCoords;

                // vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir);

                uniform vec4 color;

                void main() {
                    FragColor = vec4(color);

                    // // properties
                    // vec3 norm = normalize(Normal);
                    // vec3 viewDir = normalize(-FragPos);
                    // vec3 result = vec3(0.0);

                    // // directional lighting
                    // result = CalcDirLight(dirLight, norm, viewDir);

                    // FragColor = vec4(result, 1.0);
                }

                // vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir) {
                //     vec3 lightDir = normalize(-light.direction);

                //     // diffuse shading
                //     float diff = max(dot(normal, lightDir), 0.0);

                //     // specular shading
                //     vec3 reflectDir = reflect(-lightDir, normal);
                //     float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

                //     // combine results
                //     vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));
                //     vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));
                //     vec3 specular = light.specular * spec * material.specular;

                //     return (ambient + diffuse + specular);
                // }
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