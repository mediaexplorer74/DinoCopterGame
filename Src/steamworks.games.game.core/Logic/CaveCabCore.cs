// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.CaveCabCore
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine.Common;
using Steamworks.Games.Game.Core.Interfaces;
using Steamworks.Physics.Collisions;
using System;
using System.Collections.Generic;

#nullable disable
namespace Steamworks.Games.Game.Core.Logic
{
  public class CaveCabCore
  {
    public int _debug_firstCave;
    public float _debug_firstCaveTime;
    public List<Cave> Caves = new List<Cave>();
    public GameDifficulty Difficulty;
    public bool IsGameWon;
    public List<IPassanger> LeftPassangers;
    public CaveLevel Level;
    public bool PassangerFellInWater;
    public bool PassangerKnocked;
    public Cab PlayerCab;
    public bool PlayerCabFellInWater;
    public IDirectionPointer Pointer;
    public float ProgressMax;
    public float ProgressMin;
    public float ProgressValue;
    public List<float> Scores = new List<float>();
    public List<IPassanger> WaitingPassangers;
    private bool _gameLost;
    private bool _levelFinished;
    private string _lostMessage;
    private IDirectionPointer _pointer;

    public CaveCabCore()
    {
      this.WaitingPassangers = new List<IPassanger>();
      this.LeftPassangers = new List<IPassanger>();
    }

    public string GameLostMessage
    {
      get => this._lostMessage;
      set
      {
        this._lostMessage = value;
        this._gameLost = true;
      }
    }

    public bool IsCabEngineRunning => this.PlayerCab != null && this.PlayerCab.IsEngineRunning;

    public bool IsGameLost => this._gameLost;

    public void Initialize(CaveLevel level, GameDifficulty Difficulty)
    {
      this.Difficulty = Difficulty;
      this.Level = level;
      this.PlayerCab = new Cab();
      this.PlayerCab.Initialize(level, Difficulty);
      this.Caves = level.Caves;
      this.Pointer = (IDirectionPointer) new DirectionPointer();
      this.Pointer.DistanceFromSource = 50f;
    }

    public void Input(Vector2 vector2) => this.PlayerCab.SetEngineDirection(vector2);

    public void Update(float elapsedtime_s, float totaltime_s)
    {
      this.PassangerFellInWater = false;
      this.PlayerCabFellInWater = false;
      this.PassangerKnocked = false;
      if (this.PlayerCab.IsCrashed)
        this.GameLostMessage = "Don't hit so hard!";
      if (this.PlayerCab.FellInWater)
        this.PlayerCabFellInWater = true;
      IPassanger passanger = this.PlayerCab.LeavePassanger();
      if (passanger != null)
      {
        this.LeftPassangers.Add(passanger);
        float num = totaltime_s - passanger.TakeTime;
        passanger.TotalFlightTime = num;
      }
      this.SendLeftPassangersToCaves();
      this.ProducePassangers(totaltime_s);
      this.CheckIfLevelFinished();
      if (this.PlayerCab.IsOnGroundOrWater && !this.PlayerCab.HasPassanger)
        this.SendWaitingPassangersToCab(totaltime_s);
      else
        this.StopWaitingPassangers();
      this.SetPlayerCabCurrentCaveID(totaltime_s);
      this.PlayerCab.Update(elapsedtime_s, totaltime_s);
      IPassanger andRemoveFromList = this.PlayerCab.CheckAndTakeAndRemoveFromList(this.WaitingPassangers);
      if (andRemoveFromList != null)
      {
        float num = totaltime_s - andRemoveFromList.StartTime;
        DebugLog.WriteLine(2, (object) ("Passanger " + andRemoveFromList.PassangerID.ToString() + " waited for " + num.ToString()));
        andRemoveFromList.TakeTime = totaltime_s;
        this.SetUpProgressBar(andRemoveFromList);
      }
      this.UpdateWaitingPassangers(elapsedtime_s, totaltime_s);
      this.UpdateLeftPassangers(elapsedtime_s, totaltime_s);
      this.UpdatePointer(elapsedtime_s, totaltime_s);
    }

    private void CheckCollisionAndFall(IPassanger pass)
    {
      if (!this.HasCollisionWithCab(pass) || pass.IsInWater)
        return;
      this.PassangerKnocked = true;
      pass.Fall();
    }

    private void CheckIfLevelFinished()
    {
      if (this.LeftPassangers.Count != 0 || this.WaitingPassangers.Count != 0 || !this.NoMorePasangersInCaves() || this.PlayerCab.HasPassanger)
        return;
      this.IsGameWon = true;
    }

    private float GetScore(IPassanger passToRemove)
    {
      return (double) passToRemove.TotalFlightTime <= (double) this.Difficulty.GetTravelTime3(passToRemove.BaseTravelTime) ? (float) Math.Ceiling((1.0 - (double) passToRemove.TotalFlightTime / (double) this.Difficulty.GetTravelTime3(passToRemove.BaseTravelTime)) * 3.0) : 0.0f;
    }

    private Cave GetTargetCave(int TargetCaveID)
    {
      Cave targetCave = (Cave) null;
      foreach (Cave cave in this.Level.Caves)
      {
        if (cave.CaveID == TargetCaveID)
          targetCave = cave;
      }
      return targetCave;
    }

    private float GetTargetTravelTime(float baseTravelTime) => baseTravelTime * 3f;

    private bool HasCollisionWithCab(IPassanger pass)
    {
      return pass.Bounds.GetCollisionForBounds(this.PlayerCab.Bounds).Direction != CollisionDirection.None && !this.PlayerCab.IsOnGroundOrWater && pass.IsOnGroundOrWater;
    }

