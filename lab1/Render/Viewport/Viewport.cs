
using Silk.NET.Input;

using System.Numerics;
using Lab1.Window;

namespace Lab1.Render
{
    public record struct Viewport
    {
        private WindowServer _windowServer;
        public ICamera Camera { get; set; }
        public IEnvironment Environment { get; set; }


        public Viewport(WindowServer windowServer, IEnvironment environment, ICamera camera)
        {
            _windowServer = windowServer;
            Environment = environment;
            Camera = camera;
        }

        internal Matrix4x4 GetProjection()
        {
            return Matrix4x4.CreateOrthographic(2.0f, 2.0f * GetRatioYX(), Camera.MinDistance, Camera.MaxDistance);
        }

        private float GetRatioXY()
        {
            return _windowServer.WindowSize.X / _windowServer.WindowSize.Y;
        }

        private float GetRatioYX()
        {
            return _windowServer.WindowSize.Y / _windowServer.WindowSize.X;
        }
    }
}