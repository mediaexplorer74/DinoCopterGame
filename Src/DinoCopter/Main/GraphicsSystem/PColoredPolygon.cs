// GameManager.GraphicsSystem.PColoredPolygon

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
