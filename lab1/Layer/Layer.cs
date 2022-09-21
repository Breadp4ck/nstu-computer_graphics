using Silk.NET.OpenGL;

class Layer
{
    List<Vertex> _vertices;
    Canvas _canvas;
    Color _color;

    public Layer(GL context, Color color)
    {
        _color = color;
        _vertices = new List<Vertex>();
        _canvas = new Canvas(context, ShaderLibrary.ColorShader(context)); // TODO create class ColorShader
    }

    public void AddVertex(Vertex vertex)
    {
        _vertices.Add(vertex);

        float[] data = GetVerticesAsBufferData();
        _canvas.Attach(data);
    }

    public void Draw()
    {
        _canvas.Draw();
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