using Lab1.Render;
using Lab1.Input;
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
        private InputServer _inputServer;

        public Node Root { get; init; }

        public Scene() : base()
        {
            Root = new Node(this, "Root");

            _window = new WindowServer();
            _renderServer = new RenderServer(_window.GetGlContext());
            _inputServer = new InputServer(_window.GetInputContext());

            _window.OnWindowStartsRender += Process;
            _inputServer.OnInputEmited += Input;
        }

        public void Run()
        {
            while (_window.Running)
            {
                _window.Render();
            }
        }

        internal void LoadNode(Node node)
        {
            if (node is IRenderable)
            {
                _renderServer.Load((IRenderable)node);
            }

            node.AttachInputServer(_inputServer);
            _nodes.Add(node);
        }

        public bool IsInTree(Node node)
        {
            foreach (var sceneNode in _nodes)
            {
                if (node == sceneNode)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void Process(float delta)
        {
            base.Process(delta);

            // It must be in WindowServer, but there it is not working
            // I mean, _gl.Viewport(size);
            _renderServer.ChangeContextSize(_window.WindowSize);

            // I can add nodes and viewports dynamicly, so foreach is not working here

            for (int viewportID = 0; viewportID < _viewports.Count; viewportID++)
            {
                var viewport = _viewports[viewportID];
                _renderServer.Render(viewport, viewport.Environment);
            }

            for (int nodeID = 0; nodeID < _nodes.Count; nodeID++)
            {
                var node = _nodes[nodeID];

                node.Process(delta);

                if (node is IRenderable)
                {
                    for (int viewportID = 0; viewportID < _viewports.Count; viewportID++)
                    {
                        var viewport = _viewports[viewportID];
                        _renderServer.Render(viewport, (IRenderable)node);
                    }
                }
            }
        }

        protected void Input(InputEvent input)
        {
            foreach (var node in _nodes)
            {
                node.Input(input);
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

        public void AttachViewport(ICamera camera)
        {
            var viewport = new Viewport(
                _window,
                new Environment3D("DefaultEnvironment3D"),
                camera
            );

            _viewports.Add(viewport);
        }

        public void AttachViewport(IEnvironment environment)
        {
            var viewport = new Viewport(
                _window,
                environment,
                new Camera3D("DefaultCamera3D")
            );

            _viewports.Add(viewport);
        }

        public void AttachViewport(IEnvironment environment, ICamera camera)
        {
            var viewport = new Viewport(
                _window,
                environment,
                camera
            );

            _viewports.Add(viewport);
        }
    }
}