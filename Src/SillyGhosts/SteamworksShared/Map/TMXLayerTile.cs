// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Map.TMXLayerTile
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System.Xml.Serialization;

#nullable disable
namespace Steamworks.Shared.Map
{
  public class TMXLayerTile
  {
    private int _gid;

    [XmlAttribute]
    public int gid
    {
      get => this._gid;
      set => this._gid = value;
    }
  }
}
