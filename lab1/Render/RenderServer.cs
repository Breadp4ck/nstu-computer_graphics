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
            BufferObject<ushort> ebo = new BufferObject<ushort>(_gl, renderable.Indices, BufferTargetARB.ElementArrayBuffer, BufferUsageARB.DynamicCopy);

            VertexArrayObject<float> vao = new VertexArrayObject<float>(_gl, vbo, ebo, VertexAttribPointerType.Float);
            vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);

            renderable.Initialize(_shaderContext, vao, vbo, ebo);
        }

        public void Render(Viewport viewport, IRenderable renderable)
        {
            if ((viewport.Camera.VisualMask & renderable.VisualMask) > 0)
            {
                renderable.Vao!.Bind();
                _gl.PointSize(100.0f);

                renderable.Material.Use(viewport, renderable.Transform);
                // renderable.Draw(viewport.Camera);

                unsafe
                {
                    _gl.DrawElements(PrimitiveType.Triangles, (uint)renderable.Indices.Length, DrawElementsType.UnsignedShort, null);
                }

            }
        }

        public void Render(Viewport viewport, IEnvironment environment)
        {
            var skyColor = environment.Ambient;

            _gl!.ClearColor(skyColor.Red, skyColor.Green, skyColor.Blue, 1.0f);
            _gl!.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

            _gl!.Enable(EnableCap.DepthTest);
            _gl!.Enable(EnableCap.Blend);
            _gl!.Enable(EnableCap.LineSmooth);
            _gl!.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public void ChangeContextSize(Vector2 size)
        {
            _gl.Viewport(0, 0, (uint)size.X, (uint)size.Y);
        }
    }
}