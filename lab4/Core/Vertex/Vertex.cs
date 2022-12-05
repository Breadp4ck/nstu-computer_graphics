namespace Lab1.Core
{
    public record struct Vertex
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vertex(float x, float y)
        {
            X = x; Y = y;
        }

        public Vertex To(Vertex vertex)
        {
            return new(vertex.X - this.X, vertex.Y - this.Y);
        }
    }
}