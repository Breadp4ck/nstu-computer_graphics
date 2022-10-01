using Lab1.Render;

namespace Lab1.Scene.Scene3D
{
    public abstract class VisualInstance3D : Node3D, IRenderable
    {
        public VertexArrayObject<float>? Vao { get; private set; }
        public BufferObject<float>? Vbo { get; private set; }
        public virtual float[] Vertices { get; protected set; } = new float[0];
        public virtual ushort[] Indices { get; protected set; } = new ushort[0];

        public virtual short VisualMask { get; set; } = 1;
        public Material? Material { get; set; }

        public VisualInstance3D(Scene scene, string name) : base(scene, name) { }

        public void Initialize(ShaderContext context, VertexArrayObject<float> vao, BufferObject<float> vbo)
        {
            Material = new StandartMaterial(context);
            Vao = vao;
            Vbo = vbo;
        }

        public virtual void Draw(Camera camera) { }
    }
}