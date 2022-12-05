using Silk.NET.Input;
using System.Numerics;

namespace Lab1.App.States
{
    public class Grab : AppState
    {
        private Vector2 _initialMousePosition = Vector2.Zero;
        private Vector2 _initialPosition = Vector2.Zero;

        public Grab(App app) : base(app) { }

        public override void Enter()
        {
            _initialMousePosition = _app.MousePosition;

            var position = _app.Layers[_app.LayerID].Transform.Position;
            _initialPosition = new Vector2(position.X, position.Y);

            _app.MouseVisible(false);
        }

        public override void Exit()
        {
            var position = _app.Layers[_app.LayerID].Transform.Position;
            _initialPosition = new Vector2(position.X, position.Y);

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

            Vector2 offset = _initialMousePosition - _app.MousePosition;
            offset.X = -offset.X;

            Vector2 pos = _initialPosition + offset * 0.002f / _app.CameraScale;
            _app.Layers[_app.LayerID].Transform.Position = new Vector3(pos.X, pos.Y, 0.0f);
        }
    }
}