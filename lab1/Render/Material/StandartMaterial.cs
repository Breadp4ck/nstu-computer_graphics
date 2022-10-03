using System.Numerics;

using Lab1.Core;
using Lab1.Resources;
using ReMath = Lab1.Math;

namespace Lab1.Render
{
    public class StandartMaterial : Material
    {
        private ShaderProgram _shaderProgram;
        private uint _shaderDescriptor;

        public StandartMaterialResource Resource { get; set; } = new StandartMaterialResource();

        public StandartMaterial(ShaderContext context) : base(context)
        {
            _shaderProgram = ShaderLibrary.ColorShader(context);
            _shaderDescriptor = _shaderProgram.GetDescriptor();
        }

        internal override void Use(Viewport viewport, ReMath.Transform instanceTransform)
        {
            _context.Use(_shaderDescriptor);

            _context.SetUniform(
                _shaderDescriptor,
                "color",
                Resource.Color.Red,
                Resource.Color.Green,
                Resource.Color.Blue,
                Resource.Color.Alpha
            );

            _context.SetUniform(_shaderDescriptor, "model", instanceTransform.ViewMatrix);
            _context.SetUniform(_shaderDescriptor, "view", viewport.Camera.Transform.ViewMatrix);
            _context.SetUniform(_shaderDescriptor, "projection", viewport.GetProjection());
        }

        public override void LoadResource(MaterialResource resource)
        {
            Resource = (StandartMaterialResource)resource;
        }
    }
}