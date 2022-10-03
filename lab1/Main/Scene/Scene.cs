using Lab1.Render;
using Lab1.Window;

using Lab1.Main.Scene3D;

namespace Lab1.Main
{
    public class Scene : MainLoop
    {
        private List<Node> _nodes = new List<Node>();
        private List<Viewport> _viewports = new List<Viewport>();

        private WindowServer _window;
        private RenderServer _renderServer;

        public Node Root { get; init; }


        public Scene() : base()
        {
            Root = new Node(this, "Root");

            _window = new WindowServer();
            _renderServer = new RenderServer(_window.GetGlContext());

            _window.OnWindowStartsRender += Process;
        }

        internal void LoadNode(Node node)
        {
            if (node is IRenderable)
            {
                _renderServer.Load((IRenderable)node);
            }

            _nodes.Add(node);
        }

        protected override void Process(float delta)
        {
            base.Process(delta);

            // It must be in WindowServer, but there it is not working
            // I mean, _gl.Viewport(size);
            _renderServer.ChangeContextSize(_window.WindowSize);

            foreach (var viewport in _viewports)
            {
                _renderServer.Render(viewport, viewport.Environment);
            }

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
            var viewport = new Viewport(
                _window,
                new Environment3D("DefaultEnvironment3D"),
                new Camera3D("DefaultCamera3D")
            );

            _viewports.Add(viewport);
        }
    }
}