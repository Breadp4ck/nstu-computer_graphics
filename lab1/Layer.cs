record struct ColorRGB8 {
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }

    public ColorRGB8() {
        Red = 0; Green = 0; Blue = 0;
    }

    public ColorRGB8(byte red, byte green, byte blue) {
        Red = red; Green = green; Blue = blue;
    }

    public static ColorRGB8 RED = new ColorRGB8(255, 0, 0);
    public static ColorRGB8 BLUE = new ColorRGB8(0, 255, 0);
    public static ColorRGB8 GREEN = new ColorRGB8(0, 0, 255);
    public static ColorRGB8 WHITE = new ColorRGB8(255, 255, 255);
    public static ColorRGB8 BLACK = new ColorRGB8(0, 0, 0);
}


class Layer {
    List<Vertex<short>> _vertices;
    ColorRGB8 _color;

    public Layer(ColorRGB8 color) {
        _color = color;
        _vertices = new List<Vertex<short>>();
    }
}