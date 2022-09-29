using Lab1.Render;

namespace Lab1.Core
{
    public abstract class Material
    {
        protected ShaderContext _context;

        public Material(ShaderContext context)
        {
            _context = context;
        }

        public virtual void Use() { }
    }
}