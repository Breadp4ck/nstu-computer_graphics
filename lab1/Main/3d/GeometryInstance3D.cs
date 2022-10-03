using Lab1.Resources;
using Lab1.Render;
using Lab1.Core;

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
                MaterialResource = value.MaterialResource;

                _meshData = value;
            }
        }

        public GeometryInstance3D(string name) : base(name) { }
    }
}