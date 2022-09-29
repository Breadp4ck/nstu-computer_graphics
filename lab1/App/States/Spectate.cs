using Silk.NET.Input;
using Lab1.App.States;

namespace Lab1.App.States
{
    public class Spectate : AppState
    {
        public Spectate(App app) : base(app) { }

        public override void Enter()
        {
            _app.MakeAllLayersTransperent(1.0f);
        }

        public override void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            switch (key)
            {
                case Key.Escape:
                    _app.Close();
                    break;

                case Key.Space:
                    _app.ChangeState("Workspace");
                    break;
            }
        }
    }
}