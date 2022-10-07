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
        private Vector3 _color;
        private Vector2 _position = Vector2.Zero;
        private Vector2 _scale = Vector2.Zero;
        private float _rotation = 0.0f;
        private int _currentLayer = 0;
        private string[] _layers = new string[3] { "kek", "lol", "arbidol" };

        private GL _gl;
        private IView _window;
        private IInputContext _input;

        public string ModeName = "default";

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

            ImGuiNET.ImGui.ShowDemoWindow();

            _controller.Render();
        }

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

        private void ProcessLayerSelector()
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(0.35f);
            ImGuiNET.ImGui.SetNextWindowPos(
                new Vector2((_viewport.Size.X - 300.0f),
                10.0f
            ));

            ImGuiNET.ImGui.Begin("Layers", _windowFlags);
            {
                ImGuiNET.ImGui.BeginListBox("Layers");
                {
                    ImGuiNET.ImGui.Selectable("Item 1");
                    ImGuiNET.ImGui.SmallButton("x");
                    ImGuiNET.ImGui.Selectable("Item 2");
                    ImGuiNET.ImGui.Selectable("Item 3");
                }
                ImGuiNET.ImGui.EndListBox();
            }
            ImGuiNET.ImGui.End();
        }

        private void ProcessLayerProperties()
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(0.35f);
            ImGuiNET.ImGui.SetNextWindowPos(
                new Vector2((_viewport.Size.X - 300.0f),
                100.0f
            ));

            ImGuiNET.ImGui.Begin("Layer Properties", _windowFlags);
            {
                ImGuiNET.ImGui.ColorEdit3("Color", ref _color);

                ImGuiNET.ImGui.DragFloat2("Position", ref _position);
                ImGuiNET.ImGui.DragFloat2("Scale", ref _scale);
                ImGuiNET.ImGui.SliderFloat("Rotation", ref _rotation, 0.0f, 360.0f);
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