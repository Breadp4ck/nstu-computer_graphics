using Lab1.Render;

namespace Lab1.Resources
{
    public class TrianglePrimitive : IPrimitive
    {
        public float[] Vertices { get; private set; } = new float[9]
        {
            -0.5f, -0.5f, 0.0f,
            0.0f, 0.75f, 0.0f,
            0.5f, -0.5f, 0.0f
        };

        public ushort[] Indices { get; } = new ushort[3] { 0, 1, 2 };
        public Material Material { get; set; }
    }
}