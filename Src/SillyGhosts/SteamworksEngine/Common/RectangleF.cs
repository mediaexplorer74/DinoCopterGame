// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.RectangleF
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using System;

#nullable disable
namespace Steamworks.Engine.Common
{
  public class RectangleF
  {
    private float _x;
    private float _y;
    private float _width;
    private float _height;

    public RectangleF(float X, float Y, float Width, float Height)
    {
      this._x = X;
      this._y = Y;
      this._width = Width;
      this._height = Height;
    }

    public float X
    {
      get => this._x;
      set => this._x = value;
    }

    public float Y
    {
      get => this._y;
      set => this._y = value;
    }

    public float Width
    {
      get => this._width;
      set => this._width = value;
    }

    public float Height
    {
      get => this._height;
      set => this._height = value;
    }

    public float Left => this._x;

    public float Right => this._x + this._width;

    public float Top => this._y;

    public float Bottom => this._y + this.Height;

    public Vector2 GetIntersectionDepth(RectangleF rectB)
    {
      float num1 = this.Width / 2f;
      float num2 = this.Height / 2f;
      float num3 = rectB.Width / 2f;
      float num4 = rectB.Height / 2f;
      Vector2 vector2_1 = new Vector2((double) this.Left + (double) num1, (double) this.Top + (double) num2);
      Vector2 vector2_2 = new Vector2((double) rectB.Left + (double) num3, (double) rectB.Top + (double) num4);
      float num5 = vector2_1.X - vector2_2.X;
      float num6 = vector2_1.Y - vector2_2.Y;
      float num7 = num1 + num3;
      float num8 = num2 + num4;
      return (double) Math.Abs(num5) >= (double) num7 || (double) Math.Abs(num6) >= (double) num8 ? Vector2.Zero : new Vector2((double) num5 > 0.0 ? (double) num7 - (double) num5 : -(double) num7 - (double) num5, (double) num6 > 0.0 ? (double) (num8 - num6) : (double) (-num8 - num6));
    }

    public Vector2 GetBottomCenter()
    {
      return new Vector2((double) this.X + (double) this.Width / 2.0, (double) this.Bottom);
    }
  }
}
