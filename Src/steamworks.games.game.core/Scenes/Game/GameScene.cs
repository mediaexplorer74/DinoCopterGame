// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Scenes.Game.GameScene
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Controller;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Logic;
using Steamworks.Games.Game.Core.Sprites;
using Steamworks.Physics.Particles;
using System;
using System.Collections.Generic;

#nullable disable
namespace Steamworks.Games.Game.Core.Scenes.Game
{
  public class GameScene : Scene
  {
    public IEntity Background_Close;
    public IEntity Background_Far;
    public IEntity Bottom_Fog_Far;
    public IEntity Bottom_Fog_Near1;
    public IAnimatedEntity Cab;
    public ICamera2D Camera;
    public List<IEntity> CaveSignSprites;
    public CaveCabCore Core;
    public bool EndGameSceneActive;
    public bool InGameMenuActive;
    public IAnalogController Joystick;
    public ILayeredEntity Map;
    public ParticleSystem Particles;
    public IEntity PointerFull;
    public ProgressBar CurrentProgressBar;
    public IEntity ProgressBarBack;
    public IDrawableText TimerText;
    private GameProgress _gameProgress;
    private bool _isPointerVisible;
    private int _lastCaveSingId = -1;
    private CaveLevel _level;
    private string _ambientSoundLoop;
    private OverlayScene _overlay;
    private PassangerSprites _passangers;
    private float _uiFadeTime = 0.2f;
    public Button Button_Menu;
    private GameDifficulty _difficulty;
    public Button Button_Restart;
    public ToggleButton Button_Mute;
    public IEntity PointerEmpty;
    private bool _cabHadPassangerLastFrame;

    public GameScene(
      EngineBase context,
      CaveLevel level,
      GameProgress gameProgress,
      GameDifficulty difficulty)
      : base(context)
    {
      this._gameProgress = gameProgress;
      this._level = level;
      this._difficulty = difficulty;
      this.Core = new CaveCabCore();
      this.Core.Initialize(level, difficulty);
      this._passangers = new PassangerSprites(this.Context, this.Core);
      this.SetAmbientSound();
      this.PlayAmbientSound();
    }

    private bool IsGameWorldActive
    {
      get => !this.Context.Time.IsPaused && !this.Core.IsGameLost && !this.Core.IsGameWon;
    }

    public override void Button_Clicked(Button sender)
    {
      this.Context.ResourceManagers.CurrentSoundManager.PlaySound("click", true);
      if (sender == this.Button_Menu)
        this.Context.Pause();
      else if (sender == this.Button_Restart)
      {
        this.Context.SceneManager.ResetGameScene();
      }
      else
      {
        if (sender != this.Button_Mute)
          return;
        if (this.Button_Mute.IsToggled)
          this.Context.ResourceManagers.CurrentSoundManager.Unmute();
        else
          this.Context.ResourceManagers.CurrentSoundManager.Mute();
      }
    }

    public int GetFinalScore()
    {
      float num1 = 0.0f;
      int num2 = 0;
      foreach (float score in this.Core.Scores)
      {
        num1 += score;
        ++num2;
      }
      return (int) ((double) num1 / (double) num2);
    }

    public override bool HandleBack()
    {
      if (this.IsGameWorldActive)
        this.Pause();
      else if (this.InGameMenuActive)
        this._overlay.FadeOut(this._uiFadeTime);
      else if (this.EndGameSceneActive)
        return false;
      return true;
    }

    public override void Loaded() => DebugLog.BeginLevel(this._difficulty.LevelID);

    public override void Pause()
    {
      DebugLog.Alert(nameof (Pause));
      if (!this.IsGameWorldActive)
        return;
      this.Context.Time.Pause();
      this.ShowInGameOverlayScene((OverlayScene) new PauseOverlay(this.Context));
    }

    public override void ProcessInput()
    {
      if (!this.IsGameWorldActive)
      {
        if (this._overlay == null)
          return;
        this._overlay.ProcessInput();
      }
      else
        base.ProcessInput();
    }

