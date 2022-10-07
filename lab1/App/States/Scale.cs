using Silk.NET.Input;
using System.Numerics;

namespace Lab1.App.States
{
    public class Scale : AppState
    {
        private Vector2 _initialMousePosition = Vector2.Zero;
        private float _initialScaleFactor = 1.0f;

        public Scale(App app) : base(app) { }

        public override void Enter()
        {
            _initialMousePosition = _app.MousePosition;
            _initialScaleFactor = _app.Layers[_app.LayerID].Transform.Scale;
        }

        public override void Exit()
        {
            _initialScaleFactor = _app.Layers[_app.LayerID].Transform.Scale;
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
            _app.UpdateHoverVertexPosition();

            _app.Layers[_app.LayerID].Transform.Scale = _initialScaleFactor + (_initialMousePosition.X - _app.MousePosition.X) * 0.01f;
        }
    }
}