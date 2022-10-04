using Silk.NET.Input;

namespace Lab1.Input
{
    public enum MouseButton
    {
        LeftClick = Silk.NET.Input.MouseButton.Left,
        RightClick = Silk.NET.Input.MouseButton.Right,
        MiddleClick = Silk.NET.Input.MouseButton.Middle,
    }

    public class InputMouseButton : InputEvent
    {
        public InputMouseButton(InputServer server) : base(server) { }
    }
}