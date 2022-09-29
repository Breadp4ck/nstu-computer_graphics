using Lab1.Render;

namespace Lab1.Core.Shaders
{
    static class ShaderLibrary
    {
        public static ShaderProgram ColorShader(ShaderContext context)
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

            Shader vert = new Shader(context, ShaderType.VertexShader, vertColorSource);
            Shader frag = new Shader(context, ShaderType.FragmentShader, fragColorSource);

            ShaderProgram program = new ShaderProgram(context);
            program.AttachShader(vert);
            program.AttachShader(frag);

            return program;
        }
    }
}