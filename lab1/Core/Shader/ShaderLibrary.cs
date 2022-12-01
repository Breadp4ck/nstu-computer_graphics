using Silk.NET.OpenGL;

namespace Lab1.Core.Shaders
{
    static class ShaderLibrary
    {
        public static Shader ColorShader(GL context)
        {
            string vertColorSource = @"#version 330 core
                layout (location = 0) in vec3 vPos;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                void main() {
                    gl_Position = projection * view * model * vec4(vPos, 1.0);
                }
            ";

            string fragColorSource = @"#version 330 core
                out vec4 FragColor;

                uniform vec4 color;

                void main() {
                    FragColor = vec4(color);
                }
            ";

            return new Shader(context, vertColorSource, fragColorSource);
        }

        public static Shader OriginShader(GL context)
        {
            string vertColorSource = @"#version 330 core
                layout (location = 0) in vec3 vPos;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                out vec2 TexCoords;

                void main() {
                    gl_Position = projection * view * model * vec4(vPos, 1.0);

                    // TODO: this is crap, better to create tex coords in main program
                    TexCoords = vPos.xy;
                }
            ";

            string fragColorSource = @"#version 330 core
                out vec4 FragColor;

                in vec2 TexCoords;

                void main() {
                    float radius = 0.01;
                    float thickness = radius / 3.0;

                    float distance = sqrt(dot(TexCoords, TexCoords));

                    if (distance < radius - thickness) {
                        FragColor = vec4(0.95, 0.6, 0.0, 1.0);

                    } else if (distance < radius) {
                        FragColor = vec4(vec3(0.05), 1.0);

                    } else {
                        FragColor = vec4(0.0);
                    }
                }
            ";

            return new Shader(context, vertColorSource, fragColorSource);
        }
    }
}