using ReMath = Lab1.Math;

namespace Lab1.Scene.Scene3D
{
    public abstract class Node3D : Node
    {
        public ReMath.Transform Transform { get; set; } = new ReMath.Transform();

        public Node3D(Scene scene, string name) : base(scene, name) { }
    }
}