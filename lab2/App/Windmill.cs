using Lab1.Main.Scene3D;
using Lab1.Core;
using Lab1.Resources;

using System.Numerics;


namespace Lab1.App
{
    public class WindMill : Node3D
    {
        private float _angle = 0.0f;
        private MeshInstance3D _mill = new MeshInstance3D("Мельничные крылья");
        private MeshInstance3D _roller = new MeshInstance3D("Вал");
        private MillFoundation _foundation = new MillFoundation("Основание");
        private List<Sail> _sails = new List<Sail>();

        public class Sail : Node3D
        {
            private MeshInstance3D _sail;
            private MeshInstance3D _foundation;
            public Sail(string name) : base(name)
            {
                _sail = new MeshInstance3D($"{name}, крыло");
                _foundation = new MeshInstance3D($"{name}, основание");

                _sail.MeshData = new CubePrimitive();
                _foundation.MeshData = new CubePrimitive();

                _sail.MaterialResource = new StandartMaterialResource();
                _foundation.MaterialResource = new StandartMaterialResource();

                _sail.Transform.Scale = new Vector3(0.3f, 1.0f, 0.02f);
                _sail.Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, -0.2f);
                _sail.Translate(0.0f, 0.3f, 0.0f);

                _foundation.Transform.Scale = new Vector3(0.05f, 1.0f, 0.05f);

                AddChild(_sail);
                AddChild(_foundation);
            }
        }

        public class MillFoundation : Node3D
        {
            private MeshInstance3D[] _lowLevel = {
                new MeshInstance3D("Нижний ярус 1"),
                new MeshInstance3D("Нижний ярус 2"),
                new MeshInstance3D("Нижний ярус 3"),
                new MeshInstance3D("Нижний ярус 4"),
                new MeshInstance3D("Нижний ярус 5"),
                new MeshInstance3D("Нижний ярус 6"),
            };

            private MeshInstance3D[] _highLevel = {
                new MeshInstance3D("Верхний ярус 1"),
                new MeshInstance3D("Верхний ярус 2"),
                new MeshInstance3D("Верхний ярус 3"),
                new MeshInstance3D("Верхний ярус 4"),
                new MeshInstance3D("Верхний ярус 5"),
                new MeshInstance3D("Верхний ярус 6"),
            };

            private MeshInstance3D[] _roof = {
                new MeshInstance3D("Крыша 1"),
                new MeshInstance3D("Крыша 2"),
                new MeshInstance3D("Крыша 3"),
                new MeshInstance3D("Крыша 4"),
                new MeshInstance3D("Крыша 5"),
                new MeshInstance3D("Крыша 6"),
            };

            public MillFoundation(string name) : base(name)
            {
                var material = new StandartMaterialResource();

                for (int idx = 0; idx < 6; idx++)
                {
                    _lowLevel[idx].MeshData = new CubePrimitive();
                    _highLevel[idx].MeshData = new CubePrimitive();
                    _roof[idx].MeshData = new CubePrimitive();

                    _lowLevel[idx].MaterialResource = material;
                    _highLevel[idx].MaterialResource = material;
                    _roof[idx].MaterialResource = material;

                    var angle = Math.Functions.ToRadians(60.0f * idx);

                    _lowLevel[idx].Transform.Scale = new Vector3(3.0f, 4.0f, 0.5f);
                    _lowLevel[idx].Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle);
                    _lowLevel[idx].Translate(5.0f * (float)System.Math.Sin(angle), 0.0f, 5.0f * (float)System.Math.Cos(angle));

                    _highLevel[idx].Transform.Scale = new Vector3(2.5f, 5.0f, 0.5f);
                    _highLevel[idx].Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle);
                    _highLevel[idx].Translate(4.5f * (float)System.Math.Sin(angle), 8.0f, 4.5f * (float)System.Math.Cos(angle));

                    _roof[idx].Transform.Scale = new Vector3(2.5f, 2.0f, 2.5f);
                    _roof[idx].Transform.Rotation =
                        Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle) *
                        Quaternion.CreateFromAxisAngle(Vector3.UnitX, -0.3f);
                    _roof[idx].Translate(2.5f * (float)System.Math.Sin(angle), 14.0f, 2.5f * (float)System.Math.Cos(angle));

                    AddChild(_lowLevel[idx]);
                    AddChild(_highLevel[idx]);
                    AddChild(_roof[idx]);
                }
            }
        }

        public WindMill(string name, ushort sailCount) : base(name)
        {
            _roller.MeshData = new CubePrimitive();
            _roller.MaterialResource = new StandartMaterialResource();

            _roller.Transform.Scale = new Vector3(0.4f, 0.4f, 2.0f);
            _roller.Translate(0.0f, 0.0f, 1.0f);

            var sailMaterial = new StandartMaterialResource();

            for (int idx = 0; idx < sailCount; idx++)
            {
                var sail = new Sail($"Sail {idx}");
                var angle = Math.Functions.ToRadians(360.0f / sailCount * idx);

                sail.Transform.Scale = new Vector3(4.5f);
                sail.Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, -angle);
                sail.Translate(5.0f * (float)System.Math.Sin(angle), 5.0f * (float)System.Math.Cos(angle), -0.5f);

                _sails.Add(sail);
            }

            _mill.AddChild(_roller);

            foreach (var sail in _sails)
            {
                _mill.AddChild(sail);
            }

            _mill.Translate(0.0f, 14.0f, -6.0f);
            _foundation.Translate(0.0f, 4.0f, 0.0f);

            AddChild(_mill);
            AddChild(_foundation);
        }

        public override void Process(float delta)
        {
            base.Process(delta);

            _angle += delta;
            _mill.Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, _angle);
        }
    }
}