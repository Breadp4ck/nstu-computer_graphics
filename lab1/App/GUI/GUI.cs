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
        private string[] _layers = new string[3] { "kek", "lol", "arbidol" };

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

            ProcessModeInfo();
            ProcessLayerSelector();
            ProcessLayerProperties();

            // ImGuiNET.ImGui.ShowDemoWindow();

            _controller.Render();
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
                ImGuiNET.ImGui.ListBox("Layers", ref _currentLayer, _layers, _layers.Length);

                ImGuiNET.ImGui.BeginGroup();
                {
                    ImGuiNET.ImGui.Button("Add");
                    ImGuiNET.ImGui.SameLine();
                    ImGuiNET.ImGui.Button("Remove");
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
                124.0f
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