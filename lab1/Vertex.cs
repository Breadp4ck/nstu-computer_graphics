record struct Vertex<TNumber> {
    public TNumber X { get; set; }
    public TNumber Y { get; set; }
    
    public Vertex(TNumber x, TNumber y) {
        X = x; Y = y;
    }
}