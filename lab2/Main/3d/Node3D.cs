using System.Numerics;
using ReMath = Lab1.Math;

namespace Lab1.Main.Scene3D
{
    public class Node3D : Node
    {
        public ReMath.Transform Transform { get; set; } = new ReMath.Transform();
        public ReMath.Transform GlobalTransform
        {
            get
            {
                var global = new ReMath.Transform();

                global.Position += Transform.Position;
                global.Scale *= Transform.Scale;
                global.Rotation = Quaternion.Concatenate(global.Rotation, Transform.Rotation);

                if (Parent is Node3D)
                {
                    var parent = ((Node3D)Parent).GlobalTransform;

                    // global.Position += parent.Position;
                    global.Position = parent.Position + parent.Scale * Vector3.Transform(global.Position, parent.Rotation);
                    global.Scale *= parent.Scale;
                    global.Rotation = Quaternion.Concatenate(global.Rotation, parent.Rotation);
                }

                return global;
            }
        }

        public virtual Matrix4x4 View
        {
            get
            {
                if (Parent is Node3D)
                {
                    return Transform.ViewMatrix * ((Node3D)Parent).View;
                }

                return Transform.ViewMatrix;
            }
        }

        public Node3D(string name) : base(name) { }

        public void Translate(Vector3 offset)
        {
            var newTransfotm = Transform;
            newTransfotm.Position += offset;

            Transform = newTransfotm;
        }

        public void Translate(float x, float y, float z)
        {
            var newTransfotm = Transform;
            newTransfotm.Position += new Vector3(x, y, z);

            Transform = newTransfotm;
        }
    }
}