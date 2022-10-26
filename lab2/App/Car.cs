using System.Numerics;
using Lab1.Main.Scene3D;
using Lab1.Resources;

namespace Lab1.App
{
    public class Car : Node3D
    {
        private MeshInstance3D _body = new MeshInstance3D("Корпус");
        private MeshInstance3D _cabin = new MeshInstance3D("Кабина");
        private MeshInstance3D[] _wheels = {
            new MeshInstance3D("Переднее правое колесо"),
            new MeshInstance3D("Переднее левое колесо"),
            new MeshInstance3D("Заднее правое колесо"),
            new MeshInstance3D("Заднее левое колесо"),
        };
        private MeshInstance3D[] _windows = {
            new MeshInstance3D("Переднее окно"),
            new MeshInstance3D("Заднее окно"),
            new MeshInstance3D("Правое окно"),
            new MeshInstance3D("Левое окно"),
        };
        private MeshInstance3D[] _lights = {
            new MeshInstance3D("Передняя правая фара"),
            new MeshInstance3D("Передняя левая фара"),
            new MeshInstance3D("Задняя правая фара"),
            new MeshInstance3D("Задняя левая фара"),
        };

        private StandartMaterialResource _carMaterial = new StandartMaterialResource();
        private StandartMaterialResource _wheelMaterial = new StandartMaterialResource();
        private StandartMaterialResource _windowMaterial = new StandartMaterialResource();
        private ColorMaterialResource _headlightMaterial = new ColorMaterialResource();
        private ColorMaterialResource _backlightMaterial = new ColorMaterialResource();

        public Car(string name) : base(name)
        {
            // Меши

            _wheels[0].MeshData = new CubePrimitive();
            _wheels[1].MeshData = new CubePrimitive();
            _wheels[2].MeshData = new CubePrimitive();
            _wheels[3].MeshData = new CubePrimitive();

            _windows[0].MeshData = new CubePrimitive();
            _windows[1].MeshData = new CubePrimitive();
            _windows[2].MeshData = new CubePrimitive();
            _windows[3].MeshData = new CubePrimitive();

            _lights[0].MeshData = new CubePrimitive();
            _lights[1].MeshData = new CubePrimitive();
            _lights[2].MeshData = new CubePrimitive();
            _lights[3].MeshData = new CubePrimitive();

            _body.MeshData = new CubePrimitive();
            _cabin.MeshData = new CubePrimitive();

            // Материалы

            _carMaterial.Diffuse = new Core.Color(0.55f, 0.0f, 0.0f);
            _wheelMaterial.Diffuse = new Core.Color(0.16f, 0.14f, 0.13f);
            _windowMaterial.Diffuse = new Core.Color(0.35f, 0.58f, 0.9f);
            _headlightMaterial.Color = new Core.Color(0.98f, 0.86f, 0.05f);
            _backlightMaterial.Color = new Core.Color(0.99f, 0.08f, 0.0f);

            _wheels[0].MaterialResource = _wheelMaterial;
            _wheels[1].MaterialResource = _wheelMaterial;
            _wheels[2].MaterialResource = _wheelMaterial;
            _wheels[3].MaterialResource = _wheelMaterial;

            _windows[0].MaterialResource = _windowMaterial;
            _windows[1].MaterialResource = _windowMaterial;
            _windows[2].MaterialResource = _windowMaterial;
            _windows[3].MaterialResource = _windowMaterial;

            _lights[0].MaterialResource = _headlightMaterial;
            _lights[1].MaterialResource = _headlightMaterial;
            _lights[2].MaterialResource = _backlightMaterial;
            _lights[3].MaterialResource = _backlightMaterial;

            _body.MaterialResource = _carMaterial;
            _cabin.MaterialResource = _carMaterial;

            // Колёса

            _wheels[0].Transform.Scale = new System.Numerics.Vector3(0.4f, 0.4f, 0.1f);
            _wheels[1].Transform.Scale = new System.Numerics.Vector3(0.4f, 0.4f, 0.1f);
            _wheels[2].Transform.Scale = new System.Numerics.Vector3(0.4f, 0.4f, 0.1f);
            _wheels[3].Transform.Scale = new System.Numerics.Vector3(0.4f, 0.4f, 0.1f);

            _wheels[0].Translate(1.2f, 0.15f, 1.0f);
            _wheels[1].Translate(1.2f, 0.15f, -1.0f);
            _wheels[2].Translate(-1.3f, 0.15f, 1.0f);
            _wheels[3].Translate(-1.3f, 0.15f, -1.0f);

            // Корпус

            _body.Transform.Scale = new System.Numerics.Vector3(2.0f, 0.5f, 1.0f);
            _cabin.Transform.Scale = new System.Numerics.Vector3(1.0f, 0.4f, 0.9f);

            _body.Translate(0.0f, 0.70f, 0.0f);
            _cabin.Translate(0.0f, 1.35f, 0.0f);

            // Окна

            _windows[0].Transform.Scale = new System.Numerics.Vector3(0.3f, 0.3f, 0.88f);
            _windows[1].Transform.Scale = new System.Numerics.Vector3(0.3f, 0.3f, 0.88f);
            _windows[2].Transform.Scale = new System.Numerics.Vector3(0.9f, 0.3f, 0.02f);
            _windows[3].Transform.Scale = new System.Numerics.Vector3(0.9f, 0.3f, 0.02f);

            _windows[0].Translate(1.0f, 1.28f, 0.0f);
            _windows[1].Translate(-1.0f, 1.28f, 0.0f);
            _windows[2].Translate(0.0f, 1.42f, 0.9f);
            _windows[3].Translate(0.0f, 1.42f, -0.9f);

            _windows[0].Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, (float)(System.Math.PI / 4.0));
            _windows[1].Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, (float)(System.Math.PI / 4.0));

            // Фары

            _lights[0].Transform.Scale = new System.Numerics.Vector3(0.01f, 0.1f, 0.2f);
            _lights[1].Transform.Scale = new System.Numerics.Vector3(0.01f, 0.1f, 0.2f);
            _lights[2].Transform.Scale = new System.Numerics.Vector3(0.01f, 0.1f, 0.2f);
            _lights[3].Transform.Scale = new System.Numerics.Vector3(0.01f, 0.1f, 0.2f);

            _lights[0].Translate(2.0f, 0.45f, 0.8f);
            _lights[1].Translate(2.0f, 0.45f, -0.8f);
            _lights[2].Translate(-2.0f, 0.45f, 0.8f);
            _lights[3].Translate(-2.0f, 0.45f, -0.8f);

            // Добавление в сцену

            AddChild(_wheels[0]);
            AddChild(_wheels[1]);
            AddChild(_wheels[2]);
            AddChild(_wheels[3]);

            AddChild(_windows[0]);
            AddChild(_windows[1]);
            AddChild(_windows[2]);
            AddChild(_windows[3]);

            AddChild(_lights[0]);
            AddChild(_lights[1]);
            AddChild(_lights[2]);
            AddChild(_lights[3]);

            AddChild(_body);
            AddChild(_cabin);
        }
    }
}