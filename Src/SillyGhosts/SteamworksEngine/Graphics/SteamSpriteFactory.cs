// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.SteamSpriteFactory
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll


namespace Steamworks.Engine.Graphics
{
  public class SteamSpriteFactory : IEntityFactory
  {
    protected ITextureManager TextureManager;
    protected IFontManager FontManager;

    public SteamSpriteFactory(ITextureManager texManager, IFontManager fontManager)
    {
      this.TextureManager = texManager;
      this.FontManager = fontManager;
    }

    public IEntity Get(string Name) => (IEntity) new Sprite(this.TextureManager.Get(Name));

    public IAnimatedEntity GetAnimated(string Name)
    {
      return (IAnimatedEntity) new AnimatedSprite(this.TextureManager.Get(Name));
    }

    public IDrawableText GetText(string text, string FontName)
    {
      DrawableText text1 = new DrawableText(0.0f, 0.0f, this.FontManager.Get(FontName));
      text1.Text = text;
      return (IDrawableText) text1;
    }

    public Button GetButton(string Name)
    {
      TextureInfo DefaultTexture = this.TextureManager.Get("button_" + Name);
      Button button = new Button(DefaultTexture, this.TextureManager.Get("button_" + Name + "_click"));
      button.Height = (float) DefaultTexture.SizeY;
      button.Width = (float) DefaultTexture.SizeX;
      button.Tag = (object) Name;
      return button;
    }

    public ToggleButton GetToggleButton(string Name)
    {
      TextureInfo DefaultTexture = this.TextureManager.Get("button_" + Name);
      ToggleButton toggleButton = new ToggleButton(DefaultTexture, this.TextureManager.Get("button_" + Name + "_clicked"));
      toggleButton.Height = (float) DefaultTexture.SizeY;
      toggleButton.Width = (float) DefaultTexture.SizeX;
      toggleButton.Tag = (object) Name;
      return toggleButton;
    }

    public ProgressBar GetProgressBar(string Name)
    {
      return new ProgressBar(this.TextureManager.Get(Name), 0.0f, 1f, 0.0f);
    }
  }
}
