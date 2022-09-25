using Silk.NET.Input;
using Lab1.App.States;
using Lab1.Core;

namespace Lab1.App.States
{
    public class Workspace : AppState
    {
        public Workspace(App app) : base(app) { }

        public override void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            switch (key)
            {
                case Key.Escape:
                    _app.MakeAllLayersTransperent(1.0f);
                    _app.ChangeState("Spectate");
                    break;

                case Key.Z:
                    _app.PreviousLayer();
                    _app.SetGuiColorInputByLayerColor(_app.LayerID);
                    break;

                case Key.X:
                    _app.NextLayer();
                    _app.SetGuiColorInputByLayerColor(_app.LayerID);
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
                        _app.LayerID = _app.Layers.Count - 1;
                    }

                    _app.MakeAllLayersTransperent(0.2f);
                    _app.Layers[_app.LayerID].Transperent = 1.0f;

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