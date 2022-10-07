using Lab1.Main.Scene3D;
using Lab1.Resources;
using Lab1.Core;


namespace Lab1.App
{
    public class LightCube : MeshInstance3D
    {
        private Color _color = new Color(1.0f, 1.0f, 1.0f);
        private ColorMaterialResource _materialResource = new ColorMaterialResource();

        public Color Color
        {
            get => _color;
            set
            {
                _materialResource.Color = value;
                MaterialResource = _materialResource;
            }
        }

        public LightCube(string name) : base(name)
        {
            MeshData = new CubePrimitive();
            _materialResource.Color = _color;

            MaterialResource = _materialResource;
        }
    }
}