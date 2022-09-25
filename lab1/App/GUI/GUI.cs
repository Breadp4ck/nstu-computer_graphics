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

            ProcessColorPicker();
            ProcessModeInfo();

            _controller.Render();
        }

        public Color Color
        {
            get => new Color(_color.X, _color.Y, _color.Z);
            set
            {
                _color.X = Color.Red;
                _color.Y = Color.Green;
                _color.Z = Color.Blue;
            }
        }

        private void ProcessColorPicker()
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(0.35f);
            ImGuiNET.ImGui.SetNextWindowPos(new Vector2(10.0f, 10.0f));

            ImGuiNET.ImGui.Begin("Color Picker", _windowFlags);
            {
                ImGuiNET.ImGui.ColorEdit3("Color", ref _color);
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