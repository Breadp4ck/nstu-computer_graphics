using Lab1.Main.Scene3D;
using Lab1.Core;
using Lab1.Resources;

using System.Numerics;


namespace Lab1.App
{
    public class Spot : MeshInstance3D
    {
        private float _angle = 0.0f;

        private MeshInstance3D _foundation = new MeshInstance3D("Каркас");
        private LightCube _flashlight = new LightCube("Лампа");
        private SpotLight3D _light = new SpotLight3D("Свет");

        public Color Color
        {
            get => _flashlight.Color;
            set
            {
                _light.Diffuse = value;
                _flashlight.Color = value;
            }
        }

        public Spot(string name) : base(name)
        {
            _foundation.MeshData = new CubePrimitive();
            _foundation.MaterialResource = new StandartMaterialResource();

            _foundation.Transform.Scale = 0.2f;
            _flashlight.Transform.Scale = 0.1f;

            _flashlight.Translate(0.0f, 0.0f, 0.3f);
            _light.Translate(0.0f, 0.0f, 0.3f);

            AddChild(_foundation);
            AddChild(_flashlight);
            AddChild(_light);
        }

        public override void Process(float delta)
        {
            base.Process(delta);

            Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, _angle);
            _light.Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, _angle);

            Color = Color.FromHSV(_angle * 0.35f, 0.95f, 0.7f);

            _angle += delta;
        }
    }

}