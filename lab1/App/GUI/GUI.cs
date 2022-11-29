using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.OpenGL.Extensions.ImGui;
using System.Numerics;

using Lab1.Core;

namespace Lab1.App
{
    public class Gui
    {
        private ImGuiNET.ImGuiWindowFlags _windowFlags =
            ImGuiNET.ImGuiWindowFlags.NoDecoration |
            ImGuiNET.ImGuiWindowFlags.AlwaysAutoResize |
            ImGuiNET.ImGuiWindowFlags.NoNav |
            ImGuiNET.ImGuiWindowFlags.NoSavedSettings |
            ImGuiNET.ImGuiWindowFlags.NoFocusOnAppearing |
            ImGuiNET.ImGuiWindowFlags.NoMove;
        private ImGuiController _controller;
        private ImGuiNET.ImGuiViewport _viewport;
        private Vector3 _color = Vector3.One;
        private Vector2 _position = Vector2.Zero;
        private Vector2 _scale = Vector2.One;
        private float _rotation = 0.0f;
        private int _currentLayer = 0;
        private string[] _layers = new string[0];

        private GL _gl;
        private IView _window;
        private IInputContext _input;

        public string ModeName = "default";

        public Color Color
        {
            get => new Color(_color.X, _color.Y, _color.Z);
            set
            {
                _color.X = value.Red;
                _color.Y = value.Green;
                _color.Z = value.Blue;
            }
        }

        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 Scale { get => _scale; set => _scale = value; }
        public float Rotation { get => _rotation; set => _rotation = value; }
        public int CurrentLayer { get => _currentLayer; set => _currentLayer = value; }
        public string[] LayerNames { get => _layers; set => _layers = value; }


        public delegate void AddLayerButtonPressed();
        public delegate void RemoveLayerButtonPressed();
        public delegate void SpectateModeButtonPressed();
        public delegate void WorkspaceModeButtonPressed();
        public delegate void EditModeButtonPressed();
        public delegate void CameraZoomForawardButtonPressed();
        public delegate void CameraZoomBackButtonPressed();

        public AddLayerButtonPressed OnAddLayerButtonPressed;
        public RemoveLayerButtonPressed OnRemoveLayerButtonPressed;
        public SpectateModeButtonPressed OnSpectateModeButtonPressed;
        public WorkspaceModeButtonPressed OnWorkspaceModeButtonPressed;
        public EditModeButtonPressed OnEditModeButtonPressed;
        public CameraZoomForawardButtonPressed OnCameraZoomForawardButtonPressed;
        public CameraZoomBackButtonPressed OnCameraZoomBackButtonPressed;

        public Gui(GL gl, IView window, IInputContext input)
        {
            _gl = gl;
            _window = window;
            _input = input;

            _controller = new ImGuiController(gl, window, input);
            _viewport = new ImGuiNET.ImGuiViewport();
        }

        public Gui(GL gl, IView window, IInputContext input, ImGuiFontConfig config)
        {
            _gl = gl;
            _window = window;
            _input = input;

            _controller = new ImGuiController(_gl, _window, _input, config);
            _viewport = new ImGuiNET.ImGuiViewport();
        }

        public void SetViewportSize(Vector2 size)
        {
            _viewport.Size = size;
        }

        public void Process(float delta)
        {
            _controller.Update(delta);

            ProcessMode();
            ProcessModeInfo();
            ProcessCameraButtons();
            ProcessLayerSelector();
            ProcessLayerProperties();

            // ImGuiNET.ImGui.ShowDemoWindow();

            _controller.Render();
        }

        private void ProcessCameraButtons()
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(0.35f);
            ImGuiNET.ImGui.SetNextWindowPos(new Vector2(120.0f, 10.0f));

            ImGuiNET.ImGui.Begin("Camera", _windowFlags);
            {
                ImGuiNET.ImGui.BeginGroup();
                {
                    bool cameraZoomForward = ImGuiNET.ImGui.Button(" + ") ? true : false;
                    ImGuiNET.ImGui.SameLine();
                    bool cameraZoomBack = ImGuiNET.ImGui.Button(" - ") ? true : false;

                    if (cameraZoomForward && OnCameraZoomForawardButtonPressed != null)
                    {
                        OnCameraZoomForawardButtonPressed();
                    }

                    if (cameraZoomBack && OnCameraZoomBackButtonPressed != null)
                    {
                        OnCameraZoomBackButtonPressed();
                    }
                }
                ImGuiNET.ImGui.EndGroup();
            }
        }

