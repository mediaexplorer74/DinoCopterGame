// Steamworks.Games.Game.Core.Scenes.Menu.LevelsScene

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Logic;
using Steamworks.Games.Game.Core.Scenes.Basic;
using System.Collections.Generic;


namespace Steamworks.Games.Game.Core.Scenes.Menu
{
  public class LevelsScene : TwoBackgroundScene
  {
    private const int ProgressBarXRelative = 18;
    private const int ProgressBarYRelative = 80;
    private const int NumberXRelative = 70;
    private const int NumberYRelative = 30;
    public List<Button> LevelsButtons = new List<Button>();
    public float ButtonSizeX;
    public float ButtonSizeY;
    public int ButtonsInRow;
    public int NextLevelIndex;
    public int LevelCount;
    public GameProgress _gameProgress;
    public int DifficultyID;
    private float LockIconXRelative = 90f;
    private float LockIconYRelative = 70f;
    private bool MarketShowed;
    private Button Button_Back;
    private bool IsExiting;
    private Button Button_Windows;

    public LevelsScene(
      EngineBase Context,
      GameProgress gameProgress,
      int DifficultyID,
      int LevelCount)
      : base(Context)
    {
      this.Context = Context;
      this._gameProgress = gameProgress;
      this.DifficultyID = DifficultyID;
      this.LevelCount = LevelCount;
      this.CreateButtons();

      if (!Context.IsTrial)
        return;

      this.CreateTrialContent();
    }

    private void CreateTrialContent()
    {
      IEntity child1 = this.Context.SpriteFactory.Get("trialshadow");
      child1.Y = 130f;
      this.RootEntity.AttachChild(child1);
      IEntity child2 = this.Context.SpriteFactory.Get("available");
      child2.Y = 170f;
      child2.X = 150f;
      this.RootEntity.AttachChild(child2);
      Button button = this.Context.SpriteFactory.GetButton("buynow");
      button.Y = 280f;
      button.X = 230f;
      this.AddButton(button);
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
      base.Draw(SpriteBatch);
      this.DrawButtons(SpriteBatch);
      SpriteBatch.BeginAlpha();
      this.RootEntity.Draw(SpriteBatch);
      SpriteBatch.End();
    }

    public override void Button_Clicked(Button sender)
    {
        if (this.IsExiting)
          return;
        DebugLog.Alert("Button clicked: " + sender.Tag?.ToString());

        if (sender == this.Button_Back)
        {
            this.Context.ResourceManagers.CurrentSoundManager.PlaySound("click", true);
            this.Context.SceneManager.Back();
            this.IsExiting = true;
        }
        else if (sender == this.Button_Windows)
        {
            this.Context.NavigateUrl(
                "http://windowsphone.com/s?appId=e7d5b108-5fde-4724-8373-6099c614083d");
        }
        else if (sender.Tag is string)
        {
            this.Context.ResourceManagers.CurrentSoundManager.PlaySound("click", true);
            int num = this.MarketShowed ? 1 : 0;
        }
        else
        {
            int tag = (int)sender.Tag;
            if (tag < 0 || this._gameProgress.IsLevelLocked(tag, this.DifficultyID))
                return;
            this.Context.ResourceManagers.CurrentSoundManager.PlaySound("click", true);
            this.NextLevelIndex = tag;
            this.Context.SceneManager.SwitchSceneWithParams("GameScene", new object[2]
            {
                (object) this.NextLevelIndex,
                (object) false
            });
            this.IsExiting = true;
        }
    }

    private bool IsAvailableIfTrial(int buttonIndex)
    {
        return !this.Context.IsTrial || buttonIndex <= 4;
    }

    public void CreateButtons()
    {
      this.ButtonsInRow = 5;
      this.ButtonSizeY = 115f;
      this.ButtonSizeX = 160f;
      for (int index = 0; index < this.LevelCount; ++index)
      {
        Button button = this.CreateButton(index);
        this.CreateButtonLevelNumber(index, button);
        if (this.IsAvailableIfTrial(index))
          this.CreateProgressOrLock(index, button);
        this.AddButton(button);
      }
      this.Button_Back = this.Context.SpriteFactory.GetButton("back");
      this.Button_Back.Y = 470f;
      this.Button_Back.CenterX(this.Context.ScreenWidth);
      this.AddButton(this.Button_Back);
    }

    private void CreateProgressOrLock(int levelNumber, Button button)
    {
      if (this._gameProgress.IsLevelLocked(levelNumber, this.DifficultyID))
      {
        IEntity lockIcon = this.CreateLockIcon();
        button.AttachChild(lockIcon);
      }
      else
      {
        IEntity progress = this.CreateProgress(levelNumber);
        button.AttachChild(progress);
      }
    }

    private void CreateButtonLevelNumber(int levelNumber, Button button)
    {
      IEntity buttonNumberText1 = this.CreateButtonNumberText(levelNumber, new Color((int) byte.MaxValue, 200, 200, 200));
      IEntity buttonNumberText2 = this.CreateButtonNumberText(levelNumber, new Color((int) byte.MaxValue, 88, 88, 88));
      IEntity buttonNumberText3 = this.CreateButtonNumberText(levelNumber, new Color((int) byte.MaxValue, 149, 149, 149));
      --buttonNumberText2.X;
      --buttonNumberText2.Y;
      ++buttonNumberText3.X;
      ++buttonNumberText3.Y;
      button.AttachChild(buttonNumberText2);
      button.AttachChild(buttonNumberText3);
      button.AttachChild(buttonNumberText1);
    }

    private IEntity CreateLockIcon()
    {
      IEntity lockIcon = this.Context.SpriteFactory.Get("lock");
      lockIcon.X = this.LockIconXRelative;
      lockIcon.Y = this.LockIconYRelative;
      return lockIcon;
    }

    private IEntity CreateButtonNumberText(int levelNumber, Color color)
    {
      IDrawableText text = this.Context.SpriteFactory.GetText((levelNumber + 1).ToString(), "mediumfont");
      text.TextColor = color;
      text.X = 70f;
      if (levelNumber + 1 > 9)
        text.X -= 11f;
      text.Y = 30f;
      return (IEntity) text;
    }

    private IEntity CreateProgress(int levelNumber)
    {
      ProgressBar progressBar = this.Context.SpriteFactory.GetProgressBar("progressFront");
      progressBar.Min = 0.0f;
      progressBar.Max = 3f;
      progressBar.Value = (float) this._gameProgress.GetLevelScore(levelNumber, this.DifficultyID);
      IEntity progress = this.Context.SpriteFactory.Get("progressBack");
      progress.X = 18f;
      progress.Y = 80f;
      progress.AttachChild((IEntity) progressBar);
      return progress;
    }

    private Button CreateButton(int levelNumber)
    {
      Button button = !this.IsAvailableIfTrial(levelNumber) ? this.Context.SpriteFactory.GetButton("level_trial") : this.Context.SpriteFactory.GetButton("level");
      button.Enabled = this.IsAvailableIfTrial(levelNumber);
      button.Tag = (object) levelNumber;
      button.X = this.GetX(levelNumber);
      button.Y = this.GetY(levelNumber);
      return button;
    }

    private float GetY(int levelNumber)
    {
      return (float) (levelNumber / this.ButtonsInRow) * this.ButtonSizeY;
    }

    private float GetX(int levelNumber)
    {
      return (float) (levelNumber % this.ButtonsInRow) * this.ButtonSizeX;
    }
  }
}
