using Silk.NET.OpenGL;
using System.Numerics;

using Lab1.Math;
using Lab1.Core.Shaders;

using L1Shader = Lab1.Core.Shaders.Shader;

namespace Lab1.Core
{
    public class Origin
    {
        public Transform Transform = new Transform();
        private GL _context;
        private VertexArrayObject<float> _vao;
        private BufferObject<float> _vbo;
        private L1Shader _shader;
        private float[] _vertices;


        public Origin(GL context)
        {
            _context = context;
            _shader = ShaderLibrary.OriginShader(context);
            _vertices = new float[18] {
                -1.0f, -1.0f, -1.0f,
                -1.0f, 1.0f, -1.0f,
                1.0f, 1.0f, -1.0f,
                1.0f, 1.0f, -1.0f,
                1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
            };

            _vbo = new BufferObject<float>(context, _vertices, BufferTargetARB.ArrayBuffer);

            _vao = new VertexArrayObject<float>(context, _vbo);
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);
        }

        public void Draw(Camera camera, Transform layerTransform)
        {
            _vao.Bind();

            _vbo.Update(_vertices);
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);

            _shader.Use();

            Transform modelUnscaled = layerTransform;
            modelUnscaled.Position = new Vector3(
                modelUnscaled.Position.X * camera.Transform.Scale.X,
                modelUnscaled.Position.Y * camera.Transform.Scale.Y,
                modelUnscaled.Position.Z
            );
            modelUnscaled.Scale = Vector3.One;

            Transform viewUnscaled = camera.Transform;
            viewUnscaled.Scale = Vector3.One;

            Matrix4x4 model = modelUnscaled.ViewMatrix;
            Matrix4x4 view = viewUnscaled.ViewMatrix;
            Matrix4x4 projection = Matrix4x4.CreateOrthographic(2.0f, 2.0f * camera.ViewportRatioYX, 1e-3f, 1e2f);

            _shader.SetUniform("model", model);
            _shader.SetUniform("view", view);
            _shader.SetUniform("projection", projection);

            _context.DrawArrays(PrimitiveType.Triangles, 0, (uint)_vertices.Length / 3);
        }
    }
}