namespace Data
{
    public class Point
    {
        public int Y{ get; set; }
        public int X { get; set; }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        public int DistanceToPointSquared(Point point)
        {
            int dX = point.X - X;
            int dY = point.Y - Y;
            return dX * dX + dY * dY;
        }
    }
}
