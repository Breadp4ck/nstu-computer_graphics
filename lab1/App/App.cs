using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.Maths;
using System.Numerics;

using Lab1.App.States;
using Lab1.App;
using Lab1.Core;

namespace Lab1.App
{
    public enum AppMode
    {
        SPECTATE_MODE,
        WORKSPACE_MODE,
        EDIT_LAYER_MODE,
        MOVE_LAYER_MODE,
        SCALE_LAYER_MODE,
        ROTATE_LAYER_MODE,
    }

    public class App
    {
        private IWindow _window;
        private IInputContext _input;
        private IKeyboard _keyboard;
        private IMouse _mouse;
        private GL _gl;
        private Gui _gui;

        private AppMode _mode = AppMode.SPECTATE_MODE;

        public List<Layer> Layers = new List<Layer>();
        private Camera _camera = new Camera();

        private System.Numerics.Vector2 previousPosition = new System.Numerics.Vector2(0.0f, 0.0f);
        private Vector2 offset = new Vector2(0.0f, 0.0f);

        bool Dragging { get; set; } = false;
        public int LayerID = 0;
        int canvasID = 0;

        private Dictionary<string, AppState> _states = new Dictionary<string, AppState>();
        public string CurrentState { get; private set; } = "Spectate";

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

            _states.Add("Specatate", new States.Spectate(this));
            _states.Add("Workspace", new States.Workspace(this));
            _states.Add("Edit", new States.Edit(this));
            _states.Add("Move", new States.Move(this));
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

