using Lab1.Input;

namespace Lab1.Main
{
    public class Node
    {
        public Scene? Scene { get; protected set; }
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

        public void AddChild(Node child)
        {
            Childs.Add(child);
            child.Parent = this;

            child.Scene = Scene!;
            Scene!.LoadNode(child);
        }

        public virtual void Ready() { }
        public virtual void Process(float delta) { }
        public virtual void Input(InputEvent delta) { }
    }
}