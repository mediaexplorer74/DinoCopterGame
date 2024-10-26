// Steamworks.Games.Game.Core.Scenes.Game.LeavingGhostsHintOverlayScene

using Steamworks.Engine;
using Steamworks.Engine.Graphics;



namespace Steamworks.Games.Game.Core.Scenes.Game
{
  public class LeavingGhostsHintOverlayScene : HintScene
  {
    protected IEntity TapClick;
    protected IDrawableText Hint;
    //private Button ClickButton;
    //private bool IsExiting;
    //private bool _spaceWasUp;

    public LeavingGhostsHintOverlayScene(EngineBase engineBase)
      : base(engineBase)
    {
  
      this.TapClick = engineBase.SpriteFactory.Get("ingametut2");
      this.TapClick.CenterX(this.Context.ScreenWidth);
      this.TapClick.Y = 150f;
      this.RootEntity.AttachChild(this.TapClick);

      this.Hint = engineBase.SpriteFactory.GetText("Land next to a gate!", "mediumfont");
      this.Hint.CenterX(this.Context.ScreenWidth);
      this.Hint.X -= 205f;
      this.Hint.Y = 300f;

      this.RootEntity.AttachChild((IEntity) this.Hint);

    }//

        /*
    public override void Update(float elapsedtime_s, float totaltime_s)
    {
        List<Vector2> state = this.Context.TouchSource.GetState(true, true, true);
   
        if (state.Count > 0)
        {
            {
                this.Exit();
            }
        }

        if
        (
            this.BetterKeyboard.WasPressed(Keys.Space)
        )
        {
            this.Exit();
        }

        base.Update(elapsedtime_s, totaltime_s);
    }

    private void Exit()
    {
        if (this.IsExiting)
            return;

        this.IsExiting = true;
        this.FadeOut(0.2f);
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
        this.RootEntity.Draw(SpriteBatch);
    }

    public override void Button_Clicked(Button sender)
    {
        if (sender != this.ClickButton)
            return;
        this.Exit();
    }*/
  }
}
