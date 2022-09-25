using Silk.NET.Input;
using Lab1.App.States;
using Lab1.Core;

namespace Lab1.App.States
{
    public class Workspace : AppState
    {
        public Workspace(App app) : base(app) { }

        public new void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            switch (key)
            {
                case Key.Escape:
                    _app.MakeAllLayersTransperent(1.0f);
                    _app.ChangeState("Spectate");
                    break;

                case Key.Z:
                    _app.PreviousLayer();
                    break;

                case Key.X:
                    _app.NextLayer();
                    break;

                case Key.N:
                    if (_app.Layers.Last().GetVerticesCount() == 0)
                    {
                        _app.LayerID = _app.Layers.Count - 1;
                        _app.ChangeState("Edit");
                    }
                    else
                    {
                        _app.AddLayer();
                    }

                    _app.ChangeState("Edit");
                    _app.AddHoverVertex();

                    break;

                case Key.R:
                    _app.RemoveLayer();
                    break;


                case Key.Space:
                    _app.ChangeState("Edit");
                    _app.AddHoverVertex();
                    break;
            }
        }
    }
}