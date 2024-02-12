// GameManager.Lava

using GameManager.GraphicsSystem;

#nullable disable
namespace GameManager
{
  internal class Lava : Sprite
  {
    private int Type;

    private Lava(Point pos, int type)
      : base(10, pos)
    {
      this.Type = type;
    }

    public static Sprite CreateLava(Point pos, int type)
    {
      Lava lava1 = new Lava(pos, type);
      Sprite lava2 = (Sprite) lava1;
      lava1.Ref = lava2;
      return lava2;
    }

    public bool Kills(Point colDir)
    {
      if (this.Type == 72 || this.Type == 73 || this.Type == 74 || this.Type == 75 || this.Type == 78 || this.Type == 79)
        return true;
      if ((double) colDir.Y < 0.0)
        return this.Type == 56 || this.Type == 60 || this.Type == 61 || this.Type == 41 || this.Type == 16;
      if ((double) colDir.Y > 0.0)
        return this.Type == 57 || this.Type == 40 || this.Type == 15;
      if ((double) colDir.X < 0.0)
        return this.Type == 58 || this.Type == 60 || this.Type == 42 || this.Type == 17;
      if ((double) colDir.X <= 0.0)
        return false;
      return this.Type == 59 || this.Type == 61 || this.Type == 43 || this.Type == 18;
    }

    public static bool IsLava(int type)
    {
      return type == 59 || type == 61 || type == 43 || type == 58 || type == 60 || type == 4 || type == 72 || type == 73 || type == 74 || type == 75 || type == 78 || type == 79 || type == 56 || type == 60 || type == 61 || type == 41 || type == 57 || type == 40;
    }
  }
}
