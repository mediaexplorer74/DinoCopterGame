// GameManager.GameElements.Dust

using GameManager.GraphicsSystem;
using GameManager.Utils;
using System;

#nullable disable
namespace GameManager.GameElements
{
  internal class Dust : Sprite
  {
    public float GeneralDirection { get; private set; }

    public float DirectionEndTime { get; private set; }

    public float StartDirection { get; private set; }

    public float TargetDirection { get; private set; }

    public float DirectionStartTime { get; private set; }

    public Point SelfSpeed { get; private set; }

    private Dust(Point pos)
      : base(0, pos)
    {
      this.StartDirection = this.GeneralDirection = Util.Randf(6.28318548f);
      this.TargetDirection = 0.0f;
      this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.DustAnim));
      this.DirectionStartTime = this.DirectionEndTime = -1f;
      this.SelfSpeed = new Point();
    }

    public static Sprite CreateDust(Point pos)
    {
      Dust dust1 = new Dust(pos);
      Sprite dust2 = (Sprite) dust1;
      dust1.Ref = dust2;
      return dust2;
    }

    public override void OnCollision(Sprite s)
    {
      if (s.GetTypeId() == 100)
      {
        Dust dust = this;
        dust.Speed = dust.Speed + new Point(-0.055f, 0.0f);
      }
      if (s.GetTypeId() != 101)
        return;
      Dust dust1 = this;
      dust1.Speed = dust1.Speed + new Point(0.055f, 0.0f);
    }

    public override void Update(float time)
    {
      if ((double) this.GetPos().X > (double) GlobalMembers.MGame.GetMapSize().X 
                || (double) this.GetPos().Y > (double) GlobalMembers.MGame.GetMapSize().Y 
                || (double) this.GetPos().Y + (double) this.GetHeight() < 0.0 
                || (double) this.GetPos().X + (double) this.GetWidth() < 0.0)
      {
        this.Remove();
      }
      else
      {
        Dust dust1 = this;
        dust1.Speed = dust1.Speed + this.SelfSpeed;
        this.CheckCollisions(7);
        base.Update(time);
        Dust dust2 = this;
        dust2.Speed = dust2.Speed - this.SelfSpeed;
        this.Speed = this.Speed * 0.99f;
        Disp parent = this.Parent;
        float num1 = this.DirectionStartTime + this.DirectionEndTime - parent.GameTime;
        if ((double) num1 < 0.0)
        {
          this.DirectionStartTime = parent.GetGameTime();
          this.DirectionEndTime = 2f + Util.Randf(1f);
          this.StartDirection = this.TargetDirection + this.StartDirection;
          this.TargetDirection = (float) ((double) this.GeneralDirection + (double) Util.Randf(3.14159274f) - 1.5707963705062866) - this.StartDirection;
          num1 = 0.0f;
        }
        float num2 = this.StartDirection + this.TargetDirection * (float) (1.0 - (double) num1 / (double) this.DirectionEndTime);
        this.SelfSpeed = new Point((float) Math.Cos((double) num2), (float) Math.Sin((double) num2)) * 0.5f;
      }
    }
  }
}
