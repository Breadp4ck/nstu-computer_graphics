using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;

namespace Lab1.Core
{
    public abstract class MainLoop
    {
        private IWindow _window;
        private IInputContext _input;
        private GL _gl;

        public MainLoop(IWindow window, GL gl, IInputContext input)
        {
            _window = window;
            _input = input;
            _gl = gl;
        }

        public virtual void Render()
        {

        }
    }
}