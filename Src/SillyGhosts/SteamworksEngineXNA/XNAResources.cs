// Steamworks.Engine.XNA.XNAResources

using Steamworks.Engine.Graphics;
using Steamworks.Engine.Sound;


namespace Steamworks.Engine.XNA
{
  public class XNAResources : Resources
  {
    protected override void CreateFontManager(IDataLoader Loader)
    {
      FontManager fontManager = new FontManager();
      fontManager.Init(Loader.LoadFonts());
      this.CurrentFontManager = (IFontManager) fontManager;
    }

    protected override void CreateSoundManager(IDataLoader Loader)
    {
      XNASoundManager xnaSoundManager = new XNASoundManager();
      xnaSoundManager.Init(Loader.LoadSounds());
      this.CurrentSoundManager = (ISoundManager) xnaSoundManager;
    }

    protected override void CreateTextureManager(IDataLoader Loader)
    {
      TextureManager textureManager = new TextureManager();
      textureManager.Init(Loader.LoadTextures());
      this.CurrentTextureManager = (ITextureManager) textureManager;
    }
  }
}
