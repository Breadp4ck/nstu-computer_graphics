namespace Lab1.Render
{
    public interface IRenderable
    {
        public short VisualMask { get; set; }
        public void Initialize(RenderContext renderContext);
        public void Draw(Camera camera);
    }
}

