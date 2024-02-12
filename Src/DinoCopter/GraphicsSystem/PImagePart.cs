// GameManager.GraphicsSystem.PImagePart

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
