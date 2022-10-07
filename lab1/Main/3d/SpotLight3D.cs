using Lab1.Render;
using Lab1.Core;
using System.Numerics;

namespace Lab1.Main.Scene3D
{
    public class SpotLight3D : Node3D, ISpotLight
    {
        public Color Diffuse { get; set; } = new Color(1.0f, 1.0f, 1.0f);
        public Color Specular { get; set; } = new Color(1.0f, 1.0f, 1.0f);
        public Vector3 Position { get => GlobalTransform.Position; }
        public Vector3 Direction
        {
            get
            {
                var rotation = Transform.Rotation;

                return new Vector3(
                    2.0f * (rotation.X * rotation.Z + rotation.W * rotation.Y),
                    2.0f * (rotation.Y * rotation.Z - rotation.W * rotation.X),
                    1.0f - 2.0f * (rotation.X * rotation.X + rotation.Y * rotation.Y)
                );
            }

        }
        public float Strength { get; set; } = 1.0f;
        public float CutOff { get; set; } = 0.97f;
        public float OuterCutOff { get; set; } = 0.96f;

        public float Constant { get; set; } = 1.0f;
        public float Linear { get; } = 0.09f;
        public float Quadratic { get; } = 0.032f;

        public SpotLight3D(string name) : base(name) { }
    }
}