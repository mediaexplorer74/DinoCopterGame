// GameManager.GameLogic.Collision

using GameManager.GraphicsSystem;
using System;

#nullable disable
namespace GameManager.GameLogic
{
  public class Collision
  {
    public float Depth;
    public Point Normal;

    public Collision()
      : this(0.0f, new Point())
    {
    }

    public Collision(float depth, Point normal)
    {
      this.Depth = depth;
      this.Normal = normal;
    }

    public static Collision CollisionCircleCircle(Point m1, float r1, Point m2, float r2)
    {
      Point point = m1 - m2;
      float num1 = r1 + r2;
      float num2 = point.Len();
      return (double) num2 < (double) num1 ? new Collision(r1 + r2 - num2, (m1 - m2).Normalised()) : new Collision();
    }

    public static float CollisionPointCircleSegment(Point s, Point p1, Point d)
    {
      return d.Dot(s - p1) / d.Dot(d);
    }

    public static Collision CollisionCircleSegment(Point s, float r, Point p1, Point p2)
    {
      Point d = p2 - p1;
      float num1 = Collision.CollisionPointCircleSegment(s, p1, d);
      if ((double) num1 < 0.0)
      {
        float num2 = (s - p1).Len();
        if ((double) num2 <= (double) r)
          return new Collision(r - num2, (s - p1).Normalised());
      }
      else if ((double) num1 > 1.0)
      {
        float num3 = (s - p2).Len();
        if ((double) num3 <= (double) r)
          return new Collision(r - num3, (s - p2).Normalised());
      }
      else
      {
        Point point = p1 + d * num1;
        float num4 = (s - point).Len();
        if ((double) num4 <= (double) r)
          return new Collision(r - num4, (s - point).Normalised());
      }
      return new Collision();
    }

    public static Point DirectionAfterCollision(Point v, Point n)
    {
      Point point1 = n * v.Dot(n);
      n = new Point(-n.Y, n.X);
      Point point2 = n * v.Dot(n);
      return -point1 + point2;
    }

    public static Point DirectionAfterCollision(
      Point v,
      Point n,
      float normalDamp,
      float tangentDamp)
    {
      Point point1 = n * v.Dot(n);
      n = new Point(-n.Y, n.X);
      Point point2 = n * v.Dot(n);
      return -point1 * normalDamp + point2 * tangentDamp;
    }

    public static Collision CollisionCirclePolygon(Point s, float r, Point[] poly)
    {
      Collision collision1 = new Collision();
      for (int index = 0; index < poly.Length - 1; ++index)
      {
        Collision collision2 = Collision.CollisionCircleSegment(s, r, poly[index], poly[index + 1]);
        if ((double) collision2.Depth > (double) collision1.Depth)
          collision1 = collision2;
      }
      return collision1;
    }

    public static Collision CollisionRectangleRectangle(
      Point p1,
      Point k1,
      Point p2,
      Point k2,
      Point move)
    {
      if ((double) p1.X > (double) k2.X || (double) p2.X > (double) k1.X || (double) p1.Y > (double) k2.Y || (double) p2.Y > (double) k1.Y)
        return new Collision();
      if ((double) p1.X > (double) p2.X == (double) k1.X < (double) k2.X)
        return (double) move.Y > 0.0 ? new Collision(k1.Y - p2.Y, new Point(0.0f, -1f)) : new Collision(k2.Y - p1.Y, new Point(0.0f, 1f));
      if ((double) p1.Y > (double) p2.Y == (double) k1.Y < (double) k2.Y)
        return (double) move.X > 0.0 ? new Collision(k1.X - p2.X, new Point(-1f, 0.0f)) : new Collision(k2.X - p1.X, new Point(1f, 0.0f));
      Point point1 = p1;
      Point point2 = p2;
      if ((double) move.X > 0.0)
        point1.X = k1.X;
      else
        point2.X = k2.X;
      if ((double) move.Y > 0.0)
        point1.Y = k1.Y;
      else
        point2.Y = k2.Y;
      Point point3 = point1 - point2;
      return (double) Math.Abs(move.X) > 1.0000000116860974E-07 && ((double) Math.Abs(move.Y) < 1.0000000116860974E-07 || (double) point3.X / (double) move.X < (double) point3.Y / (double) move.Y) ? ((double) move.X > 0.0 ? new Collision(Math.Abs(point3.X), new Point(-1f, 0.0f)) : new Collision(Math.Abs(point3.X), new Point(1f, 0.0f))) : ((double) move.Y > 0.0 ? new Collision(Math.Abs(point3.Y), new Point(0.0f, -1f)) : new Collision(Math.Abs(point3.Y), new Point(0.0f, 1f)));
    }
  }
}
