using Lab1.Core;
using System.Numerics;

namespace Lab1.Render
{
    public class DirectionalLightEmpty : IDirectionalLight
    {
        public Color Diffuse { get; } = new Color(0.0f, 0.0f, 0.0f);
        public Color Specular { get; } = new Color(0.0f, 0.0f, 0.0f);
        public Vector3 Direction { get; } = -Vector3.UnitY;
        public float Strength { get; } = 0.0f;
    }
}