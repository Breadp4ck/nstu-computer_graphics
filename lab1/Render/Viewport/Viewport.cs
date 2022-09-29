using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;

namespace Lab1.Render
{
    public record struct Viewport
    {
        private IWindow _window;
        private IInputContext _input;
        private GL _gl;

        public Camera Camera { get; set; }


        public Viewport(IWindow window, GL gl, IInputContext input, Camera camera)
        {
            _window = window;
            _input = input;
            _gl = gl;
            Camera = camera;
        }
    }
}