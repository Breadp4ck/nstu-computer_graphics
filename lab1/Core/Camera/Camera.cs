using System.Numerics;
using ReMath = Lab1.Math;

namespace Lab1.Core
{
    public class Camera
    {
        public ReMath.Transform Transform;
        private Vector3 _cameraPosition = new Vector3(0.0f, 0.0f, 1.0f);
        private Vector3 _cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 _cameraUp = Vector3.UnitY;
        private Vector3 _cameraDirection = Vector3.Zero;

        public Camera()
        {
            Transform = new ReMath.Transform();
            Transform.Position = _cameraPosition;
        }

        public Vector3 CameraPosition
        {
            get => _cameraPosition;
            set
            {
                _cameraPosition = value;
                Transform.Position = _cameraPosition;
            }
        }

        // TODO: Bullshit. I need Viewport class, that uses Camera
        public Vector2 ViewportSize { get; set; }
        public float ViewportRatioXY { get => ViewportSize.X / ViewportSize.Y; }
        public float ViewportRatioYX { get => ViewportSize.Y / ViewportSize.X; }
    }
}