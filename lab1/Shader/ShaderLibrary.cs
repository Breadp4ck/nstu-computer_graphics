using Silk.NET.OpenGL;

static class ShaderLibrary
{
    public static Shader ColorShader(GL context)
    {
        string vertColorSource = @"#version 330 core
            layout (location = 0) in vec3 vPos;

            void main() {
                gl_Position = vec4(vPos, 1.0);
            }
        ";

        string fragColorSource = @"#version 330 core
            out vec4 FragColor;

            uniform vec3 color;

            void main() {
                FragColor = vec4(color, 1.0f);
            }
        ";

        return new Shader(context, vertColorSource, fragColorSource);
    }
}