    public override void Unloaded() => this.StopAmbientSound();

    public override void Update(float elapsedtime_s, float totaltime_s)
    {
      base.Update(elapsedtime_s, totaltime_s);
      this.TimerText.Text = totaltime_s.ToString();
      if (!this.Context.Time.IsPaused)
      {
        if (this.IsGameWorldActive)
          this.PassInputToCore();
        this.UpdateGameWorld(elapsedtime_s, totaltime_s);
        if (!this.EndGameSceneActive && !this.InGameMenuActive)
          this.WinOrLose();
      }
      if (this._overlay == null)
        return;
      this._overlay.Update(elapsedtime_s, totaltime_s);
      if (!this.Context.Time.IsPaused || !this._overlay.FadedOut)
        return;
      this._overlay = (OverlayScene) null;
      this.InGameMenuActive = false;
      this.Context.Time.Continue();
    }

    public void FirstUpdate() => this.UpdateGameWorld(0.0f, 0.0f);

    private void PassInputToCore() => this.Core.Input(this.Joystick.GetState());

    private void PlayAmbientSound()
    {
      if (string.IsNullOrEmpty(this._ambientSoundLoop))
        return;
      this.Context.ResourceManagers.CurrentSoundManager.PlayLoop(this._ambientSoundLoop);
    }

    private void PlayLoop(string soundName)
    {
      if (!this.IsGameWorldActive)
        return;
      this.Context.ResourceManagers.CurrentSoundManager.PlayLoop(soundName);
    }

    private void PlaySound(string soundName, bool OnceAtTime)
    {
      if (!this.IsGameWorldActive)
        return;
      this.Context.ResourceManagers.CurrentSoundManager.PlaySound(soundName, OnceAtTime);
    }

    private int SaveScore()
    {
      int finalScore = this.GetFinalScore();
      if (finalScore > this._gameProgress.GetLevelScore(this.Core.Difficulty.LevelID, (int) this.Core.Difficulty.Name))
        this._gameProgress.SetLevelScore(this.Core.Difficulty.LevelID, (int) this.Core.Difficulty.Name, finalScore);
      return finalScore;
    }

    private void SetAmbientSound()
    {
      if (this._level.Rain && !this._level.Ice)
        this._ambientSoundLoop = "rain";
      else if (this._level.Wind)
        this._ambientSoundLoop = "wind";
      else
        this._ambientSoundLoop = "night";
    }

    private void ShowLostScene()
    {
      this.ShowEndGameOverlayScene((OverlayScene) new GameLostScene(this.Context));
    }

    private void ShowOverlayScene(OverlayScene overlay)
    {
      this._overlay = overlay;
      this._overlay.FadeIn(this._uiFadeTime);
    }

    private void ShowEndGameOverlayScene(OverlayScene overlay)
    {
      this.ShowOverlayScene(overlay);
      this.EndGameSceneActive = true;
    }

    private void ShowInGameOverlayScene(OverlayScene overlay)
    {
      this.ShowOverlayScene(overlay);
      this.InGameMenuActive = true;
    }

    private void ShowWonScene(int Score)
    {
      this.ShowEndGameOverlayScene((OverlayScene) new GameWonScene(this.Context, Score));
      DebugLog.EndLevel(Score);
    }

    private void ShowWonSceneOrLevelScene()
    {
      int Score = this.SaveScore();
      if (this.Context.IsTrial && this.Core.Difficulty.LevelID == 4 || this.Core.Difficulty.LevelID == 19)
      {
        this.Context.SceneManager.SwitchScene("LevelsScene");
        DebugLog.EndGame();
      }
      else
        this.ShowWonScene(Score);
      this.EndGameSceneActive = true;
    }

