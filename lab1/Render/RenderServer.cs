using Silk.NET.OpenGL;
using System.Numerics;

namespace Lab1.Render
{
    public class RenderServer
    {
        private GL _gl;
        private ShaderContext _shaderContext;
        private IEnvironment _currentEnvironment;
        private IDirectionalLight _currentDirectionalLight;
        private IPointLight[] _currentPointLights;

        public RenderServer(GL gl)
        {
            _gl = gl;
            _shaderContext = new ShaderContext(_gl);

        }

        public void Load(IRenderable renderable)
        {
            BufferObject<float> vbo = new BufferObject<float>(_gl, renderable.Vertices, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);
            BufferObject<ushort> ebo = new BufferObject<ushort>(_gl, renderable.Indices, BufferTargetARB.ElementArrayBuffer, BufferUsageARB.DynamicCopy);

            VertexArrayObject<float, ushort> vao = new VertexArrayObject<float, ushort>(_gl, vbo, ebo);
            // vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 3, 0);

            // Next chapter in engine development
            vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
            vao.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
            vao.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);

            renderable.Initialize(_shaderContext, vao, vbo, ebo);
        }

        public void Render(Viewport viewport, IRenderable renderable)
        {
            if ((viewport.Camera.VisualMask & renderable.VisualMask) > 0)
            {
                renderable.Material.Use(viewport, renderable.View);

                renderable.Material.Attach(_currentEnvironment);
                renderable.Material.Attach(_currentDirectionalLight);
                renderable.Material.Attach(_currentPointLights);
                renderable.Material.Attach(viewport.Camera);

                unsafe
                {
                    // TODO: It's cringe, but if it will be removed,
                    // then indices will be broken (opengl will use indices of the next IRenderable)
                    // idk why it's working this way, but it has to be I think
                    renderable.Ebo!.Update(renderable.Indices);
                    renderable.Vbo!.Update(renderable.Vertices);

                    _gl.DrawElements(PrimitiveType.Triangles, (uint)renderable.Indices.Length, DrawElementsType.UnsignedShort, null);
                }

            }
        }

        public void ApplyEnvironment(Viewport viewport, IEnvironment environment)
        {
            var skyColor = environment.SkyColor;

            _gl!.ClearColor(skyColor.Red, skyColor.Green, skyColor.Blue, 1.0f);
            _gl!.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

            _gl!.Enable(EnableCap.DepthTest);
            _gl!.Enable(EnableCap.Blend);
            _gl!.Enable(EnableCap.LineSmooth);
            _gl!.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _currentEnvironment = environment;
        }

        public void ApplyDirectionalLight(Viewport viewport, IDirectionalLight directionalLight)
        {
            _currentDirectionalLight = directionalLight;
        }

        public void ApplyPointLights(Viewport viewport, IPointLight[] pointLights)
        {
            _currentPointLights = pointLights;
        }

        public void ChangeContextSize(Vector2 size)
        {
            _gl.Viewport(0, 0, (uint)size.X, (uint)size.Y);
        }
    }
}