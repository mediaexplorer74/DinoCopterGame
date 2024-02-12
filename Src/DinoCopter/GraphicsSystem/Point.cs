// GameManager.GraphicsSystem.Point

using System;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class Point
  {
    public float X;
    public float Y;

    public Point(float x, float y)
    {
      this.X = x;
      this.Y = y;
    }

    public Point(Point p)
    {
      this.X = p.X;
      this.Y = p.Y;
    }

    public Point()
      : this(0.0f, 0.0f)
    {
    }

    public override string ToString() => "(" + (object) this.X + ", " + (object) this.Y + ")";

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      return (object) obj.GetType() == (object) typeof (Point) && this.Equals((Point) obj);
    }

    public override int GetHashCode() => this.X.GetHashCode() * 397 ^ this.Y.GetHashCode();

    public float Len()
    {
      return (float) Math.Sqrt((double) this.X * (double) this.X + (double) this.Y * (double) this.Y);
    }

    public float Dot(Point p)
    {
      return (float) ((double) this.X * (double) p.X + (double) this.Y * (double) p.Y);
    }

    public Point Normalised()
    {
      float num = this.Len();
      return (double) num <= 0.0 ? new Point(0.0f, 0.0f) : new Point(this.X / num, this.Y / num);
    }

    public float Angle()
    {
      float num = (float) Math.Atan((double) this.Y / (double) this.X);
      if ((double) this.X < 0.0)
        num += 3.14159274f;
      if ((double) num < 0.0)
        num += 6.28318548f;
      return num;
    }

    public bool NonZero() => (double) this.X != 0.0 || (double) this.Y != 0.0;

    public static Point operator -(Point p) => new Point(-p.X, -p.Y);

    public static bool operator ==(Point p1, Point p2)
    {
      if (object.ReferenceEquals((object) p1, (object) null) && object.ReferenceEquals((object) p2, (object) null))
        return true;
      return !object.ReferenceEquals((object) p1, (object) null) && !object.ReferenceEquals((object) p2, (object) null) && (double) p1.X == (double) p2.X && (double) p1.Y == (double) p2.Y;
    }

    public static bool operator !=(Point p1, Point p2) => !(p1 == p2);

    public static Point operator +(Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y);

    public static Point operator +(Point p, float p2) => new Point(p.X + p2, p.Y + p2);

    public static Point operator -(Point p1, Point p2) => new Point(p1.X - p2.X, p1.Y - p2.Y);

    public static Point operator -(Point p, float p2) => new Point(p.X - p2, p.Y - p2);

    public static Point operator *(Point p1, Point p2) => new Point(p1.X * p2.X, p1.Y * p2.Y);

    public static Point operator *(Point p, float p2) => new Point(p.X * p2, p.Y * p2);

    public static Point operator /(Point p1, Point p2) => new Point(p1.X / p2.X, p1.Y / p2.Y);

    public static Point operator /(Point p, float p2) => new Point(p.X / p2, p.Y / p2);

    public bool Equals(Point other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other.X.Equals(this.X) && other.Y.Equals(this.Y);
    }
  }
}
