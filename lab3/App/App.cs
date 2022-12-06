using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.Maths;

using Lab1.Core;

namespace Lab1.App
{
    public sealed class App
    {
        private IWindow _window;
        private IInputContext? _input;
        private IKeyboard? _keyboard;
        private IMouse? _mouse;
        private GL? _gl;
        private Gui? _gui;

        private Canvas? _canvas;

        public App()
        {
            var options = WindowOptions.Default;

            options.Title = "Simple Drawer";
            options.Size = new Vector2D<int>(1280, 720);
            options.API = new GraphicsAPI(ContextAPI.OpenGL, new APIVersion(4, 6));
            options.Samples = 4;

            _window = Window.Create(options);

            _window.Load += OnWindowLoad;
            _window.Resize += OnWindowResize;
            _window.Render += OnWindowRender;
            _window.Closing += OnWindowClosing;

            _window.Run();
        }

        public void Close()
        {
            _window.Close();
        }

        private void FrameSetup()
        {
            _gl!.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

            _gl.Enable(EnableCap.DepthTest);
            _gl.Enable(EnableCap.Blend);
            _gl.Enable(EnableCap.LineSmooth);
            _gl.Enable(EnableCap.Multisample);
            _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        private void OnWindowLoad()
        {
            _input = _window.CreateInput();
            _gl = _window.CreateOpenGL();

            _keyboard = _input.Keyboards.FirstOrDefault()!;
            _mouse = _input.Mice.FirstOrDefault()!;
            _gui = new Gui(_gl, _window, _input);

            _canvas = new Canvas(_gl);
        }

        private void OnWindowRender(double delta)
        {
            FrameSetup();

            _canvas!.Draw();
        }

        private void OnWindowResize(Vector2D<int> size)
        {

        }

        private void OnWindowClosing()
        {
            _window.Load -= OnWindowLoad;
            _window.Resize -= OnWindowResize;
            _window.Render -= OnWindowRender;
            _window.Closing -= OnWindowClosing;
        }
    }
}