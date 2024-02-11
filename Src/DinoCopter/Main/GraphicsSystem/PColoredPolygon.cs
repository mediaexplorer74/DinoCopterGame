// Decompiled with JetBrains decompiler
// Type: GameManager.GraphicsSystem.PColoredPolygon
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class PColoredPolygon
  {
    public int VertexNum { get; set; }

    public Point[] Vertexes { get; set; }

    public Color[] Colors { get; set; }

    public float SizeX { get; set; }

    public float SizeY { get; set; }

    public float MinX { get; set; }

    public float MinY { get; set; }

    public VertexPositionColor[] PointList { get; set; }
  }
}
