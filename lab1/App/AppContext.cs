using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;
using System.Numerics;

using Lab1.Core;

namespace Lab1.App
{
    public class AppContext
    {
        internal IInputContext? Input;
        internal IKeyboard? Keyboard;
        internal IMouse? Mouse;
        internal GL? Gl;
        internal Gui? Gui;

        public bool Initialized { get; private set; } = false;

        public AppContext() { }

        public void AttachWindow(IWindow window)
        {
            Input = window.CreateInput();
            Gl = window.CreateOpenGL();

            Gui = new Gui(Gl, window, Input);
            Gui.SetFont("App/Fonts/Raleway/Raleway-Regular.ttf", 14);

            Keyboard = Input.Keyboards.FirstOrDefault();
            Mouse = Input.Mice.FirstOrDefault();

            Initialized = true;
        }

        internal void FrameSetup(Color background)
        {
            if (Initialized)
            {
                Gl!.ClearColor(background.Red, background.Green, background.Blue, 1.0f);

                Gl!.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

                Gl!.Enable(EnableCap.DepthTest);
                Gl!.Enable(EnableCap.Blend);
                Gl!.Enable(EnableCap.LineSmooth);
                Gl!.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            }
        }

        internal Vector2 MousePosition
        {
            get => Mouse!.Position;
        }
    }
}