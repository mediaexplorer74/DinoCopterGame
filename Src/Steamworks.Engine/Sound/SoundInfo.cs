// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Sound.SoundInfo
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Steamworks.Shared.Sound;
using System.Xml.Serialization;

#nullable disable
namespace Steamworks.Engine.Sound
{
  public class SoundInfo : SoundInfoBase
  {
    [XmlIgnore]
    public Song Song;
    [XmlIgnore]
    public SoundEffect Sound;
    [XmlIgnore]
    public SoundEffectInstance SoundInstance;
  }
}
