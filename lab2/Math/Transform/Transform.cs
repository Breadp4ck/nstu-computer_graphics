using System.Numerics;

namespace Lab1.Math
{
    public class Transform
    {
        public Vector3 Position { get; set; } = new Vector3(0, 0, 0);

        public Vector3 Scale { get; set; } = new Vector3(1.0f, 1.0f, 1.0f);

        public Quaternion Rotation { get; set; } = Quaternion.Identity;

        //Note: The order here does matter.
        public Matrix4x4 ViewMatrix =>
            Matrix4x4.Identity *
            Matrix4x4.CreateScale(Scale) *
            Matrix4x4.CreateFromQuaternion(Rotation) *
            Matrix4x4.CreateTranslation(Position);
    }
}