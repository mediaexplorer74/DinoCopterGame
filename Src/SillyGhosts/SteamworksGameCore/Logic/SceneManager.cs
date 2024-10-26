// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.SceneManager
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Interfaces;
using System;


namespace Steamworks.Games.Game.Core.Logic
{
  public class SceneManager : ISceneManager
  {
    public GameDifficulty CurrentDifficulty;
    public Scene CurrentScene;
    public IDrawableText FadeInOutText;
    public GameProgress CurrentGameProgress;
    public ICaveCabDataLoader ManagerDataLoader;
    protected float FadeTime = 0.4f;
    private int CurrentLevelIndex;
    private object[] CurrentParameters;
    private string CurrentSceneName;
    private EngineBase Engine;
    private int levelCount;
    private CaveLevel nextLevel;
    public SceneCreator Creator;
    public IEntity FadeInOutRectangle;

    public SceneManager(EngineBase Context)
    {
      this.Engine = Context;
      this.FadeInOutRectangle = (IEntity) new DrawableRectangle(Context, Context.ScreenWidth * 2f, Context.ScreenWidth * 2f, new Color((int) byte.MaxValue, 0, 0, 0));
      this.FadeInOutRectangle.X = 400f;
      this.FadeInOutRectangle.Y = 240f;
      this.FadeInOutText = Context.SpriteFactory.GetText("", "smallfont");
      this.FadeInOutText.X = 20f;
      this.FadeInOutText.Y = 20f;
    }

    public bool Back()
    {
      if (this.CurrentScene.HandleBack())
        return true;
      if (this.CurrentSceneName == "LevelsScene")
      {
        this.SwitchScene("DifficultyMenuScene");
        return true;
      }
      if (this.CurrentSceneName == "GameScene")
      {
        this.SwitchSceneWithParams("LevelsScene", new object[1]
        {
          (object) this.CurrentDifficulty
        });
        return true;
      }
      if (this.CurrentSceneName == "DifficultyMenuScene" || this.CurrentSceneName == "TutorialScene" || this.CurrentSceneName == "CreditsScene")
      {
        this.SwitchScene("MainMenuScene");
        return true;
      }
      int num = this.CurrentSceneName == "MainMenuScene" ? 1 : 0;
      return false;
    }

    public void Draw()
    {
      if (this.CurrentScene == null)
        return;
      this.CurrentScene.Draw(this.Engine.SpriteBatch);
      this.Engine.SpriteBatch.BeginAlpha();
      this.FadeInOutRectangle.Draw(this.Engine.SpriteBatch);
      this.FadeInOutText.Draw(this.Engine.SpriteBatch);
      this.Engine.SpriteBatch.End();
    }

    public void NextLevel()
    {
      DebugLog.WriteLine(0, (object) ("LevelID " + this.CurrentLevelIndex.ToString() + " " + this.nextLevel.Name + " completed in " + this.Engine.Time.TotalTime_s.ToString()));
      ++this.CurrentLevelIndex;
      if (this.CurrentLevelIndex < this.levelCount)
        this.SwitchSceneWithParams("GameScene", new object[2]
        {
          (object) this.CurrentLevelIndex,
          (object) false
        });
      else
        this.SwitchSceneWithParams("LevelsScene", new object[1]
        {
          (object) this.CurrentDifficulty
        });
    }

    public void ResetGameScene()
    {
      this.FadeInOutText.Text = "";
      this.SwitchSceneWithParams("GameScene", new object[2]
      {
        (object) this.CurrentLevelIndex,
        (object) true
      });
    }

    public void SwitchScene(string Name) => this.SwitchSceneWithParams(Name, (object[]) null);

