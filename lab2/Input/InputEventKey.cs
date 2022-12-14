using Silk.NET.Input;

namespace Lab1.Input
{
    public enum KeyboardButton
    {
        W,
        D,
        S,
        A,
        Space,
        Shift,
        Tab,
        Unknown,
    }

    public class InputEventKey : InputEvent
    {
        public KeyboardButton Button { get; init; }
        public InputEventKey(InputServer server, KeyboardButton button, bool isPressed = false) : base(server)
        {
            Button = button;
            IsInvoked = isPressed;
        }

        public InputEventKey(InputServer server, Key button, bool isPressed = false) : base(server)
        {
            Button = button switch
            {
                Key.W => KeyboardButton.W,
                Key.S => KeyboardButton.S,
                Key.A => KeyboardButton.A,
                Key.D => KeyboardButton.D,
                Key.Space => KeyboardButton.Space,
                Key.ShiftLeft => KeyboardButton.Shift,
                Key.ShiftRight => KeyboardButton.Shift,
                Key.Tab => KeyboardButton.Tab,
                _ => KeyboardButton.Unknown,
            };

            IsInvoked = isPressed;
        }
    }
}