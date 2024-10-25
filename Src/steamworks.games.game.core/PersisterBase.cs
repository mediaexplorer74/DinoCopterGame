// Decompiled with JetBrains decompiler
// Type: steamworks.games.game.core.PersisterBase
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

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
