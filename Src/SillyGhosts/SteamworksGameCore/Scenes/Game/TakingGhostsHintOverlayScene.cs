// Steamworks.Games.Game.Core.Scenes.Game.TakingGhostsHintOverlayScene

using Steamworks.Engine;
using Steamworks.Engine.Graphics;


namespace Steamworks.Games.Game.Core.Scenes.Game
{
  public class TakingGhostsHintOverlayScene : HintScene
  {
    private IEntity TapClick;
    private IDrawableText Hint;

    public TakingGhostsHintOverlayScene(EngineBase context)
      : base(context)
    {
      this.TapClick = context.SpriteFactory.Get("ingametut1");
      this.TapClick.CenterX(this.Context.ScreenWidth);
      this.TapClick.Y = 150f;
      this.RootEntity.AttachChild(this.TapClick);

      this.Hint = context.SpriteFactory.GetText("Land next to the ghost!", "mediumfont");
      this.Hint.CenterX(this.Context.ScreenWidth);
      this.Hint.X -= 240f;
      this.Hint.Y = 310f;
      this.RootEntity.AttachChild((IEntity) this.Hint);
    }
  }
}
