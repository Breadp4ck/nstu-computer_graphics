using Lab1.Math;
using Lab1.Resources;

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

        public virtual void LoadResource(MaterialResource resource) { }
    }
}