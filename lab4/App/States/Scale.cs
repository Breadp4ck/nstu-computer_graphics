using Silk.NET.Input;
using System.Numerics;

namespace Lab1.App.States
{
    public class Scale : AppState
    {
        private Vector2 _initialMousePosition = Vector2.Zero;
        private Vector2 _initialScaleFactor = Vector2.One;

        public Scale(App app) : base(app) { }

        public override void Enter()
        {
            _initialMousePosition = _app.MousePosition;

            var scale = _app.Layers[_app.LayerID].Transform.Scale;
            _initialScaleFactor = new Vector2(scale.X, scale.Y);

            _app.MouseVisible(false);
        }

        public override void Exit()
        {
            var scale = _app.Layers[_app.LayerID].Transform.Scale;
            _initialScaleFactor = new Vector2(scale.X, scale.Y);

            _app.MouseVisible(true);
        }

        public override void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            switch (key)
            {
                case Key.Escape:
                    _app.ChangeState("Workspace");
                    break;
            }
        }

        public override void OnMouseDown(IMouse mouse, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    _app.ChangeState("Workspace");
                    break;

                case MouseButton.Right:
                    _app.ChangeState("Workspace");
                    break;
            }
        }

        public override void OnMouseMove(IMouse mouse, System.Numerics.Vector2 position)
        {
            _app.UpdateHoverPoint();
            _app.UpdateLayerGuiData(_app.LayerID);

            var offset = _initialMousePosition - _app.MousePosition;
            offset.X = -offset.X;

            var scale = _initialScaleFactor + offset * 0.01f;
            _app.Layers[_app.LayerID].Transform.Scale = new Vector3(scale.X, scale.Y, _app.Layers[_app.LayerID].Transform.Scale.Z);
        }
    }
}