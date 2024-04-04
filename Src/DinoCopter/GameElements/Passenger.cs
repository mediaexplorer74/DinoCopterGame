// GameManager.GameElements.Passenger

using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace GameManager.GameElements
{
  internal class Passenger : Sprite
  {
    public GlobalMembers.PassengerState State { get; set; }

    public float StateChangeTime { get; set; }

    public int TargetStation { get; set; }

    public int FromStation { get; set; }

    public int PassengerType { get; set; }

    public float FloatStart { get; set; }

    private Passenger(Point _pos, int _passengerType, int _fromStation)
      : base(2, _pos)
    {
      this.PassengerType = _passengerType;
      this.TargetStation = -1;
      this.FromStation = _fromStation;
      this.FloatStart = -15f;
    }

    public void SetState(GlobalMembers.PassengerState newState)
    {
      if (this.State == GlobalMembers.PassengerState.PassengerStateSink)
        return;
      this.State = newState;
      this.StateChangeTime = GlobalMembers.MGame.GetGameTime();
      switch (this.State)
      {
        case GlobalMembers.PassengerState.PassengerStateShow:
          this.SetPaintable(Paintable.CreateModulate(GlobalMembers.MGame.PassengerStand[this.PassengerType][0], new Color(1f, 1f, 1f, 0.0f), new Color(1f, 1f, 1f, 1f), 1f));
          break;
        case GlobalMembers.PassengerState.PassengerStateGoToStation:
        case GlobalMembers.PassengerState.PassengerStateGoIn:
        case GlobalMembers.PassengerState.PassengerStateGoOut:
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.PassengerWalkAnim[this.PassengerType]));
          break;
        case GlobalMembers.PassengerState.PassengerStateWait:
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.PassengerStandAnim[this.PassengerType]));
          break;
        case GlobalMembers.PassengerState.PassengerStateTellStation:
          this.GenerateStation();
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.PassengerSpeakAnim[this.PassengerType]));
          break;
        case GlobalMembers.PassengerState.PassengerStateHide:
          this.SetPaintable(Paintable.CreateModulate(GlobalMembers.MGame.PassengerStand[this.PassengerType][0], new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0.0f), 1f));
          this.SetLiveTime(1f);
          break;
        case GlobalMembers.PassengerState.PassengerStateFall:
          GlobalMembers.MGame.EnterTutorial(7);
          if (!GlobalMembers.MGame.HasLose)
            GlobalMembers.Manager.PlaySound(GlobalMembers.SfxPassengerHit);
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.PassengerSinkAnim[this.PassengerType]));
          break;
        case GlobalMembers.PassengerState.PassengerStateFloat:
        case GlobalMembers.PassengerState.PassengerStateSwimTo:
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.PassengerSwimAnim[this.PassengerType]));
          break;
        case GlobalMembers.PassengerState.PassengerStateFloatTellStation:
          this.GenerateStation();
          break;
        case GlobalMembers.PassengerState.PassengerStateSink:
          this.SetPaintable(Paintable.Copy(GlobalMembers.MGame.PassengerSinkAnim[this.PassengerType]));
          this.SetSpeed(new Point(0.0f, -2f));
          break;
      }
    }

    private void GenerateStation()
    {
      if (this.TargetStation != -1 && GlobalMembers.MGame.Spawns[this.TargetStation].CanTakePassenger())
        return;
      int maxValue = -1;
      for (int index = 0; index < 10; ++index)
      {
        if (GlobalMembers.MGame.Spawns[index].CanTakePassenger())
          ++maxValue;
      }
      int num = new Random().Next(0, maxValue);
      for (int index = 0; index < 10; ++index)
      {
        if (GlobalMembers.MGame.Spawns[index].CanTakePassenger() && index != this.FromStation)
        {
          if (num == 0)
          {
            this.TargetStation = index;
            break;
          }
          --num;
        }
      }
    }

    public static Sprite CreatePassenger(Point _pos, int _passengerType, int _fromStation)
    {
      Passenger passenger1 = new Passenger(_pos, _passengerType, _fromStation);
      Sprite passenger2 = (Sprite) passenger1;
      passenger1.Ref = passenger2;
      return passenger2;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
      if ((double) this.Speed.X < 0.0)
        this.Img.Mirror = true;
      else
        this.Img.Mirror = false;
      if (this.State != GlobalMembers.PassengerState.PassengerStateInChopper)
        base.Render(spriteBatch);
      this.RenderCloud(spriteBatch);
    }

    public virtual void RenderCloud(SpriteBatch spriteBatch)
    {
      switch (this.State)
      {
        case GlobalMembers.PassengerState.PassengerStateTellStation:
        case GlobalMembers.PassengerState.PassengerStateFloatTellStation:
          bool flag = (double) this.GetPos().X > (double) GlobalMembers.MGame.Player.GetPos().X;
          GlobalMembers.MGame.Cloud.Mirror = flag;
          GlobalMembers.MGame.Cloud.Paint(GlobalMembers.ToPx(this.GetPos().X + this.GetWidth() / 2f), GlobalMembers.ToPx(this.GetPos().Y + this.GetHeight()), 4 | (flag ? 32 : 8), spriteBatch);
          GlobalMembers.MGame.Cloud.Mirror = false;
          GlobalMembers.MGame.StationsCloudSigns[this.TargetStation].Paint(GlobalMembers.ToPx(this.GetPos().X + this.GetWidth() / 2f) + (float) ((double) GlobalMembers.MGame.Cloud.GetWidth() / 2.0 * (flag ? -1.0 : 1.0)), (float) ((double) GlobalMembers.ToPx(this.GetPos().Y + this.GetHeight()) + (double) GlobalMembers.MGame.Cloud.GetHeight() - (double) GlobalMembers.MGame.Cloud.GetWidth() / 2.0), 18, spriteBatch);
          break;
      }
    }

    public override void Update(float time)
    {
      Player player1 = (Player) GlobalMembers.MGame.GetPlayer();
      Sprite player2 = GlobalMembers.MGame.GetPlayer();
      Point pos1 = this.GetPos();
      Point pos2 = player2.GetPos();
      base.Update(time);
      switch (this.State)
      {
        case GlobalMembers.PassengerState.PassengerStateShow:
          if ((double) GlobalMembers.MGame.GetGameTime() - (double) this.StateChangeTime >= 1.0)
          {
            this.SetState(GlobalMembers.PassengerState.PassengerStateGoToStation);
            break;
          }
          break;
        case GlobalMembers.PassengerState.PassengerStateGoToStation:
          Point platform = GlobalMembers.MGame.GetSpawns()[this.FromStation].GetPlatform();
          if ((double) GlobalMembers.MGame.GetSpawns()[this.FromStation].GetPos().X < (double) platform.X)
            platform.X -= 2f;
          else
            platform.X += 2f;
          platform.X = Math.Min((float) GlobalMembers.MGame.GetSpawns()[this.FromStation].GetRight(), Math.Max(platform.X, (float) GlobalMembers.MGame.GetSpawns()[this.FromStation].GetLeft()));
          float num1 = (float) ((double) platform.X + 0.5 - (double) this.GetPos().X - (double) this.GetWidth() / 2.0);
          if ((double) Math.Abs(num1) > 0.10000000149011612)
          {
            this.SetSpeed(new Point(((double) num1 < 0.0 ? -1f : 1f) * 2f, 0.0f));
            break;
          }
          this.SetSpeed(new Point());
          this.SetState(GlobalMembers.PassengerState.PassengerStateWait);
          break;
        case GlobalMembers.PassengerState.PassengerStateWait:
          if (GlobalMembers.MGame.GetSpawns()[this.FromStation].IsOnStation(player2) && player1.AcceptMorePassengers())
          {
            this.SetState(GlobalMembers.PassengerState.PassengerStateTellStation);
            break;
          }
          break;
        case GlobalMembers.PassengerState.PassengerStateTellStation:
          if (GlobalMembers.MGame.GetSpawns()[this.FromStation].IsOnStation(player2) && player1.AcceptMorePassengers())
          {
            if ((double) GlobalMembers.MGame.GetGameTime() - (double) this.StateChangeTime >= 3.0)
            {
              this.SetState(GlobalMembers.PassengerState.PassengerStateGoIn);
              break;
            }
            break;
          }
          this.SetState(GlobalMembers.PassengerState.PassengerStateWait);
          break;
        case GlobalMembers.PassengerState.PassengerStateGoIn:
          if (GlobalMembers.MGame.GetSpawns()[this.FromStation].IsOnStation(player2) && player1.AcceptMorePassengers())
          {
            float num2 = (float) ((double) player2.GetPos().X + (double) player2.GetWidth() / 2.0 - (double) this.GetPos().X - (double) this.GetWidth() / 2.0);
            if ((double) Math.Abs(num2) > 0.05000000074505806)
            {
              this.SetSpeed(new Point(((double) num2 < 0.0 ? -1f : 1f) * 2f, 0.0f));
              break;
            }
            if ((double) Math.Abs(num2) < 0.05000000074505806)
            {
              this.SetSpeed(new Point());
              this.SetState(GlobalMembers.PassengerState.PassengerStateInChopper);
              player1.PassengerIn();
              break;
            }
            break;
          }
          this.SetState(GlobalMembers.PassengerState.PassengerStateGoToStation);
          break;
        case GlobalMembers.PassengerState.PassengerStateInChopper:
          this.SetPos(player2.GetPos() + new Point((float) (((double) GlobalMembers.MGame.GetPlayer().GetWidth() - (double) this.GetWidth()) / 2.0), 0.0f));
          if (GlobalMembers.MGame.GetSpawns()[this.TargetStation].IsOnStation(player2))
          {
            this.SetState(GlobalMembers.PassengerState.PassengerStateGoOut);
            player1.PassengerOut();
            break;
          }
          break;
        case GlobalMembers.PassengerState.PassengerStateGoOut:
          float num3 = (float) ((double) GlobalMembers.MGame.GetSpawns()[this.TargetStation].GetPos().X + 0.5 - (double) this.GetPos().X - (double) this.GetWidth() / 2.0);
          if ((double) Math.Abs(num3) >= (double) Math.Abs(this.Speed.X * time))
          {
            this.SetSpeed(new Point(((double) num3 < 0.0 ? -1f : 1f) * 2f, 0.0f));
            break;
          }
          this.SetSpeed(new Point());
          this.SetState(GlobalMembers.PassengerState.PassengerStateHide);
          break;
        case GlobalMembers.PassengerState.PassengerStateHide:
          if ((double) GlobalMembers.MGame.GetGameTime() - (double) this.StateChangeTime >= 1.0)
          {
            GlobalMembers.MGame.PassengerDelivered();
            this.Remove();
            break;
          }
          break;
        case GlobalMembers.PassengerState.PassengerStateFall:
          Passenger passenger = this;
          passenger.Speed = passenger.Speed + GlobalMembers.MGame.GetGravity(this.Ref) * time;
          break;
        case GlobalMembers.PassengerState.PassengerStateFloat:
          if ((double) GlobalMembers.MGame.GetGameTime() - (double) this.FloatStart >= 15.0)
          {
            this.SetState(GlobalMembers.PassengerState.PassengerStateSink);
            this.SetSpeed(new Point(0.0f, -2f));
          }
          if (player1.IsFloatsOnWater() && player1.AcceptMorePassengers())
          {
            this.SetState(GlobalMembers.PassengerState.PassengerStateFloatTellStation);
            break;
          }
          break;
        case GlobalMembers.PassengerState.PassengerStateFloatTellStation:
          if (player1.IsFloatsOnWater() && player1.AcceptMorePassengers())
          {
            if ((double) GlobalMembers.MGame.GetGameTime() - (double) this.StateChangeTime >= 3.0)
            {
              this.SetState(GlobalMembers.PassengerState.PassengerStateSwimTo);
              break;
            }
            break;
          }
          this.SetState(GlobalMembers.PassengerState.PassengerStateFloat);
          break;
        case GlobalMembers.PassengerState.PassengerStateSwimTo:
          if (player1.IsFloatsOnWater() && player1.AcceptMorePassengers())
          {
            float num4 = (float) ((double) player2.GetPos().X + 0.5 - (double) this.GetPos().X - (double) this.GetWidth() / 2.0);
            if ((double) Math.Abs(num4) > 0.5)
            {
              this.SetSpeed(new Point(((double) num4 < 0.0 ? -1f : 1f) * 2f, 0.0f));
              break;
            }
            if ((double) Math.Abs(num4) < 0.05000000074505806)
            {
              this.SetSpeed(new Point());
              this.SetState(GlobalMembers.PassengerState.PassengerStateInChopper);
              player1.PassengerIn();
              break;
            }
            break;
          }
          this.SetSpeed(new Point());
          this.SetState(GlobalMembers.PassengerState.PassengerStateFloat);
          break;
        case GlobalMembers.PassengerState.PassengerStateSink:
          if ((double) this.GetPos().Y < -(double) this.GetHeight())
          {
            this.Remove();
            break;
          }
          break;
      }
      if (this.State != GlobalMembers.PassengerState.PassengerStateShow && this.State != GlobalMembers.PassengerState.PassengerStateGoOut && this.State != GlobalMembers.PassengerState.PassengerStateHide && this.State != GlobalMembers.PassengerState.PassengerStateFloat && this.State != GlobalMembers.PassengerState.PassengerStateSink && this.State != GlobalMembers.PassengerState.PassengerStateSwimTo && this.State != GlobalMembers.PassengerState.PassengerStateFloatTellStation && this.State != GlobalMembers.PassengerState.PassengerStateFall && this.State != GlobalMembers.PassengerState.PassengerStateInChopper && Util.RectsOverlaps(pos1.X, pos1.Y, pos1.X + this.GetWidth(), pos1.Y + this.GetHeight(), pos2.X, pos2.Y, pos2.X + player2.GetWidth(), pos2.Y + player2.GetHeight()) && (double) player2.GetSpeed().Len() > 0.10000000149011612)
      {
        this.SetSpeed(new Point());
        this.SetState(GlobalMembers.PassengerState.PassengerStateFall);
      }
      if ((double) GlobalMembers.MGame.GetWaterLevel() - (double) this.GetPos().Y >= (double) this.GetHeight() * 0.5 && this.State != GlobalMembers.PassengerState.PassengerStateSink && this.State != GlobalMembers.PassengerState.PassengerStateFloatTellStation && this.State != GlobalMembers.PassengerState.PassengerStateSwimTo && this.State != GlobalMembers.PassengerState.PassengerStateFloat && this.State != GlobalMembers.PassengerState.PassengerStateInChopper)
      {
        this.FloatStart = this.Parent.GetGameTime();
        this.SetState(GlobalMembers.PassengerState.PassengerStateFloat);
        this.SetSpeed(new Point());
      }
      if (this.State == GlobalMembers.PassengerState.PassengerStateFloat || this.State == GlobalMembers.PassengerState.PassengerStateSwimTo || this.State == GlobalMembers.PassengerState.PassengerStateFloatTellStation)
        this.SetPos(new Point(this.GetPos().X, GlobalMembers.MGame.GetWaterLevel() - this.GetHeight() * 0.5f));
      if ((double) this.GetPos().Y >= (double) GlobalMembers.MGame.WaterLevel - 2.0 || this.State == GlobalMembers.PassengerState.PassengerStateInChopper)
        return;
      GlobalMembers.MGame.Lose(2);
    }

    public override void OnAdd()
    {
      base.OnAdd();
      this.SetState(GlobalMembers.PassengerState.PassengerStateShow);
      this.SetPos(this.GetPos() + new Point((float) (0.5 - (double) this.GetWidth() / 2.0), 0.0f));
    }

    public GlobalMembers.PassengerState GetState() => this.State;

    public int GetTargetStation() => this.TargetStation;

    public int GetFromStation() => this.FromStation;
  }
}
