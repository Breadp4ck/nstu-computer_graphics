using Lab1.Core;

namespace Lab1.Render
{
    public interface IEnvironment
    {
        public Color Ambient { get; }
        public Color SkyColor { get; }
    }
}