using Lab1.Core;

namespace Lab1.Scene.Scene3D
{
    public class Point3D : VisualInstance3D
    {
        public Color Color { get; set; } = new Color();
        public float Size { get; set; } = 1.0f;

        public override float[] Vertices { get; protected set; } = new float[3] { 0.0f, 0.0f, 0.0f };

        public Point3D(Scene scene, string name) : base(scene, name) { }
    }
}