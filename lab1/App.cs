using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.Maths;

class App
{
    private IWindow _window;
    private IInputContext _input;
    private IKeyboard _keyboard;
    private IMouse _mouse;
    private GL _gl;

    private List<Layer> layers = new List<Layer>();

    public App()
    {
        var options = WindowOptions.Default;

        options.Title = "Simple Drawer";
        options.Size = new Vector2D<int>(1280, 720);

        _window = Window.Create(options);

        _window.Load += OnWindowLoad;
        _window.Resize += OnWindowResize;
        _window.Render += OnWindowRender;
        _window.Closing += OnWindowClosing;

        _window.Run();
    }

    public void AddLayer(Color color) { /* TODO */ }

    public void RemoveLayer(string name) { /* TODO */ }

    private void OnWindowLoad()
    {
        _input = _window.CreateInput();
        _gl = _window.CreateOpenGL();

        _keyboard = _input.Keyboards.FirstOrDefault();
        _mouse = _input.Mice.FirstOrDefault();

        if (_keyboard != null)
        {
            _keyboard.KeyDown += (IKeyboard keyboard, Key key, int arg3) => // arg3 ???
            {
                switch (key)
                {
                    case Key.Escape:
                        _window.Close();
                        break;
                }
            };
        }

        if (_mouse != null)
        {
            _mouse.MouseDown += (IMouse mouse, MouseButton button) =>
            {
                layers[0].AddVertex(
                    new Vertex(
                        2.0f * _mouse.Position.X / _window.Size.X - 1.0f,
                        -(2.0f * _mouse.Position.Y / _window.Size.Y - 1.0f)
                    )
                );
            };
        }

        _gl.PointSize(20f);

        layers.Add(new Layer(_gl, new Color(0.2f, 0.5f, 0.7f)));
    }

    private unsafe void OnWindowRender(double _delta)
    {
        _gl.ClearColor(0.2f, 0.2f, 0.3f, 1.0f);
        _gl.Enable(EnableCap.DepthTest);
        _gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

        layers[0].Draw();
    }

    private void OnWindowResize(Vector2D<int> size)
    {
        _gl.Viewport(size);
    }

    private void OnWindowClosing()
    {
        _window.Load -= OnWindowLoad;
        _window.Resize -= OnWindowResize;
        _window.Render -= OnWindowRender;
        _window.Closing -= OnWindowClosing;
    }
}