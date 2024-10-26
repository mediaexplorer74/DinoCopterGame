// Steamworks.Games.Game.Core.Scenes.Game.WaterGhostsHintOverlayScene

using Steamworks.Engine;
using Steamworks.Engine.Graphics;


namespace Steamworks.Games.Game.Core.Scenes.Game
{
  public class WaterGhostsHintOverlayScene : HintScene
  {
    private IEntity TapClick;
    private IDrawableText Hint;

    public WaterGhostsHintOverlayScene(EngineBase context)
      : base(context)
    {
      this.TapClick = context.SpriteFactory.Get("ingametut3");
      this.TapClick.CenterX(this.Context.ScreenWidth);
      this.TapClick.Y = 170f;
      this.RootEntity.AttachChild(this.TapClick);
      this.Hint = context.SpriteFactory.GetText("Land in the water!", "mediumfont");
      this.Hint.CenterX(this.Context.ScreenWidth);
      this.Hint.X -= 190f;
      this.Hint.Y = 290f;
      this.RootEntity.AttachChild((IEntity) this.Hint);
    }
  }
}
