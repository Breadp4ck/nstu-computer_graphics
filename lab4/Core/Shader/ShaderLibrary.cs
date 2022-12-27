using Silk.NET.OpenGL;

namespace Lab1.Core.Shaders
{
    static class ShaderLibrary
    {
        public static Shader ColorShader(GL context)
        {
            string vertSource = @"#version 330 core
                layout (location = 0) in vec3 vPos;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                void main() {
                    gl_Position = projection * view * model * vec4(vPos, 1.0);
                }
            ";

            string fragSource = @"#version 330 core
                out vec4 FragColor;

                uniform vec4 color;

                void main() {
                    FragColor = vec4(color);
                }
            ";

            return new Shader(context, vertSource, fragSource);
        }

        public static Shader OriginShader(GL context)
        {
            string vertSource = @"#version 330 core
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

            string fragSource = @"#version 330 core
                out vec4 FragColor;

                in vec2 TexCoords;

                void main() {
                    float radius = 0.006;
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

            return new Shader(context, vertSource, fragSource);
        }

        public static Shader CellFieldShader(GL context)
        {
            string vertSource = @"#version 330 core
                layout (location = 0) in vec3 vPos;
                layout (location = 1) in vec2 vTexCoords;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                out vec2 TexCoords;

                void main() {
                    gl_Position = projection * view * model * vec4(vPos, 1.0);
                    TexCoords = vTexCoords;
                }
            ";

            string fragSource = @"#version 330 core
                out vec4 FragColor;

                in vec2 TexCoords;

                uniform float scale;
                uniform vec3 offset;

                void main() {
                    float thickness = 0.001;
                    float interval = 0.05 * scale;

                    float x = TexCoords.x + offset.x;
                    float y = TexCoords.y + offset.y;

                    if (y <= thickness && y >= -thickness) {
                        FragColor = vec4(0.95, 0.4, 0.5, 0.5);

                    } else if (x <= thickness && x >= -thickness) {
                        FragColor = vec4(0.4, 0.9, 0.7, 0.5);

                    } else if (scale <= 0.75 && (mod(x, interval * 10.0) + thickness <= thickness * 2 || mod(y, interval * 10.0) + thickness <= thickness * 2)) {
                        FragColor = vec4(0.6, 0.6, 0.7, 0.05 * (scale + 2.00));

                    } else if (mod(x, interval) + thickness <= thickness * 2 || mod(y, interval) + thickness <= thickness * 2) {
                        FragColor = vec4(0.6, 0.6, 0.7, 0.10 * scale);

                    } else if (mod(x, interval / 10.0) + thickness <= thickness * 2 || mod(y, interval / 10.0) + thickness <= thickness * 2) {
                        FragColor = vec4(0.6, 0.6, 0.7, 0.05 * (scale - 0.50));

                    } else {
                        FragColor = vec4(0.0);
                    }
                }
            ";

            return new Shader(context, vertSource, fragSource);
        }

        public static Shader QuibicBezierShader(GL context)
        {
            string vertSource = @"#version 330 core
                layout (location = 0) in vec3 vPos;
                layout (location = 1) in vec2 vTexCoords;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                void main() {
                    gl_Position = projection * view * model * vec4(vPos, 1.0);
                }
            ";

            string geomSource = @"#version 330 core
                layout (lines_adjacency) in;
                layout (line_strip, max_vertices = 30) out;

                vec4 to_bezier(float t, vec4 p1, vec4 p2, vec4 p3, vec4 p4) {
                    return     p1 * ( (1-t) * (1-t) * (1-t) ) +
                           3 * p2 * (   t   * (1-t) * (1-t) ) +
                           3 * p3 * (   t   *   t   * (1-t) ) +
                               p4 * (   t   *   t   *   t   ) ;
                }

                void main() {
                    int segments = 30;

                    for (int segment = 0; segment < segments; segment++) {
                        gl_Position = to_bezier(
                            float(segment) / float(segments - 1),
                            gl_in[0].gl_Position,
                            gl_in[1].gl_Position,
                            gl_in[2].gl_Position,
                            gl_in[3].gl_Position
                        );
                        EmitVertex();
                    }

                    EndPrimitive();
                } 
            ";

            string fragSource = @"#version 330 core
                out vec4 FragColor;

                uniform vec4 color;

                void main() {
                    FragColor = vec4(color);
                }
            ";

            return new Shader(context, vertSource, fragSource, geomSource);
        }

        public static Shader SelectablePointShader(GL context)
        {
            string vertSource = @"#version 330 core
                layout (location = 0) in vec3 vPos;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                void main() {
                    gl_Position = projection * view * model * vec4(vPos, 1.0);
                }
            ";

            string geomSource = @"#version 330 core
                layout (points) in;
                layout (triangle_strip, max_vertices = 4) out;

                out vec2 TexCoords;

                uniform float yx_scale_factor;

                void main() {
                    float length = 0.020;

                    vec4 p1 = gl_in[0].gl_Position;
                    vec4 p2 = gl_in[0].gl_Position;
                    vec4 p3 = gl_in[0].gl_Position;
                    vec4 p4 = gl_in[0].gl_Position;

                    p1.x -= length * yx_scale_factor; p1.y -= length;
                    p2.x += length * yx_scale_factor; p2.y -= length;
                    p3.x -= length * yx_scale_factor; p3.y += length;
                    p4.x += length * yx_scale_factor; p4.y += length;

                    gl_Position = p1;
                    TexCoords = vec2(-1.0, -1.0);
                    EmitVertex();
                    
                    gl_Position = p2;
                    TexCoords = vec2(1.0, -1.0);
                    EmitVertex();

                    gl_Position = p3;
                    TexCoords = vec2(-1.0, 1.0);
                    EmitVertex();

                    gl_Position = p4;
                    TexCoords = vec2(1.0, 1.0);
                    EmitVertex();

                    EndPrimitive();
                } 
            ";

            string fragSource = @"#version 330 core
                out vec4 FragColor;

                in vec2 TexCoords;

                uniform vec3 color;

                void main() {
                    float radius = 0.5;
                    float thickness = radius / 4.0;

                    float distance = sqrt(dot(TexCoords, TexCoords));

                    if (distance < radius - thickness) {
                        FragColor = vec4(color, 0.4);

                    } else if (distance < radius) {
                        FragColor = vec4(vec3(1.0) - color, 0.9);

                    } else {
                        FragColor = vec4(0.0);
                    }
                }
            ";

            return new Shader(context, vertSource, fragSource, geomSource);
        }

        public static Shader LineShader(GL context)
        {
            string vertSource = @"#version 330 core
                layout (location = 0) in vec3 vPos;
                layout (location = 1) in vec2 vTexCoords;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                void main() {
                    gl_Position = projection * view * model * vec4(vPos, 1.0);
                }
            ";

            string fragSource = @"#version 330 core
                out vec4 FragColor;

                uniform vec3 color;

                void main() {
                    FragColor = vec4(color, 0.25);
                }
            ";

            return new Shader(context, vertSource, fragSource);
        }
    }
}