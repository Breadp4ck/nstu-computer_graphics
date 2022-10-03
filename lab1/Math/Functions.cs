namespace Lab1.Math
{
    public static class Functions
    {
        public static float ToRadians(float degrees)
        {
            return (float)(System.Math.PI / 180.0) * degrees;
        }

        public static float ToDegress(float radians)
        {
            return (float)(180.0 / System.Math.PI) * radians;
        }
    }
}