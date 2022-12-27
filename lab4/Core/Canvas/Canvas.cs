using Silk.NET.OpenGL;
using System.Numerics;

using Lab1.Math;
using Lab1.Core.Shaders;

using L1Shader = Lab1.Core.Shaders.Shader;

namespace Lab1.Core
{
    class Canvas
    {
        private GL _context;
        private VertexArrayObject<float> _vao;
        private BufferObject<float> _vbo;
        private VertexArrayObject<float> _vaoLines;
        private BufferObject<float> _vboLines;
        private L1Shader _curveShader;
        private L1Shader _pointShader;
        private L1Shader _lineShader;
        private Color _color;
        private float[] _vertices;
        private ushort[] _indices;
        private float[] _lineVertices;

        public bool DrawLast = false;
        public bool DrawElementInfo = false;
        public bool ApplyCanvasTransform = false;

        public Canvas(GL context, Color color)
        {
            _context = context;
            _color = color;
            _curveShader = ShaderLibrary.QuibicBezierShader(context);
            _pointShader = ShaderLibrary.SelectablePointShader(context);
            _lineShader = ShaderLibrary.LineShader(context);
            _vertices = new float[0];
            _indices = new ushort[0];
            _lineVertices = new float[0];

            _vbo = new BufferObject<float>(context, _vertices, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);
            _vboLines = new BufferObject<float>(context, _lineVertices, BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicCopy);

            _vao = new VertexArrayObject<float>(context, _vbo);
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);

            _vaoLines = new VertexArrayObject<float>(context, _vbo);
            _vaoLines.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);
        }

        public void AttachData(float[] vertices)
        {
            _vertices = vertices;
        }

        public void AttachLinesData(float[] lineVertices)
        {
            _lineVertices = lineVertices;
        }

        public unsafe void Draw(Camera camera, Transform layerTransform)
        {
            _vao.Bind();

            _vbo.Update(_vertices);
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);

            Matrix4x4 model = Matrix4x4.Identity;

            if (ApplyCanvasTransform)
            {
                model *= layerTransform.ViewMatrix;
            }

            _curveShader.Use();

            _curveShader.SetUniform("color", _color.Red, _color.Green, _color.Blue, _color.Alpha);
            _curveShader.SetUniform("model", model);
            _curveShader.SetUniform("view", camera.Transform.ViewMatrix);
            _curveShader.SetUniform("projection", Matrix4x4.CreateOrthographic(2.0f, 2.0f * camera.ViewportRatioYX, 1e-3f, 1e2f));

            _context.DrawArrays(PrimitiveType.LinesAdjacency, 0, (uint)_vertices.Length / 3 - 1);

            if (DrawElementInfo)
            {
                _pointShader.Use();

                _pointShader.SetUniform("yx_scale_factor", camera.ViewportRatioYX);
                _pointShader.SetUniform("color", _color.Red, _color.Green, _color.Blue);
                _pointShader.SetUniform("model", model);
                _pointShader.SetUniform("view", camera.Transform.ViewMatrix);
                _pointShader.SetUniform("projection", Matrix4x4.CreateOrthographic(2.0f, 2.0f * camera.ViewportRatioYX, 1e-3f, 1e2f));

                if (DrawLast)
                {
                    _context.DrawArrays(PrimitiveType.Points, 0, (uint)_vertices.Length / 3 - 1);
                }
                else
                {
                    _context.DrawArrays(PrimitiveType.Points, 0, (uint)_vertices.Length / 3);
                }


                _vaoLines.Bind();

                _vboLines.Update(_lineVertices);
                _vaoLines.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0, 0);

                _lineShader.Use();
                _lineShader.SetUniform("color", _color.Red, _color.Green, _color.Blue);
                _lineShader.SetUniform("model", model);
                _lineShader.SetUniform("view", camera.Transform.ViewMatrix);
                _lineShader.SetUniform("projection", Matrix4x4.CreateOrthographic(2.0f, 2.0f * camera.ViewportRatioYX, 1e-3f, 1e2f));

                _context.DrawArrays(PrimitiveType.Lines, 0, (uint)_lineVertices.Length / 3);
            }
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }
    }
}