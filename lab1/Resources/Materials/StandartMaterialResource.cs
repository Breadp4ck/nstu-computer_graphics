using Lab1.Core;
namespace Lab1.Resources
{
    public class StandartMaterialResource : MaterialResource
    {
        public Color Diffuse { get; set; } = new Color(0.5f, 0.8f, 0.8f);
        public Color Specular { get; set; } = new Color(0.5f, 0.5f, 0.5f);
        public float Shininess { get; set; } = 32.0f;
        public StandartMaterialResource() { }
    }
}