using Lab1.Core;

namespace Lab1.Main.Scene3D
{
    public class Point3D : VisualInstance3D
    {
        public Color Color { get; set; } = new Color();
        public float Size { get; set; } = 1.0f;

        public override float[] Vertices { get; protected set; } = new float[3] { 0.0f, 0.0f, 0.0f };

        public Point3D(string name) : base(name) { }
    }
}