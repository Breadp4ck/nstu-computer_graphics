namespace Lab1.Input
{
    public abstract class InputEvent
    {
        protected InputServer _server;
        public bool IsInvoked { get; internal set; } = false;

        public InputEvent(InputServer server)
        {
            _server = server;
        }

        public bool IsActionPressed(string actionName)
        {
            foreach (var (name, _) in _server.Actions)
            {
                if (name == actionName)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsActionReleased(string actionName)
        {
            foreach (var (name, _) in _server.Actions)
            {
                if (name == actionName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}