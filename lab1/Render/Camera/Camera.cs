using System.Numerics;
using ReMath = Lab1.Math;

namespace Lab1.Render
{
    public abstract class Camera
    {
        private Vector3 _cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 _cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 _cameraUp = Vector3.UnitY;
        private Vector3 _cameraDirection = Vector3.Zero;

        public short VisualMask { get; set; } = 1;
        public float MinDistance { get; set; } = 0.001f;
        public float MaxDistance { get; set; } = 100.0f;
        public ReMath.Transform Transform { get; set; } = new ReMath.Transform();

        public Camera() { }
    }
}