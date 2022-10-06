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

            var camera = new FlyCamera3D("MainCamera");
            _scene.Root.AddChild(camera);

            var lol1 = new Kek("LoL 1");
            var lol2 = new Kek("LoL 2");
            var lol3 = new Kek("LoL 3");

            var light = new SpotLight3D("Свет");

            lol1.Translate(0.0f, 0.0f, 10.0f);
            lol2.Translate(0.0f, 0.0f, 6.0f);
            lol3.Translate(0.0f, 0.0f, 3.0f);

            _scene.Root.AddChild(lol1);
            lol1.AddChild(lol2);
            lol2.AddChild(lol3);

            _scene.Root.AddChild(light);
            Console.WriteLine(light.Direction);

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
            kek.Diffuse = new Color(0.1f, 0.2f, 0.7f);

            MaterialResource = kek;
        }

        public override void Process(float delta)
        {
            base.Process(delta);

            Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, _angle);

            _angle += delta;
        }
    }

    public class FlyCamera3D : Camera3D
    {
        private Vector2 cameraRotation = new Vector2((float)(-System.Math.PI / 2.0f), 0.0f);

        // private float pitch = 0.0f;
        // private float yaw = 0.0f;

        public float Speed { get; private set; } = 10.0f;
        public FlyCamera3D(string name) : base(name) { }

        public override void Process(float delta)
        {
            base.Process(delta);

            Vector3 direction = Vector3.Zero;

            if (InputServer!.IsActionPressed("movement_forward"))
            {
                direction += _cameraFront;
            }

            if (InputServer!.IsActionPressed("movement_backward"))
            {
                direction -= _cameraFront;
            }

            if (InputServer!.IsActionPressed("movement_left"))
            {
                direction -= Vector3.Cross(_cameraFront, _cameraUp);
            }

            if (InputServer!.IsActionPressed("movement_right"))
            {
                direction += Vector3.Cross(_cameraFront, _cameraUp);
            }

            if (InputServer!.IsActionPressed("movement_upward"))
            {
                direction.Y += 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_down"))
            {
                direction.Y -= 1.0f;
            }

            _cameraFront = new Vector3(
                (float)(System.Math.Cos(cameraRotation.X) * System.Math.Cos(cameraRotation.Y)),
                (float)System.Math.Sin(cameraRotation.Y),
                (float)(System.Math.Sin(cameraRotation.X) * System.Math.Cos(cameraRotation.Y))
            );

            // var rotationX = Quaternion.CreateFromAxisAngle(Vector3.UnitY, cameraRotation.X);
            // var rotationY = Quaternion.CreateFromAxisAngle(Vector3.UnitX, cameraRotation.Y);

            // Transform.Rotation = Quaternion.Concatenate(rotationX, rotationY);

            if (direction != Vector3.Zero)
            {
                direction = Vector3.Normalize(direction);
                // var angle = cameraRotation.X;

                // direction = new Vector3(
                //     (float)(direction.X * System.Math.Cos(-angle) + direction.Z * System.Math.Sin(-angle)),
                //     direction.Y,
                //     (float)(direction.Z * System.Math.Cos(-angle) - direction.X * System.Math.Sin(-angle))
                // );

                Translate(direction * delta * Speed);

                // var lol = new MeshInstance3D("Куб");
                // lol.MeshData = new CubePrimitive();

                // var kek = new StandartMaterialResource();
                // kek.Color = new Color(0.1f, 0.2f, 0.7f);
                // lol.MaterialResource = kek;

                // Parent!.AddChild(lol);

                // lol.Transform = GlobalTransform;
                // lol.Transform.Position = GlobalTransform.Position - Vector3.UnitY * 3.0f;
            }
        }

        public override void Input(InputEvent input)
        {
            base.Input(input);

            if (input is InputMouseMotion)
            {
                var offset = ((InputMouseMotion)input).Offset;

                cameraRotation.X += offset.X * 0.004f;
                cameraRotation.Y -= offset.Y * 0.004f;

                if (cameraRotation.Y > (System.Math.PI - 0.02) / 2.0)
                {
                    cameraRotation.Y = (float)(System.Math.PI - 0.02) / 2.0f;
                }
                else if (cameraRotation.Y < -(System.Math.PI - 0.02) / 2.0)
                {
                    cameraRotation.Y = -(float)(System.Math.PI - 0.02) / 2.0f;
                }
            }
        }
    }
}