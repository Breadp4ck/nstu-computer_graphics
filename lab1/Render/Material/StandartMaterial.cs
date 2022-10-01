using System.Numerics;

using Lab1.Core;
using ReMath = Lab1.Math;

namespace Lab1.Render
{
    public class StandartMaterial : Material
    {
        private ShaderProgram _shaderProgram;
        private uint _shaderDescriptor;

        public Color Color { get; set; } = new Color(0.9f, 0.4f, 0.3f);

        public StandartMaterial(ShaderContext context) : base(context)
        {
            _shaderProgram = ShaderLibrary.ColorShader(context);
            _shaderDescriptor = _shaderProgram.GetDescriptor();
        }

        internal override void Use(Viewport viewport, ReMath.Transform instanceTransform)
        {
            _context.Use(_shaderDescriptor);

            _context.SetUniform(_shaderDescriptor, "color", Color.Red, Color.Green, Color.Blue, Color.Alpha);
            _context.SetUniform(_shaderDescriptor, "model", instanceTransform.ViewMatrix);
            _context.SetUniform(_shaderDescriptor, "view", viewport.Camera.Transform.ViewMatrix);
            _context.SetUniform(_shaderDescriptor, "projection", viewport.GetProjection());
        }
    }
}