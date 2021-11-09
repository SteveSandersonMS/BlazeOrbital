namespace SatelliteSimulator.Rendering;

public struct Point
{
    public float X, Y;

    public Point(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float LengthSquared => X* X + Y * Y;
    public float Length => (float)Math.Sqrt(LengthSquared);
    public Point Direction => this / Length;

    public static bool operator ==(Point a, Point b)
        => (a.X == b.X) && (a.Y == b.Y);
    public static bool operator !=(Point a, Point b)
        => (a.X != b.X) || (a.Y != b.Y);
    public static Point operator +(Point a, Point b)
        => new Point(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b)
        => new Point(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, Point b)
        => new Point(a.X * b.X, a.Y * b.Y);
    public static Point operator *(Point a, float scale)
        => new Point(a.X * scale, a.Y * scale);
    public static Point operator /(Point a, float scale)
        => new Point(a.X / scale, a.Y / scale);

    public override bool Equals(object obj)
        => (obj is Point p) && (this == p);

    public override int GetHashCode()
        => HashCode.Combine(X, Y);
}
