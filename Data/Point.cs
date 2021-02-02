namespace Data
{
    public class Point
    {
        public float Y{ get; set; }
        public float X { get; set; }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        public float DistanceToPointSquared(Point point)
        {
            float dX = point.X - X;
            float dY = point.Y - Y;
            return dX * dX + dY * dY;
        }
    }
}
