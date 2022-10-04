using Lab1.Render;
using Lab1.Resources;

namespace Lab1.Main.Scene3D
{
    public abstract class VisualInstance3D : Node3D, IRenderable
    {
        private MaterialResource _materialResource = new StandartMaterialResource();
        public BufferObject<float>? Vbo { get; private set; }
        public BufferObject<ushort>? Ebo { get; private set; }
        public virtual float[] Vertices { get; protected set; } = new float[0];
        public virtual ushort[] Indices { get; protected set; } = new ushort[0];

        public virtual short VisualMask { get; set; } = 1;
        public Material? Material { get; protected set; }
        public MaterialResource MaterialResource
        {
            get => _materialResource;
            set
            {
                if (Material != null)
                {
                    Material!.LoadResource(value);
                }
            }
        }

        public VisualInstance3D(string name) : base(name) { }

        public void Initialize(ShaderContext context, VertexArrayObject<float, ushort> vao, BufferObject<float> vbo, BufferObject<ushort> ebo)
        {
            Material = new StandartMaterial(context);

            Vbo = vbo;
            Ebo = ebo;
        }

        public virtual void Draw(ICamera camera) { }
    }
}