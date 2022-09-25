using Silk.NET.OpenGL;

using Lab1.Math;

namespace Lab1.Core
{
    public class Layer
    {
        public Transform Transform;
        List<Vertex> _vertices;
        Canvas _canvas;
        Color _color;
        private float _zIndex = 0.0f;

        public Layer(GL context, Color color, float zIndex)
        {
            _color = color;
            _vertices = new List<Vertex>();
            _canvas = new Canvas(context, color);
            Transform = new Transform();
            _zIndex = zIndex;
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
                _canvas.Color = _color;
            }
        }

        public float Transperent
        {
            get => _color.Alpha;
            set
            {
                _color.Alpha = value;
                _canvas.Color = _color;
            }
        }

        public void Draw(Camera camera)
        {
            _canvas.Draw(camera.Transform, Transform);
        }

        public void Clear()
        {
            _color = new Color();

            _vertices.Clear();

            float[] data = new float[0] { };
            _canvas.AttachData(data);
        }

        public bool DrawLast
        {
            set => _canvas.DrawLast = value;
            get => _canvas.DrawLast;
        }

        public bool ApplyLayerTransform
        {
            set => _canvas.ApplyCanvasTransform = value;
            get => _canvas.ApplyCanvasTransform;
        }

        private float[] GetVerticesAsBufferData()
        {
            float[] data = new float[_vertices.Count * 3];

            for (int iVert = 0, iData = 0; iVert < _vertices.Count; iVert++)
            {
                data[iData++] = _vertices[iVert].X;
                data[iData++] = _vertices[iVert].Y;
                data[iData++] = _zIndex;
            }

            return data;
        }
    }
}