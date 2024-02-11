// Decompiled with JetBrains decompiler
// Type: GameManager.GraphicsSystem.PImagePart
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class PImagePart
  {
    public float MinX { get; set; }

    public float MinY { get; set; }

    public float W { get; set; }

    public float H { get; set; }

    public Paintable Image { get; set; }

    public float[] TextureCoords { get; set; }

    public float[] Coords { get; set; }

    public int CoordsNum { get; set; }
  }
}
