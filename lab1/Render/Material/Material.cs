using Lab1.Math;

namespace Lab1.Render
{
    public abstract class Material
    {
        protected ShaderContext _context;

        public Material(ShaderContext context)
        {
            _context = context;
        }

        internal virtual void Use(Viewport viewport, Transform instanceTransform) { }
    }
}