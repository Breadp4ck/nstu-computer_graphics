using Silk.NET.Input;
using Lab1.Core;

namespace Lab1.App.States
{
    public class Edit : AppState
    {
        private Vertex _center = new Vertex();
        private bool _dragging = false;

        public Edit(App app) : base(app) { }

        public override void Enter()
        {
            _app.MakeAllLayersTransperent(0.2f);
            _app.Layers[_app.LayerID].DrawElementInfo = true;
            _app.Layers[_app.LayerID].Transperent = 1.0f;

            _app.Layers[_app.LayerID].ApplyLayerTransform = false;
            _app.AddHoverPoint();
        }

        public override void Exit()
        {
            _dragging = false;

            _app.Layers[_app.LayerID].ApplyLayerTransform = true;
            _app.RemoveHoverPoint();
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
                    _center = _app.HoverablePoint();
                    _dragging = true;
                    break;

                case MouseButton.Right:
                    _app.Layers[_app.LayerID].RemoveLastPoint();
                    _app.Layers[_app.LayerID].ChangeLastPoint(_app.HoverablePoint());
                    break;
            }
        }

        public override void OnMouseUp(IMouse mouse, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    _dragging = false;
                    var to = _center.To(_app.HoverablePoint());

                    _app.AddPoint(_center, _center.To(_app.HoverablePoint()));
                    _app.UpdateHoverPoint(_app.HoverablePoint(), new Vertex());
                    break;
            }
        }

        public override void OnMouseMove(IMouse mouse, System.Numerics.Vector2 position)
        {
            if (_dragging)
            {
                var offset = _app.HoverablePoint();
                _app.UpdateHoverPoint(_center, _center.To(offset));
            }
            else
            {
                _app.UpdateHoverPoint();
            }
        }

        public override void OnMouseScroll(IMouse _mouse, ScrollWheel scroll)
        {
            if (_dragging)
            {
                var offset = _app.HoverablePoint();
                _app.UpdateHoverPoint(_center, _center.To(offset));
            }
            else
            {
                _app.UpdateHoverPoint();
            }
        }
    }
}