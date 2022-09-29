using Silk.NET.Input;

namespace Lab1.App.States
{
    public class Workspace : AppState
    {
        public Workspace(App app) : base(app) { }

        public override void Enter()
        {
            _app.MakeAllLayersTransperent(0.2f);
            _app.Layers[_app.LayerID].Transperent = 1.0f;
        }

        public override void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            switch (key)
            {
                case Key.Escape:
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

                    _app.ChangeState("Edit");
                    break;

                case Key.G:
                    _app.ChangeState("Grab");
                    break;

                case Key.R:
                    _app.ChangeState("Rotate");
                    // _app.RemoveLayer();
                    break;


                case Key.S:
                    _app.ChangeState("Scale");
                    break;


                case Key.Space:
                    _app.ChangeState("Edit");
                    break;
            }
        }
    }
}