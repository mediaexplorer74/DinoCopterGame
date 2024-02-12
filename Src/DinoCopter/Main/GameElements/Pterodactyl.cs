// GameManager.GameElements.Pterodactyl

using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager.GameElements
{
  internal class Pterodactyl : Sprite
  {
    public bool Dead { get; private set; }

    public float StartY { get; private set; }

    public float DieTime { get; private set; }

    private Pterodactyl(Point pos)
      : base(4, pos)
    {
      this.Dead = false;
      this.SetPaintable(GlobalMembers.Game.PterodactylAnim);
      this.StartY = pos.Y;
    }

    public static Sprite CreatePterodactyl(Point pos)
    {
      Pterodactyl pterodactyl1 = new Pterodactyl(pos);
      Sprite pterodactyl2 = (Sprite) pterodactyl1;
      pterodactyl1.Ref = pterodactyl2;
      pterodactyl2.SetSpeed(new Point(2f, 0.0f));
      return pterodactyl2;
    }

    public override void Update(float time)
    {
      base.Update(time);
      if (this.Dead)
      {
        Pterodactyl pterodactyl = this;
        pterodactyl.Speed = pterodactyl.Speed + GlobalMembers.Game.GetGravity(this.Ref) * time;
        if ((double) GlobalMembers.Game.GetGameTime() - (double) this.DieTime <= 30.0)
          return;
        this.Dead = false;
        this.SetPos(new Point(GlobalMembers.Game.GetMapSize().X, this.StartY));
        this.SetSpeed(new Point(2f, 0.0f));
        this.SetTypeId(4);
      }
      else
      {
        if ((double) this.Speed.X > 0.0 && (double) this.GetPos().X > (double) GlobalMembers.Game.GetMapSize().X)
          this.SetPos(new Point(-this.GetWidth(), this.GetPos().Y));
        if ((double) this.Speed.X < 0.0 && (double) this.GetPos().X < (double) this.GetWidth())
          this.SetPos(new Point(GlobalMembers.Game.GetMapSize().X, this.GetPos().X));
        this.CheckCollisions(5);
      }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
      base.Render(spriteBatch);
      GlobalMembers.Game.EnterTutorial(4);
    }

    public override void OnCollision(Sprite s)
    {
    }

    public bool OnStoneHit()
    {
      if (!this.Dead)
      {
        this.Dead = true;
        this.SetTypeId(0);
        if (!GlobalMembers.Game.HasLose)
          GlobalMembers.Manager.PlaySound(GlobalMembers.SfxPterodactylHit);
        this.DieTime = GlobalMembers.Game.GetGameTime();
      }
      return true;
    }
  }
}
