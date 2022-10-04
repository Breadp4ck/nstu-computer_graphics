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

            Actions["movement_forward"] = new InputEventKey(this, KeyboardButton.W);
            Actions["movement_backward"] = new InputEventKey(this, KeyboardButton.S);
            Actions["movement_left"] = new InputEventKey(this, KeyboardButton.A);
            Actions["movement_right"] = new InputEventKey(this, KeyboardButton.D);
        }

        private void HandleRawKeyUpInput(IKeyboard keyboard, Key key, int arg3)
        {
            if (OnInputEmited != null)
            {
                var input = new InputEventKey(this, key, false);
                OnInputEmited!.Invoke(input);

                foreach (var (_, action) in Actions)
                {
                    if (action is InputEventKey && input.Button == ((InputEventKey)action).Button)
                    {
                        action.IsInvoked = input.IsInvoked;
                    }
                }
            }
        }

        private void HandleRawKeyDownInput(IKeyboard keyboard, Key key, int arg3)
        {
            if (OnInputEmited != null)
            {
                var input = new InputEventKey(this, key, true);
                OnInputEmited!.Invoke(input);

                foreach (var (_, action) in Actions)
                {
                    if (action is InputEventKey && input.Button == ((InputEventKey)action).Button)
                    {
                        action.IsInvoked = input.IsInvoked;
                    }
                }
            }
        }

        public bool IsActionPressed(string actionName)
        {
            return Actions[actionName].IsInvoked;
        }

        public bool IsActionReleased(string actionName)
        {
            return !Actions[actionName].IsInvoked;
        }
    }
}