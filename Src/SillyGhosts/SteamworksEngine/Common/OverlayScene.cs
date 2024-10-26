// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.OverlayScene
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Graphics;


namespace Steamworks.Engine.Common
{
  public abstract class OverlayScene : Scene
  {
    private bool _fadingOut;
    public bool FadedOut;

    public OverlayScene(EngineBase Context)
      : base(Context)
    {
      DrawableRectangle child = new DrawableRectangle(Context, Context.ScreenWidth * 2f, Context.ScreenHeight * 2f, new Color(180, 0, 0, 0));
      child.X = Context.ScreenWidth / 2f;
      child.Y = Context.ScreenHeight / 2f;
      this.RootEntity.AttachChild((IEntity) child);
    }

    public virtual void FadeIn(float FadeTime) => this.RootEntity.FadeIn(FadeTime);

    public virtual void FadeOut(float FadeTime)
    {
      this.RootEntity.FadeOut(FadeTime);
      this._fadingOut = true;
    }

    public override void Update(float elapsedtime_s, float totaltime_s)
    {
      base.Update(elapsedtime_s, totaltime_s);
      if (!this.RootEntity.AlphaFadeComplete || !this._fadingOut)
        return;
      this.FadedOut = true;
    }
  }
}
