using ReMath = Lab1.Math;

namespace Lab1.Render
{
    public interface ICamera
    {
        public short VisualMask { get; set; }
        public float MinDistance { get; set; }
        public float MaxDistance { get; set; }
        public ReMath.Transform Transform { get; set; }
    }
}