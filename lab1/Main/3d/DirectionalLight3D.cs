using Lab1.Core;
using Lab1.Render;
using System.Numerics;

namespace Lab1.Main.Scene3D
{
    public class DirectionalLight3D : Node3D, IDirectionalLight
    {
        public Color Diffuse { get; set; } = new Color(1.0f, 1.0f, 1.0f);
        public Color Specular { get; set; } = new Color(0.5f, 0.5f, 0.5f);
        public Vector3 Direction { get; set; } = new Vector3(-1.0f, -1.0f, 0.0f);
        public float Strength { get; set; } = 1.0f;

        public DirectionalLight3D(string name) : base(name) { }
    }
}