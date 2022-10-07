using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.Maths;
using System.Numerics;

using Lab1.App.States;
using Lab1.Core;

namespace Lab1.App
{
    public class App
    {
        private IWindow _window;
        private AppContext _context = new AppContext();

        public List<Layer> Layers = new List<Layer>();
        private Camera _camera = new Camera();

        private System.Numerics.Vector2 previousPosition = new System.Numerics.Vector2(0.0f, 0.0f);

        public bool Dragging { get; set; } = false;
        public int LayerID = 0;

        private Dictionary<string, AppState> _states = new Dictionary<string, AppState>();
        public string CurrentState { get; private set; } = "Spectate";
        public Vector2 MouseOffset { get; private set; } = new Vector2(0.0f, 0.0f);
        public Vector2 MousePosition { get => _context.MousePosition; }

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

            _states.Add("Spectate", new States.Spectate(this));
            _states.Add("Workspace", new States.Workspace(this));
            _states.Add("Edit", new States.Edit(this));
            _states.Add("Grab", new States.Grab(this));
            _states.Add("Scale", new States.Scale(this));
            _states.Add("Rotate", new States.Rotate(this));

            _window.Run();
        }

        public void Close()
        {
            _window.Close();
        }

        public void NextLayer()
        {
            Layers[LayerID].Transperent = 0.2f;
            LayerID = (LayerID == Layers.Count - 1) ? 0 : LayerID + 1;
            Layers[LayerID].Transperent = 1.0f;
        }

        public void PreviousLayer()
        {
            Layers[LayerID].Transperent = 0.2f;
            LayerID = (LayerID == 0) ? Layers.Count - 1 : LayerID - 1;
            Layers[LayerID].Transperent = 1.0f;
        }

        public void SetGuiColorInputByLayerColor(int layerID)
        {
            _context.Gui!.Color = Layers[layerID].Color;
        }

        public void AddLayer()
        {
            Layers.Add(new Layer(_context.Gl!, Color.FromHSV(0.0f, 0.77f, 0.95f), LayerID * 0.001f));
        }

        public void RemoveLayer()
        {
            if (Layers.Count > 0)
            {
                Layers.RemoveAt(LayerID);
                LayerID = Layers.Count == LayerID ? LayerID - 1 : LayerID;
            }
            else
            {
                Layers[0].Clear();
            }
        }

        public void AddHoverVertex()
        {
            Layers[LayerID].DrawLast = false;

            Layers[LayerID].AddVertex(
                  new Vertex(
                      -_camera.CameraPosition.X + 2.0f * _context.MousePosition.X / _window.Size.X - 1.0f,
                      -_camera.CameraPosition.Y - (2.0f * _context.MousePosition.Y / _window.Size.Y - 1.0f)
                  )
              );
        }

        public void UpdateHoverVertexPosition()
        {
            if (Layers[LayerID].DrawLast == false)
            {
                Layers[LayerID].ChangeLastVertex(
                    new Vertex(
                        (-_camera.CameraPosition.X + (2.0f * (_context.MousePosition.X) / _window.Size.X - 1.0f)) / (_camera.Transform.Scale),
                        (-_camera.CameraPosition.Y - (2.0f * (_context.MousePosition.Y) / _window.Size.Y - 1.0f)) / (_camera.Transform.Scale * _camera.ViewportRatioXY)
                    )
                );
            }
        }

        public void RemoveHoverVertex()
        {
            Layers[LayerID].DrawLast = true;
            Layers[LayerID].RemoveLastVertex();
        }

        public void AddVertexByMousePosition()
        {
            Layers[LayerID].AddVertex(
                new Vertex(
                    (-_camera.CameraPosition.X + (2.0f * (_context.MousePosition.X) / _window.Size.X - 1.0f)) / (_camera.Transform.Scale),
                    (-_camera.CameraPosition.Y - (2.0f * (_context.MousePosition.Y) / _window.Size.Y - 1.0f)) / (_camera.Transform.Scale * _camera.ViewportRatioXY)
                )
            );
        }

