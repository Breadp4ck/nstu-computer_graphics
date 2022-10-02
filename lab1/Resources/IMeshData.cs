using Lab1.Render;

namespace Lab1.Resources
{
    public interface IMeshData
    {
        public float[] Vertices { get; }
        public ushort[] Indices { get; }
        public Material Material { get; }
    }
}