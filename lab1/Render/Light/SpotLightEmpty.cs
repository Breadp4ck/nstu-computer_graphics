using Lab1.Core;
using System.Numerics;

namespace Lab1.Render
{
    public class SpotLightEmpty : ISpotLight
    {
        public Color Diffuse { get; } = new Color();
        public Color Specular { get; } = new Color();
        public Vector3 Position { get; } = Vector3.Zero;
        public Vector3 Direction { get; } = -Vector3.UnitY;
        public float Strength { get; } = 0.0f;

        public float CutOff { get; } = 0.25f;
        public float OuterCutOff { get; } = 0.75f;

        public float Constant { get; } = 1.0f;
        public float Linear { get; } = 0.0f;
        public float Quadratic { get; } = 0.0f;
    }
}