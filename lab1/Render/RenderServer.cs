using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;

namespace Lab1.Render
{
    public class RenderServer
    {
        private IWindow _window;
        private IInputContext _input;
        private GL _gl;

        public RenderServer(IWindow window, GL gl, IInputContext input)
        {
            _window = window;
            _input = input;
            _gl = gl;
        }

        public void Render(Viewport viewport, IRenderable visualInstance)
        {
            if ((viewport.Camera.VisualMask & visualInstance.VisualMask) > 0)
            {
                visualInstance.Draw(viewport.Camera);
            }
        }
    }
}