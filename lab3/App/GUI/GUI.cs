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
        // private ImGuiNET.ImGuiWindowFlags _windowFlags =
        //     ImGuiNET.ImGuiWindowFlags.NoDecoration |
        //     ImGuiNET.ImGuiWindowFlags.AlwaysAutoResize |
        //     ImGuiNET.ImGuiWindowFlags.NoNav |
        //     ImGuiNET.ImGuiWindowFlags.NoSavedSettings |
        //     ImGuiNET.ImGuiWindowFlags.NoFocusOnAppearing |
        //     ImGuiNET.ImGuiWindowFlags.NoMove;
        private ImGuiController _controller;
        private ImGuiNET.ImGuiViewport _viewport;

        private GL _gl;
        private IView _window;
        private IInputContext _input;

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

            // ImGuiNET.ImGui.ShowDemoWindow();

            _controller.Render();
        }
    }
}