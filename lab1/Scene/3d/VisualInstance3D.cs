using Lab1.Render;

namespace Lab1.Scene.Scene3D
{
    public abstract class VisualInstance3D : Node3D, IRenderable
    {
        public short VisualMask { get; set; } = 0;

        public VisualInstance3D(Scene scene, string name) : base(scene, name) { }

        public void Initialize(RenderContext renderContext)
        {
            // Default realistaion
        }

        public virtual void Draw(Camera camera) { }
    }
}