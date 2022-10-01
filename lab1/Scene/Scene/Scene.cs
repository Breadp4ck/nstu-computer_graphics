using Lab1.Render;
using Lab1.Window;

using System.Numerics;

namespace Lab1.Scene
{
    public class Scene : MainLoop
    {
        private List<Node> _nodes = new List<Node>();
        private List<Viewport> _viewports = new List<Viewport>();

        private WindowServer _window;
        private RenderServer _renderServer;


        public Scene() : base()
        {
            _window = new WindowServer();
            _renderServer = new RenderServer(_window.GetGlContext());

            _window.OnWindowStartsRender += Process;
        }

        public void AddNode(Node node)
        {
            _nodes.Add(node);

            if (node is IRenderable)
            {
                _renderServer.Load((IRenderable)node);
            }
        }

        protected override void Process(float delta)
        {
            base.Process(delta);

            // It must be in WindowServer, but there it is not working
            // I mean, _gl.Viewport(size);
            _renderServer.ChangeContextSize(_window.WindowSize);

            foreach (var node in _nodes)
            {
                node.Process(delta);

                if (node is IRenderable)
                {
                    foreach (var viewport in _viewports)
                    {
                        _renderServer.Render(viewport, (IRenderable)node);
                    }
                }
            }
        }

        public void AttachViewport()
        {
            var viewport = new Viewport(_window, new Camera3D());
            _viewports.Add(viewport);
        }
    }
}