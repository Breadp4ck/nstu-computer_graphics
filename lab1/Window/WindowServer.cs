using Silk.NET.Windowing;
using Silk.NET.OpenGL;
using Silk.NET.Input;
using Silk.NET.Maths;

using System.Numerics;
using Lab1.Input;

namespace Lab1.Window
{


    public delegate void WindowLoaded();
    public delegate void WindowStartsRender(float delta);
    public delegate void WindowResized(Vector2 size);
    public delegate void WindowClosed();

    public class WindowServer
    {
        private IWindow _window;
        private GL? _gl;
        private IInputContext? _input;

        public event WindowLoaded? OnWindowLoaded;
        public event WindowStartsRender? OnWindowStartsRender;
        public event WindowResized? OnWindowResized;
        public event WindowClosed? OnWindowClosed;

        public Vector2 WindowSize { get; private set; } = Vector2.Zero;

        public WindowServer()
        {
            var options = WindowOptions.Default;

            options.Title = "Simple Drawer";
            options.Size = new Vector2D<int>(1280, 720);

            _window = Silk.NET.Windowing.Window.Create(options);

            _window.Load += OnWindowLoad;
            _window.Resize += OnWindowResize;
            _window.Render += OnWindowRender;
            _window.Closing += OnWindowClosing;

            _window.Initialize();

            Thread thread = new Thread(_window.Run);
            thread.Start();
        }

        public void Close()
        {
            _window.Close();
        }

        public GL GetGlContext()
        {
            return _gl!;
        }

        public IInputContext GetInputContext()
        {
            return _input!;
        }

        private void OnWindowLoad()
        {
            _gl = _window.CreateOpenGL();
            _input = _window.CreateInput();

            if (OnWindowLoaded != null)
            {
                OnWindowLoaded!.Invoke();
            }
        }

        private void OnWindowRender(double delta)
        {
            _gl!.ClearColor(0.1f, 0.3f, 0.5f, 1.0f);

            _gl!.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

            _gl!.Enable(EnableCap.DepthTest);
            _gl!.Enable(EnableCap.Blend);
            _gl!.Enable(EnableCap.LineSmooth);
            _gl!.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            if (OnWindowStartsRender != null)
            {
                OnWindowStartsRender!.Invoke((float)delta);
            }
        }

        private void OnWindowResize(Vector2D<int> size)
        {
            var newSize = new Vector2(size.X, size.Y);
            WindowSize = newSize;

            if (OnWindowResized != null)
            {
                OnWindowResized!.Invoke(newSize);
            }
        }

        private void OnWindowClosing()
        {
            _window.Load -= OnWindowLoad;
            _window.Resize -= OnWindowResize;
            _window.Render -= OnWindowRender;
            _window.Closing -= OnWindowClosing;

            _window.Close();

            if (OnWindowClosed != null)
            {
                OnWindowClosed!.Invoke();
            }
        }
    }
}