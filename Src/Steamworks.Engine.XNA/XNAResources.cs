// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.XNA.XNAResources
// Assembly: Steamworks.Engine.XNA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 77251232-89D3-43D8-AA91-5C656BCC5CF5
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.XNA.dll

using Steamworks.Engine.Graphics;
using Steamworks.Engine.Sound;

#nullable disable
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
