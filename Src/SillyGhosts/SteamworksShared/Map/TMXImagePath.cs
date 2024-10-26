// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Map.TMXImagePath
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System.Xml.Serialization;


namespace Steamworks.Shared.Map
{
  public class TMXImagePath
  {
    private string _source;

    [XmlAttribute]
    public string source
    {
      get => this._source;
      set => this._source = value;
    }
  }
}
