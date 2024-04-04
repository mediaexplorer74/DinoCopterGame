// GameManager.GameElements.Chaser

using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace GameManager.GameElements
{
  public class Chaser : Sprite
  {
    public GlobalMembers.ChaserState State { get; set; }

    public float StateChangeTime { get; set; }

    public int Left { get; set; }

    public int Right { get; set; }

    public int WereHit { get; set; }

    public bool Mirror { get; set; }

    private Chaser(Point pos)
      : base(3, pos)
    {
      this.WereHit = 0;
      this.Mirror = false;
    }

    public void SetState(GlobalMembers.ChaserState newState)
    {
      this.State = newState;
      this.StateChangeTime = GlobalMembers.MGame.GetGameTime();
      this.Speed = new Point();
      switch (this.State)
      {
        case GlobalMembers.ChaserState.ChaserStateSleep:
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.TriceratopsEatAnim));
          break;
        case GlobalMembers.ChaserState.ChaserStateCaution:
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.TriceratopsLookAnim));
          break;
        case GlobalMembers.ChaserState.ChaserStateChase:
          if (!GlobalMembers.MGame.HasLose)
            GlobalMembers.Manager.PlaySound(GlobalMembers.SfxTriceratopsRoar[Util.Random.Next(GlobalMembers.SfxTriceratopsRoar.Length)]);
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.TriceratopsRunAnim));
          break;
        case GlobalMembers.ChaserState.ChaserStateInjured:
          if (!GlobalMembers.MGame.HasLose)
            GlobalMembers.Manager.PlaySound(GlobalMembers.SfxTriceratopsHit);
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.TriceratopsInjuredAnim));
          break;
      }
    }

    private bool IsInRange(Sprite s)
    {
      Point pos1 = s.GetPos();
      Point pos2 = this.GetPos();
      return (double) pos1.X < (double) (this.Right + 1) && (double) pos1.X + (double) s.GetWidth() > (double) this.Left && (double) pos1.Y < (double) pos2.Y + (double) this.GetHeight() && (double) pos1.Y + (double) s.GetHeight() > (double) pos2.Y;
    }

    public static Sprite CreateChaser(Point _pos)
    {
      Chaser chaser1 = new Chaser(_pos);
      Sprite chaser2 = (Sprite) chaser1;
      chaser1.Ref = chaser2;
      return chaser2;
    }

    public bool OnStoneHit()
    {
      if (this.State == GlobalMembers.ChaserState.ChaserStateInjured)
        return false;
      this.SetState(GlobalMembers.ChaserState.ChaserStateInjured);
      return true;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
      if (this.IsInRange(GlobalMembers.MGame.GetPlayer()) && this.State != GlobalMembers.ChaserState.ChaserStateInjured)
        this.Mirror = (double) GlobalMembers.MGame.GetPlayer().GetPos().X + (double) GlobalMembers.MGame.GetPlayer().GetWidth() / 2.0 - (double) this.GetPos().X - (double) this.GetWidth() / 2.0 < 0.0;
      this.Img.Mirror = this.Mirror;
      base.Render(spriteBatch);
      this.Img.Mirror = false;
      GlobalMembers.MGame.EnterTutorial(5);
    }

    public override void Update(float time)
    {
      base.Update(time);
      switch (this.State)
      {
        case GlobalMembers.ChaserState.ChaserStateSleep:
          if (this.IsInRange(GlobalMembers.MGame.GetPlayer()))
          {
            this.SetState(GlobalMembers.ChaserState.ChaserStateCaution);
            break;
          }
          break;
        case GlobalMembers.ChaserState.ChaserStateCaution:
          if ((double) GlobalMembers.MGame.GetGameTime() - (double) this.StateChangeTime > 3.0)
          {
            if (this.IsInRange(GlobalMembers.MGame.GetPlayer()))
            {
              this.SetState(GlobalMembers.ChaserState.ChaserStateChase);
              break;
            }
            this.SetState(GlobalMembers.ChaserState.ChaserStateSleep);
            break;
          }
          break;
        case GlobalMembers.ChaserState.ChaserStateChase:
          Player player = (Player) GlobalMembers.MGame.GetPlayer();
          Point pos = GlobalMembers.MGame.GetPlayer().GetPos();
          if (this.IsInRange(GlobalMembers.MGame.GetPlayer()))
          {
            this.SetSpeed(new Point(((double) this.GetPos().X > (double) pos.X ? -1f : 1f) * 3f, 0.0f));
            break;
          }
          this.SetState(GlobalMembers.ChaserState.ChaserStateCaution);
          break;
        case GlobalMembers.ChaserState.ChaserStateInjured:
          if ((double) GlobalMembers.MGame.GetGameTime() - (double) this.StateChangeTime > 10.0)
          {
            this.SetState(GlobalMembers.ChaserState.ChaserStateCaution);
            break;
          }
          break;
      }
      this.CheckCollisions(5);
      this.WereHit = Math.Max(0, --this.WereHit);
    }

    public override void OnAdd()
    {
      base.OnAdd();
      Point pos = this.GetPos();
      Point platformSize = GlobalMembers.MGame.GetPlatformSize((int) pos.X, (int) pos.Y);
      this.Left = (int) platformSize.X;
      this.Right = (int) platformSize.Y;
      this.SetState(GlobalMembers.ChaserState.ChaserStateSleep);
    }

    public override void OnCollision(Sprite s)
    {
      if (s.GetTypeId() != 1 || this.State != GlobalMembers.ChaserState.ChaserStateChase)
        return;
      if (this.WereHit == 0)
        s.SetSpeed(s.Speed + new Point((float) Util.sgn(s.GetPos().X - this.GetPos().X), 1f).Normalised() * 20f);
      this.WereHit += 2;
    }

    public GlobalMembers.ChaserState GetState() => this.State;
  }
}
