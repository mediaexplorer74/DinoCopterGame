// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Map.TMXLayer
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace Steamworks.Shared.Map
{
  public class TMXLayer
  {
    private bool _blockingLayer;
    private string _name;
    private int _width;
    private int _height;
    public float _parallax = 1f;
    private List<TMXLayerTile> _data;
    private TMXLayer layer;
    private int newWidth;
    private int newHeight;

    public TMXLayer() => this.data = new List<TMXLayerTile>();

    [XmlAttribute]
    public bool BlockingLayer
    {
      get => this._blockingLayer;
      set => this._blockingLayer = value;
    }

    [XmlAttribute]
    public string name
    {
      get => this._name;
      set => this._name = value;
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
    public float Parallax
    {
      get => this._parallax;
      set => this._parallax = value;
    }

    [XmlArray]
    [XmlArrayItem(ElementName = "tile")]
    public List<TMXLayerTile> data
    {
      get => this._data;
      set => this._data = value;
    }

    public TMXLayerTile GetTile(int x, int y)
    {
      if (x != -1 && y != -1 && x != this.width)
        return this.data[x + y * this.width];
      return new TMXLayerTile() { gid = 1 };
    }

    public bool ContainsTileAt(int x, int y)
    {
      return x >= -1 && y >= -1 && x <= this.width && y < this.height;
    }

    public void InitNewLayer(int width, int height)
    {
      this.width = width;
      this.height = height;
      for (int index1 = 0; index1 < width; ++index1)
      {
        for (int index2 = 0; index2 < height; ++index2)
          this.data.Add(new TMXLayerTile());
      }
    }

    internal void Fill(TMXLayer layer)
    {
      int num1 = Math.Min(this.width, layer.width);
      int num2 = Math.Min(this.height, layer.height);
      for (int x = 0; x < num1; ++x)
      {
        for (int y = 0; y < num2; ++y)
          this.GetTile(x, y).gid = layer.GetTile(x, y).gid;
      }
    }
  }
}
