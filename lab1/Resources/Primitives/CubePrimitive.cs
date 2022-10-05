using Lab1.Render;

namespace Lab1.Resources
{
    public class CubePrimitive : IPrimitive
    {
        // public float[] Vertices { get; private set; } = new float[24]
        // {
        //     // front
        //     -1.0f, -1.0f,  1.0f,
        //     1.0f, -1.0f,  1.0f,
        //     1.0f,  1.0f,  1.0f,
        //     -1.0f,  1.0f,  1.0f,
        //     // back
        //     -1.0f, -1.0f, -1.0f,
        //     1.0f, -1.0f, -1.0f,
        //     1.0f,  1.0f, -1.0f,
        //     -1.0f,  1.0f, -1.0f
        // };

        // public ushort[] Indices { get; } = new ushort[36]
        // {
        //     // front
        //     0, 1, 2,
        //     2, 3, 0,
        //     // right
        //     1, 5, 6,
        //     6, 2, 1,
        //     // back
        //     7, 6, 5,
        //     5, 4, 7,
        //     // left
        //     4, 0, 3,
        //     3, 7, 4,
        //     // bottom
        //     4, 5, 1,
        //     1, 0, 4,
        //     // top
        //     3, 2, 6,
        //     6, 7, 3
        // };

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
            -1.0f, -1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
            -1.0f, +1.0f, +1.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            -1.0f, +1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
            -1.0f, -1.0f, +1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 1.0f,
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