using Lab1.Render;
using System.Numerics;

using ReMath = Lab1.Math;

namespace Lab1.Main.Scene3D
{
    public class Camera3D : Node3D, ICamera
    {
        private Vector3 _cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 _cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 _cameraUp = Vector3.UnitY;
        private Vector3 _cameraDirection = Vector3.Zero;

        public Camera3D(Scene scene, string name) : base(scene, name) { }

        public short VisualMask { get; set; } = 1;
        public float MinDistance { get; set; } = 0.001f;
        public float MaxDistance { get; set; } = 100.0f;
    }
}