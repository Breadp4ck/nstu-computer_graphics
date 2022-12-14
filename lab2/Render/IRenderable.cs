using Lab1.Resources;
using System.Numerics;

namespace Lab1.Render
{
    public interface IRenderable
    {
        public short VisualMask { get; set; }
        public Material Material { get; }
        public MaterialResource MaterialResource { get; }
        public Matrix4x4 View { get; }
        public VertexArrayObject<float, ushort> Vao { get; }
        public BufferObject<float>? Vbo { get; }
        public BufferObject<ushort>? Ebo { get; }
        public float[] Vertices { get; }
        public ushort[] Indices { get; }

        public void Initialize(Material material, VertexArrayObject<float, ushort> vao, BufferObject<float> vbo, BufferObject<ushort> ebo);
        public void Draw(ICamera camera);
    }
}

