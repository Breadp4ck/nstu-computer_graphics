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

            var cube1 = new MeshInstance3D("Куб 1");
            var cube2 = new MeshInstance3D("Куб 2");
            var cube3 = new MeshInstance3D("Куб 3");
            var cube4 = new MeshInstance3D("Куб 4");

            cube1.MeshData = new CubePrimitive();
            cube2.MeshData = new CubePrimitive();
            cube3.MeshData = new CubePrimitive();
            cube4.MeshData = new CubePrimitive();

            var cubeMat = new StandartMaterialResource();
            cubeMat.Color = new Color(0.6f, 0.2f, 0.3f);

            var kek = new StandartMaterialResource();
            kek.Color = new Color(0.1f, 0.2f, 0.7f);

            cube1.MaterialResource = cubeMat;
            cube2.MaterialResource = cubeMat;
            cube3.MaterialResource = cubeMat;
            cube4.MaterialResource = kek;

            cube1.Translate(0.0f, 0.0f, -3.0f);
            cube2.Translate(0.0f, 0.0f, -3.0f);
            cube3.Translate(0.0f, 3.0f, -3.0f);
            cube4.Translate(0.0f, 0.0f, -10.0f);

            var camera = new FlyCamera3D("MainCamera");
            _scene.Root.AddChild(camera);

            _scene.Root.AddChild(cube1);
            cube1.AddChild(cube2);
            cube2.AddChild(cube3);
            //camera.AddChild(cube4);

            var lol1 = new Kek("LoL 1");
            var lol2 = new Kek("LoL 2");
            var lol3 = new Kek("LoL 3");

            lol1.Translate(0.0f, 0.0f, 8.0f);
            lol2.Translate(0.0f, 0.0f, 3.0f);
            lol3.Translate(0.0f, 0.0f, 3.0f);

            _scene.Root.AddChild(lol1);
            lol1.AddChild(lol2);
            lol2.AddChild(lol3);

            var env = new Environment3D("Environment");
            env.SkyColor = new Color(0.02f, 0.05f, 0.1f);

            _scene.AttachViewport(env, camera);

            _scene.Run();
        }
    }

    public class Kek : MeshInstance3D
    {
        private float _angle = 0.0f;

        public Kek(string name) : base(name)
        {
            MeshData = new CubePrimitive();

            var kek = new StandartMaterialResource();
            kek.Color = new Color(0.1f, 0.2f, 0.7f);

            MaterialResource = kek;
        }

        public override void Process(float delta)
        {
            base.Process(delta);

            Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitX, _angle);

            _angle += delta;
        }
    }

    public class FlyCamera3D : Camera3D
    {
        private Vector2 cameraRotation = Vector2.Zero;

        public float Speed { get; private set; } = 10.0f;
        public FlyCamera3D(string name) : base(name) { }

        public override void Process(float delta)
        {
            base.Process(delta);

            Vector3 direction = Vector3.Zero;

            if (InputServer!.IsActionPressed("movement_forward"))
            {
                direction.Z -= 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_backward"))
            {
                direction.Z += 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_left"))
            {
                direction.X -= 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_right"))
            {
                direction.X += 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_upward"))
            {
                direction.Y += 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_down"))
            {
                direction.Y -= 1.0f;
            }

            var rotationX = Quaternion.CreateFromAxisAngle(Vector3.UnitY, cameraRotation.X);
            var rotationY = Quaternion.CreateFromAxisAngle(Vector3.UnitX, cameraRotation.Y);

            Transform.Rotation = Quaternion.Concatenate(rotationX, rotationY);

            if (direction != Vector3.Zero)
            {
                direction = Vector3.Normalize(direction);
                var angle = cameraRotation.X;

                direction = new Vector3(
                    (float)(direction.X * System.Math.Cos(-angle) + direction.Z * System.Math.Sin(-angle)),
                    direction.Y,
                    (float)(direction.Z * System.Math.Cos(-angle) - direction.X * System.Math.Sin(-angle))
                );

                Translate(direction * delta * Speed);
            }
        }

        public override void Input(InputEvent input)
        {
            base.Input(input);

            if (input is InputMouseMotion)
            {
                var offset = ((InputMouseMotion)input).Offset;

                cameraRotation.X += offset.X * 0.004f;
                cameraRotation.Y += offset.Y * 0.004f;

                if (cameraRotation.Y > (System.Math.PI - 0.02) / 2.0)
                {
                    cameraRotation.Y = (float)(System.Math.PI - 0.02) / 2.0f;
                }
                else if (cameraRotation.Y < -(System.Math.PI - 0.02) / 2.0)
                {
                    cameraRotation.Y = -(float)(System.Math.PI - 0.02) / 2.0f;
                }

                Console.WriteLine(GlobalTransform.Rotation);

                foreach (Node3D child in Childs)
                {
                    Console.WriteLine(child.GlobalTransform.Rotation);
                }
            }
        }
    }
}