// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Sprites.BackgroundNearMenu
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;

#nullable disable
namespace Steamworks.Games.Game.Core.Sprites
{
  public class BackgroundNearMenu : BackgroundNear
  {
    public BackgroundNearMenu(EngineBase context, float width, float height)
      : base(context, width, height, 1f)
    {
      this._background_near.X = 0.0f;
    }
  }
}
