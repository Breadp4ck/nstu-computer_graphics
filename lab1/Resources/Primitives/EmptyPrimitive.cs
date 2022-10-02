using Lab1.Render;

namespace Lab1.Resources
{
    public class EmptyPrimitive : IPrimitive
    {
        public float[] Vertices { get; private set; } = new float[0];

        public ushort[] Indices { get; } = new ushort[0];
        public Material Material { get; set; }

    }
}