        private void ProcessLayerSelector()
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(0.35f);
            ImGuiNET.ImGui.SetNextWindowPos(
                new Vector2((_viewport.Size.X - 300.0f),
                10.0f
            ));

            ImGuiNET.ImGui.Begin("Layers", _windowFlags);
            {
                ImGuiNET.ImGui.PushItemWidth(234.0f);

                ImGuiNET.ImGui.BeginListBox("Layers", new Vector2(0.0f, 5.0f * ImGuiNET.ImGui.GetTextLineHeightWithSpacing()));
                {
                    for (int idx = 0; idx < _layers.Length; idx++)
                    {
                        bool isSelected = (CurrentLayer == idx);

                        if (ImGuiNET.ImGui.Selectable(_layers[idx], isSelected))
                        {
                            CurrentLayer = idx;
                        }

                        if (isSelected)
                        {
                            ImGuiNET.ImGui.SetItemDefaultFocus();
                        }
                    }
                }
                ImGuiNET.ImGui.EndListBox();

                ImGuiNET.ImGui.BeginGroup();
                {
                    bool addClicked = ImGuiNET.ImGui.Button("Add") ? true : false;
                    ImGuiNET.ImGui.SameLine();
                    bool removeClicked = ImGuiNET.ImGui.Button("Remove") ? true : false;

                    if (addClicked && OnAddLayerButtonPressed != null)
                    {
                        OnAddLayerButtonPressed();
                    }

                    if (removeClicked && OnRemoveLayerButtonPressed != null)
                    {
                        OnRemoveLayerButtonPressed();
                    }
                }
                ImGuiNET.ImGui.EndGroup();
            }
            ImGuiNET.ImGui.End();
        }

        private void ProcessMode()
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(0.35f);
            ImGuiNET.ImGui.SetNextWindowPos(new Vector2(10.0f, 10.0f));

            ImGuiNET.ImGui.Begin("App Modes", _windowFlags);
            {
                ImGuiNET.ImGui.BeginGroup();
                {
                    bool spectateClicked = ImGuiNET.ImGui.Button("S") ? true : false;
                    ImGuiNET.ImGui.SameLine();
                    bool workspaceClicked = ImGuiNET.ImGui.Button("W") ? true : false;
                    ImGuiNET.ImGui.SameLine();
                    bool editClicked = ImGuiNET.ImGui.Button("E") ? true : false;

                    if (spectateClicked && OnSpectateModeButtonPressed != null)
                    {
                        OnSpectateModeButtonPressed();
                    }

                    if (workspaceClicked && OnWorkspaceModeButtonPressed != null)
                    {
                        OnWorkspaceModeButtonPressed();
                    }

                    if (editClicked && OnEditModeButtonPressed != null)
                    {
                        OnEditModeButtonPressed();
                    }
                }
                ImGuiNET.ImGui.EndGroup();
            }
            ImGuiNET.ImGui.End();
        }

        private void ProcessLayerProperties()
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(0.35f);
            ImGuiNET.ImGui.SetNextWindowPos(
                new Vector2((_viewport.Size.X - 300.0f),
                152.0f
            ));

            ImGuiNET.ImGui.Begin("Layer Properties", _windowFlags);
            {
                ImGuiNET.ImGui.ColorEdit3("Color", ref _color);

                ImGuiNET.ImGui.DragFloat2("Position", ref _position, 0.01f);
                ImGuiNET.ImGui.DragFloat2("Scale", ref _scale, 0.01f);
                ImGuiNET.ImGui.SliderFloat("Rotation", ref _rotation, -180.0f, 180.0f);
            }
            ImGuiNET.ImGui.End();
        }

        private void ProcessModeInfo()
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(0.35f);
            ImGuiNET.ImGui.SetNextWindowPos(
                new Vector2((_viewport.Size.X - ImGuiNET.ImGui.CalcTextSize(ModeName).X) * 0.5f,
                10.0f
            ));

            ImGuiNET.ImGui.Begin("Text", _windowFlags);
            {
                ImGuiNET.ImGui.Text(ModeName);
            }
            ImGuiNET.ImGui.End();
        }

        public void SetFont(string source, int fontSize)
        {
            ImGuiFontConfig config = new ImGuiFontConfig(source, fontSize);

            _controller = new ImGuiController(_gl, _window, _input, config);
        }
    }
}