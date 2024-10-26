// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Map.TMXMapData
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace Steamworks.Shared.Map
{
  [XmlRoot(ElementName = "map")]
  public class TMXMapData
  {
    [XmlAttribute]
    public string version;
    [XmlAttribute]
    public orientationType orientation;
    private int _width;
    private int _height;
    private int _tilewidth;
    private int _tileheight;
    private List<TMXTileset> _tilesets;
    private List<TMXLayer> _layers;

    public TMXMapData()
    {
      this.tilesets = new List<TMXTileset>();
      this.layers = new List<TMXLayer>();
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

    [XmlElement(ElementName = "tileset")]
    public List<TMXTileset> tilesets
    {
      get => this._tilesets;
      set => this._tilesets = value;
    }

    [XmlElement(ElementName = "layer")]
    public List<TMXLayer> layers
    {
      get => this._layers;
      set => this._layers = value;
    }

    public void InitNewMap()
    {
      this.tileheight = this.tilewidth;
      TMXLayer tmxLayer = new TMXLayer();
      tmxLayer.InitNewLayer(this.width, this.height);
      this.layers.Add(tmxLayer);
    }

    public static TMXMapData Load(Stream data)
    {
      return new XmlSerializer(typeof (TMXMapData)).Deserialize(data) as TMXMapData;
    }

    public int GetLayerIndex(TMXLayer tmxlayer) => this.layers.IndexOf(tmxlayer);

    public TMXTileset GetTileset(TMXLayerTile tile)
    {
      TMXTileset tileset1 = (TMXTileset) null;
      foreach (TMXTileset tileset2 in this.tilesets)
      {
        if (tile.gid < tileset2.firstgid)
          return tileset1;
        tileset1 = tileset2;
      }
      return tileset1 != null ? tileset1 : throw new Exception("Tileset not found! Tile.gid = " + tile.gid.ToString());
    }

    public void Resize(int newWidth, int newHeight)
    {
      List<TMXLayer> tmxLayerList = new List<TMXLayer>();
      tmxLayerList.AddRange((IEnumerable<TMXLayer>) this.layers);
      this.layers.Clear();
      foreach (TMXLayer layer in tmxLayerList)
      {
        TMXLayer tmxLayer = new TMXLayer();
        tmxLayer.InitNewLayer(newWidth, newHeight);
        tmxLayer.Fill(layer);
        tmxLayer.BlockingLayer = layer.BlockingLayer;
        tmxLayer.Parallax = layer.Parallax;
        this.layers.Add(tmxLayer);
      }
      this.width = newWidth;
      this.height = newHeight;
    }
  }
}
