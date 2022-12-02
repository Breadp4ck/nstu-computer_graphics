using Silk.NET.OpenGL;
using System.Numerics;

using Lab1.Math;
using Lab1.Core.Shaders;

using L1Shader = Lab1.Core.Shaders.Shader;

namespace Lab1.Core
{
    public class CellField
    {
        public Transform Transform = new Transform();
        private GL _context;
        private VertexArrayObject<float> _vao;
        private BufferObject<float> _vbo;
        private L1Shader _shader;
        private float[] _vertices;
        private float _scale = 2.0f;


        public CellField(GL context)
        {
            _context = context;
            _shader = ShaderLibrary.CellFieldShader(context);
            _vertices = new float[30] {
            //  Position               Tex Coords
                _scale * -1.0f, _scale * -1.0f, -1.0f,   -1.0f, -1.0f,
                _scale * -1.0f, _scale * +1.0f, -1.0f,   -1.0f, +1.0f,
                _scale * +1.0f, _scale * +1.0f, -1.0f,   +1.0f, +1.0f,
                _scale * +1.0f, _scale * +1.0f, -1.0f,   +1.0f, +1.0f,
                _scale * +1.0f, _scale * -1.0f, -1.0f,   +1.0f, -1.0f,
                _scale * -1.0f, _scale * -1.0f, -1.0f,   -1.0f, -1.0f
            };

            _vbo = new BufferObject<float>(context, _vertices, BufferTargetARB.ArrayBuffer);

            _vao = new VertexArrayObject<float>(context, _vbo);
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            _vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);
        }

        public void Draw(Camera camera, Transform layerTransform)
        {
            _vao.Bind();

            _vbo.Update(_vertices);
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            _vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            _shader.Use();
            _shader.SetUniform(
                "scale",
                camera.Transform.Scale.X
            );
            _shader.SetUniform(
                "offset",
                -camera.Transform.Position.X / _scale,
                -camera.Transform.Position.Y / _scale,
                -camera.Transform.Position.Z
            );

            Matrix4x4 model = Matrix4x4.Identity;
            Matrix4x4 view = Matrix4x4.Identity;
            Matrix4x4 projection = Matrix4x4.CreateOrthographic(2.0f, 2.0f * camera.ViewportRatioYX, 1e-3f, 1e2f);

            _shader.SetUniform("model", model);
            _shader.SetUniform("view", view);
            _shader.SetUniform("projection", projection);

            _context.DrawArrays(PrimitiveType.Triangles, 0, (uint)_vertices.Length / 3);
        }
    }
}