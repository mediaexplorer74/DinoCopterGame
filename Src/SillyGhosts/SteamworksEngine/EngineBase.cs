// Steamworks.Engine.EngineBase


using Steamworks.Engine.Common;
using Steamworks.Engine.Controller;
using Steamworks.Engine.Graphics;


namespace Steamworks.Engine
{
  public abstract class EngineBase
  {
    private bool _isTrial;
    private Resources _resourceManagers;
    private ISceneManager _sceneManager;
    private float _screenHeight;
    private float _screenWidth;
    private IGameSettings _settings;
    private ISpriteBatch _spriteBatch;
    private IEntityFactory _spriteFactory;
    private GameTime _time;
    private ITouchSource _touchSource;
    private float _scale = 1f;

    public EngineBase(IDataLoader Loader, IGameSettings Settings)
    {
      this.CreateResources();
      this.ResourceManagers.CreateResourceManagers(Loader);
      this.ResourceManagers.LoadSettings(Settings);
      this.Settings = Settings;
      this.SpriteFactory = (IEntityFactory) new SteamSpriteFactory(
          this.ResourceManagers.CurrentTextureManager, this.ResourceManagers.CurrentFontManager);
      this.Time = new GameTime();
    }

    protected abstract void CreateResources();

    public bool IsTrial
    {
      get => this._isTrial;
      set => this._isTrial = value;
    }

    public Resources ResourceManagers
    {
      get => this._resourceManagers;
      protected set => this._resourceManagers = value;
    }

    public ISceneManager SceneManager
    {
      get => this._sceneManager;
      set => this._sceneManager = value;
    }

    public float ScreenHeight
    {
      get => this._screenHeight;
      set => this._screenHeight = value;
    }

    public float ScreenWidth
    {
      get => this._screenWidth;
      set => this._screenWidth = value;
    }

    public IGameSettings Settings
    {
      get => this._settings;
      private set => this._settings = value;
    }

    public ISpriteBatch SpriteBatch
    {
      get => this._spriteBatch;
      set
      {
        this._spriteBatch = value;
        this._spriteBatch.Scale = this._scale;
      }
    }

    public IEntityFactory SpriteFactory
    {
      get => this._spriteFactory;
      set => this._spriteFactory = value;
    }

    public GameTime Time
    {
      get => this._time;
      private set => this._time = value;
    }

    public ITouchSource TouchSource
    {
      get => this._touchSource;
      set
      {
        this._touchSource = value;
        this._touchSource.Scale = this._scale;
      }
    }

    public float Scale
    {
      get => this._scale;
      set
      {
        this._scale = value;
        if (this.SpriteBatch != null)
          this.SpriteBatch.Scale = this._scale;
        if (this.TouchSource == null)
          return;
        this.TouchSource.Scale = this._scale;
      }
    }

    public abstract TextureInfo CreateTexture(Color color, float Width, float Height);

    public abstract void NavigateUrl(string url);

    public void Pause()
    {
        this.SceneManager.Pause();
    }
    }
}
