using Lab1.Main;
using Lab1.Main.Scene3D;
using Lab1.Resources;
using Lab1.Core;
using Lab1.Input;

using System.Numerics;

namespace Lab1.App
{
    public class App
    {
        private Scene _scene;

        public App()
        {
            _scene = new Scene();

            // _scene.AttachViewport();

            var myRectangle1 = new MeshInstance3D("Прямоугольник");
            myRectangle1.MeshData = new RectanglePrimitive();

            var cude = new MeshInstance3D("Куб");
            cude.MeshData = new CubePrimitive();

            var myTriangle = new MeshInstance3D("Треугольник");
            myTriangle.MeshData = new TrianglePrimitive();

            _scene.Root.AddChild(cude);
            _scene.Root.AddChild(myTriangle);
            _scene.Root.AddChild(myRectangle1);


            var matRectangle1 = new StandartMaterialResource();
            var matRectangle2 = new StandartMaterialResource();
            var matTriangle = new StandartMaterialResource();

            myRectangle1.Translate(new Vector3(0.0f, 1.0f, -10.5f));
            cude.Translate(new Vector3(0.0f, 0.0f, -10.5f));
            myTriangle.Translate(new Vector3(0.0f, -1.0f, -14.0f));

            matRectangle1.Color = new Color(0.2f, 0.7f, 0.3f);
            matRectangle2.Color = new Color(0.2f, 0.3f, 0.8f);
            matTriangle.Color = new Color(0.6f, 0.2f, 0.3f);

            myRectangle1.MaterialResource = matRectangle1;
            cude.MaterialResource = matRectangle2;
            myTriangle.MaterialResource = matTriangle;

            var camera = new FlyCamera3D("MainCamera");
            _scene.Root.AddChild(camera);

            _scene.AttachViewport(camera);

            _scene.Run();
        }
    }

    public class FlyCamera3D : Camera3D
    {
        public float Speed { get; private set; } = 10.0f;
        public FlyCamera3D(string name) : base(name) { }

        public override void Process(float delta)
        {
            base.Process(delta);

            Vector3 direction = Vector3.Zero;

            if (InputServer!.IsActionPressed("movement_forward"))
            {
                direction.Z += 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_backward"))
            {
                direction.Z -= 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_left"))
            {
                direction.X += 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_right"))
            {
                direction.X -= 1.0f;
            }

            if (direction != Vector3.Zero)
            {
                direction = Vector3.Normalize(direction);
                Translate(direction * delta * Speed);
            }
        }
    }
}