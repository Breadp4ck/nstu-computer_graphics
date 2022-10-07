using System.Numerics;
using Lab1.Resources;

namespace Lab1.Render
{
    public class ColorMaterial : Material
    {
        private ShaderProgram _shaderProgram;
        private uint _shaderDescriptor;

        public ColorMaterialResource Resource { get; set; } = new ColorMaterialResource();

        public ColorMaterial(ShaderContext context) : base(context)
        {
            _shaderProgram = ShaderLibrary.ColorShader(context);
            _shaderDescriptor = _shaderProgram.GetDescriptor();
        }

        internal override void Use(Viewport viewport, Matrix4x4 view)
        {
            _context.Use(_shaderDescriptor);

            _context.SetUniform(
                _shaderDescriptor,
                "material.color",
                Resource.Color.Red,
                Resource.Color.Green,
                Resource.Color.Blue,
                Resource.Color.Alpha
            );

            _context.SetUniform(_shaderDescriptor, "model", view);
            _context.SetUniform(_shaderDescriptor, "view", viewport.Camera.View);
            _context.SetUniform(_shaderDescriptor, "projection", viewport.GetProjection());
        }

        public override void LoadResource(MaterialResource resource)
        {
            Resource = (ColorMaterialResource)resource;
        }
    }
}