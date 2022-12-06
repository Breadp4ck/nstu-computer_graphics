using System.Numerics;

namespace Lab1.Math
{
    public record struct Transform
    {
        public Vector3 Position { get; set; } = Vector3.Zero;

        public Vector3 Scale { get; set; } = Vector3.One;

        public Quaternion Rotation { get; set; } = Quaternion.Identity;

        public Transform() { }

        //Note: The order here does matter.
        public Matrix4x4 ViewMatrix =>
            Matrix4x4.Identity *
            Matrix4x4.CreateScale(Scale) *
            Matrix4x4.CreateFromQuaternion(Rotation) *
            Matrix4x4.CreateTranslation(Position);
    }
}