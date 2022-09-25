using Silk.NET.Input;
using Lab1.App;

namespace Lab1.App.States
{
    public abstract class AppState
    {
        protected App _app = null!;

        public AppState(App app)
        {
            _app = app;
        }

        public void Enter() { }
        public void Render(float delta) { }
        public void Exit() { }

        public void OnKeyDown(IKeyboard keyboard, Key key, int arg3) { }
        public void OnKeyUp(IKeyboard keyboard, Key key, int arg3) { }

        public void OnMouseDown(IMouse mouse, MouseButton button) { }
        public void OnMouseUp(IMouse mouse, MouseButton button) { }
        public void OnMouseMove(IMouse mouse, System.Numerics.Vector2 position) { }
        public void OnMouseScroll(IMouse _mouse, ScrollWheel scroll) { }
    }
}