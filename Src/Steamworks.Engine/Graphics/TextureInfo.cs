// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.TextureInfo
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework.Graphics;
using Steamworks.Engine.Common;
using Steamworks.Shared.Graphics;
using System.Xml.Serialization;

#nullable disable
namespace Steamworks.Engine.Graphics
{
  public class TextureInfo : TextureInfoBase
  {
    [XmlIgnore]
    public Texture2D Texture;

    [XmlIgnore]
    public RectangleF SourceRectangle
    {
      get
      {
        return new RectangleF((float) this.SourceX, (float) this.SourceY, (float) this.SizeX, (float) this.SizeY);
      }
    }
  }
}
