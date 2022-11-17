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

            //  Мельница
            // ---------------------------------------

            var mill = new Windmill("Мельница", 5);
            mill.Translate(-8.0f, 0.0f, 10.0f);
            _scene.Root.AddChild(mill);

            //  Автомобили
            // ---------------------------------------

            var cars = new CarDirector("Автодиллер");
            _scene.Root.AddChild(cars);

            //  Земля
            // ---------------------------------------

            var ground = new Ground("Земля");
            ground.Translate(0.0f, -1.0f, 0.0f);
            _scene.Root.AddChild(ground);

            //  Окружение
            // ---------------------------------------

            var env = new Environment3D("Environment");
            env.SkyColor = new Color(0.21f, 0.21f, 0.41f);
            env.Ambient = new Color(0.3f, 0.2f, 0.2f);

            //  Основное освещение
            // ---------------------------------------

            var sun = new DirectionalLight3D("Солнце");

            sun.Transform.Rotation = Quaternion.CreateFromAxisAngle(
                Vector3.One,
                (float)(System.Math.PI / 4.0f)
            );

            sun.Diffuse = new Color(1.0f, 1.0f, 1.0f);
            sun.Strength = 1.0f;

            _scene.Root.AddChild(sun);

            //  Гуй
            // ---------------------------------------

            var ui = new SimpleUI("UI", env, sun, mill, cars.Car1, cars.Car2);
            _scene.Root.AddChild(ui);

            // Добавление камеры и окружения в сцену
            _scene.AttachViewport(env, camera);

            // Запуск сцена
            _scene.Run();
        }
    }
}