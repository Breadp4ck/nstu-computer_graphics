using Lab1.Core;
using Lab1.Render;

namespace Lab1.Main.Scene3D
{
    public class Environment3D : Node3D, IEnvironment
    {
        //public Color Ambient { get; set; } = new Color();
        public Color Ambient { get; set; } = new Color(0.2f, 0.01f, 0.1f);
        public Color SkyColor { get; set; } = new Color();

        public Environment3D(string name) : base(name) { }
    }
}