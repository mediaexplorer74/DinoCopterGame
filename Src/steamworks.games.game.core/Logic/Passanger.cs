// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.Passanger
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine.Common;
using Steamworks.Games.Game.Core.Interfaces;
using Steamworks.Physics.Body;
using System.Collections.Generic;

#nullable disable
namespace Steamworks.Games.Game.Core.Logic
{
  public class Passanger : RigidBody, IPassanger, IUpdateable, IPositionable, IRigidBody
  {
    public Vector2 TargetPosition;
    private float _baseTravelTime;
    private int _passangerID;
    private int _sourceCaveID;
    private float _startTime;
    private PassangerState _state;
    private float _takeTime;
    private int _targetCaveID;
    private float _totalFlightTime;
    private bool _wasSendToCab;

    public float BaseTravelTime
    {
      get => this._baseTravelTime;
      set => this._baseTravelTime = value;
    }

    public int PassangerID
    {
      get => this._passangerID;
      set => this._passangerID = value;
    }

    public int SourceCaveID
    {
      get => this._sourceCaveID;
      set => this._sourceCaveID = value;
    }

    public float StartTime
    {
      get => this._startTime;
      set => this._startTime = value;
    }

    public PassangerState State
    {
      get => this._state;
      set => this._state = value;
    }

    public float TakeTime
    {
      get => this._takeTime;
      set => this._takeTime = value;
    }

    public int TargetCaveID
    {
      get => this._targetCaveID;
      set => this._targetCaveID = value;
    }

    public float TotalFlightTime
    {
      get => this._totalFlightTime;
      set => this._totalFlightTime = value;
    }

    public bool WasSendToCab
    {
      get => this._wasSendToCab;
      set => this._wasSendToCab = value;
    }

    protected override void FellToWater() => this.TargetPosition.Y = this.Position.Y;

    public void CalculateWalking()
    {
      Vector2 vector2 = this.TargetPosition.Minus(this.Position);
      if ((double) vector2.Length() < 3.0)
      {
        this.Position = this.TargetPosition;
        this.Stop();
      }
      else
      {
        List<Vector2> dynamicForces = this.DynamicForces;
        if ((double) vector2.X < 0.0)
        {
          dynamicForces.Add(new Vector2(-10000.0, 0.0));
          this.State = PassangerState.WalkingLeft;
        }
        else
        {
          if ((double) vector2.X <= 0.0)
            return;
          dynamicForces.Add(new Vector2(10000.0, 0.0));
          this.State = PassangerState.WalkingRight;
        }
      }
    }

    public void Fall()
    {
      this.Stop();
      this.CheckForPlatformCollisions = false;
      this.State = PassangerState.Falling;
    }

    public void Go(float TargetX)
    {
      this.TargetPosition = new Vector2((double) TargetX, (double) this.Y);
    }

    public void Stop()
    {
      this.TargetPosition = new Vector2((double) this.X, (double) this.Y);
      this.Velocity.X = 0.0f;
      this.Acceleration.X = 0.0f;
      this.DynamicForces.Clear();
    }

    public override void Update(float elapsedtime_ms, float totaltime_ms)
    {
      base.Update(elapsedtime_ms, totaltime_ms);
      if (!this.IsOnGroundOrWater)
        return;
      this.State = PassangerState.Idle;
      this.CalculateWalking();
    }
  }
}
