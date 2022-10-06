using Lab1.Core;
using System.Numerics;

namespace Lab1.Render
{
    public interface IDirectionalLight
    {
        public Color Diffuse { get; }
        public Color Specular { get; }
        public Vector3 Direction { get; }
        public float Strength { get; }
    }
}