        public void AddLayer()
        {
            Layers.Add(new Layer(_gl, Color.FromHSV(0.0f, 0.77f, 0.95f), LayerID * 0.001f));
            LayerID += 1;
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
                      -_camera.CameraPosition.X + 2.0f * _mouse.Position.X / _window.Size.X - 1.0f,
                      -_camera.CameraPosition.Y - (2.0f * _mouse.Position.Y / _window.Size.Y - 1.0f)
                  )
              );
        }

        public void RemoveHoverVertex()
        {
            Layers[LayerID].DrawLast = true;
            Layers[LayerID].RemoveLastVertex();
        }

        private void OnWindowLoad()
        {
            _input = _window.CreateInput();
            _gl = _window.CreateOpenGL();

            _gui = new Gui(_gl, _window, _input);
            _gui.SetFont("App/Fonts/Raleway/Raleway-Regular.ttf", 14);

            _keyboard = _input.Keyboards.FirstOrDefault();
            _mouse = _input.Mice.FirstOrDefault();

            if (_keyboard != null)
            {
                foreach (var key in _states.Keys)
                {
                    _keyboard.KeyUp += _states[key].OnKeyUp;
                    _keyboard.KeyDown += _states[key].OnKeyDown;
                }

                _keyboard.KeyDown += (IKeyboard keyboard, Key key, int arg3) =>
                {
                    switch (AppMode)
                    {
                        case AppMode.WORKSPACE_MODE:
                            switch (key)
                            {
                                case Key.Escape:
                                    MakeAllLayersTransperent(1.0f);
                                    AppMode = AppMode.SPECTATE_MODE;
                                    break;

                                case Key.Z:
                                    Layers[layerID].Transperent = 0.2f;
                                    layerID = (layerID == 0) ? Layers.Count - 1 : layerID - 1;
                                    Layers[layerID].Transperent = 1.0f;
                                    break;

                                case Key.X:
                                    Layers[layerID].Transperent = 0.2f;
                                    layerID = (layerID == Layers.Count - 1) ? 0 : layerID + 1;
                                    Layers[layerID].Transperent = 1.0f;
                                    break;

                                case Key.N:
                                    if (Layers.Last().GetVerticesCount() == 0)
                                    {
                                        layerID = Layers.Count - 1;
                                        AppMode = AppMode.EDIT_LAYER_MODE;
                                    }
                                    else
                                    {
                                        layers.Add(new Layer(_gl, Color.FromHSV(0.0f, 0.77f, 0.95f), layerID * 0.001f));
                                        layerID += 1;
                                    }

                                    AppMode = AppMode.EDIT_LAYER_MODE;

                                    layers[layerID].AddVertex(
                                        new Vertex(
                                            -_camera.CameraPosition.X + 2.0f * _mouse.Position.X / _window.Size.X - 1.0f,
                                            -_camera.CameraPosition.Y - (2.0f * _mouse.Position.Y / _window.Size.Y - 1.0f)
                                        )
                                    );
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
                                    layers[layerID].DrawLast = false;

                                    AppMode = AppMode.EDIT_LAYER_MODE;

                                    layers[layerID].AddVertex(
                                        new Vertex(
                                            -_camera.CameraPosition.X + 2.0f * _mouse.Position.X / _window.Size.X - 1.0f,
                                            -_camera.CameraPosition.Y - (2.0f * _mouse.Position.Y / _window.Size.Y - 1.0f)
                                        )
                                    );

                                    break;
                            }
                            break;

                        case AppMode.EDIT_LAYER_MODE:
                            switch (key)
                            {
                                case Key.Escape:
                                    layers[layerID].DrawLast = true;

                                    AppMode = AppMode.WORKSPACE_MODE;

                                    layers[layerID].RemoveLastVertex();
                                    break;

                                case Key.S:
                                    layers[layerID].Transform.Scale = 0.2f;
                                    break;
                            }
                            break;
                    }
                };
            }

            if (_mouse != null)
            {
                foreach (var key in _states.Keys)
                {
                    _mouse.MouseUp += _states[key].OnMouseUp;
                    _mouse.MouseDown += _states[key].OnMouseDown;
                    _mouse.MouseMove += _states[key].OnMouseMove;
                    _mouse.Scroll += _states[key].OnMouseScroll;
                }

                _mouse.MouseDown += (IMouse mouse, MouseButton button) =>
                {
                    Dragging = button == MouseButton.Middle ? true : Dragging;

                    switch (AppMode)
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
                                case MouseButton.Left:
                                    layers[layerID].AddVertex(
                                        new Vertex(
                                            (-_camera.CameraPosition.X + (2.0f * (_mouse.Position.X) / _window.Size.X - 1.0f)) / (_camera.Transform.Scale),
                                            (-_camera.CameraPosition.Y - (2.0f * (_mouse.Position.Y) / _window.Size.Y - 1.0f)) / (_camera.Transform.Scale)
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
                    Dragging = button == MouseButton.Middle ? false : Dragging;
                };

                _mouse.MouseMove += (IMouse mouse, System.Numerics.Vector2 position) =>
                {
                    offset = position - previousPosition;
                    offset.Y *= -1.0f;
                    previousPosition = position;

                    if (Dragging)
                    {
                        Vector3 pos = _camera.CameraPosition;
                        pos.X += offset.X * 0.002f;
                        pos.Y += offset.Y * 0.002f;
                        _camera.CameraPosition = new System.Numerics.Vector3(pos.X, pos.Y, pos.Z);
                    };

                    if (AppMode == AppMode.EDIT_LAYER_MODE)
                    {
                        layers[layerID].ChangeLastVertex(
                            new Vertex(
                                (-_camera.CameraPosition.X + (2.0f * (position.X) / _window.Size.X - 1.0f)) / (_camera.Transform.Scale),
                                (-_camera.CameraPosition.Y - (2.0f * (position.Y) / _window.Size.Y - 1.0f)) / (_camera.Transform.Scale)
                            )
                        );
                    }
                };

                _mouse.Scroll += (IMouse _mouse, ScrollWheel scroll) =>
                {
                    _camera.Transform.Scale += scroll.Y * 0.1f;
                    _camera.Transform.Scale = _camera.Transform.Scale <= 2.5f ? _camera.Transform.Scale : 2.5f;
                    _camera.Transform.Scale = _camera.Transform.Scale >= 0.1f ? _camera.Transform.Scale : 0.1f;

                    if (AppMode == AppMode.EDIT_LAYER_MODE)
                    {
                        layers[layerID].ChangeLastVertex(
                            new Vertex(
                                (-_camera.CameraPosition.X + (2.0f * (_mouse.Position.X) / _window.Size.X - 1.0f)) / (_camera.Transform.Scale),
                                (-_camera.CameraPosition.Y - (2.0f * (_mouse.Position.Y) / _window.Size.Y - 1.0f)) / (_camera.Transform.Scale)
                            )
                        );
                    }
                };
            }

            layers.Add(new Layer(_gl, Color.FromHSV(0.0f, 0.77f, 0.95f), 0.001f));
            layers[0].Transperent = 0.2f;
        }

        private unsafe void OnWindowRender(double delta)
        {
            FrameSetup();

            foreach (var layer in layers)
            {
                layer.Draw(_camera);
            }

            _gui.Process((float)delta);

            layers[layerID].Color = _gui.Color;
        }

        private void OnWindowResize(Vector2D<int> size)
        {
            _gl.Viewport(size);
            _gui.SetViewportSize(new Vector2(size.X, size.Y));
            Console.WriteLine($"Viewport size: {size.X} {size.Y}");
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
            foreach (var layer in layers)
            {
                layer.Transperent = alpha;
            }
        }

        private void FrameSetup()
        {
            Color background = _mode switch
            {
                AppMode.SPECTATE_MODE => Color.FromHSV(0.0f, 0.2f, 0.3f),
                AppMode.WORKSPACE_MODE => Color.FromHSV(0.2f, 0.2f, 0.3f),
                AppMode.EDIT_LAYER_MODE => Color.FromHSV(0.4f, 0.2f, 0.3f),
                AppMode.MOVE_LAYER_MODE => Color.FromHSV(0.7f, 0.2f, 0.3f),
                AppMode.SCALE_LAYER_MODE => Color.FromHSV(0.8f, 0.2f, 0.3f),
                AppMode.ROTATE_LAYER_MODE => Color.FromHSV(0.9f, 0.2f, 0.3f),
                _ => Color.FromHSV(0.0f, 0.2f, 0.5f),
            };

            _gl.ClearColor(background.Red, background.Green, background.Blue, 1.0f);
            _gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

            _gl.Enable(EnableCap.DepthTest);
            _gl.Enable(EnableCap.Blend);
            _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        internal void ChangeState(string newStateName)
        {
            if (!_states.ContainsKey(newStateName))
            {
                return;
            }

            DisposeStateEventHangling(CurrentState);
            ListenStateEventHangling(newStateName);

            _gui.ModeName = newStateName;
        }

        private void ListenStateEventHangling(string stateName)
        {
            if (_keyboard != null)
            {
                _keyboard.KeyUp += _states[stateName].OnKeyUp;
                _keyboard.KeyDown += _states[stateName].OnKeyDown;
            }

            if (_mouse != null)
            {
                _mouse.MouseUp += _states[stateName].OnMouseUp;
                _mouse.MouseDown += _states[stateName].OnMouseDown;
                _mouse.MouseMove += _states[stateName].OnMouseMove;
                _mouse.Scroll += _states[stateName].OnMouseScroll;
            }
        }

        private void DisposeStateEventHangling(string stateName)
        {
            if (_keyboard != null)
            {
                _keyboard.KeyUp -= _states[stateName].OnKeyUp;
                _keyboard.KeyDown -= _states[stateName].OnKeyDown;
            }

            if (_mouse != null)
            {
                _mouse.MouseUp -= _states[stateName].OnMouseUp;
                _mouse.MouseDown -= _states[stateName].OnMouseDown;
                _mouse.MouseMove -= _states[stateName].OnMouseMove;
                _mouse.Scroll -= _states[stateName].OnMouseScroll;
            }
        }

        public AppMode AppMode
        {
            get => _mode;
            set
            {
                _mode = value;
                _gui.ModeName = _mode switch
                {
                    AppMode.SPECTATE_MODE => "Spectate",
                    AppMode.WORKSPACE_MODE => "Workspace",
                    AppMode.EDIT_LAYER_MODE => "Edit",
                    AppMode.MOVE_LAYER_MODE => "Mode",
                    AppMode.SCALE_LAYER_MODE => "Scale",
                    AppMode.ROTATE_LAYER_MODE => "Rotate",
                    _ => "Unknown mode",
                };
            }
        }
    }
}