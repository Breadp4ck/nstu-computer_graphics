using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Input;

using Lab1.Render;

namespace Lab1.Scene
{
    public class Scene : MainLoop
    {
        private List<IRenderable> _visualInstances = new List<IRenderable>();
        private List<Camera> _cameras = new List<Camera>();
        private List<Viewport> _viewports = new List<Viewport>();
        private int _currentCameraId = -1;

        public Scene(IWindow window, GL gl, IInputContext input) : base(window, gl, input) { }

        public override void Render()
        {
            base.Render();

            if (_currentCameraId >= 0)
            {
                foreach (var visualInstance in _visualInstances)
                {
                    visualInstance.Draw(_cameras[_currentCameraId]);
                }
            }
        }

        public void AttachCamera(Camera camera)
        {
            _cameras.Add(camera);
        }

        public void SelectCamera(int cameraId)
        {
            if (cameraId < _cameras.Count && cameraId >= 0)
            {
                _currentCameraId = cameraId;
            }
        }
    }
}