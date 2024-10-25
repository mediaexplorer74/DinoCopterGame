// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.Color
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

#nullable disable
namespace Steamworks.Engine.Common
{
  public struct Color
  {
    public int R;
    public int G;
    public int B;
    public int A;

    public Color(int A, int R, int G, int B)
    {
      this.A = A;
      this.R = R;
      this.G = G;
      this.B = B;
    }

    public static Color FromARGB(int A, int R, int G, int B) => new Color(A, R, G, B);

    public uint GetUIntColor() => (uint) (this.R << 16 | this.G << 8) | (uint) this.B;

    public static explicit operator Microsoft.Xna.Framework.Color(Color color)
    {
      return new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
    }
  }
}
