using Silk.NET.Input;
using Lab1.App.States;

namespace Lab1.App.States
{
    public class Spectate : AppState
    {
        public Spectate(App app) : base(app) { }

        public new void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            switch (key)
            {
                case Key.Escape:
                    _app.Close();
                    break;

                case Key.Space:
                    _app.MakeAllLayersTransperent(0.2f);
                    _app.Layers[_app.LayerID].Transperent = 1.0f;
                    _app.ChangeState("Workspace");
                    break;
            }
        }
    }
}