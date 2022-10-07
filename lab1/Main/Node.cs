using Lab1.Input;

namespace Lab1.Main
{
    public class Node
    {
        public Scene? Scene { get; internal protected set; }
        public InputServer? InputServer { get; protected set; }
        public Node? Parent { get; protected set; }
        public List<Node> Childs { get; protected set; } = new List<Node>();
        public string Name { get; private set; }

        public bool RootNode { get; init; }

        public Node(string name)
        {
            Name = name;

            RootNode = false;
        }

        public Node(Scene scene, string name)
        {
            Scene = scene;
            Name = name;

            RootNode = true;
        }

        public virtual void AddChild(Node child)
        {
            if (Scene != null)
            {
                if (Scene!.IsInTree(child))
                {
                    // TODO: or maybe better throw exception?
                    Console.WriteLine($"ERROR: The node {child.Name} is already in the Scene. It will not be added.");
                    return;
                }

                child.Scene = Scene!;
                Scene!.LoadNode(child);
            }

            Childs.Add(child);
            child.Parent = this;
        }

        public virtual void Ready() { }
        public virtual void Process(float delta) { }
        public virtual void Input(InputEvent input) { }

        public void AttachInputServer(InputServer server)
        {
            InputServer = server;
        }
    }
}