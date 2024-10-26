// Steamworks.Games.Game.Core.Scenes.Game.HintScene

using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Input.Touch;
using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using System.Collections.Generic;


namespace Steamworks.Games.Game.Core.Scenes.Game
{
  public class HintScene : OverlayScene
  {
    protected IEntity TapClick;
    protected IEntity Hint;
    private Button ClickButton;
    private bool IsExiting;
    private bool _spaceWasUp;

    public HintScene(EngineBase engineBase)
      : base(engineBase)
    {
      this.TapClick = (IEntity) engineBase.SpriteFactory.GetText(
          "(Hit space / touch screen)",//"(Hit space or esc to exit)", 
          "smallfont");

      this.TapClick.Y = this.Context.ScreenHeight - 75f;
      this.TapClick.CenterX(this.Context.ScreenWidth);
      this.TapClick.X -= 240f;
      this.RootEntity.AttachChild(this.TapClick);

      this.Hint = (IEntity) engineBase.SpriteFactory.GetText(nameof (Hint), "bigfont");
      this.Hint.CenterX(this.Context.ScreenWidth);
      this.Hint.X -= 85f;
      this.Hint.Y = 0.0f;

      this.RootEntity.AttachChild(this.Hint);
    }

    public override void Update(float elapsedtime_s, float totaltime_s)
    {
        List<Vector2> state = this.Context.TouchSource.GetState(true, true, true);

        if (state.Count > 0)
        {
            //if
            /* ((double) state[0].X == -100.0  && (double) state[0].Y == -100.0
            || (double) state[0].X == -101.0 && (double) state[0].Y == -101.0)*/
            //((double)state[0].X > -100.0 && (double)state[0].X < 10000.0
            //|| (double)state[0].Y > -100.0 && (double)state[0].Y < 10000.0)
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
    }
  }
}
