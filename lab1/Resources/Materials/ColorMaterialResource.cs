using Lab1.Core;
namespace Lab1.Resources
{
    public class ColorMaterialResource : MaterialResource
    {
        public Color Color { get; set; } = new Color(1.0f, 1.0f, 1.0f);
        public ColorMaterialResource() { }
    }
}