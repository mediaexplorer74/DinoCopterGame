// GameManager.GameElements.DustSpawner

using GameManager.GraphicsSystem;
using GameManager.Utils;

#nullable disable
namespace GameManager.GameElements
{
  public class DustSpawner : Sprite
  {
    public float NextSpawn { get; private set; }

    private DustSpawner(Point pos)
      : base(9, pos)
    {
      this.SetPaintable(Paintable.CreateInvisibleRect(1f, 1f));
    }

    public static Sprite CreateDustSpawner(Point pos)
    {
      DustSpawner dustSpawner1 = new DustSpawner(pos);
      Sprite dustSpawner2 = (Sprite) dustSpawner1;
      dustSpawner1.Ref = dustSpawner2;
      return dustSpawner2;
    }

    public override void Update(float time)
    {
      Disp parent = this.Parent;
      float gameTime = parent.GetGameTime();
      if ((double) gameTime < (double) this.NextSpawn)
        return;
      this.NextSpawn = gameTime + 3f + Util.Randf(1f);
      Sprite dust = Dust.CreateDust(this.GetPos());
      parent.AddSprite(dust, 6);
    }
  }
}
