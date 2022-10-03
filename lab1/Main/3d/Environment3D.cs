using Lab1.Core;
using Lab1.Render;

namespace Lab1.Main.Scene3D
{
    public class Environment3D : Node3D, IEnvironment
    {
        public Color Ambient { get; set; } = new Color();
        public Color SkyColor { get; set; } = new Color();

        public Environment3D(string name) : base(name) { }
    }
}