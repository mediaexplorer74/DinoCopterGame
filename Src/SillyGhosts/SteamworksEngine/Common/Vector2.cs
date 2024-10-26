// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.Vector2
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using System;


namespace Steamworks.Engine.Common
{
  public struct Vector2
  {
    public float X;
    public float Y;

    public static Vector2 Zero => new Vector2(0.0, 0.0);

    public Vector2(double X, double Y)
    {
      this.X = (float) X;
      this.Y = (float) Y;
    }

    public override string ToString() => this.X.ToString() + ";" + this.Y.ToString();

    public override bool Equals(object obj)
    {
      return obj is Vector2 vector2 && (double) vector2.X == (double) this.X && (double) vector2.Y == (double) this.Y;
    }

    public float Length()
    {
      return (float) Math.Sqrt((double) this.X * (double) this.X + (double) this.Y * (double) this.Y);
    }

    public Vector2 Clamp(float Max)
    {
      float val1 = this.Length();
      if ((double) val1 == 0.0)
        return Vector2.Zero;
      float num = Math.Min(val1, Max);
      return new Vector2((double) this.X * ((double) num / (double) val1), (double) this.Y * ((double) num / (double) val1));
    }

    public Vector2 Plus(Vector2 vector2)
    {
      return new Vector2((double) this.X + (double) vector2.X, (double) this.Y + (double) vector2.Y);
    }

    public Vector2 Minus(Vector2 vector2)
    {
      return new Vector2((double) this.X - (double) vector2.X, (double) this.Y - (double) vector2.Y);
    }

    public Vector2 Multiply(double number)
    {
      return new Vector2((double) (this.X * (float) number), (double) (this.Y * (float) number));
    }

    public Vector2 Divide(double number)
    {
      return new Vector2((double) (this.X / (float) number), (double) (this.Y / (float) number));
    }

    public static explicit operator Microsoft.Xna.Framework.Vector2(Vector2 vector)
    {
      return new Microsoft.Xna.Framework.Vector2(vector.X, vector.Y);
    }

    public override int GetHashCode() => base.GetHashCode();

    public Vector2 Normalize()
    {
      return (double) this.Length() == 0.0 ? Vector2.Zero : new Vector2((double) this.X / (double) this.Length(), (double) this.Y / (double) this.Length());
    }

    public float Angle()
    {
      return (float) (Math.Atan2((double) this.Y, (double) this.X) - Math.Atan2(-1.0, 0.0));
    }
  }
}
