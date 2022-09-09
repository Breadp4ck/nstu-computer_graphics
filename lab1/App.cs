using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.Maths;

class App {
    private IWindow _window;
    private IInputContext _input;
    private IKeyboard _keyboard;
    private IMouse _mouse; 
    private GL _gl;

    private VertexArrayObject<float, uint> vao;
    private BufferObject<float> vbo;
    private BufferObject<uint> ebo;
    private List<Layer> layers = new List<Layer>();
    //List<Vertex<short>> vertices = new List<Vertex<short>>();
    float[] vertices = {
        0.0f, 0.7f, 0.0f,
        -0.7f, -0.7f, 0.0f,
        0.7f, -0.7f, 0.0f,
    };

    uint[] indices = {
        0, 1, 2
    };

    private Shader _shaderVertices;
    private Shader _shaderFigures;
    
    public App() {
        var options = WindowOptions.Default;

        options.Title = "Simple Drawer";
        options.Size = new Vector2D<int>(1280, 720);

        _window = Window.Create(options);

        _window.Load += OnWindowLoad;
        _window.Resize += OnWindowResize;
        _window.Render += OnWindowRender;
        _window.Closing += OnWindowClosing;

        //vertices.Add(new Vertex<short>(20, 20));
        //vertices.Add(new Vertex<short>(30, 20));
        //vertices.Add(new Vertex<short>(40, 30));

        _window.Run();
    }

    private void OnWindowLoad() {
        _input = _window.CreateInput();
        _gl = _window.CreateOpenGL();

        _keyboard = _input.Keyboards.FirstOrDefault();
        _mouse = _input.Mice.FirstOrDefault();

        // TODO make a here https://www.youtube.com/watch?v=YApRxaB6upY&t=1792s
        // maybe that helps us to remove null devices
        if (_keyboard != null) {
            _keyboard.KeyDown += (IKeyboard keyboard, Key key, int arg3) => {
                switch (key) {
                    case Key.Escape:
                        _window.Close();
                        break;
                    
                    case Key.P:
                        vbo.Update(vertices);
                        break;
                }
            };
        }

        if (_mouse != null) {
            _mouse.MouseDown += (IMouse mouse, MouseButton button) => {
                /*if (button == MouseButton.Left) {
                    vertices[0] = -1.0f + 2.0f * mouse.Position.X / (float) _window.Size.X;
                    vertices[1] = +1.0f - 2.0f * mouse.Position.Y / (float) _window.Size.Y;
                    vbo.Update(vertices);
                }*/
            };

            _mouse.MouseMove += (IMouse mouse, System.Numerics.Vector2 _offset) => {
                if (mouse.IsButtonPressed(MouseButton.Left)) {
                    vertices[0] = -1.0f + 2.0f * mouse.Position.X / (float) _window.Size.X;
                    vertices[1] = +1.0f - 2.0f * mouse.Position.Y / (float) _window.Size.Y;
                    vbo.Update(vertices);
                }
            };
        }

        vbo = new BufferObject<float>(_gl, vertices, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);
        ebo = new BufferObject<uint>(_gl, indices, BufferTargetARB.ElementArrayBuffer, BufferUsageARB.DynamicCopy);

        vao = new VertexArrayObject<float, uint>(_gl, vbo, ebo);
        vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 3, 0);

        _shaderFigures = new Shader(_gl, "shaders/figure.vert", "shaders/figure.frag");
        _shaderVertices = new Shader(_gl, "shaders/vertex.vert", "shaders/vertex.frag");
    }

    private unsafe void OnWindowRender(double _delta) {
        _gl.ClearColor(0.2f, 0.2f, 0.3f, 1.0f);
        _gl.Clear(ClearBufferMask.ColorBufferBit);

        _gl.PointSize(20.0f);
        //_gl.Enable(EnableCap.LineSmooth);

        vao.Bind();

        _shaderFigures.Use();
        _gl.DrawElements(PrimitiveType.Triangles, (uint) indices.Length, DrawElementsType.UnsignedInt, null);

        _shaderVertices.Use();
        _gl.DrawElements(PrimitiveType.Points, (uint) indices.Length, DrawElementsType.UnsignedInt, null);
    }

    private void OnWindowResize(Vector2D<int> size) {
        _gl.Viewport(size);
    }

    private void OnWindowClosing() {
        _window.Load -= OnWindowLoad;
        _window.Resize -= OnWindowResize;
        _window.Render -= OnWindowRender;
        _window.Closing -= OnWindowClosing;
    }
}