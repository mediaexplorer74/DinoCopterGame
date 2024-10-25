// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Scenes.Menu.DifficultyMenuScene
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Logic;
using Steamworks.Games.Game.Core.Scenes.Basic;

#nullable disable
namespace Steamworks.Games.Game.Core.Scenes.Menu
{
  public class DifficultyMenuScene : HeaderedScene
  {
    public Button Button_Easy;
    public Button Button_Normal;
    public Button Button_Hard;
    public Button Button_Back;
    private bool IsExiting;

    public DifficultyMenuScene(EngineBase Context)
      : base(Context)
    {
      this.CreateDifficultyMenu();
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
      base.Draw(SpriteBatch);
      this.DrawButtons(SpriteBatch);
    }

    public override void Update(float elapsedtime_s, float totaltime_s)
    {
      base.Update(elapsedtime_s, totaltime_s);
    }

    public override void Button_Clicked(Button sender)
    {
      if (this.IsExiting)
        return;
      DebugLog.Alert("Button clicked: " + sender.Tag?.ToString());
      this.Context.ResourceManagers.CurrentSoundManager.PlaySound("click", true);
      if (sender == this.Button_Back)
        this.Context.SceneManager.Back();
      else if (sender == this.Button_Easy)
        this.Context.SceneManager.SwitchSceneWithParams("LevelsScene", new object[1]
        {
          (object) GameDifficulty.Easy
        });
      else if (sender == this.Button_Normal)
        this.Context.SceneManager.SwitchSceneWithParams("LevelsScene", new object[1]
        {
          (object) GameDifficulty.Normal
        });
      else if (sender == this.Button_Hard)
        this.Context.SceneManager.SwitchSceneWithParams("LevelsScene", new object[1]
        {
          (object) GameDifficulty.Hard
        });
      this.IsExiting = true;
    }

    private void CreateDifficultyMenu()
    {
      this.MainHeader = this.Context.SpriteFactory.Get("select_difficulty");
      this.MainHeader.X = 144f;
      this.MainHeader.Y = 30f;
      this.Button_Easy = this.Context.SpriteFactory.GetButton("easy");
      this.Button_Easy.CenterX(this.Context.ScreenWidth);
      this.Button_Easy.Y = 180f;
      this.Button_Normal = this.Context.SpriteFactory.GetButton("normal");
      this.Button_Normal.CenterX(this.Context.ScreenWidth);
      this.Button_Normal.Y = 260f;
      this.Button_Hard = this.Context.SpriteFactory.GetButton("hard");
      this.Button_Hard.CenterX(this.Context.ScreenWidth);
      this.Button_Hard.Y = 340f;
      this.AddButton(this.Button_Easy);
      this.AddButton(this.Button_Normal);
      this.AddButton(this.Button_Hard);
      this.Button_Back = this.Context.SpriteFactory.GetButton("back");
      this.Button_Back.Y = 470f;
      this.Button_Back.CenterX(this.Context.ScreenWidth);
      this.AddButton(this.Button_Back);
    }
  }
}
