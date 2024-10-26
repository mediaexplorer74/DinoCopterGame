// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Graphics.ResourceInfo
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System.Xml.Serialization;


namespace Steamworks.Shared.Graphics
{
  public class ResourceInfo
  {
    [XmlAttribute]
    public string Name;
    [XmlAttribute]
    public string FilePath;
    [XmlAttribute]
    public string Extension;
  }
}