        private void OnWindowLoad()
        {
            _context.AttachWindow(_window);

            if (_context.Mouse != null)
            {
                _context.Mouse.MouseDown += (IMouse mouse, MouseButton button) =>
                {
                    Dragging = button == MouseButton.Middle ? true : Dragging;
                };

                _context.Mouse.MouseUp += (IMouse mouse, MouseButton button) =>
                {
                    Dragging = button == MouseButton.Middle ? false : Dragging;
                };

                _context.Mouse.MouseMove += (IMouse mouse, System.Numerics.Vector2 position) =>
                {
                    Vector2 offset = position - previousPosition;
                    offset.Y *= -1.0f;
                    previousPosition = position;

                    MouseOffset = offset;

                    if (Dragging)
                    {
                        Vector3 pos = _camera.CameraPosition;

                        pos.X += MouseOffset.X * 0.002f;
                        pos.Y += MouseOffset.Y * 0.002f;

                        _camera.CameraPosition = new System.Numerics.Vector3(pos.X, pos.Y, pos.Z);
                    };
                };

                _context.Mouse.Scroll += (IMouse _mouse, ScrollWheel scroll) =>
                {
                    // Look like crap
                    _camera.Transform.Scale += scroll.Y * 0.1f;
                    _camera.Transform.Scale = _camera.Transform.Scale <= 2.5f ? _camera.Transform.Scale : 2.5f;
                    _camera.Transform.Scale = _camera.Transform.Scale >= 0.1f ? _camera.Transform.Scale : 0.1f;
                };
            }

            Layers.Add(new Layer(_context.Gl!, Color.FromHSV(0.0f, 0.77f, 0.95f), 0.001f));
            Layers[0].Transperent = 0.2f;

            ChangeState("Spectate");
        }

        private void OnWindowRender(double delta)
        {
            Color background = Color.FromHSV(0.7f, 0.2f, 0.3f);
            _context.FrameSetup(background);

            foreach (var layer in Layers)
            {
                layer.Draw(_camera);
            }

            _context.Gui!.Process((float)delta);
            _states[CurrentState].Render(delta);

            Layers[LayerID].Color = _context.Gui!.Color;
        }

        private void OnWindowResize(Vector2D<int> size)
        {
            _context.Gl!.Viewport(size);
            _context.Gui!.SetViewportSize(new Vector2(size.X, size.Y));
            _camera.ViewportSize = new Vector2(size.X, size.Y);
        }

        private void OnWindowClosing()
        {
            _window.Load -= OnWindowLoad;
            _window.Resize -= OnWindowResize;
            _window.Render -= OnWindowRender;
            _window.Closing -= OnWindowClosing;
        }

        internal void MakeAllLayersTransperent(float alpha)
        {
            foreach (var layer in Layers)
            {
                layer.Transperent = alpha;
            }
        }

        internal void ChangeState(string newStateName)
        {
            if (!_states.ContainsKey(newStateName))
            {
                return;
            }

            _states[CurrentState].Exit();

            DisposeStateEventHangling(CurrentState);
            ListenStateEventHangling(newStateName);

            _context.Gui!.ModeName = newStateName;
            CurrentState = newStateName;

            _states[CurrentState].Enter();
        }

        private void ListenStateEventHangling(string stateName)
        {
            if (_context.Keyboard != null)
            {
                _context.Keyboard.KeyUp += _states[stateName].OnKeyUp;
                _context.Keyboard.KeyDown += _states[stateName].OnKeyDown;
            }

            if (_context.Mouse != null)
            {
                _context.Mouse.MouseUp += _states[stateName].OnMouseUp;
                _context.Mouse.MouseDown += _states[stateName].OnMouseDown;
                _context.Mouse.MouseMove += _states[stateName].OnMouseMove;
                _context.Mouse.Scroll += _states[stateName].OnMouseScroll;
            }
        }

        private void DisposeStateEventHangling(string stateName)
        {
            if (_context.Keyboard != null)
            {
                _context.Keyboard.KeyUp -= _states[stateName].OnKeyUp;
                _context.Keyboard.KeyDown -= _states[stateName].OnKeyDown;
            }

            if (_context.Mouse != null)
            {
                _context.Mouse.MouseUp -= _states[stateName].OnMouseUp;
                _context.Mouse.MouseDown -= _states[stateName].OnMouseDown;
                _context.Mouse.MouseMove -= _states[stateName].OnMouseMove;
                _context.Mouse.Scroll -= _states[stateName].OnMouseScroll;
            }
        }
    }
}