namespace Lab1.Resources
{
    public interface IMeshData
    {
        public float[] Vertices { get; }
        public ushort[] Indices { get; }
        public MaterialResource MaterialResource { get; }
    }
}