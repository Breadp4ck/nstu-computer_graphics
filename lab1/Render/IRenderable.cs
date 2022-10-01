using Lab1.Math;

namespace Lab1.Render
{
    public interface IRenderable
    {
        public short VisualMask { get; set; }
        public Material Material { get; }
        public Transform Transform { get; }
        public VertexArrayObject<float>? Vao { get; }
        public BufferObject<float>? Vbo { get; }
        public float[] Vertices { get; }
        public ushort[] Indices { get; }

        public void Initialize(ShaderContext context, VertexArrayObject<float> vao, BufferObject<float> vbo);
        public void Draw(Camera camera);
    }
}

