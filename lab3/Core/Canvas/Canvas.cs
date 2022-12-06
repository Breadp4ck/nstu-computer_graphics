using Silk.NET.OpenGL;

namespace Lab1.Core
{
    class Canvas
    {
        private GL _context;
        private VertexArrayObject<float> _vao;
        private BufferObject<float> _vbo;
        private Shader _shader;
        private float[] _vertices;


        public Canvas(GL context)
        {
            _context = context;
            _shader = Shader.FromFiles(context, "Core/Canvas/CanvasVert.glsl", "Core/Canvas/CanvasFrag.glsl");

            _vertices = new float[18] {
            //  Position           
                -1.0f, -1.0f, 0.0f,
                -1.0f, +1.0f, 0.0f,
                +1.0f, +1.0f, 0.0f,
                +1.0f, +1.0f, 0.0f,
                +1.0f, -1.0f, 0.0f,
                -1.0f, -1.0f, 0.0f,
            };

            _vbo = new BufferObject<float>(context, _vertices, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);

            _vao = new VertexArrayObject<float>(context, _vbo);
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);
        }

        public unsafe void Draw()
        {
            _vao.Bind();

            _shader.Use();

            UpdateBuffer();
            _context.DrawArrays(PrimitiveType.Triangles, 0, (uint)_vertices.Length / 3);
        }

        private void UpdateBuffer()
        {
            _vbo.Update(_vertices);
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);
        }
    }
}