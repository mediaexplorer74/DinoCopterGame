// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Scenes.Game.GameWonScene
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Microsoft.Xna.Framework.Input;
using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;

#nullable disable
namespace Steamworks.Games.Game.Core.Scenes.Game
{
  public class GameWonScene : OverlayScene
  {
    public GameWonScene(EngineBase Context, int Score)
      : base(Context)
    {
      DebugLog.Alert("Game won scene");
      IDrawableText text1 = Context.SpriteFactory.GetText("Good job!", "bigfont");
      text1.X = 240f;
      text1.Y = 0.0f;
      this.RootEntity.AttachChild((IEntity) text1);
      IDrawableText text2 = Context.SpriteFactory.GetText("Your score:", "smallfont");
      text2.X = 300f;
      text2.Y = 110f;
      this.RootEntity.AttachChild((IEntity) text2);
      this.CreateProgressBar(Score);
      this.CreateMenu();
    }

    private void CreateProgressBar(int Score)
    {
      ProgressBar progressBar = this.Context.SpriteFactory.GetProgressBar("progressFront");
      progressBar.Min = 0.0f;
      progressBar.Max = 3f;
      progressBar.Value = (float) Score;
      IEntity child = this.Context.SpriteFactory.Get("progressBack");
      child.X = 340f;
      child.Y = 170f;
      child.AttachChild((IEntity) progressBar);
      this.RootEntity.AttachChild(child);
    }

    private void CreateMenu()
    {
      Button button1 = this.Context.SpriteFactory.GetButton("replay");
      button1.CenterX(this.Context.ScreenWidth);
      button1.Y = 220f;
      this.AddButton(button1);
      Button button2 = this.Context.SpriteFactory.GetButton("nextlevel");
      button2.CenterX(this.Context.ScreenWidth);
      button2.Y = 305f;
      this.AddButton(button2);
      Button button3 = this.Context.SpriteFactory.GetButton("exit");
      button3.CenterX(this.Context.ScreenWidth);
      button3.Y = 390f;
      this.AddButton(button3);
    }

    public override void Button_Clicked(Button sender)
    {
      DebugLog.Alert("Button clicked: " + sender.Tag?.ToString());
      if ((string) sender.Tag == "replay")
        this.Context.SceneManager.ResetGameScene();
      if ((string) sender.Tag == "nextlevel")
        this.Context.SceneManager.NextLevel();
      if (!((string) sender.Tag == "exit"))
        return;
      this.Context.SceneManager.SwitchScene("LevelsScene");
    }

    public override void Draw(ISpriteBatch SpriteBatch) => this.RootEntity.Draw(SpriteBatch);

    public override void ProcessInput()
    {
      base.ProcessInput();
      if (!this.BetterKeyboard.WasPressed(Keys.Enter))
        return;
      this.Context.SceneManager.NextLevel();
    }
  }
}
