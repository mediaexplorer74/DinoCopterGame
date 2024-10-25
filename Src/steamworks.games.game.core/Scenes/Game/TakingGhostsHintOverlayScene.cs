// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Scenes.Game.TakingGhostsHintOverlayScene
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Graphics;

#nullable disable
namespace Steamworks.Games.Game.Core.Scenes.Game
{
  public class TakingGhostsHintOverlayScene : HintScene
  {
    private IEntity _tutGraphic;
    private IDrawableText _tutText;

    public TakingGhostsHintOverlayScene(EngineBase context)
      : base(context)
    {
      this._tutGraphic = context.SpriteFactory.Get("ingametut1");
      this._tutGraphic.CenterX(this.Context.ScreenWidth);
      this._tutGraphic.Y = 150f;
      this.RootEntity.AttachChild(this._tutGraphic);
      this._tutText = context.SpriteFactory.GetText("Land next to the ghost!", "mediumfont");
      this._tutText.CenterX(this.Context.ScreenWidth);
      this._tutText.X -= 240f;
      this._tutText.Y = 310f;
      this.RootEntity.AttachChild((IEntity) this._tutText);
    }
  }
}
