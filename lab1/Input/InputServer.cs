using Silk.NET.Input;

namespace Lab1.Input
{
    public delegate void InputEmited(InputEvent inputEvent);

    public class InputServer
    {
        private IInputContext _input;
        private IKeyboard? _keyboard;
        private IMouse? _mouse;

        public Dictionary<string, InputEvent> Actions { get; set; } = new Dictionary<string, InputEvent>();
        public event InputEmited? OnInputEmited;

        public InputServer(IInputContext input)
        {
            _input = input;

            _keyboard = _input.Keyboards.FirstOrDefault();
            _mouse = _input.Mice.FirstOrDefault();

            // TODO: handle null
            _keyboard!.KeyUp += HandleRawKeyUpInput;
            _keyboard!.KeyDown += HandleRawKeyDownInput;

            Actions["move_forward"] = new InputEventKey(this, KeyboardButton.W);
            Actions["move_backward"] = new InputEventKey(this, KeyboardButton.S);
            Actions["move_left"] = new InputEventKey(this, KeyboardButton.A);
            Actions["move_right"] = new InputEventKey(this, KeyboardButton.D);
        }

        private void HandleRawKeyUpInput(IKeyboard keyboard, Key key, int arg3)
        {
            if (OnInputEmited != null)
            {
                OnInputEmited!.Invoke(new InputEventKey(this, key, false));
            }
        }

        private void HandleRawKeyDownInput(IKeyboard keyboard, Key key, int arg3)
        {
            if (OnInputEmited != null)
            {
                OnInputEmited!.Invoke(new InputEventKey(this, key, true));
            }
        }

        public bool IsActionPressed(string inputName)
        {
            return Actions[inputName].IsInvoked;
        }

        public bool IsActionReleased(string inputName)
        {
            return !Actions[inputName].IsInvoked;
        }
    }
}