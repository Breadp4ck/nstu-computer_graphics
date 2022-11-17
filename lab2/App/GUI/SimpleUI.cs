using Lab1.Control;
using Lab1.Main.Scene3D;
using Lab1.Core;
using System.Numerics;

namespace Lab1.App
{
    public class SimpleUI : VisualInstanceControl
    {
        private ImGuiNET.ImGuiWindowFlags _windowFlags =
            ImGuiNET.ImGuiWindowFlags.NoDecoration |
            ImGuiNET.ImGuiWindowFlags.AlwaysAutoResize |
            ImGuiNET.ImGuiWindowFlags.NoNav |
            ImGuiNET.ImGuiWindowFlags.NoSavedSettings |
            ImGuiNET.ImGuiWindowFlags.NoFocusOnAppearing |
            ImGuiNET.ImGuiWindowFlags.NoMove;

        private Vector3 _dirLightColor = new Vector3(1.0f, 1.0f, 1.0f);
        private float _dirLightStrength = 1.0f;
        private Vector3 _skyColor = new Vector3(0.41f, 0.41f, 0.81f);
        private Vector3 _skyAmbientColor = new Vector3(0.1f, 0.05f, 0.05f);
        private int _sailCount = 5;
        private float _car1Speed = 20.0f;
        private float _car2Speed = 25.0f;
        private Vector3 _car1Color = new Vector3(0.98f, 0.1f, 0.2f);
        private Vector3 _car2Color = new Vector3(0.3f, 0.2f, 0.98f);

        private Environment3D _env;
        private DirectionalLight3D _sun;
        private Windmill _mill;
        private Car _car1;
        private Car _car2;

        public SimpleUI(string name, Environment3D env, DirectionalLight3D sun, Windmill mill, Car car1, Car car2) : base(name)
        {
            _env = env;
            _sun = sun;
            _mill = mill;
            _car1 = car1;
            _car2 = car2;
        }

        public override void Process(float delta)
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(0.45f);
            ImGuiNET.ImGui.SetNextWindowPos(new Vector2(10.0f, 10.0f));

            ImGuiNET.ImGui.Begin("Text", _windowFlags);
            {
                ImGuiNET.ImGui.Text("Environment");
                ImGuiNET.ImGui.ColorEdit3("Directional light color", ref _dirLightColor);
                ImGuiNET.ImGui.SliderFloat("Directional light strength", ref _dirLightStrength, 0.0f, 5.0f);
                ImGuiNET.ImGui.ColorEdit3("Ambient light color", ref _skyAmbientColor);
                ImGuiNET.ImGui.ColorEdit3("Sky color", ref _skyColor);

                ImGuiNET.ImGui.Text("Cars");
                ImGuiNET.ImGui.SliderFloat("Car1 speed (m/s)", ref _car1Speed, 10.0f, 50.0f);
                ImGuiNET.ImGui.SliderFloat("Car2 speed (m/s)", ref _car2Speed, 10.0f, 50.0f);
                ImGuiNET.ImGui.ColorEdit3("Car1 color", ref _car1Color);
                ImGuiNET.ImGui.ColorEdit3("Car2 color", ref _car2Color);
            }
            ImGuiNET.ImGui.End();

            _env.SkyColor = new Color(_skyColor.X, _skyColor.Y, _skyColor.Z);
            _env.Ambient = new Color(_skyAmbientColor.X, _skyAmbientColor.Y, _skyAmbientColor.Z);
            _sun.Diffuse = new Color(_dirLightColor.X, _dirLightColor.Y, _dirLightColor.Z);
            _sun.Strength = _dirLightStrength;

            _car1.Color = new Color(_car1Color.X, _car1Color.Y, _car1Color.Z);
            _car2.Color = new Color(_car2Color.X, _car2Color.Y, _car2Color.Z);
            _car1.Speed = _car1Speed;
            _car2.Speed = _car2Speed;
        }
    }
}