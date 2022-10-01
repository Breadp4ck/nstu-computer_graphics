using Silk.NET.OpenGL;
using System.Numerics;

namespace Lab1.Render
{
    public class RenderServer
    {
        private GL _gl;
        private ShaderContext _shaderContext;

        public RenderServer(GL gl)
        {
            _gl = gl;
            _shaderContext = new ShaderContext(_gl);
        }

        public void Load(IRenderable renderable)
        {
            BufferObject<float> vbo = new BufferObject<float>(_gl, renderable.Vertices, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);

            VertexArrayObject<float> vao = new VertexArrayObject<float>(_gl, vbo, VertexAttribPointerType.Float);
            vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);

            renderable.Initialize(_shaderContext, vao, vbo);
        }

        public void Render(Viewport viewport, IRenderable renderable)
        {
            if ((viewport.Camera.VisualMask & renderable.VisualMask) > 0)
            {
                renderable.Vao!.Bind();
                _gl.PointSize(100.0f);

                renderable.Material.Use(viewport, renderable.Transform);
                // renderable.Draw(viewport.Camera);

                _gl.DrawArrays(PrimitiveType.Points, 0, (uint)renderable.Vertices.Length / 3);
            }
        }

        public void ChangeContextSize(Vector2 size)
        {
            _gl.Viewport(0, 0, (uint)size.X, (uint)size.Y);
        }
    }
}