using Lab1.Math;
using Lab1.Resources;
using System.Numerics;

namespace Lab1.Render
{
    public abstract class Material
    {
        protected ShaderContext _context;

        public Material(ShaderContext context)
        {
            _context = context;
        }

        internal virtual void Use(Viewport viewport, Matrix4x4 view) { }

        public virtual void LoadResource(MaterialResource resource) { }

        public virtual void Attach(IEnvironment environment) { }
        public virtual void Attach(IDirectionalLight directionalLight) { }
        public virtual void Attach(IPointLight[] pointLights) { }
        public virtual void Attach(ICamera camera) { }
    }
}