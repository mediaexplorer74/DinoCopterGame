// GameManager.GraphicsSystem.PImage

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
