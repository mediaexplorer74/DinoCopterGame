// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Interfaces.IDirectionPointer
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine.Common;


namespace Steamworks.Games.Game.Core.Interfaces
{
  public interface IDirectionPointer : IUpdateable, IVisibleObject, IPositionable
  {
    float SourceX { get; set; }

    float SourceY { get; set; }

    float TargetX { get; set; }

    float TargetY { get; set; }

    float DistanceFromSource { get; set; }
  }
}
