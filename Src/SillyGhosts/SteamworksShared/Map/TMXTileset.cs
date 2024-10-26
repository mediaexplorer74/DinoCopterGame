// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Map.TMXTileset
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System.Collections.Generic;
using System.Xml.Serialization;


namespace Steamworks.Shared.Map
{
  public class TMXTileset
  {
    private string _name;
    private int _firstgid;
    private int _tilewidth;
    private int _tileheight;
    private int _width;
    private int _height;
    private TMXImagePath _image;
    private List<TMXTileInTileset> _tiles;

    public TMXTileset() => this.tiles = new List<TMXTileInTileset>();

    [XmlAttribute]
    public string name
    {
      get => this._name;
      set => this._name = value;
    }

    [XmlAttribute]
    public int firstgid
    {
      get => this._firstgid;
      set => this._firstgid = value;
    }

    [XmlAttribute]
    public int tilewidth
    {
      get => this._tilewidth;
      set => this._tilewidth = value;
    }

    [XmlAttribute]
    public int tileheight
    {
      get => this._tileheight;
      set => this._tileheight = value;
    }

    [XmlAttribute]
    public int width
    {
      get => this._width;
      set => this._width = value;
    }

    [XmlAttribute]
    public int height
    {
      get => this._height;
      set => this._height = value;
    }

    [XmlElement(ElementName = "image")]
    public TMXImagePath image
    {
      get => this._image;
      set => this._image = value;
    }

    [XmlElement(ElementName = "tile")]
    public List<TMXTileInTileset> tiles
    {
      get => this._tiles;
      set => this._tiles = value;
    }

    public override bool Equals(object obj)
    {
      return !(obj is TMXTileset) ? base.Equals(obj) : this.name == (obj as TMXTileset).name;
    }
  }
}
