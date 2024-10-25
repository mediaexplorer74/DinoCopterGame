// steamworks.games.game.core.PersisterBase

using Steamworks.Games.Game.Core.Interfaces;

#nullable disable
namespace steamworks.games.game.core
{
  public abstract class PersisterBase : IPersister
  {
    private byte _defaultValue;

    public PersisterBase(byte defaultValue = 255) => this._defaultValue = defaultValue;

    public byte DefaultValue
    {
      get => this._defaultValue;
      set => this._defaultValue = value;
    }

    public abstract void Save(byte[] bytesToSave, int saveByteCount, string SaveName);

    public abstract byte[] Load(string SaveName, int saveByteCount);

    protected byte[] GetBytes(int saveByteCount)
    {
      byte[] bytes = new byte[saveByteCount];
      for (int index = 0; index < saveByteCount; ++index)
        bytes[index] = this.DefaultValue;
      return bytes;
    }
  }
}
