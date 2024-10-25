// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Resources
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Graphics;
using Steamworks.Engine.Sound;

#nullable disable
namespace Steamworks.Engine
{
  public abstract class Resources
  {
    private ITextureManager _textureManager;
    private IFontManager _fontManager;
    private ISoundManager _soundManager;

    public ITextureManager CurrentTextureManager
    {
      get => this._textureManager;
      set => this._textureManager = value;
    }

    public IFontManager CurrentFontManager
    {
      get => this._fontManager;
      set => this._fontManager = value;
    }

    public ISoundManager CurrentSoundManager
    {
      get => this._soundManager;
      set => this._soundManager = value;
    }

    public void CreateResourceManagers(IDataLoader Loader)
    {
      this.CreateTextureManager(Loader);
      this.CreateFontManager(Loader);
      this.CreateSoundManager(Loader);
    }

    protected abstract void CreateSoundManager(IDataLoader Loader);

    protected abstract void CreateFontManager(IDataLoader Loader);

    protected abstract void CreateTextureManager(IDataLoader Loader);

    internal void LoadSettings(IGameSettings Settings)
    {
      this.CurrentSoundManager.SoundEnabled = Settings.IsSoundEnabled();
      this.CurrentSoundManager.MusicEnabled = Settings.IsMusicEnabled();
    }
  }
}