    private void StopAmbientSound()
    {
      if (string.IsNullOrEmpty(this._ambientSoundLoop))
        return;
      this.Context.ResourceManagers.CurrentSoundManager.Stop(this._ambientSoundLoop);
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
      this.DrawBackground(SpriteBatch);
      this.DrawFogFar(SpriteBatch);
      this.DrawMap(SpriteBatch);
      SpriteBatch.BeginAlphaCam(this.Camera, 1f, 1f);
      this._passangers.Draw(SpriteBatch);
      this.DrawCab(SpriteBatch);
      this.DrawFogNear(SpriteBatch);
      this.DrawPointer(SpriteBatch);
      SpriteBatch.End();
      this.DrawUI(SpriteBatch);
      if (this._overlay == null)
        return;
      SpriteBatch.BeginAlpha();
      this._overlay.Draw(SpriteBatch);
      SpriteBatch.End();
    }

    private void DrawBackground(ISpriteBatch SpriteBatch)
    {
      SpriteBatch.BeginAlphaCam(this.Camera, 0.1f, 0.1f);
      this.Background_Far.Draw(SpriteBatch);
      SpriteBatch.End();
      SpriteBatch.BeginAlphaCam(this.Camera, 0.3f, 0.3f);
      this.Background_Close.Draw(SpriteBatch);
      SpriteBatch.End();
    }

    private void DrawCab(ISpriteBatch SpriteBatch) => this.Cab.Draw(SpriteBatch);

    private void DrawFogFar(ISpriteBatch SpriteBatch)
    {
      SpriteBatch.BeginAlphaCam(this.Camera, 1f, 1f);
      this.Bottom_Fog_Far.Draw(SpriteBatch);
      SpriteBatch.End();
    }

    private void DrawFogNear(ISpriteBatch SpriteBatch) => this.Bottom_Fog_Near1.Draw(SpriteBatch);

    private void DrawMap(ISpriteBatch SpriteBatch)
    {
      foreach (ILayerEntity layerEntity in this.Map.GetLayerEntities())
      {
        SpriteBatch.BeginAlphaCam(this.Camera, layerEntity.Parallax, layerEntity.Parallax);
        layerEntity.Draw(SpriteBatch);
        SpriteBatch.End();
      }
    }

    private void DrawPointer(ISpriteBatch SpriteBatch)
    {
      this.PointerFull.Draw(SpriteBatch);
      this.PointerEmpty.Draw(SpriteBatch);
    }

    private void DrawUI(ISpriteBatch SpriteBatch)
    {
      SpriteBatch.BeginAlpha();
      if (this.Particles != null)
        this.Particles.Draw(SpriteBatch);
      if (this._lastCaveSingId != -1)
        this.CaveSignSprites[this._lastCaveSingId].Draw(SpriteBatch);
      this.ProgressBarBack.Draw(SpriteBatch);
      this.CurrentProgressBar.Draw(SpriteBatch);
      this.RootEntity.Draw(SpriteBatch);
      SpriteBatch.End();
    }

    private void StopSound(string soundName)
    {
      this.Context.ResourceManagers.CurrentSoundManager.Stop(soundName);
    }

    private void UpdateCabEntity(float elapsedtime_s, float totaltime_s)
    {
      this.Cab.X = this.Core.PlayerCab.X;
      this.Cab.Y = this.Core.PlayerCab.Y;
      if (this.Core.IsCabEngineRunning)
        this.Cab.PlayLoop("flying");
      else
        this.Cab.PlayLoop("idle");
      if (this.Core.PlayerCab.Bumped)
        this.PlaySound("hit", true);
      this.Cab.Update(elapsedtime_s, totaltime_s);
    }

    private void UpdateCamera()
    {
      this.Camera.LookAt(this.Cab.X + this.Cab.Center.X / 2f, this.Cab.Y + this.Cab.Center.Y / 2f);
    }

