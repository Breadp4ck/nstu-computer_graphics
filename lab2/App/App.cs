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

            sun.Diffuse = new Color(1.0f, 1.0f, 1.0f);

            _scene.Root.AddChild(sun);

            //  Крутящийся прожектор и стена
            // ---------------------------------------

            var spot = new Spot("Прожектор");
            var wall = new Wall("Стена");

            spot.Translate(0.0f, 0.0f, 6.0f);
            wall.Translate(0.0f, 0.0f, 10.0f);

            _scene.Root.AddChild(spot);
            _scene.Root.AddChild(wall);

            //  Лампы
            // ---------------------------------------

            var lamp1 = new Lamp("Лампа 1");
            var lamp2 = new Lamp("Лампа 2");
            var lamp3 = new Lamp("Лампа 3");

            lamp1.Translate(0.0f, 7.0f, 10.0f);

            lamp1.Color = new Color(1.0f, 0.0f, 0.0f);
            lamp2.Color = new Color(0.0f, 1.0f, 0.0f);
            lamp3.Color = new Color(0.0f, 0.0f, 1.0f);

            _scene.Root.AddChild(lamp1);
            lamp1.AddChild(lamp2);
            lamp2.AddChild(lamp3);

            //  Мельница
            // ---------------------------------------

            var mill = new WindMill("Мельница", 5);
            mill.Translate(-20.0f, 0.0f, 20.0f);
            _scene.Root.AddChild(mill);

            //  Автомобили
            // ---------------------------------------

            var car1 = new Car("Автомобиль 1");
            car1.Translate(0.0f, -0.15f, -12.5f);
            _scene.Root.AddChild(car1);

            //  Земля
            // ---------------------------------------

            var ground = new Ground("Земля");
            ground.Translate(0.0f, -1.0f, 0.0f);
            _scene.Root.AddChild(ground);

            //  Окружение
            // ---------------------------------------

            var env = new Environment3D("Environment");
            env.SkyColor = new Color(0.506f, 0.776f, 0.910f);

            // Добавление камеры и окружения в сцену
            _scene.AttachViewport(env, camera);

            // Запуск сцена
            _scene.Run();
        }
    }
}