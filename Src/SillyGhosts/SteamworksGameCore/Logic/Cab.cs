// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.Cab
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine.Common;
using Steamworks.Games.Game.Core.Interfaces;
using Steamworks.Physics.Body;
using Steamworks.Physics.Collisions;
using System.Collections.Generic;


namespace Steamworks.Games.Game.Core.Logic
{
  public class Cab : 
    EnginePoweredPlatformRigidBody,
    ICab,
    IPositionable,
    IUpdateable,
    IEnginePoweredPlatformRigidBody,
    IRigidBody
  {
    private IPassanger _passanger;
    private int _currentCaveID;

    public IPassanger Passanger
    {
      get => this._passanger;
      set => this._passanger = value;
    }

    public int CurrentCaveID
    {
      get => this._currentCaveID;
      set => this._currentCaveID = value;
    }

    public bool HasPassanger => this.Passanger != null;

    public Cab() => this.CurrentCaveID = -1;

    public void Initialize(CaveLevel level, GameDifficulty Difficulty)
    {
      this.Position.X = (float) level.StartX;
      this.Position.Y = (float) level.StartY;
      this.CurrentCollisionDetector = (ICollisionDetector) new TMXMapCollisionDetector(level.CaveMap);
      this.Size = new Vector2(64.0, 64.0);
      switch (Difficulty.Name)
      {
        case GameDifficultyNames.Easy:
          this.MaximumBounceVelocity = 190f;
          this.AirDensity = 1f / 1000f;
          break;
        case GameDifficultyNames.Hard:
          this.MaximumBounceVelocity = 150f;
          this.AirDensity = 5E-05f;
          break;
        default:
          this.MaximumBounceVelocity = 160f;
          this.AirDensity = 0.0005f;
          break;
      }
      this.EnginePower = 70000f;
      this.Mass = 200f;
      this.Gravity = 50f;
      this.WaterLevel = (float) (level.CaveMap.height * level.CaveMap.tileheight - level.CaveMap.tileheight / 2 + 15);
      this.WaterDensity = 0.015f;
      this.WaterDragDensityDown = 0.015f;
      this.WaterDragDensityUp = 0.2f;
      this.IsOnGroundOrWater = false;
      this.VelocityDecreaseOnBump = this.VelocityDecreaseOnBumpPerpendicular = 0.7f;
      this.BoundingBox = new RectangleF(16f, 8f, 32f, 56f);
      if (level.Ice)
        this.VelocityDecreaseOnBumpPerpendicular = 0.01f;
      if (level.Wind)
        this.ConstantForces.Add(new Vector2(25000.0, 0.0));
      if (!level.Rain)
        return;
      this.ConstantForces.Add(new Vector2(0.0, 15000.0));
    }

    public IPassanger LeavePassanger()
    {
      if (!this.HasPassanger || !this.IsOnGroundOrWater || this.CurrentCaveID != this.Passanger.TargetCaveID)
        return (IPassanger) null;
      IPassanger passanger = this.Passanger;
      passanger.Y = this.Bounds.Bottom - passanger.Height;
      passanger.X = this.Bounds.Left;
      passanger.CheckForPlatformCollisions = true;
      this.Passanger = (IPassanger) null;
      return passanger;
    }

    private bool IsGoodToTake(IPassanger passanger)
    {
      return this.Bounds.GetCollisionForBounds(passanger.Bounds).Direction != CollisionDirection.None && this.CurrentCaveID == passanger.SourceCaveID && this.IsOnGroundOrWater && passanger.IsOnGroundOrWater && !this.HasPassanger;
    }

    private bool CheckAndTake(IPassanger passanger)
    {
      return this.IsGoodToTake(passanger) && this.TakePassanger(passanger);
    }

    public bool TakePassanger(IPassanger passanger)
    {
      this.Passanger = passanger;
      this.Passanger.Stop();
      return true;
    }

    public IPassanger CheckAndTakeAndRemoveFromList(List<IPassanger> Passangers)
    {
      int index1 = -1;
      IPassanger andRemoveFromList = (IPassanger) null;
      for (int index2 = 0; index2 < Passangers.Count; ++index2)
      {
        if (this.CheckAndTake(Passangers[index2]))
        {
          andRemoveFromList = Passangers[index2];
          index1 = index2;
          break;
        }
      }
      if (index1 > -1)
        Passangers.RemoveAt(index1);
      return andRemoveFromList;
    }

    public void SetEngineDirection(Vector2 vector2) => this.EngineDirection = vector2;
  }
}