    private void UpdateCaveSprites(float elapsedtime_s, float totaltime_s)
    {
      if (this._lastCaveSingId != -1)
        this.CaveSignSprites[this._lastCaveSingId].Update(elapsedtime_s, totaltime_s);
      if (this.Core.PlayerCab.HasPassanger)
      {
        if (this._lastCaveSingId != -1)
          return;
        this._lastCaveSingId = this.Core.PlayerCab.Passanger.TargetCaveID;
        if ((double) this.CaveSignSprites[this._lastCaveSingId].Alpha != 0.0)
          return;
        this.CaveSignSprites[this._lastCaveSingId].FadeIn(this._uiFadeTime);
      }
      else
      {
        if (this._lastCaveSingId == -1)
          return;
        if ((double) this.CaveSignSprites[this._lastCaveSingId].Alpha == 1.0)
          this.CaveSignSprites[this._lastCaveSingId].FadeOut(this._uiFadeTime);
        if (!this.CaveSignSprites[this._lastCaveSingId].AlphaFadeComplete)
          return;
        this._lastCaveSingId = -1;
      }
    }

    private void UpdateGameWorld(float elapsedtime_s, float totaltime_s)
    {
      this.Core.Update(elapsedtime_s, totaltime_s);
      if (this.Core.PassangerFellInWater)
      {
        this.PlaySound("splashsmall", true);
        if (!this._gameProgress.WasHintShowed(Hint.WaterGhost))
          this.ShowHint(Hint.WaterGhost);
      }
      if (this.Core.PlayerCabFellInWater)
        this.PlaySound("splashbig", true);
      if (this.Core.PassangerKnocked)
        this.PlaySound("yell", true);
      if (this.Core.PlayerCab.IsEngineRunning)
        this.PlayLoop("flap");
      else
        this.StopSound("flap");
      this.Background_Close.Update(elapsedtime_s, totaltime_s);
      this.UpdateProgressBar(elapsedtime_s, totaltime_s);
      this.UpdateCaveSprites(elapsedtime_s, totaltime_s);
      this._passangers.Update(elapsedtime_s, totaltime_s);
      if (this._passangers.HasGhostAppeared || this._passangers.HasGhostDisappeared)
        this.PlaySound("ghost", false);
      if (this._passangers.HasGhostAppeared && !this._gameProgress.WasHintShowed(Hint.TakingGhosts))
        this.ShowHint(Hint.TakingGhosts);
      if (this._passangers.HasGhostDisappeared && !this._gameProgress.WasHintShowed(Hint.LeavingGhosts))
        this.ShowHint(Hint.LeavingGhosts);
      this.UpdateCabEntity(elapsedtime_s, totaltime_s);
      this.UpdatePointerEntity(elapsedtime_s, totaltime_s);
      this.UpdateCamera();
      this.UpdateSprites(elapsedtime_s, totaltime_s);
      this.UpdateWaves(elapsedtime_s, totaltime_s);
      if (this.Particles == null)
        return;
      this.Particles.AddParticles(elapsedtime_s);
      this.Particles.Update(elapsedtime_s, totaltime_s);
    }

    private void ShowHint(Hint hint)
    {
      this.Context.Time.Pause();
      switch (hint)
      {
        case Hint.TakingGhosts:
          this.ShowInGameOverlayScene((OverlayScene) new TakingGhostsHintOverlayScene(this.Context));
          this._gameProgress.HintShowed(Hint.TakingGhosts);
          break;
        case Hint.LeavingGhosts:
          this.ShowInGameOverlayScene((OverlayScene) new LeavingGhostsHintOverlayScene(this.Context));
          this._gameProgress.HintShowed(Hint.LeavingGhosts);
          break;
        case Hint.WaterGhost:
          this.ShowInGameOverlayScene((OverlayScene) new WaterGhostsHintOverlayScene(this.Context));
          this._gameProgress.HintShowed(Hint.WaterGhost);
          break;
        default:
          this.ShowInGameOverlayScene((OverlayScene) new HintScene(this.Context));
          break;
      }
    }

