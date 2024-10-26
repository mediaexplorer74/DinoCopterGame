// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Graphics.TextureInfoBase
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace Steamworks.Shared.Graphics
{
  public class TextureInfoBase : ResourceInfo
  {
    [XmlAttribute]
    public int SourceX;
    [XmlAttribute]
    public int SourceY;
    [XmlAttribute]
    public int SizeX;
    [XmlAttribute]
    public int SizeY;
    [XmlArray]
    public List<Animation> Animations;

    public Animation GetAnimation(string name)
    {
      if (this.Animations != null)
      {
        foreach (Animation animation in this.Animations)
        {
          if (animation.Name == name)
            return animation;
        }
        throw new Exception("No animation named " + name + " on sprite " + this.Name);
      }
      return (Animation) null;
    }
  }
}
