using Silk.NET.OpenGL;

using Lab1.Math;

namespace Lab1.Core
{
    public record struct QubicBezierCurveElement
    {
        public QubicBezierCurveElement(Vertex center)
        {
            Center = center;
            Left = new Vertex(center.X, center.Y);
            Right = new Vertex(center.X, center.Y);
        }

        public QubicBezierCurveElement(Vertex center, Vertex left, Vertex right)
        {
            Center = center;
            Left = left;
            Right = right;
        }

        public Vertex Center { get; set; }
        public Vertex Left { get; set; }
        public Vertex Right { get; set; }
    }

    public class Layer
    {
        public Transform Transform = new Transform();
        private List<Vertex> _vertices;
        private Canvas _canvas;
        private List<QubicBezierCurveElement> _curve;
        private Color _color;
        private float _zIndex = 0.0f;

        public string Name { get; set; } = "Layer";

        public Layer(GL context, Color color, string name, float zIndex)
        {
            _color = color;
            Name = name;
            _vertices = new List<Vertex>();
            _curve = new List<QubicBezierCurveElement>();
            _canvas = new Canvas(context, color);
        }

        public void AddPoint(Vertex vertex)
        {
            _curve.Add(new QubicBezierCurveElement(vertex));

            float[] data = GetCurvePointsBuffer();
            _canvas.AttachData(data);
        }

        public void AddPoint(Vertex vertex, Vertex offset)
        {
            _curve.Add(new QubicBezierCurveElement(
                vertex,
                new Vertex(vertex.X - offset.X, vertex.Y - offset.Y),
                new Vertex(vertex.X + offset.X, vertex.Y + offset.Y)
            ));

            float[] data = GetCurvePointsBuffer();
            _canvas.AttachData(data);
        }

        public void AddPointSmooth(Vertex vertex)
        {
            if (_curve.Count > 0)
            {
                Vertex to = _curve.Last().Right.To(vertex);

                _curve.Add(
                    new QubicBezierCurveElement(
                        vertex,
                        new Vertex(vertex.X - to.X / 2.25f, vertex.Y - to.Y / 2.25f),
                        new Vertex(vertex.X + to.X / 2.25f, vertex.Y + to.Y / 2.25f)
                    )
                );
            }
            else
            {
                _curve.Add(
                    new QubicBezierCurveElement(
                        vertex,
                        new Vertex(vertex.X - 0.1f, vertex.Y - 0.1f),
                        new Vertex(vertex.X + 0.1f, vertex.Y + 0.1f)
                    )
                );
            }

            float[] data = GetCurvePointsBuffer();
            _canvas.AttachData(data);
        }

        public void RemovePoint(int position)
        {
            if (_curve.Count > 1 && position < _curve.Count)
            {
                _curve.RemoveAt(position);

                float[] data = GetCurvePointsBuffer();
                _canvas.AttachData(data);
            }
        }

        public void RemoveLastPoint()
        {
            if (_curve.Count > 1)
            {
                _curve.RemoveAt(_curve.Count - 1);

                float[] data = GetCurvePointsBuffer();
                _canvas.AttachData(data);
            }
        }

        private float[] GetCurvePointsBuffer()
        {
            float[] data = new float[_curve.Count * 4 * 3];

            for (int iElem = 0, iData = 0; iElem < _curve.Count - 1; iElem++)
            {
                data[iData++] = _curve[iElem].Center.X;
                data[iData++] = _curve[iElem].Center.Y;
                data[iData++] = _zIndex;

                data[iData++] = _curve[iElem].Right.X;
                data[iData++] = _curve[iElem].Right.Y;
                data[iData++] = _zIndex;

                data[iData++] = _curve[iElem + 1].Left.X;
                data[iData++] = _curve[iElem + 1].Left.Y;
                data[iData++] = _zIndex;

                data[iData++] = _curve[iElem + 1].Center.X;
                data[iData++] = _curve[iElem + 1].Center.Y;
                data[iData++] = _zIndex;
            }

            return data;
        }

        public void ChangeLastPoint(Vertex vertex)
        {
            if (_curve.Count != 0)
            {
                Vertex to = _curve.Last().Right.To(vertex);

                var element = new QubicBezierCurveElement(
                    vertex,
                    new Vertex(vertex.X - to.X / 2.25f, vertex.Y - to.Y / 2.25f),
                    new Vertex(vertex.X + to.X / 2.25f, vertex.Y + to.Y / 2.25f)
                );

                _curve[_curve.Count - 1] = element;

                // _canvas.ChangeLastPoint(vertex.X, vertex.Y, 0.0f);
                // TODO: wow, needs to be optimized
                float[] data = GetCurvePointsBuffer();
                _canvas.AttachData(data);
            }
        }

        public void ChangeLastPoint(Vertex center, Vertex offset)
        {
            if (_curve.Count != 0)
            {
                var element = new QubicBezierCurveElement(
                    center,
                    new Vertex(center.X - offset.X, center.Y - offset.Y),
                    new Vertex(center.X + offset.X, center.Y + offset.Y)
                );

                _curve[_curve.Count - 1] = element;

                // _canvas.ChangeLastPoint(vertex.X, vertex.Y, 0.0f);
                // TODO: wow, needs to be optimized
                float[] data = GetCurvePointsBuffer();
                _canvas.AttachData(data);
            }
        }

        public int GetCurveElementsCount() => _curve.Count;

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
            _canvas.Draw(camera, Transform);
        }

        public void Clear()
        {
            Transform = new Transform();
            _color = new Color();
            _curve.Clear();

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