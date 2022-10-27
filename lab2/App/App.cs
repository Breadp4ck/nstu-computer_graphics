using Lab1.Main;
using Lab1.Main.Scene3D;
using Lab1.Core;

using System.Numerics;

namespace Lab1.App
{
    public class App
    {
        private Scene _scene;

        public App()
        {
            _scene = new Scene(); // Создаём сцену

            //  Камера + управление
            // ---------------------------------------

            var camera = new FlyCamera3D("MainCamera");
            _scene.Root.AddChild(camera);

            //  Основное освещение
            // ---------------------------------------

            var sun = new DirectionalLight3D("Солнце");

            sun.Transform.Rotation = Quaternion.CreateFromAxisAngle(
                Vector3.One,
                (float)(System.Math.PI / 4.0f)
            );

            sun.Diffuse = new Color(0.4f, 0.4f, 0.5f);
            sun.Strength = 1.0f;

            _scene.Root.AddChild(sun);

            //  Мельница
            // ---------------------------------------

            var mill = new WindMill("Мельница", 5);
            mill.Translate(-8.0f, 0.0f, 10.0f);
            _scene.Root.AddChild(mill);

            //  Автомобили
            // ---------------------------------------

            var car1 = new Car("Автомобиль 1");
            car1.Translate(-10.0f, -0.15f, -8.5f);
            _scene.Root.AddChild(car1);

            var car2 = new Car("Автомобиль 2");
            car2.Translate(8.0f, -0.15f, -11.5f);
            car2.Transform.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, (float)System.Math.PI);
            car2.Transform.Scale = new Vector3(0.1f, 0.1f, 0.1f);
            _scene.Root.AddChild(car2);

            //  Земля
            // ---------------------------------------

            var ground = new Ground("Земля");
            ground.Translate(0.0f, -1.0f, 0.0f);
            _scene.Root.AddChild(ground);

            //  Окружение
            // ---------------------------------------

            var env = new Environment3D("Environment");
            env.SkyColor = new Color(0.121f, 0.121f, 0.121f);
            env.Ambient = new Color(0.3f, 0.2f, 0.2f);

            // Добавление камеры и окружения в сцену
            _scene.AttachViewport(env, camera);

            // Запуск сцена
            _scene.Run();
        }
    }
}