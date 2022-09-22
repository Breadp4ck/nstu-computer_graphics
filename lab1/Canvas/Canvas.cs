using Silk.NET.OpenGL;
using System.Numerics;

class Canvas
{
    private GL _context;
    private VertexArrayObject<float> _vao;
    private BufferObject<float> _vbo;
    private Shader _shader;
    private Color _color;
    private float[] _vertices;
    private ushort[] _indices;

    public bool DrawLast = false;
    public bool ApplyCanvasTransform = false;

    public Canvas(GL context, Color color)
    {
        _context = context;
        _color = color;
        _shader = ShaderLibrary.ColorShader(context);
        _vertices = new float[0];
        _indices = new ushort[0];

        _vbo = new BufferObject<float>(context, _vertices, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);

        _vao = new VertexArrayObject<float>(context, _vbo);
        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);
    }

    public Canvas(GL context, Color color, float[] data)
    {
        _context = context;
        _color = color;
        _shader = ShaderLibrary.ColorShader(context);
        _vertices = new float[0];
        _indices = new ushort[0];

        _vbo = new BufferObject<float>(context, _vertices, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);

        _vao = new VertexArrayObject<float>(context, _vbo);
        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);
    }

    public void AttachData(float[] vertices)
    {
        _vertices = vertices;
    }

    public void ChangeLastPoint(float x, float y, float z)
    {
        _vertices[_vertices.Length - 3] = x;
        _vertices[_vertices.Length - 2] = y;
        _vertices[_vertices.Length - 1] = z;
    }

    public unsafe void Draw(Transform cameraTransform, Transform layerTransform)
    {
        UpdateBuffer();

        _vao.Bind();

        _context.PointSize(10f);
        _context.LineWidth(2f);

        _shader.Use();

        Matrix4x4 model = Matrix4x4.Identity;

        if (ApplyCanvasTransform)
        {
            model *= layerTransform.ViewMatrix;
        }

        _shader.SetUniform("color", _color.Red, _color.Green, _color.Blue);
        _shader.SetUniform("model", model * cameraTransform.Scale);
        _shader.SetUniform("view", cameraTransform.ViewMatrix);
        _shader.SetUniform("projection", Matrix4x4.CreateOrthographic(2.0f, 2.0f, 1e-3f, 1e2f));


        _context.DrawArrays(PrimitiveType.LineStrip, 0, (uint)_vertices.Length / 3);
        //_context.DrawElements(PrimitiveType.Lines, (uint)_indices.Length, DrawElementsType.UnsignedShort, null);

        if (DrawLast)
        {
            _context.DrawArrays(PrimitiveType.Points, 0, (uint)_vertices.Length / 3);
        }
        else if (_vertices.Length > 0)
        {
            _context.DrawArrays(PrimitiveType.Points, 0, (uint)_vertices.Length / 3 - 1);
        }
    }

    public Color Color
    {
        get => _color;
        set => _color = value;
    }

    private void UpdateBuffer()
    {
        _vbo.Update(_vertices);
        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);
    }
}