// Steamworks.Games.Game.Core.Scenes.Menu.MainMenuScene

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Logic;
using Steamworks.Games.Game.Core.Scenes.Basic;


namespace Steamworks.Games.Game.Core.Scenes.Menu
{
  public class MainMenuScene : HeaderedScene
  {
    public Button Button_Credits;
    public Button Button_How;
    public ToggleButton Button_Music;
    public ToggleButton Button_Sound;
    public Button Button_Start;
    public Button Button_Windows;
    public IDrawableText AvailableText;
    private bool IsExiting;
    private GameProgress _gameProgress;

    public MainMenuScene(EngineBase context, GameProgress gameProgress)
      : base(context)
    {
      this.CreateMenu();
      this.Button_Music.IsToggled = context.Settings.IsMusicEnabled();
      this.Button_Sound.IsToggled = context.Settings.IsSoundEnabled();
      this._gameProgress = gameProgress;
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
      base.Draw(SpriteBatch);
      this.DrawButtons(SpriteBatch);
      SpriteBatch.BeginAlpha();
      this.RootEntity.Draw(SpriteBatch);
      SpriteBatch.End();
    }

    // Button_Clicked
    public override void Button_Clicked(Button sender)
    {
        if (this.IsExiting)
          return;
        DebugLog.Alert("Button clicked: " + sender.Tag?.ToString());

        if (sender == this.Button_Start)
        {
            this.Context.SceneManager.SwitchScene("DifficultyMenuScene");
            this.IsExiting = true;
        }
        else if (sender == this.Button_How)
        {
            this.Context.SceneManager.SwitchScene("TutorialScene");
            this.IsExiting = true;
        }
        else if (sender == this.Button_Music)
        {
            DebugLog.Alert("Music " + this.Button_Music.IsToggled.ToString());
            if (this.Button_Music.IsToggled)
            {
                this.Context.ResourceManagers.CurrentSoundManager.MusicEnabled = true;
                this.Context.Settings.EnableMusic();
                this.Context.ResourceManagers.CurrentSoundManager.PlayMusic("music");
            }
            else
            {
                this.Context.ResourceManagers.CurrentSoundManager.StopMusic("music");
                this.Context.Settings.DisableMusic();
                this.Context.ResourceManagers.CurrentSoundManager.MusicEnabled = false;
            }
        }
        else if (sender == this.Button_Sound)
        {
            DebugLog.Alert("Sound " + this.Button_Music.IsToggled.ToString());

            this.Context.ResourceManagers.CurrentSoundManager.SoundEnabled
                        = this.Button_Sound.IsToggled;

            if (this.Button_Sound.IsToggled)
                this.Context.Settings.EnableSound();
            else
                this.Context.Settings.DisableSound();
        }
        else if (sender == this.Button_Credits)
        {
            this.Context.SceneManager.SwitchScene("CreditsScene");
            this.IsExiting = true;
        }
        else if (sender == this.Button_Windows)
        {
            this.Context.NavigateUrl(
                "http://windowsphone.com/s?appId=e7d5b108-5fde-4724-8373-6099c614083d");
        }

        this.Context.ResourceManagers.CurrentSoundManager.PlaySound("click", true);
    }//Button_Clicked


    // CreateMenu
    private void CreateMenu()
    {
      this.MainHeader.Y -= 20f;
      int num = 150;
      this.Button_Start = this.Context.SpriteFactory.GetButton("start");
      this.Button_Start.CenterX(this.Context.ScreenWidth);
      this.Button_Start.Y = (float) num;
      this.Button_How = this.Context.SpriteFactory.GetButton("how");
      this.Button_How.CenterX(this.Context.ScreenWidth);
      this.Button_How.Y = (float) (num + 75);
      this.Button_Music = this.Context.SpriteFactory.GetToggleButton("music");
      this.Button_Music.CenterX(this.Context.ScreenWidth);
      this.Button_Music.X = 200f;
      this.Button_Music.Y = (float) (num + 150);
      this.Button_Sound = this.Context.SpriteFactory.GetToggleButton("sound");
      this.Button_Sound.CenterX(this.Context.ScreenWidth);
      this.Button_Sound.X = 420f;
      this.Button_Sound.Y = (float) (num + 150);
      this.Button_Credits = this.Context.SpriteFactory.GetButton("credits");
      this.Button_Credits.CenterX(this.Context.ScreenWidth);
      this.Button_Credits.Y = (float) (num + 225);
      this.AddButton(this.Button_Start);
      this.AddButton(this.Button_How);
      this.AddButton((Button) this.Button_Music);
      this.AddButton((Button) this.Button_Sound);
      this.AddButton(this.Button_Credits);
    }//

    public override void Loaded()
    {
      base.Loaded();
      this.Context.ResourceManagers.CurrentSoundManager.PlayMusic("music");
      DebugLog.BeginGame();
    }//
  }
}
