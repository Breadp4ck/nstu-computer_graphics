using Lab1.Core;
using System.Numerics;

namespace Lab1.Render
{
    public interface IPointLight
    {
        public Color Diffuse { get; }
        public Color Specular { get; }
        public Vector3 Position { get; }
        public float Strength { get; }

        public float Constant { get; }
        public float Linear { get; }
        public float Quadratic { get; }
    }
}