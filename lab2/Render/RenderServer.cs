using Silk.NET.OpenGL;
using System.Numerics;

using Lab1.Resources;

namespace Lab1.Render
{
    public class RenderServer
    {
        private GL _gl;
        private ShaderContext _shaderContext;
        private IEnvironment _currentEnvironment;
        private IDirectionalLight[] _currentDirectionalLights = new IDirectionalLight[0];
        private IPointLight[] _currentPointLights = new IPointLight[0];
        private ISpotLight[] _currentSpotLights = new ISpotLight[0];

        public static int MaxDirectionalLightCount { get; } = 2;
        public static int MaxPointLightCount { get; } = 16;
        public static int MaxSpotLightCount { get; } = 4;

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

            // Next chapter in engine development
            vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
            vao.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
            vao.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);

            Material material = renderable.MaterialResource is StandartMaterialResource
                ? new StandartMaterial(_shaderContext)
                : new ColorMaterial(_shaderContext);

            renderable.Initialize(material, vao, vbo, ebo);
        }

        public void Render(Viewport viewport, IRenderable renderable)
        {
            if ((viewport.Camera.VisualMask & renderable.VisualMask) > 0)
            {
                renderable.Vao!.Bind();

                renderable.Material.Use(viewport, renderable.View);

                renderable.Material.Attach(_currentEnvironment);
                renderable.Material.Attach(_currentDirectionalLights);
                renderable.Material.Attach(_currentPointLights);
                renderable.Material.Attach(_currentSpotLights);
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

        public void ApplyDirectionalLight(Viewport viewport, IDirectionalLight[] directionalLights)
        {
            _currentDirectionalLights = directionalLights;
        }

        public void ApplyPointLights(Viewport viewport, IPointLight[] pointLights)
        {
            _currentPointLights = pointLights;
        }

        public void ApplySpotLights(Viewport viewport, ISpotLight[] spotLights)
        {
            _currentSpotLights = spotLights;
        }

        public void ChangeContextSize(Vector2 size)
        {
            _gl.Viewport(0, 0, (uint)size.X, (uint)size.Y);
        }
    }
}