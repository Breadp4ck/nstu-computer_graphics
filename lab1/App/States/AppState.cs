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

        public virtual void Enter() { }
        public virtual void Render(double delta) { }
        public virtual void Exit() { }

        public virtual void OnKeyDown(IKeyboard keyboard, Key key, int arg3) { }
        public virtual void OnKeyUp(IKeyboard keyboard, Key key, int arg3) { }

        public virtual void OnMouseDown(IMouse mouse, MouseButton button) { }
        public virtual void OnMouseUp(IMouse mouse, MouseButton button) { }
        public virtual void OnMouseMove(IMouse mouse, System.Numerics.Vector2 position) { }
        public virtual void OnMouseScroll(IMouse _mouse, ScrollWheel scroll) { }
    }
}