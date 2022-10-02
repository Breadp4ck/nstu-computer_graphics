using Lab1.Resources;

namespace Lab1.Main.Scene3D
{
    public abstract class GeometryInstance3D : VisualInstance3D
    {
        protected IMeshData? _meshData;
        public virtual IMeshData MeshData
        {
            get => _meshData!;
            set
            {
                Indices = value.Indices;
                Vertices = value.Vertices;
                Material = value.Material;

                _meshData = value;
            }
        }

        public GeometryInstance3D(Scene scene, string name) : base(scene, name) { }
    }
}