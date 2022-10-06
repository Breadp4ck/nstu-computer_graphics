using Lab1.Render;
using Lab1.Core;
using System.Numerics;

namespace Lab1.Main.Scene3D
{
    public class PointLight3D : Node3D, IPointLight
    {
        public Color Diffuse { get; set; } = new Color(1.0f, 1.0f, 1.0f);
        public Color Specular { get; set; } = new Color(1.0f, 1.0f, 1.0f);
        public Vector3 Position { get => Transform.Position; set => Transform.Position = value; }
        public float Strength { get; set; } = 1.0f;

        public float Constant { get; set; } = 1.0f;
        public float Linear { get; } = 0.09f;
        public float Quadratic { get; } = 0.032f;

        public PointLight3D(string name) : base(name) { }
    }
}