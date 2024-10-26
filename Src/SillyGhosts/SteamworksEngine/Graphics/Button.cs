// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.Button
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll


namespace Steamworks.Engine.Graphics
{
  public class Button : Entity
  {
    protected IEntity DefaultSprite;
    protected IEntity ClickedSprite;
    private bool _enabled = true;

    public bool Enabled
    {
      get => this._enabled;
      set => this._enabled = value;
    }

    public Button(TextureInfo DefaultTexture, TextureInfo ClickedTexture)
    {
      this.DefaultSprite = (IEntity) new Sprite(DefaultTexture);
      this.ClickedSprite = (IEntity) new Sprite(ClickedTexture);
      this.ClickedSprite.Alpha = 0.0f;
      this.AttachChild(this.DefaultSprite);
      this.AttachChild(this.ClickedSprite);
    }

    public virtual void Click() => this.ClickedSprite.FadeOut(0.1f);

    protected override void FadeAlphaChildren(float from, float to, float totaltime_s)
    {
      this.DefaultSprite.FadeAlpha(from, to, totaltime_s);
    }
  }
}
