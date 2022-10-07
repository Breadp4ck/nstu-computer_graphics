using Lab1.Main.Scene3D;
using Lab1.Core;
using System.Numerics;

namespace Lab1.App
{
    public class Lamp : Node3D
    {
        private LightCube _lamp = new LightCube("Лампа");
        private PointLight3D _light = new PointLight3D("Свет");

        public float Offset { get; protected set; } = 0.0f;

        public Color Color
        {
            get => _lamp.Color;
            set
            {
                _light.Diffuse = value;
                _lamp.Color = value;
            }
        }

        public Lamp(string name) : base(name)
        {
            _lamp.Transform.Scale = 0.1f;

            AddChild(_lamp);
            AddChild(_light);
        }

        public override void Process(float delta)
        {
            base.Process(delta);

            Vector3 newPosition = Transform.Position;
            newPosition.X = (float)System.Math.Sin(Offset * 2.0f) * 3.0f;

            Transform.Position = newPosition;

            Offset += delta;
        }
    }
}