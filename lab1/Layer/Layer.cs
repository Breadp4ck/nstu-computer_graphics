using Silk.NET.OpenGL;

class Layer
{
    Transform transform;
    List<Vertex> _vertices;
    Canvas _canvas;
    Color _color;
    public float Hue = 0.0f; // KOLHOZ (TODO: Add to Color struct)

    public Layer(GL context, Color color)
    {
        _color = color;
        _vertices = new List<Vertex>();
        _canvas = new Canvas(context, color);
    }

    public void AddVertex(Vertex vertex)
    {
        _vertices.Add(vertex);

        float[] data = GetVerticesAsBufferData();
        _canvas.AttachData(data);
    }

    public void RemoveLastVertex()
    {
        if (_vertices.Count > 1)
        {
            _vertices.RemoveAt(_vertices.Count - 1);

            float[] data = GetVerticesAsBufferData();
            _canvas.AttachData(data);
        }

    }

    public void ChangeLastVertex(Vertex vertex)
    {
        if (_vertices.Count != 0)
        {
            _vertices[_vertices.Count - 1] = vertex;
            _canvas.ChangeLastPoint(vertex.X, vertex.Y, 0.0f);
        }
    }

    public int GetVerticesCount() => _vertices.Count;

    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            _canvas.Color = value;
        }
    }

    public void Draw(Camera camera)
    {
        _canvas.Draw(camera.Transform);
    }

    public void Clear()
    {
        Hue = 0.0f;
        _color = Color.FromHSV(Hue, 0.77f, 0.95f);

        _vertices.Clear();

        float[] data = new float[0] { };
        _canvas.AttachData(data);
    }

    public bool DrawLast
    {
        set => _canvas.DrawLast = value;
        get => _canvas.DrawLast;
    }

    private float[] GetVerticesAsBufferData()
    {
        float[] data = new float[_vertices.Count * 3];

        for (int iVert = 0, iData = 0; iVert < _vertices.Count; iVert++)
        {
            data[iData++] = _vertices[iVert].X;
            data[iData++] = _vertices[iVert].Y;
            data[iData++] = 0.0f;
        }

        return data;
    }
}