    public void SwitchSceneWithParams(string Name, object[] parameters)
    {
      this.CurrentSceneName = Name;
      this.CurrentParameters = parameters;
      if (this.CurrentScene == null || this.CurrentScene.State == SceneState.Finished)
      {
        switch (Name)
        {
          case "LevelsScene":
            if (parameters != null && parameters.Length != 0)
              this.CurrentDifficulty = parameters[0] as GameDifficulty;
            this.levelCount = this.ManagerDataLoader.GetLevelsCount();
            this.CurrentScene = this.Creator.CreateLevelsScene(this.levelCount, this.CurrentDifficulty);
            break;
          case "GameScene":
            this.CurrentDifficulty.LevelID = this.CurrentLevelIndex;
            this.CurrentScene = this.Creator.CreateGameScene(this.nextLevel, this.CurrentDifficulty);
            break;
          case "MainMenuScene":
            this.CurrentDifficulty = (GameDifficulty) null;
            this.CurrentScene = (Scene) this.Creator.CreateMainMenuScene();
            break;
          case "DifficultyMenuScene":
            this.CurrentDifficulty = (GameDifficulty) null;
            this.CurrentScene = (Scene) this.Creator.CreateDifficultyMenuScene();
            break;
          case "TutorialScene":
            this.CurrentDifficulty = (GameDifficulty) null;
            bool FromStart = false;
            if (parameters != null && parameters.Length != 0)
              FromStart = (bool) parameters[0];
            this.CurrentScene = this.Creator.CreateTutorialScene(FromStart);
            break;
          case "CreditsScene":
            this.CurrentDifficulty = (GameDifficulty) null;
            this.CurrentScene = this.Creator.CreateCreditsScene();
            break;
          default:
            throw new Exception("No scene for " + Name);
        }
      }
      else
      {
        if (Name == "GameScene")
        {
          this.CurrentLevelIndex = (int) parameters[0];
          this.LoadNextLevel(this.CurrentLevelIndex, (bool) parameters[1]);
          DebugLog.WriteLine(0, (object) ("LevelID " + this.CurrentLevelIndex.ToString() + " starting."));
        }
        else
          this.FadeInOutText.Text = "";
        this.CurrentScene.State = SceneState.FadeOut;
      }
    }

    public void SwitchToFirstScene(GameDifficulty difficulty)
    {
      this.FadeInOutText.Text = "";
      if (difficulty == null)
        this.SwitchSceneWithParams("MainMenuScene", (object[]) null);
      else
        this.SwitchSceneWithParams("LevelsScene", new object[1]
        {
          (object) difficulty
        });
    }

    public void Update(float ElapsedTime_s)
    {
      float num1 = 1f / 30f;
      int num2 = 1;
      float ElapsedTime_s1 = 0.0f;
      if ((double) ElapsedTime_s > (double) num1)
      {
        num2 = (int) Math.Floor((double) ElapsedTime_s / (double) num1);
        ElapsedTime_s1 = ElapsedTime_s - num1 * (float) num2;
        ElapsedTime_s = num1;
      }
      for (int index = 0; index < num2; ++index)
        this.UpdateStep(ElapsedTime_s);
      if ((double) ElapsedTime_s1 <= 0.0)
        return;
      this.UpdateStep(ElapsedTime_s1);
    }

    private void UpdateStep(float ElapsedTime_s)
    {
      if (this.CurrentScene == null)
        return;
      this.Engine.Time.UpdateTime(ElapsedTime_s);
      switch (this.CurrentScene.State)
      {
        case SceneState.Started:
          this.Engine.Time.ResetTime();
          this.FadeInOutRectangle.FadeOut(this.FadeTime);
          this.FadeInOutText.FadeOut(this.FadeTime);
          this.CurrentScene.State = SceneState.FadeIn;
          break;
        case SceneState.FadeIn:
          if (this.FadeInOutRectangle.AlphaFadeComplete)
          {
            DebugLog.Alert("Scene Loaded: " + this.CurrentSceneName);
            this.CurrentScene.State = SceneState.Active;
            this.CurrentScene.Loaded();
            this.LogMemoryUsage();
            break;
          }
          break;
        case SceneState.FadeOut:
          this.FadeInOutRectangle.FadeIn(this.FadeTime);
          this.FadeInOutText.FadeIn(this.FadeTime);
          this.CurrentScene.State = SceneState.FadingOut;
          break;
        case SceneState.Finished:
          this.SwitchSceneWithParams(this.CurrentSceneName, this.CurrentParameters);
          break;
        case SceneState.FadingOut:
          if (this.FadeInOutRectangle.AlphaFadeComplete)
          {
            this.CurrentScene.Unloaded();
            this.CurrentScene.State = SceneState.Finished;
            break;
          }
          break;
      }
      this.FadeInOutRectangle.Update(ElapsedTime_s, ElapsedTime_s);
      this.FadeInOutText.Update(ElapsedTime_s, this.Engine.Time.TotalTime_s);
      if (this.CurrentScene.State == SceneState.Started || this.CurrentScene.State == SceneState.Finished)
        return;
      this.CurrentScene.ProcessInput();
      this.CurrentScene.Update(ElapsedTime_s, this.Engine.Time.TotalTime_s);
    }

    private void LoadNextLevel(int CurrentLevelIndex, bool reset)
    {
      this.nextLevel = this.ManagerDataLoader.LoadLevel(CurrentLevelIndex, this.CurrentDifficulty);
      if (reset)
        return;
      this.FadeInOutText.Text = "Next level: " + this.nextLevel.Name;
    }

    public void Pause()
    {
      if (this.CurrentScene == null)
        return;
      this.CurrentScene.Pause();
    }

    private void LogMemoryUsage()
    {
    }
  }
}
