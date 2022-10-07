using Lab1.Core;
using Lab1.Render;
using System.Numerics;

namespace Lab1.Main.Scene3D
{
    public class DirectionalLight3D : Node3D, IDirectionalLight
    {
        public Color Diffuse { get; set; } = new Color(1.0f, 1.0f, 1.0f);
        public Color Specular { get; set; } = new Color(0.5f, 0.5f, 0.5f);
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

        public DirectionalLight3D(string name) : base(name) { }
    }
}