    private void UpdatePointerEntity(float elapsedtime_s, float totaltime_s)
    {
      if (this.Core.PlayerCab.HasPassanger)
        this.UpdatePointerFromCore(this.PointerFull, elapsedtime_s, totaltime_s);
      else
        this.UpdatePointerFromCore(this.PointerEmpty, elapsedtime_s, totaltime_s);
      if (this.Core.Pointer.IsVisible)
      {
        if (!this.Core.PlayerCab.HasPassanger && this._cabHadPassangerLastFrame)
        {
          if ((double) this.PointerFull.Alpha > 0.0)
            this.PointerFull.FadeOut(this._uiFadeTime);
          this.PointerEmpty.FadeIn(this._uiFadeTime);
        }
        else if (this.Core.PlayerCab.HasPassanger && !this._cabHadPassangerLastFrame)
        {
          if ((double) this.PointerEmpty.Alpha > 0.0)
            this.PointerEmpty.FadeOut(this._uiFadeTime);
          this.PointerFull.FadeIn(this._uiFadeTime);
        }
      }
      this.PointerEmpty.Update(elapsedtime_s, totaltime_s);
      this.PointerFull.Update(elapsedtime_s, totaltime_s);
      if (this._isPointerVisible != this.Core.Pointer.IsVisible)
      {
        if (this.Core.Pointer.IsVisible)
        {
          if (this.Core.PlayerCab.HasPassanger)
            this.PointerFull.FadeIn(this._uiFadeTime);
          else
            this.PointerEmpty.FadeIn(this._uiFadeTime);
        }
        else
        {
          if ((double) this.PointerFull.Alpha > 0.0)
            this.PointerFull.FadeOut(this._uiFadeTime);
          if ((double) this.PointerEmpty.Alpha > 0.0)
            this.PointerEmpty.FadeOut(this._uiFadeTime);
        }
        this._isPointerVisible = this.Core.Pointer.IsVisible;
      }
      this._cabHadPassangerLastFrame = this.Core.PlayerCab.HasPassanger;
    }

    private void UpdatePointerFromCore(IEntity Pointer, float elapsedtime_s, float totaltime_s)
    {
      Pointer.X = this.Core.Pointer.X - this.PointerFull.Width / 2f;
      Pointer.Y = this.Core.Pointer.Y - this.PointerFull.Height / 2f;
      Pointer.Rotation = this.Core.Pointer.Rotation;
    }

    private void UpdateProgressBar(float elapsedtime_s, float totaltime_s)
    {
      this.CurrentProgressBar.Update(elapsedtime_s, totaltime_s);
      this.ProgressBarBack.Update(elapsedtime_s, totaltime_s);
      if (this.Core.PlayerCab.HasPassanger)
      {
        if ((double) this.CurrentProgressBar.Alpha == 0.0)
        {
          this.CurrentProgressBar.FadeIn(this._uiFadeTime);
          this.ProgressBarBack.FadeIn(this._uiFadeTime);
          this.CurrentProgressBar.Min = this.Core.ProgressMin;
          this.CurrentProgressBar.Max = this.Core.ProgressMax;
        }
        this.CurrentProgressBar.Value = totaltime_s;
      }
      else
      {
        if ((double) this.CurrentProgressBar.Alpha != 1.0)
          return;
        this.CurrentProgressBar.FadeOut(this._uiFadeTime);
        this.ProgressBarBack.FadeOut(this._uiFadeTime);
      }
    }

    private void UpdateSprites(float elapsedtime_s, float totaltime_s)
    {
      this.Cab.Update(elapsedtime_s, totaltime_s);
    }

    private void UpdateWaves(float elapsedtime_s, float totaltime_s)
    {
      float num = (float) ((int) (Math.Sin((double) totaltime_s % (2.0 * Math.PI)) * 10.0 * 1000.0) / 1000);
      this.Bottom_Fog_Far.X = num;
      this.Bottom_Fog_Near1.X = -num;
    }

    private void WinOrLose()
    {
      if (this.Core.IsGameLost)
      {
        this.ShowLostScene();
        if (this.Core.PlayerCab.IsCrashed)
          this.Context.ResourceManagers.CurrentSoundManager.PlaySound("crash", true);
        DebugLog.Alert("Crash");
      }
      else
      {
        if (!this.Core.IsGameWon || this._level.TestOnly)
          return;
        this.ShowWonSceneOrLevelScene();
      }
    }
  }
}
