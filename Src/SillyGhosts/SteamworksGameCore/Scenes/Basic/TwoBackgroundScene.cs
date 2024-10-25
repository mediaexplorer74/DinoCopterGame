// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Scenes.Basic.TwoBackgroundScene
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Sprites;

#nullable disable
namespace Steamworks.Games.Game.Core.Scenes.Basic
{
  public abstract class TwoBackgroundScene : Scene
  {
    public IEntity Background_Close;
    public IEntity Background_Far;

    public TwoBackgroundScene(EngineBase Context)
      : base(Context)
    {
      this.Background_Far = Context.SpriteFactory.Get("background_far");
      this.Background_Far.X = -100f;
      this.Background_Far.Y = -20f;
      this.Background_Close = (IEntity) new BackgroundNearMenu(Context, Context.ScreenWidth, Context.ScreenHeight);
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
      SpriteBatch.BeginAlpha();
      this.Background_Far.Draw(SpriteBatch);
      this.Background_Close.Draw(SpriteBatch);
      SpriteBatch.End();
    }

    public override void Update(float elapsedtime_s, float totaltime_s)
    {
      base.Update(elapsedtime_s, totaltime_s);
      this.Background_Close.Update(elapsedtime_s, totaltime_s);
    }
  }
}
