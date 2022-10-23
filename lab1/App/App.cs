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
        public int LayerID { get; set; } = 0;
        public int TotalLayers { get; private set; } = 0;

        private Dictionary<string, AppState> _states = new Dictionary<string, AppState>();
        public string CurrentState { get; private set; } = "Spectate";
        public Vector2 MouseOffset { get; private set; } = new Vector2(0.0f, 0.0f);
        public Vector2 MousePosition { get => _context.MousePosition; }

        public float CameraScale { get => _camera.Transform.Scale.X; }

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

        public void MouseVisible(bool visibility)
        {
            if (visibility)
            {
                _context.Mouse!.Cursor.CursorMode = CursorMode.Normal;
            }
            else
            {
                _context.Mouse!.Cursor.CursorMode = CursorMode.Disabled;
            }

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

        public void UpdateLayerGuiData(int layerID)
        {
            if (_context.Gui!.CurrentLayer != layerID && CurrentState != "Spectate")
            {
                MakeAllLayersTransperent(0.2f);
                Layers[layerID].Transperent = 1.0f;
            }

            _context.Gui!.CurrentLayer = layerID;

            var position = Layers[layerID].Transform.Position;
            var scale = Layers[layerID].Transform.Scale;
            var rotation = Layers[layerID].Transform.Rotation;

            _context.Gui!.Color = Layers[layerID].Color;
            _context.Gui!.Position = new Vector2(position.X, position.Y);
            _context.Gui!.Scale = new Vector2(scale.X, scale.Y);

            var angle = (float)(System.Math.Asin(rotation.Z) * 2.0);

            _context.Gui!.Rotation = angle * 180.0f / (float)System.Math.PI;
            _context.Gui!.LayerNames = Layers.Select(layer => layer.Name).ToArray();
        }

        public void UpdateLayerDataWithGui(int layerID)
        {

            var position = _context.Gui!.Position;
            var scale = _context.Gui!.Scale;

            float rotation = (float)(_context.Gui!.Rotation * System.Math.PI / 180.0);
            var quaternion = new Quaternion();

            quaternion.W = (float)System.Math.Cos(rotation / 2.0);
            quaternion.Z = (float)System.Math.Sin(rotation / 2.0);

            Layers[layerID].Color = _context.Gui!.Color;
            Layers[layerID].Transform.Position = new Vector3(position.X, position.Y, Layers[layerID].Transform.Position.Z);
            Layers[layerID].Transform.Scale = new Vector3(scale.X, scale.Y, Layers[layerID].Transform.Scale.Z);
            Layers[layerID].Transform.Rotation = quaternion;
        }

        public void AddLayer()
        {
            TotalLayers += 1;
            Layers.Add(new Layer(_context.Gl!, Color.FromHSV(0.0f, 0.77f, 0.95f), $"Layer {TotalLayers}", LayerID * 0.001f));

            UpdateLayerGuiData(LayerID);
        }

        public void RemoveLayer()
        {
            if (Layers.Count > 1)
            {
                Layers.RemoveAt(LayerID);
                LayerID = Layers.Count == LayerID ? LayerID - 1 : LayerID;
            }
            else
            {
                Layers[0].Clear();
            }

            UpdateLayerGuiData(LayerID);
        }

        public void AddHoverVertex()
        {
            Layers[LayerID].DrawLast = false;

            Layers[LayerID].AddVertex(
                  new Vertex(
                        (-_camera.CameraPosition.X + (2.0f * (_context.MousePosition.X) / _window.Size.X - 1.0f)) / (_camera.Transform.Scale.X),
                        (-_camera.CameraPosition.Y * _camera.ViewportRatioXY - (2.0f * (_context.MousePosition.Y) / _window.Size.Y - 1.0f)) / (_camera.Transform.Scale.X * _camera.ViewportRatioXY)
                  )
              );
        }

        public void UpdateHoverVertexPosition()
        {
            if (Layers[LayerID].DrawLast == false)
            {
                Layers[LayerID].ChangeLastVertex(
                    new Vertex(
                        (-_camera.CameraPosition.X + (2.0f * (_context.MousePosition.X) / _window.Size.X - 1.0f)) / (_camera.Transform.Scale.X),
                        (-_camera.CameraPosition.Y * _camera.ViewportRatioXY - (2.0f * (_context.MousePosition.Y) / _window.Size.Y - 1.0f)) / (_camera.Transform.Scale.X * _camera.ViewportRatioXY)
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
                    (-_camera.CameraPosition.X + (2.0f * (_context.MousePosition.X) / _window.Size.X - 1.0f)) / (_camera.Transform.Scale.X),
                    (-_camera.CameraPosition.Y * _camera.ViewportRatioXY - (2.0f * (_context.MousePosition.Y) / _window.Size.Y - 1.0f)) / (_camera.Transform.Scale.X * _camera.ViewportRatioXY)
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
                    var scale = _camera.Transform.Scale;

                    scale.X += scroll.Y * 0.1f;
                    scale.X = scale.X <= 2.5f ? scale.X : 2.5f;
                    scale.X = scale.X >= 0.1f ? scale.X : 0.1f;

                    scale.Y = scale.X;
                    _camera.Transform.Scale = scale;
                };
            }

            AddLayer();
            UpdateLayerGuiData(LayerID);

            _context.Gui!.OnAddLayerButtonPressed += AddLayer;
            _context.Gui!.OnRemoveLayerButtonPressed += RemoveLayer;

            _context.Gui!.OnSpectateModeButtonPressed += () =>
            {
                // TODO: Bug:
                // Pressed button affects on field as mouse click
                // So we remove the point with RemoveHoverVertex();
                if (CurrentState == "Edit")
                {
                    RemoveHoverVertex();
                }

                ChangeState("Spectate");
            };
            _context.Gui!.OnWorkspaceModeButtonPressed += () =>
            {
                // TODO: Bug:
                // Pressed button affects on field as mouse click
                // So we remove the point with RemoveHoverVertex();
                if (CurrentState == "Edit")
                {
                    RemoveHoverVertex();
                }

                ChangeState("Workspace");
            };
            _context.Gui!.OnEditModeButtonPressed += () => ChangeState("Edit");

            Layers[0].Transperent = 0.2f;

            ChangeState("Spectate");
        }

        private void OnWindowRender(double delta)
        {
            Color background = Color.FromHSV(0.7f, 0.2f, 0.1f);
            _context.FrameSetup(background);

            foreach (var layer in Layers)
            {
                layer.Draw(_camera);
            }

            _context.Gui!.Process((float)delta);
            _states[CurrentState].Render(delta);


            if (_context.Gui!.CurrentLayer != LayerID)
            {
                if (CurrentState != "Spectate")
                {
                    MakeAllLayersTransperent(0.2f);
                    Layers[_context.Gui!.CurrentLayer].Transperent = 1.0f;
                }

                UpdateLayerGuiData(_context.Gui!.CurrentLayer);
            }

            LayerID = _context.Gui!.CurrentLayer;
            UpdateLayerDataWithGui(LayerID);
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