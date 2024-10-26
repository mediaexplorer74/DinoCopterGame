// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Map.TMXTileInTileset
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System.Collections.Generic;
using System.Xml.Serialization;


namespace Steamworks.Shared.Map
{
  public class TMXTileInTileset
  {
    private int _id;
    private List<TMXProperty> _properties;

    public TMXTileInTileset() => this.properties = new List<TMXProperty>();

    [XmlAttribute]
    public int id
    {
      get => this._id;
      set => this._id = value;
    }

    [XmlArrayItem(ElementName = "property")]
    [XmlArray]
    public List<TMXProperty> properties
    {
      get => this._properties;
      set => this._properties = value;
    }
  }
}
