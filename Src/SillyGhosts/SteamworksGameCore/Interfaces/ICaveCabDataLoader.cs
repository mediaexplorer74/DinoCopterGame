// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Interfaces.ICaveCabDataLoader
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Games.Game.Core.Logic;


namespace Steamworks.Games.Game.Core.Interfaces
{
  public interface ICaveCabDataLoader : IDataLoader
  {
    int GetLevelsCount();

    CaveLevel LoadLevel(int levelNumber, GameDifficulty CurrentDifficulty);
  }
}
