// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Map.TMXProperty
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System.Xml.Serialization;


namespace Steamworks.Shared.Map
{
  public class TMXProperty
  {
    private string _name;
    private string _value;

    [XmlAttribute]
    public string name
    {
      get => this._name;
      set => this._name = value;
    }

    [XmlAttribute]
    public string value
    {
      get => this._value;
      set => this._value = value;
    }
  }
}
