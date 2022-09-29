using Lab1.Input;

namespace Lab1.Scene
{
    public abstract class Node
    {
        private Scene _scene;
        public string Name { get; private set; }

        public Node(Scene scene, string name)
        {
            _scene = scene;
            Name = name;
        }

        public virtual void Ready() { }
        public virtual void Process(float delta) { }
        public virtual void Input(InputEvent delta) { }
    }
}