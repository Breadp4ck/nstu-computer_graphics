namespace Lab1.Core
{
    public record struct Color
    {
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }
        public float Alpha { get; set; }

        public Color()
        {
            Red = 0; Green = 0; Blue = 0; Alpha = 1.0f;
        }

        public Color(float red, float green, float blue)
        {
            Red = red; Green = green; Blue = blue; Alpha = 1.0f;
        }

        public Color(float red, float green, float blue, float alpha)
        {
            Red = red; Green = green; Blue = blue; Alpha = alpha;
        }

        public static Color FromHSV(float hue, float saturation, float value)
        {
            float chroma = value * saturation;
            float hueContribution = (hue * 6.0f) % 6;
            float hueOffset = chroma * (1.0f - System.Math.Abs(hueContribution % 2 - 1));

            int hueStep = (int)System.Math.Floor(((hue * 6.0f) % 6));

            Color tempColor = hueStep switch
            {
                0 => new Color(chroma, hueOffset, 0.0f),
                1 => new Color(hueOffset, chroma, 0.0f),
                2 => new Color(0.0f, chroma, hueOffset),
                3 => new Color(0.0f, hueOffset, chroma),
                4 => new Color(hueOffset, 0.0f, chroma),
                5 => new Color(chroma, 0.0f, hueOffset),
                _ => new Color(0.0f, 0.0f, 0.0f), // Impossible statement
            };

            float m = value - chroma;

            Color color = new Color(
                tempColor.Red + m,
                tempColor.Green + m,
                tempColor.Blue + m
            );

            return color;
        }
    }
}