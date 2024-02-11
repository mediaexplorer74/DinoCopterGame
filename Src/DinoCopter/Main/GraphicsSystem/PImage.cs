// Decompiled with JetBrains decompiler
// Type: GameManager.GraphicsSystem.PImage
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class PImage
  {
    public float[] Coords;

    public float W { get; set; }

    public float H { get; set; }

    public int TextureWidth { get; set; }

    public int TextureHeight { get; set; }

    public bool KeepPixelData { get; set; }

    public float[] TextureCoords { get; set; }

    public Texture2D Texture { get; set; }

    public PImage()
    {
      this.TextureCoords = new float[8];
      this.Coords = new float[8];
    }
  }
}
