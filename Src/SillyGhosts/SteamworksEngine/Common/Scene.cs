// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.Scene
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Controller;
using Steamworks.Engine.Graphics;
using System.Collections.Generic;

#nullable disable
namespace Steamworks.Engine.Common
{
  public abstract class Scene : IUpdateable
  {
    protected EngineBase Context;
    protected BetterKeyboard BetterKeyboard = new BetterKeyboard();
    public SceneState State;
    protected List<Button> Buttons = new List<Button>();
    protected Entity RootEntity = new Entity();

    public abstract void Draw(ISpriteBatch SpriteBatch);

    public void AddButton(Button button)
    {
      this.Buttons.Add(button);
      this.RootEntity.AttachChild((IEntity) button);
    }

    public Scene(EngineBase Context) => this.Context = Context;

    protected void DrawButtons(ISpriteBatch SpriteBatch)
    {
      SpriteBatch.BeginAlpha();
      foreach (Entity button in this.Buttons)
        button.Draw(SpriteBatch);
      SpriteBatch.End();
    }

    public abstract void Button_Clicked(Button sender);

    public virtual void Update(float elapsedtime_s, float totaltime_s)
    {
      this.RootEntity.Update(elapsedtime_s, totaltime_s);
    }

    public virtual void Pause()
    {
    }

    public virtual void ProcessInput()
    {
      List<Vector2> state = this.Context.TouchSource.GetState(false, false, true);
      if (state.Count > 0)
      {
        foreach (Button button in this.Buttons)
        {
          if (button.Visible && button.Enabled && button.IsTouched(state[0]))
          {
            button.Click();
            this.Button_Clicked(button);
          }
        }
      }
      this.BetterKeyboard.Update();
    }

    public virtual bool HandleBack() => false;

    public virtual void Loaded()
    {
    }

    public virtual void Unloaded()
    {
    }
  }
}
