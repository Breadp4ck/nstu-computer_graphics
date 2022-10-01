namespace Lab1.Render
{
    public class Shader : IDisposable
    {
        private ShaderContext _context;
        private uint _shader;

        public Shader(ShaderContext context, ShaderType type, string source)
        {
            _context = context;
            _shader = context.CreateShader(type, source);
        }

        static Shader FromFiles(ShaderContext context, ShaderType type, string path)
        {
            string source = File.ReadAllText(path);

            return new Shader(context, type, source);
        }

        internal uint GetDescriptor()
        {
            return _shader;
        }

        public void DeleteShader()
        {
            _context.DeleteShader(_shader);
        }

        public void Dispose()
        {
            _context.DeleteShader(_shader);
        }
    }
}