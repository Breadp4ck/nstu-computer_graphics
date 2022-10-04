using System.Numerics;
using ReMath = Lab1.Math;

namespace Lab1.Main.Scene3D
{
    public class Node3D : Node
    {
        public ReMath.Transform Transform { get; set; } = new ReMath.Transform();

        public Node3D(string name) : base(name) { }


        public void Translate(Vector3 offset)
        {
            Transform.Position += offset;
        }

        public void Translate(float x, float y, float z)
        {
            Transform.Position += new Vector3(x, y, z);
        }
    }
}