using Lab1.Render;
using Lab1.Core.Shaders;

namespace Lab1.Core
{
    public class StandartMaterial : Material
    {
        private ShaderProgram _shaderProgram;

        public StandartMaterial(ShaderContext context) : base(context)
        {
            _shaderProgram = ShaderLibrary.ColorShader(context);
        }

        public override void Use()
        {
            _context.Use(_shaderProgram.GetDescriptor());
        }
    }
}