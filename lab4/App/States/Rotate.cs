using Silk.NET.Input;
using System.Numerics;

namespace Lab1.App.States
{
    public class Rotate : AppState
    {
        private Vector2 _initialMousePosition = Vector2.Zero;
        private float _initialRotateFactor = 0.0f;

        public Rotate(App app) : base(app) { }

        public override void Enter()
        {
            _initialMousePosition = _app.MousePosition;
            // TODO: set rotation of current layer

            _app.MouseVisible(false);
        }

        public override void Exit()
        {
            _initialRotateFactor = _initialRotateFactor + (_initialMousePosition.X - _app.MousePosition.X) * 0.01f;

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

            _app.Layers[_app.LayerID].Transform.Rotation = Quaternion.CreateFromRotationMatrix(
                Matrix4x4.CreateRotationZ(
                    _initialRotateFactor + (_initialMousePosition.X - _app.MousePosition.X) * 0.01f
                )
            );
        }
    }
}