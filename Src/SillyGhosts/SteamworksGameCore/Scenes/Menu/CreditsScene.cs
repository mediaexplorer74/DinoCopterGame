// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Scenes.Menu.CreditsScene
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Scenes.Basic;


namespace Steamworks.Games.Game.Core.Scenes.Menu
{
  public class CreditsScene : TwoBackgroundScene
  {
    private IEntity Credits;
    private float CreditsSpeed = 70f;
    private bool RollCredits;
    private bool Switched;
    private Button Button_Back;
    private bool IsExiting;

    public CreditsScene(EngineBase Context)
      : base(Context)
    {
      this.Credits = Context.SpriteFactory.Get("credits");
      this.Credits.CenterX(Context.ScreenWidth);
      this.Credits.Y = 120f;
      this.RootEntity.AttachChild(this.Credits);
      this.Button_Back = Context.SpriteFactory.GetButton("back");
      this.Button_Back.X = 660f;
      this.Button_Back.Y = 10f;
      this.AddButton(this.Button_Back);
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
      base.Draw(SpriteBatch);
      SpriteBatch.BeginAlpha();
      this.RootEntity.Draw(SpriteBatch);
      SpriteBatch.End();
    }

    public override void Update(float elapsedtime_s, float totaltime_s)
    {
      base.Update(elapsedtime_s, totaltime_s);
      if (this.RollCredits)
        this.Credits.Y -= elapsedtime_s * this.CreditsSpeed;
      if ((double) this.Credits.Y + (double) this.Credits.Height >= 100.0 || this.Switched)
        return;
      this.Context.SceneManager.SwitchScene("MainMenuScene");
      this.Switched = true;
    }

    public override void Loaded()
    {
      base.Loaded();
      this.RollCredits = true;
    }

    public override void Button_Clicked(Button sender)
    {
      if (this.IsExiting)
        return;
      DebugLog.Alert("Button clicked: " + sender.Tag?.ToString());
      this.Context.ResourceManagers.CurrentSoundManager.PlaySound("click", true);
      if (sender == this.Button_Back)
        this.Context.SceneManager.Back();
      this.IsExiting = true;
    }
  }
}
