using Lab1.Render;

namespace Lab1.Resources
{
    public class CubePrimitive : IPrimitive
    {
        public float[] Vertices { get; private set; } = new float[]
        {
        // | Position          | Normal          | UV
        // - Back
            -1.0f, -1.0f, +1.0f, 0.0f, 0.0f, +1.0f, 0.0f, 0.0f,
            +1.0f, -1.0f, +1.0f, 0.0f, 0.0f, +1.0f, 1.0f, 0.0f,
            +1.0f, +1.0f, +1.0f, 0.0f, 0.0f, +1.0f, 1.0f, 1.0f,
            -1.0f, +1.0f, +1.0f, 0.0f, 0.0f, +1.0f, 0.0f, 1.0f,
        // - Front
            -1.0f, +1.0f, -1.0f, 0.0f, 0.0f, -1.0f, 1.0f, 0.0f,
            +1.0f, +1.0f, -1.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f,
            +1.0f, -1.0f, -1.0f, 0.0f, 0.0f, -1.0f, 0.0f, 1.0f,
            -1.0f, -1.0f, -1.0f, 0.0f, 0.0f, -1.0f, 1.0f, 1.0f,
        // - Right
            +1.0f, -1.0f, -1.0f, +1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            +1.0f, +1.0f, -1.0f, +1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
            +1.0f, +1.0f, +1.0f, +1.0f, 0.0f, 0.0f, 1.0f, 1.0f,
            +1.0f, -1.0f, +1.0f, +1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
        // - Left
            -1.0f, -1.0f, +1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
            -1.0f, +1.0f, +1.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            -1.0f, +1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
            -1.0f, -1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 1.0f,
        // - Top
            +1.0f, +1.0f, -1.0f, 0.0f, +1.0f, 0.0f, 1.0f, 0.0f,
            -1.0f, +1.0f, -1.0f, 0.0f, +1.0f, 0.0f, 0.0f, 0.0f,
            -1.0f, +1.0f, +1.0f, 0.0f, +1.0f, 0.0f, 0.0f, 1.0f,
            +1.0f, +1.0f, +1.0f, 0.0f, +1.0f, 0.0f, 1.0f, 1.0f,
        // - Bottom
            +1.0f, -1.0f, +1.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f,
            -1.0f, -1.0f, +1.0f, 0.0f, -1.0f, 0.0f, 1.0f, 0.0f,
            -1.0f, -1.0f, -1.0f, 0.0f, -1.0f, 0.0f, 1.0f, 1.0f,
            +1.0f, -1.0f, -1.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.1f
        };

        public ushort[] Indices { get; } = new ushort[36]
        {
            0, 1, 2, 2, 3, 0,       // front
            4, 5, 6, 6, 7, 4,       // back
            8, 9, 10, 10, 11, 8,    // right
            12, 13, 14, 14, 15, 12, // left
            16, 17, 18, 18, 19, 16, // top
            20, 21, 22, 22, 23, 20, // bottom
        };

        public MaterialResource MaterialResource { get; set; } = new StandartMaterialResource();
    }
}