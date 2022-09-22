using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.Maths;
using System.Numerics;


enum AppMode
{
    SPECTATE_MODE,
    WORKSPACE_MODE,
    EDIT_LAYER_MODE,
    DRAW_LAYER_MODE,
    MOVE_LAYER_MODE,
    SCALE_LAYER_MODE,
    ROTATE_LAYER_MODE,
}

class App
{
    private IWindow _window;
    private IInputContext _input;
    private IKeyboard _keyboard;
    private IMouse _mouse;
    private GL _gl;

    private AppMode _mode = AppMode.SPECTATE_MODE;

    private List<Layer> layers = new List<Layer>();
    private Camera _camera = new Camera();

    private System.Numerics.Vector2 previousPosition = new System.Numerics.Vector2(0.0f, 0.0f);
    private Vector2 offset = new Vector2(0.0f, 0.0f);

    bool dragging = false;
    int layerID = 0;
    int canvasID = 0;

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
                switch (_mode)
                {
                    case AppMode.SPECTATE_MODE:
                        switch (key)
                        {
                            case Key.Escape:
                                _window.Close();
                                break;

                            case Key.Space:
                                _mode = AppMode.WORKSPACE_MODE;
                                break;
                        }
                        break;

                    case AppMode.WORKSPACE_MODE:
                        switch (key)
                        {
                            case Key.Escape:
                                _mode = AppMode.SPECTATE_MODE;
                                break;

                            case Key.Z:
                                layerID = (layerID == 0) ? layers.Count - 1 : layerID - 1;
                                break;

                            case Key.X:
                                layerID = (layerID == layers.Count - 1) ? 0 : layerID + 1;
                                break;

                            case Key.N:
                                if (layers.Last().GetVerticesCount() == 0)
                                {
                                    layerID = layers.Count - 1;
                                    _mode = AppMode.EDIT_LAYER_MODE;
                                }
                                else
                                {
                                    layers.Add(new Layer(_gl, Color.FromHSV(0.0f, 0.77f, 0.95f)));
                                    layerID += 1;
                                }
                                _mode = AppMode.EDIT_LAYER_MODE;
                                break;

                            case Key.R:
                                if (layers.Count > 0)
                                {
                                    layers.RemoveAt(layerID);
                                    layerID = layers.Count == layerID ? layerID - 1 : layerID;
                                }
                                else
                                {
                                    layers[0].Clear();
                                }
                                break;


                            case Key.Space:
                                _mode = AppMode.EDIT_LAYER_MODE;
                                break;
                        }
                        break;

                    case AppMode.EDIT_LAYER_MODE:
                        switch (key)
                        {
                            case Key.Escape:
                                _mode = AppMode.WORKSPACE_MODE;
                                break;

                            case Key.E:
                                Console.WriteLine($"Kek: {layerID}");
                                layers[layerID].DrawLast = false;
                                _mode = AppMode.DRAW_LAYER_MODE;
                                layers[layerID].AddVertex(
                                    new Vertex(
                                        -_camera.CameraPosition.X + 2.0f * _mouse.Position.X / _window.Size.X - 1.0f,
                                        -_camera.CameraPosition.Y - (2.0f * _mouse.Position.Y / _window.Size.Y - 1.0f)
                                    )
                                );
                                break;
                        }
                        break;

                    case AppMode.DRAW_LAYER_MODE:
                        switch (key)
                        {
                            case Key.Escape:
                                layers[layerID].DrawLast = true;
                                _mode = AppMode.EDIT_LAYER_MODE;
                                layers[layerID].RemoveLastVertex();
                                break;
                        }
                        break;
                }
            };
        }

        if (_mouse != null)
        {
            _mouse.MouseDown += (IMouse mouse, MouseButton button) =>
            {
                if (button == MouseButton.Middle)
                {
                    dragging = true;
                }

                switch (_mode)
                {
                    case AppMode.SPECTATE_MODE:
                        switch (button)
                        {

                        }
                        break;

                    case AppMode.WORKSPACE_MODE:
                        switch (button)
                        {

                        }
                        break;

                    case AppMode.EDIT_LAYER_MODE:
                        switch (button)
                        {

                        }
                        break;

                    case AppMode.DRAW_LAYER_MODE:
                        switch (button)
                        {
                            case MouseButton.Left:
                                layers[layerID].AddVertex(
                                    new Vertex(
                                        -_camera.CameraPosition.X + 2.0f * _mouse.Position.X / _window.Size.X - 1.0f,
                                        -_camera.CameraPosition.Y - (2.0f * _mouse.Position.Y / _window.Size.Y - 1.0f)
                                    )
                                );
                                break;

                            case MouseButton.Right:
                                layers[layerID].RemoveLastVertex();
                                break;
                        }
                        break;
                }
            };

            _mouse.MouseUp += (IMouse mouse, MouseButton button) =>
            {
                if (button == MouseButton.Middle)
                {
                    dragging = false;
                }
            };

            _mouse.MouseMove += (IMouse mouse, System.Numerics.Vector2 position) =>
            {
                offset = position - previousPosition;
                offset.Y *= -1.0f;
                previousPosition = position;

                if (dragging)
                {
                    Vector3 pos = _camera.CameraPosition;
                    pos.X += offset.X * 0.002f;
                    pos.Y += offset.Y * 0.002f;
                    _camera.CameraPosition = new System.Numerics.Vector3(pos.X, pos.Y, pos.Z);
                };

                if (_mode == AppMode.DRAW_LAYER_MODE)
                {
                    layers[layerID].ChangeLastVertex(
                        new Vertex(
                            -_camera.CameraPosition.X + 2.0f * (position.X) / _window.Size.X - 1.0f,
                            -_camera.CameraPosition.Y - (2.0f * (position.Y) / _window.Size.Y - 1.0f)
                        )
                    );
                }
            };

            _mouse.Scroll += (IMouse _mouse, ScrollWheel scroll) =>
            {
                Console.WriteLine($"{layerID} {layers[layerID].Hue}");
                // TODO: It's cringe
                layers[layerID].Hue = (layers[layerID].Hue + 0.02f) % 1.0f;
                layers[layerID].Color = Color.FromHSV(layers[layerID].Hue, 0.77f, 0.95f);
            };
        }

        layers.Add(new Layer(_gl, Color.FromHSV(0.0f, 0.77f, 0.95f)));
    }

    private unsafe void OnWindowRender(double _delta)
    {
        Color background = _mode switch
        {
            AppMode.SPECTATE_MODE => Color.FromHSV(0.0f, 0.2f, 0.3f),
            AppMode.WORKSPACE_MODE => Color.FromHSV(0.1f, 0.2f, 0.3f),
            AppMode.EDIT_LAYER_MODE => Color.FromHSV(0.2f, 0.2f, 0.3f),
            AppMode.DRAW_LAYER_MODE => Color.FromHSV(0.3f, 0.2f, 0.3f),
            AppMode.MOVE_LAYER_MODE => Color.FromHSV(0.5f, 0.2f, 0.3f),
            AppMode.SCALE_LAYER_MODE => Color.FromHSV(0.7f, 0.2f, 0.3f),
            AppMode.ROTATE_LAYER_MODE => Color.FromHSV(0.9f, 0.2f, 0.3f),
            _ => Color.FromHSV(0.0f, 0.2f, 0.5f),
        };

        _gl.ClearColor(background.Red, background.Green, background.Blue, 1.0f);
        _gl.Enable(EnableCap.DepthTest);
        _gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

        foreach (var layer in layers)
        {
            layer.Draw(_camera);
        }
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