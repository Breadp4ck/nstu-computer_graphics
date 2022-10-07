using Lab1.Render;
using System.Numerics;

using ReMath = Lab1.Math;

namespace Lab1.Main.Scene3D
{
    public class Camera3D : Node3D, ICamera
    {
        protected Vector3 _cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
        protected Vector3 _cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        protected Vector3 _cameraUp = Vector3.UnitY;
        protected Vector3 _cameraDirection = Vector3.Zero;

        public Camera3D(string name) : base(name) { }

        public short VisualMask { get; set; } = 1;
        public float MinDistance { get; set; } = 0.001f;
        public float MaxDistance { get; set; } = 100.0f;
        public float Fov { get; set; } = Lab1.Math.Functions.ToRadians(70.0f);
        public Vector3 Position { get => Transform.Position; }

        public CameraMode Mode { get; set; } = CameraMode.Perspective;

        public override Matrix4x4 View
        {
            get => Matrix4x4.CreateLookAt(GlobalTransform.Position, GlobalTransform.Position + _cameraFront, _cameraUp);
            // Matrix4x4.CreateTranslation(-GlobalTransform.Position) *
            // Matrix4x4.CreateFromQuaternion(GlobalTransform.Rotation) *
            // Matrix4x4.CreateScale(GlobalTransform.Scale);
        }

        public Matrix4x4 GetProjection(Vector2 viewportSize)
        {
            switch (Mode)
            {
                case CameraMode.Perspective:
                    return Matrix4x4.CreatePerspectiveFieldOfView(Fov, viewportSize.X / viewportSize.Y, MinDistance, MaxDistance);

                case CameraMode.Orthographic:
                    return Matrix4x4.CreateOrthographic(2.0f, 2.0f * viewportSize.Y / viewportSize.X, MinDistance, MaxDistance);
            }

            return Matrix4x4.Identity; // Rust is better
        }
    }
}