// Steamworks.Games.Game.Core.Scenes.Game.PauseOverlay

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;


namespace Steamworks.Games.Game.Core.Scenes.Game
{
  public class PauseOverlay : OverlayScene
  {
    public PauseOverlay(EngineBase Context)
      : base(Context)
    {
      DebugLog.Alert("Game pause scene");
      IDrawableText text = Context.SpriteFactory.GetText("Paused", "bigfont");
      text.X = 280f;
      text.Y = 20f;
      this.RootEntity.AttachChild((IEntity) text);
      Button button1 = Context.SpriteFactory.GetButton("resume");
      button1.CenterX(Context.ScreenWidth);
      button1.Y = 170f;
      this.AddButton(button1);
      Button button2 = Context.SpriteFactory.GetButton("restart");
      button2.CenterX(Context.ScreenWidth);
      button2.Y = (float) byte.MaxValue;
      this.AddButton(button2);
      Button button3 = Context.SpriteFactory.GetButton("exit");
      button3.CenterX(Context.ScreenWidth);
      button3.Y = 340f;
      this.AddButton(button3);
    }

    public override void Button_Clicked(Button sender)
    {
      DebugLog.Alert("Button clicked: " + sender.Tag?.ToString());
      if ((string) sender.Tag == "resume")
        this.FadeOut(0.2f);
      if ((string) sender.Tag == "restart")
        this.Context.SceneManager.ResetGameScene();
      if (!((string) sender.Tag == "exit"))
        return;
      this.Context.SceneManager.SwitchScene("LevelsScene");
    }

    public override void Draw(ISpriteBatch SpriteBatch) => this.RootEntity.Draw(SpriteBatch);
  }
}
