using Lab1.Core;
using System.Numerics;

namespace Lab1.Render
{
    public class PointLightEmpty : IPointLight
    {
        public Color Diffuse { get; } = new Color();
        public Color Specular { get; } = new Color();

        public Vector3 Position { get; } = Vector3.Zero;

        public float Strength { get; } = 0.0f;

        public float Constant { get; } = 1.0f;
        public float Linear { get; } = 0.0f;
        public float Quadratic { get; } = 0.0f;
    }
}