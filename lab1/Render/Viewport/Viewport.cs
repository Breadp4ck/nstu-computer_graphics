
using Silk.NET.Input;

using System.Numerics;
using Lab1.Window;

namespace Lab1.Render
{
    public record struct Viewport
    {
        private WindowServer _windowServer;
        public Camera Camera { get; set; }


        public Viewport(WindowServer windowServer, Camera camera)
        {
            _windowServer = windowServer;
            Camera = camera;
        }

        internal Matrix4x4 GetProjection()
        {
            return Matrix4x4.CreateOrthographic(2.0f, 2.0f * GetRatioXY(), Camera.MinDistance, Camera.MaxDistance);
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