    private bool NoMorePasangersInCaves()
    {
      bool flag = true;
      foreach (Cave cave in this.Level.Caves)
      {
        if (cave.HasPassangers)
          flag = false;
      }
      return flag;
    }

    private void ProducePassangers(float totaltime_s)
    {
      foreach (Cave cave in this.Caves)
      {
        Passanger passanger = cave.ProducePassangerIfNeeded(totaltime_s);
        if (passanger != null)
        {
          passanger.CurrentCollisionDetector = (ICollisionDetector) new TMXMapCollisionDetector(this.Level.CaveMap);
          this.WaitingPassangers.Add((IPassanger) passanger);
          DebugLog.WriteLine(1, (object) ("Cave " + cave.CaveID.ToString() + " produced passanger at " + totaltime_s.ToString() + " s."));
        }
      }
    }

    private void SendLeftPassangersToCaves()
    {
      foreach (IPassanger leftPassanger in this.LeftPassangers)
      {
        Cave targetCave = this.GetTargetCave(leftPassanger.TargetCaveID);
        leftPassanger.Go(targetCave.X);
      }
    }

    private void SendWaitingPassangersToCab(float totaltime_s)
    {
      foreach (IPassanger waitingPassanger in this.WaitingPassangers)
      {
        if (waitingPassanger.SourceCaveID == this.PlayerCab.CurrentCaveID && this.PlayerCab.IsOnGroundOrWater && waitingPassanger.IsOnGroundOrWater)
        {
          waitingPassanger.Go(this.PlayerCab.X);
          waitingPassanger.WasSendToCab = true;
        }
      }
    }

    private void SetPlayerCabCurrentCaveID(float totaltime_s)
    {
      int num = -1;
      if (this.PlayerCab.IsInWater)
        num = -2;
      else if (this.PlayerCab.IsOnGroundOrWater)
      {
        foreach (Cave cave in this.Caves)
        {
          if ((double) cave.Y == Math.Round((double) this.PlayerCab.Y + (double) this.PlayerCab.Height))
          {
            num = cave.CaveID;
            break;
          }
        }
      }
      this.PlayerCab.CurrentCaveID = num;
    }

    private void SetSourceCave(IPassanger pass)
    {
      if (!pass.IsInWater)
        return;
      pass.SourceCaveID = -2;
    }

    private void SetUpProgressBar(IPassanger TakenPassanger)
    {
      this.ProgressMin = TakenPassanger.TakeTime;
      this.ProgressValue = TakenPassanger.TakeTime;
      this.ProgressMax = TakenPassanger.TakeTime + this.Difficulty.GetTravelTime3(TakenPassanger.BaseTravelTime);
    }

    private void StopWaitingPassangers()
    {
      foreach (IPassanger waitingPassanger in this.WaitingPassangers)
      {
        if (waitingPassanger.WasSendToCab)
        {
          waitingPassanger.WasSendToCab = false;
          waitingPassanger.Stop();
        }
      }
    }

    private void UpdateLeftPassangers(float timeelapsed_s, float totaltime_s)
    {
      IPassanger passToRemove = (IPassanger) null;
      foreach (IPassanger leftPassanger in this.LeftPassangers)
      {
        if (leftPassanger.FellInWater)
          this.PassangerFellInWater = true;
        leftPassanger.Update(timeelapsed_s, totaltime_s);
        Cave targetCave = this.GetTargetCave(leftPassanger.TargetCaveID);
        if ((double) leftPassanger.X == (double) targetCave.X)
          passToRemove = leftPassanger;
      }
      if (passToRemove == null)
        return;
      this.LeftPassangers.RemoveAt(this.LeftPassangers.IndexOf(passToRemove));
      this.Scores.Add(this.GetScore(passToRemove));
    }

    private void UpdatePointer(float timeelapsed_s, float totaltime_s)
    {
      this.Pointer.IsVisible = false;
      this.Pointer.SourceX = this.PlayerCab.X + this.PlayerCab.Width / 2f;
      this.Pointer.SourceY = this.PlayerCab.Y + this.PlayerCab.Height / 2f;
      bool flag = true;
      if (this.PlayerCab.HasPassanger)
      {
        Cave targetCave = this.GetTargetCave(this.PlayerCab.Passanger.TargetCaveID);
        this.Pointer.TargetX = targetCave.X + targetCave.PassangerHeight / 2f;
        this.Pointer.TargetY = targetCave.Y - targetCave.PassangerHeight / 2f;
      }
      else if (this.WaitingPassangers.Count > 0)
      {
        IPassanger waitingPassanger = this.WaitingPassangers[0];
        this.Pointer.TargetX = waitingPassanger.X + waitingPassanger.Width / 2f;
        this.Pointer.TargetY = waitingPassanger.Y + waitingPassanger.Height / 2f;
      }
      else
      {
        this.Pointer.IsVisible = false;
        flag = false;
      }
      this.Pointer.Update(timeelapsed_s, totaltime_s);
      if (!((double) new Vector2((double) this.Pointer.TargetX, (double) this.Pointer.TargetY).Minus(new Vector2((double) this.Pointer.SourceX, (double) this.Pointer.SourceY)).Length() > 100.0 & flag))
        return;
      this.Pointer.IsVisible = true;
    }

    private void UpdateWaitingPassangers(float timeelapsed_s, float totaltime_s)
    {
      foreach (IPassanger waitingPassanger in this.WaitingPassangers)
      {
        waitingPassanger.Update(timeelapsed_s, totaltime_s);
        this.CheckCollisionAndFall(waitingPassanger);
        this.SetSourceCave(waitingPassanger);
        double startTime = (double) waitingPassanger.StartTime;
        if (waitingPassanger.FellInWater)
          this.PassangerFellInWater = true;
      }
    }
  }
}
