// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Sound.SoundInfoBase
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using Steamworks.Shared.Graphics;
using System.Xml.Serialization;

#nullable disable
namespace Steamworks.Shared.Sound
{
  public class SoundInfoBase : ResourceInfo
  {
    [XmlIgnore]
    public bool IsPlaying;
    [XmlIgnore]
    public bool IsLoop;
    [XmlAttribute]
    public bool IsMusic;
  }
}
