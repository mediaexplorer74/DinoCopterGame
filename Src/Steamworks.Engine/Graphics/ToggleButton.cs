// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.ToggleButton
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

#nullable disable
namespace Steamworks.Engine.Graphics
{
  public class ToggleButton : Button
  {
    private bool _isToggled;

    public bool IsToggled
    {
      get => this._isToggled;
      set
      {
        this._isToggled = value;
        if (value)
        {
          this.ClickedSprite.Alpha = 1f;
          this.DefaultSprite.Alpha = 0.0f;
        }
        else
        {
          this.ClickedSprite.Alpha = 0.0f;
          this.DefaultSprite.Alpha = 1f;
        }
      }
    }

    public ToggleButton(TextureInfo DefaultTexture, TextureInfo ClickedTexture)
      : base(DefaultTexture, ClickedTexture)
    {
    }

    public override void Click() => this.IsToggled = !this.IsToggled;

    protected override void FadeAlphaChildren(float from, float to, float totaltime_s)
    {
      if (this.IsToggled)
        this.ClickedSprite.FadeAlpha(from, to, totaltime_s);
      else
        this.DefaultSprite.FadeAlpha(from, to, totaltime_s);
    }
  }
}
