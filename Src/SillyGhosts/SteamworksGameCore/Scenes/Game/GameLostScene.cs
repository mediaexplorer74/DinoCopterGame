// Steamworks.Games.Game.Core.Scenes.Game.GameLostScene

using Microsoft.Xna.Framework.Input;
using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;


namespace Steamworks.Games.Game.Core.Scenes.Game
{
  public class GameLostScene : OverlayScene
  {
    public GameLostScene(EngineBase Context)
      : base(Context)
    {
      DebugLog.Alert("Game lost scene");
      IDrawableText text1 = Context.SpriteFactory.GetText("Crashed!", "bigfont");
      text1.X = 240f;
      text1.Y = 20f;
      this.RootEntity.AttachChild((IEntity) text1);
      IDrawableText text2 = Context.SpriteFactory.GetText("Try not to hit so hard.", "smallfont");
      text2.X = 190f;
      text2.Y = 130f;
      this.RootEntity.AttachChild((IEntity) text2);
      Button button1 = Context.SpriteFactory.GetButton("replay");
      button1.CenterX(Context.ScreenWidth);
      button1.Y = 200f;
      this.AddButton(button1);
      Button button2 = Context.SpriteFactory.GetButton("exit");
      button2.CenterX(Context.ScreenWidth);
      button2.Y = 320f;
      this.AddButton(button2);
    }

    public override void Button_Clicked(Button sender)
    {
      DebugLog.Alert("Button clicked: " + sender.Tag?.ToString());

      if ((string) sender.Tag == "replay")
        this.Context.SceneManager.ResetGameScene();

      if (!((string) sender.Tag == "exit"))
        return;

      this.Context.SceneManager.SwitchScene("LevelsScene");
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
        this.RootEntity.Draw(SpriteBatch);
    }

    public override void ProcessInput()
    {
      base.ProcessInput();
      
      if (!this.BetterKeyboard.WasPressed(Keys.Enter))
        return;

      this.Context.SceneManager.ResetGameScene();
    }
  }
}
