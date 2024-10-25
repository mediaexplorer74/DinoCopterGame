// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Scenes.Menu.TutorialScene
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Logic;

#nullable disable
namespace Steamworks.Games.Game.Core.Scenes.Menu
{
  public class TutorialScene : Scene
  {
    public Button Button_Skip;
    public IEntity[] Pages;
    private int CurrentPageIndex;
    public Button Button_Prev;
    public Button Button_Next;
    public Button Button_Finish;
    private int PageCount = 5;
    private bool NextPage;
    private bool PrevPage;
    private float FadeTime = 0.2f;
    private IEntity CurrentPage;
    private IEntity PreviousPage;
    private GameProgress _gameProgress;
    private Button Button_Back;
    private bool IsExiting;

    public TutorialScene(EngineBase context, GameProgress gameProgress)
      : base(context)
    {
      this._gameProgress = gameProgress;
      this.CreatePages();
      this.CreateButtons();
      this.AddButtons();
      this.CurrentPage = this.Pages[0];
    }

    public override void Draw(ISpriteBatch SpriteBatch)
    {
      SpriteBatch.BeginAlpha();
      if (this.PreviousPage != null)
        this.PreviousPage.Draw(SpriteBatch);
      this.CurrentPage.Draw(SpriteBatch);
      SpriteBatch.End();
      this.DrawButtons(SpriteBatch);
    }

    public override void Update(float elapsedtime_s, float totaltime_s)
    {
      base.Update(elapsedtime_s, totaltime_s);
      foreach (IUpdateable page in this.Pages)
        page.Update(elapsedtime_s, totaltime_s);
      this.CurrentPage = this.Pages[this.CurrentPageIndex];
      if (this.CurrentPageIndex < this.PageCount - 1)
      {
        this.Button_Next.Visible = true;
        this.Button_Finish.Visible = false;
      }
      else
      {
        this.Button_Next.Visible = false;
        this.Button_Finish.Visible = true;
      }
      if (this.CurrentPageIndex > 0)
        this.Button_Prev.Visible = true;
      else
        this.Button_Prev.Visible = false;
      if (this.NextPage)
      {
        this.NextPage = false;
        this.PreviousPage = this.CurrentPage;
        this.PreviousPage.FadeOut(this.FadeTime);
        ++this.CurrentPageIndex;
        this.CurrentPage = this.Pages[this.CurrentPageIndex];
        this.CurrentPage.FadeIn(this.FadeTime);
      }
      else
      {
        if (!this.PrevPage)
          return;
        this.PrevPage = false;
        this.PreviousPage = this.CurrentPage;
        this.PreviousPage.FadeOut(this.FadeTime);
        --this.CurrentPageIndex;
        this.CurrentPage = this.Pages[this.CurrentPageIndex];
        this.CurrentPage.FadeIn(this.FadeTime);
      }
    }

    public override void Button_Clicked(Button sender)
    {
      if (this.IsExiting)
        return;
      DebugLog.Alert("Button clicked: " + sender.Tag?.ToString());
      this.Context.ResourceManagers.CurrentSoundManager.PlaySound("click", true);
      if (sender == this.Button_Back)
        this.Context.SceneManager.Back();
      else if (sender == this.Button_Skip || sender == this.Button_Finish)
      {
        this.Context.SceneManager.SwitchScene("MainMenuScene");
        this.IsExiting = true;
      }
      else if (sender == this.Button_Prev)
      {
        this.PrevPage = true;
      }
      else
      {
        if (sender != this.Button_Next)
          return;
        this.NextPage = true;
      }
    }

    private void AddButtons()
    {
      this.AddButton(this.Button_Skip);
      this.AddButton(this.Button_Prev);
      this.AddButton(this.Button_Next);
      this.AddButton(this.Button_Finish);
    }

    private void CreateButtons()
    {
      this.Button_Skip = this.Context.SpriteFactory.GetButton("skip");
      this.Button_Skip.X = 0.0f;
      this.Button_Skip.Y = 400f;
      this.Button_Prev = this.Context.SpriteFactory.GetButton("prev");
      this.Button_Prev.X = 430f;
      this.Button_Prev.Y = 400f;
      this.Button_Next = this.Context.SpriteFactory.GetButton("next");
      this.Button_Next.X = 630f;
      this.Button_Next.Y = 400f;
      this.Button_Finish = this.Context.SpriteFactory.GetButton("finish");
      this.Button_Finish.X = 620f;
      this.Button_Finish.Y = 400f;
      this.Button_Back = this.Context.SpriteFactory.GetButton("back");
      this.Button_Back.Y = 470f;
      this.Button_Back.CenterX(this.Context.ScreenWidth);
      this.AddButton(this.Button_Back);
    }

    private void CreatePages()
    {
      this.Pages = new IEntity[this.PageCount];
      for (int index = 0; index < this.PageCount; ++index)
        this.Pages[index] = this.Context.SpriteFactory.Get("tutorial_" + index.ToString());
    }
  }
}
