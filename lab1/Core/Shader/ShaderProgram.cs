using Silk.NET.OpenGL;
using System.Numerics;

using Lab1.Render;


namespace Lab1.Core.Shaders
{
    public class ShaderProgram : IDisposable
    {
        private uint _program;
        private ShaderContext _context;

        public ShaderProgram(ShaderContext context)
        {
            _context = context;
            _program = context.CreateProgram();
        }

        public void AttachShader(Shader shader)
        {
            _context.LinkShader(_program, shader.GetDescriptor());
        }

        // This suck
        internal uint GetDescriptor()
        {
            return _program;
        }

        public void Dispose()
        {
            _context.DeleteProgram(_program);
        }
    }
}