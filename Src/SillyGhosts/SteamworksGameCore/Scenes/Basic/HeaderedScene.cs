// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Scenes.Basic.HeaderedScene
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Graphics;


namespace Steamworks.Games.Game.Core.Scenes.Basic
{
  public abstract class HeaderedScene : TwoBackgroundScene
  {
    public IEntity MainHeader;

    public HeaderedScene(EngineBase Context)
      : base(Context)
    {
      this.MainHeader = Context.SpriteFactory.Get("main_header");
      this.MainHeader.X = 144f;
      this.MainHeader.Y = -10f;
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
      base.Draw(SpriteBatch);
      SpriteBatch.BeginAlpha();
      this.MainHeader.Draw(SpriteBatch);
      SpriteBatch.End();
      this.DrawButtons(SpriteBatch);
    }
  }
}
