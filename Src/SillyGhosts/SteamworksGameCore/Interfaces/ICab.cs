// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Interfaces.ICab
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine.Common;
using Steamworks.Physics.Body;
using System.Collections.Generic;


namespace Steamworks.Games.Game.Core.Interfaces
{
  public interface ICab : IPositionable, IUpdateable, IEnginePoweredPlatformRigidBody, IRigidBody
  {
    IPassanger Passanger { get; set; }

    IPassanger CheckAndTakeAndRemoveFromList(List<IPassanger> WaitingPassangers);

    IPassanger LeavePassanger();

    void SetEngineDirection(Vector2 vector2);

    int CurrentCaveID { get; set; }

    bool HasPassanger { get; }
  }
}
