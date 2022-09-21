using Silk.NET.OpenGL;

public class Canvas
{
    private GL _context;
    private VertexArrayObject<float> _vao;
    private BufferObject<float> _vbo;
    private Shader _shader;
    private float[] _data;

    public Canvas(GL context, Shader shader)
    {
        _context = context;
        _shader = shader;
        _data = new float[0];

        _vbo = new BufferObject<float>(context, _data, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);

        _vao = new VertexArrayObject<float>(context, _vbo);
        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 3, 0);
    }

    public Canvas(GL context, Shader shader, float[] data)
    {
        _context = context;
        _shader = shader;
        _data = data;

        _vbo = new BufferObject<float>(context, _data, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);

        _vao = new VertexArrayObject<float>(context, _vbo);
        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 3, 0);
    }

    public void Attach(float[] data)
    {
        _data = data;
        _vbo.Update(data);
        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);
    }

    public void Draw()
    {
        _vao.Bind();

        _shader.SetUniform("color", 1.0f, 1.0f, 0.2f);
        _shader.Use();

        _context.DrawArrays(PrimitiveType.Triangles, 0, (uint)_data.Length / 3);
        _context.DrawArrays(PrimitiveType.Points, 0, (uint)_data.Length / 3);
    }
}