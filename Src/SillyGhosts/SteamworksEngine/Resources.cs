// Steamworks.Engine.Resources

using Steamworks.Engine.Graphics;
using Steamworks.Engine.Sound;


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
