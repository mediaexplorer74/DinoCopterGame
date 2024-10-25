// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Collisions.CollisionExtensions
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;
using Steamworks.Shared.Map;
using System;

#nullable disable
namespace Steamworks.Physics.Collisions
{
  public static class CollisionExtensions
  {
    public static RectangleF GetTileBounds(this TMXMapData Map, int x, int y)
    {
      return new RectangleF((float) (x * Map.tilewidth), (float) (y * Map.tilewidth), (float) Map.tilewidth, (float) Map.tilewidth);
    }

    public static RectangleF GetMapBounds(this TMXMapData Map)
    {
      return new RectangleF(0.0f, 0.0f, (float) (Map.tilewidth * Map.width), (float) (Map.tilewidth * Map.height));
    }

    public static Collision GetCollisionForBounds(this RectangleF Bounds1, RectangleF Bounds2)
    {
      CollisionDirection collisionDirection = CollisionDirection.None;
      Vector2 intersectionDepth = Bounds2.GetIntersectionDepth(Bounds1);
      if (!intersectionDepth.Equals((object) Vector2.Zero))
      {
        float num = Math.Abs(intersectionDepth.X);
        collisionDirection = (double) Math.Abs(intersectionDepth.Y) >= (double) num ? ((double) intersectionDepth.X <= 0.0 ? CollisionDirection.Left : CollisionDirection.Right) : ((double) intersectionDepth.Y <= 0.0 ? CollisionDirection.Top : CollisionDirection.Bottom);
      }
      return new Collision()
      {
        Intersection = intersectionDepth,
        Direction = collisionDirection
      };
    }

    public static RectangleF GetRotatedRectangle(RectangleF rectangle, float Angle, Vector2 Origin)
    {
      Vector2 Point1 = new Vector2((double) rectangle.Left, (double) rectangle.Top);
      Vector2 Point2 = new Vector2((double) rectangle.Left, (double) rectangle.Bottom);
      Vector2 Point3 = new Vector2((double) rectangle.Right, (double) rectangle.Top);
      Vector2 Point4 = new Vector2((double) rectangle.Right, (double) rectangle.Bottom);
      double Angle1 = (double) Angle;
      Vector2 Origin1 = Origin;
      Vector2 vector2_1 = CollisionExtensions.RotatePoint(Point1, (float) Angle1, Origin1);
      Vector2 vector2_2 = CollisionExtensions.RotatePoint(Point2, Angle, Origin);
      Vector2 vector2_3 = CollisionExtensions.RotatePoint(Point3, Angle, Origin);
      Vector2 vector2_4 = CollisionExtensions.RotatePoint(Point4, Angle, Origin);
      float X = Math.Min(Math.Min(vector2_1.X, vector2_2.X), Math.Min(vector2_3.X, vector2_4.X));
      float Y = Math.Min(Math.Min(vector2_1.Y, vector2_2.Y), Math.Min(vector2_3.Y, vector2_4.Y));
      float Width = Math.Max(Math.Max(vector2_1.X, vector2_2.X), Math.Max(vector2_3.X, vector2_4.X)) - X;
      float Height = Math.Max(Math.Max(vector2_1.Y, vector2_2.Y), Math.Max(vector2_3.Y, vector2_4.Y)) - Y;
      return new RectangleF(X, Y, Width, Height);
    }

    private static Vector2 RotatePoint(Vector2 Point, float Angle, Vector2 Origin)
    {
      Point.X = (float) ((double) Origin.X + ((double) Point.X - (double) Origin.X) * Math.Cos((double) Angle) + ((double) Point.Y - (double) Origin.Y) * Math.Sin((double) Angle));
      Point.Y = (float) ((double) Origin.Y - ((double) Point.X - (double) Origin.X) * Math.Sin((double) Angle) + ((double) Point.Y - (double) Origin.Y) * Math.Cos((double) Angle));
      return Point;
    }
  }
}
