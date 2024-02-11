// Decompiled with JetBrains decompiler
// Type: GameManager.GameElements.Player
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

using GameManager.GameLogic;
using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace GameManager.GameElements
{
  internal class Player : Sprite
  {
    public bool FirstCrash = true;

    public float CopterAnimPhase { get; set; }

    public float WhirlAnimPhase { get; set; }

    public bool OnGround { get; set; }

    public float WaterFloatEnergyStart { get; set; }

    public float WaterFloatStart { get; set; }

    public bool FloatsOnWater { get; set; }

    public int PassengersIn { get; set; }

    public int MaxPassengers { get; set; }

    private Player()
      : base(1, new Point())
    {
      this.PassengersIn = 0;
      this.MaxPassengers = 1;
      this.Img = GlobalMembers.Game.Copter[0];
    }

    public static Sprite CreatePlayer()
    {
      Player player1 = new Player();
      Sprite player2 = (Sprite) player1;
      player1.Ref = player2;
      player1.WhirlAnimPhase = player1.CopterAnimPhase = 0.0f;
      player1.FloatsOnWater = false;
      player1.WaterFloatEnergyStart = -1f;
      player1.OnGround = false;
      return player2;
    }

    public bool IsFloatsOnWater()
    {
      return this.FloatsOnWater && (double) GlobalMembers.Game.GetGameTime() - (double) this.WaterFloatStart > 1.0;
    }

    public bool IsOnGround()
    {
      return this.OnGround && (double) this.GetForce().Len() < 0.004999999888241291;
    }

    public override void OnAdd()
    {
      this.Size = new Point(GlobalMembers.FromPx(GlobalMembers.Game.Copter[0].GetWidth()), GlobalMembers.FromPx(GlobalMembers.Game.Copter[0].GetHeight() + GlobalMembers.Game.Whirl[0].GetHeight()));
    }

    public override void OnCollision(Sprite s)
    {
      if (s.GetTypeId() == 4)
        GlobalMembers.Game.Lose(3);
      if (s.GetTypeId() == 100)
      {
        Player player = this;
        player.Speed = player.Speed + new Point(-0.055f, 0.0f);
      }
      if (s.GetTypeId() == 101)
      {
        Player player = this;
        player.Speed = player.Speed + new Point(0.055f, 0.0f);
      }
      if (s.GetTypeId() == 8)
      {
        GlobalMembers.Game.AddEnergy(0.25f);
        s.Remove();
      }
      if (s.GetTypeId() != -2 && s.GetTypeId() != 10)
        return;
      Collision collision = Collision.CollisionRectangleRectangle(this.GetPos(), this.GetPos() + this.GetSize(), s.GetPos(), s.GetPos() + s.GetSize(), this.GetPos() - this.GetOldPos() - s.GetPos() + s.GetOldPos());
      if ((double) (collision.Normal * this.Speed.Dot(collision.Normal)).Len() >= 5.0)
        GlobalMembers.Game.Lose(0);
      if (s.GetTypeId() == 10 && (s as Lava).Kills(collision.Normal))
        GlobalMembers.Game.Lose(0);
      if ((double) this.Speed.Dot(collision.Normal) < 0.0)
      {
        this.Speed = Collision.DirectionAfterCollision(this.Speed, collision.Normal, 0.5f, 0.95f);
        Point point = this.GetPos() - this.GetOldPos();
        this.SetPos(this.GetPos() - collision.Normal * point.Dot(collision.Normal));
      }
      if ((double) this.Speed.Len() > 0.5 && this.FirstCrash)
      {
        GlobalMembers.SfxGroundHitInstance.Play();
        if (GlobalMembers.Game.HasLose)
          this.FirstCrash = false;
      }
      if ((double) collision.Normal.Y > 0.0)
        this.OnGround = true;
      GlobalMembers.Game.EnterTutorial(2);
    }

    public override void Update(float time)
    {
      if (!this.FloatsOnWater)
      {
        this.FloatsOnWater = (double) this.GetPos().Y < (double) GlobalMembers.Game.WaterLevel && (double) this.GetPos().Y + (double) this.GetHeight() > (double) GlobalMembers.Game.WaterLevel;
        if (this.FloatsOnWater)
          this.WaterFloatStart = GlobalMembers.Game.GetGameTime();
      }
      else
        this.FloatsOnWater = (double) this.GetPos().Y < (double) GlobalMembers.Game.WaterLevel && (double) this.GetPos().Y + (double) this.GetHeight() > (double) GlobalMembers.Game.WaterLevel;
      this.OnGround = false;
      float y1 = GlobalMembers.FromPx(GlobalMembers.TileSize.X * 13f) * time;
      float x = GlobalMembers.FromPx(GlobalMembers.TileSize.X * 6f) * time;
      bool flag = false;
      if (KeyHelper.KeysState[AbsKey.Left] == GameManager.Utils.State.Down)
      {
        Player player = this;
        player.Speed = player.Speed - new Point(x, 0.0f);
        flag = true;
      }
      if (KeyHelper.KeysState[AbsKey.Right] == GameManager.Utils.State.Down)
      {
        Player player = this;
        player.Speed = player.Speed + new Point(x, 0.0f);
        flag = true;
      }
      if (KeyHelper.KeysState[AbsKey.Up] == GameManager.Utils.State.Down)
      {
        Player player = this;
        player.Speed = player.Speed + new Point(0.0f, y1);
        flag = true;
      }
      if (KeyHelper.KeysState[AbsKey.Down] == GameManager.Utils.State.Down)
      {
        Player player = this;
        player.Speed = player.Speed - new Point(0.0f, y1 / 2f);
        flag = true;
      }
      if (!flag && GlobalMembers.Game.GetMoveDir().NonZero())
      {
        flag = true;
        Player player = this;
        player.Speed = player.Speed + new Point(GlobalMembers.Game.GetMoveDir().X * x, GlobalMembers.Game.GetMoveDir().Y * y1);
      }
      Player player1 = this;
      player1.Speed = player1.Speed * 0.99f;
      Player player2 = this;
      player2.Speed = player2.Speed + GlobalMembers.Game.GetGravity(this.Ref) * time;
      float y2 = this.GetPos().Y;
      if ((double) y2 < (double) GlobalMembers.Game.GetWaterLevel() && (double) this.OldPos.Y >= (double) GlobalMembers.Game.GetWaterLevel() && !GlobalMembers.Game.HasLose)
        GlobalMembers.Manager.PlaySound(GlobalMembers.SfxSplash);
      if ((double) y2 < (double) GlobalMembers.Game.GetWaterLevel())
      {
        float num = Math.Min(2000f, (float) (((double) GlobalMembers.Game.GetWaterLevel() - (double) y2) * 2000.0) * time / this.GetWidth());
        this.Speed.Y += time * num;
      }
      float num1 = this.Speed.Len();
      if (flag)
        this.WhirlAnimPhase += (this.CopterAnimPhase += num1 * 2f / y1);
      float num2 = 0.0f;
      if (flag)
        num2 = 0.5f + Math.Min(0.8f, Math.Max(0.0f, (float) (0.800000011920929 * ((double) num1 - 0.0) / 2.5)));
      GlobalMembers.SfxCopterRotorInstance.Volume = num2 / 1.3f;
      while ((double) this.CopterAnimPhase > 1900.0)
        this.CopterAnimPhase -= 1900f;
      while ((double) this.WhirlAnimPhase > 30000.0)
        this.WhirlAnimPhase -= 30000f;
      base.Update(time);
      this.CheckCollisions(1);
      this.CheckCollisions(3);
      this.CheckCollisions(2);
      this.CheckCollisions(7);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
      Point pos = this.GetPos();
      GlobalMembers.Game.Copter[(int) ((double) this.CopterAnimPhase / 1900.0 * (double) GlobalMembers.Game.Copter.Count / 2.0) + (this.PassengersIn > 0 ? 1 : 0) * GlobalMembers.Game.Copter.Count / 2].Paint(GlobalMembers.ToPx(pos.X), GlobalMembers.ToPx(pos.Y), 12, spriteBatch);
      GlobalMembers.Game.Whirl[(int) ((double) this.WhirlAnimPhase / 30000.0 * (double) GlobalMembers.Game.Whirl.Count)].Paint(GlobalMembers.ToPx(pos.X + this.GetWidth() / 2f), GlobalMembers.ToPx(pos.Y) + GlobalMembers.Game.Copter[0].GetHeight(), 20, spriteBatch);
      for (int index = 0; index < 10; ++index)
      {
        Sprite passenger1 = GlobalMembers.Game.Spawns[index].GetPassenger();
        if (passenger1 != null)
        {
          Passenger passenger2 = passenger1 as Passenger;
          if (passenger2.GetState() == GlobalMembers.PassengerState.PassengerStateInChopper)
          {
            Point point1 = new Point(GlobalMembers.Game.Spawns[passenger2.GetTargetStation()].GetPlatform().X, GlobalMembers.Game.Spawns[passenger2.GetTargetStation()].GetPlatform().Y + 1f) + passenger2.GetSize() / 2f;
            Point point2 = this.GetPos() + this.GetSize() / 2f;
            Point point3 = point1 - point2;
            if ((double) point3.Len() > 3.0)
            {
              float angle = 6.28318548f - point3.Angle();
              Paintable rotated = Paintable.CreateRotated(GlobalMembers.Game.ArrowImg, new Point(GlobalMembers.Game.ArrowImg.GetWidth() / 2f, GlobalMembers.Game.ArrowImg.GetHeight() / 2f), angle);
              Point point4 = point3.Normalised() * 2f + point2;
              rotated.Paint(GlobalMembers.ToPx(point4.X), GlobalMembers.ToPx(point4.Y), 18, spriteBatch);
            }
          }
        }
      }
    }

    public int GetPassengersIn() => this.PassengersIn;

    public void PassengerIn() => ++this.PassengersIn;

    public void PassengerOut() => --this.PassengersIn;

    public bool AcceptMorePassengers() => this.PassengersIn < this.MaxPassengers;
  }
}
