// Steamworks.Games.Game.Core.Logic.GameSettings

using Steamworks.Engine;
using Steamworks.Games.Game.Core.Interfaces;


namespace Steamworks.Games.Game.Core.Logic
{
  public class GameSettings : IGameSettings
  {
    private const string SettingsFile = "Settings";
    private int _soundIndex;
    private int _musicIndex = 1;
    private IPersister _persister;
    private byte[] _settings;
    private byte _defaultSetting = 1;

    public GameSettings(IPersister persister)
    {
      this._persister = persister;
      this.Load();
    }

    private void Load()
    {
      this._settings = this._persister.Load("Settings", 2);
      if ((int) this._settings[0] == (int) this._persister.DefaultValue)
        this._settings[0] = this._defaultSetting;
      if ((int) this._settings[1] != (int) this._persister.DefaultValue)
        return;
      this._settings[1] = this._defaultSetting;
    }

    private void Save()
    {
        this._persister.Save(this._settings, 2, "Settings");
    }

    public void EnableMusic()
    {
      this._settings[this._musicIndex] = (byte) 1;
      this.Save();
    }

    public void DisableMusic()
    {
      this._settings[this._musicIndex] = (byte) 0;
      this.Save();
    }

    public void EnableSound()
    {
      this._settings[this._soundIndex] = (byte) 1;
      this.Save();
    }

    public void DisableSound()
    {
      this._settings[this._soundIndex] = (byte) 0;
      this.Save();
    }

    public bool IsSoundEnabled()
    {
        return this._settings[this._soundIndex] == (byte)1;
    }

    public bool IsMusicEnabled()
    {
        return this._settings[this._musicIndex] == (byte)1;
    }
  }
}
