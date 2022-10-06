using ReMath = Lab1.Math;
using System.Numerics;

namespace Lab1.Render
{
    public interface ICamera
    {
        public short VisualMask { get; set; }
        public float MinDistance { get; set; }
        public float MaxDistance { get; set; }
        public float Fov { get; set; }
        public Matrix4x4 View { get; }
        public Vector3 Position { get; }
        public CameraMode Mode { get; set; }
        public Matrix4x4 GetProjection(Vector2 viewportSize);
    }
}