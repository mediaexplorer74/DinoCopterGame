// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Interfaces.IPassanger
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine.Common;
using Steamworks.Games.Game.Core.Logic;
using Steamworks.Physics.Body;

#nullable disable
namespace Steamworks.Games.Game.Core.Interfaces
{
  public interface IPassanger : IUpdateable, IPositionable, IRigidBody
  {
    int TargetCaveID { get; set; }

    PassangerState State { get; set; }

    int SourceCaveID { get; set; }

    bool WasSendToCab { get; set; }

    void Go(float TargetX);

    void Stop();

    void Fall();

    float TakeTime { get; set; }

    float StartTime { get; set; }

    float BaseTravelTime { get; set; }

    int PassangerID { get; set; }

    float TotalFlightTime { get; set; }
  }
}
