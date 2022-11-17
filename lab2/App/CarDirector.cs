using Lab1.Main.Scene3D;
using Lab1.Core;
using System.Numerics;

namespace Lab1.App
{
    public class CarDirector : Node3D
    {
        private float _distance = 32.0f;

        public Car Car1 { get; private set; } = new Car("Автомобиль 1");
        public Car Car2 { get; private set; } = new Car("Автомобиль 2");

        public CarDirector(string name) : base(name)
        {
            AddChild(Car1);
            AddChild(Car2);

            Car2.Speed = 25.0f;
            Car2.Color = new Color(0.2f, 0.2f, 0.6f);
            Car2.Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, (float)System.Math.PI);

        }

        public override void Process(float delta)
        {
            base.Process(delta);

            var offset1 = (Car1.Transform.Position.X + _distance / 2.0f + delta * Car1.Speed) % _distance - _distance / 2.0f;
            var offset2 = (-Car2.Transform.Position.X + _distance / 2.0f + delta * Car2.Speed) % _distance - _distance / 2.0f;

            Car1.Transform.Position = new Vector3(offset1, 0.0f, -8.5f);
            Car2.Transform.Position = new Vector3(-offset2, 0.0f, -11.5f);
        }
    }
}