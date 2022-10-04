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
        Unknown,
    }

    public class InputEventKey : InputEvent
    {
        KeyboardButton Button { get; init; }
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
                _ => KeyboardButton.Unknown,
            };

            IsInvoked = isPressed;
        }
    }
}