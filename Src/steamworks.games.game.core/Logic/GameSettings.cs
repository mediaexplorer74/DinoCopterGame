// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.GameSettings
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Games.Game.Core.Interfaces;

#nullable disable
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

    private void Save() => this._persister.Save(this._settings, 2, "Settings");

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

    public bool IsSoundEnabled() => this._settings[this._soundIndex] == (byte) 1;

    public bool IsMusicEnabled() => this._settings[this._musicIndex] == (byte) 1;
  }
}
