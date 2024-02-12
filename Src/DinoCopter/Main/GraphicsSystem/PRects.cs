// GameManager.GraphicsSystem.PRects

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class PRects
  {
    public float[] Coords;

    public float W { get; set; }

    public float H { get; set; }

    public Color Color { get; set; }

    public Texture2D Texture { get; set; }

    public void CreateTexture()
    {
      Texture2D texture2D = new Texture2D(DispManager.GraphicsDev, (int) this.W, (int) this.H);
      Color[] data = new Color[(int) this.W * (int) this.H];
      for (int index = 0; index < data.Length; ++index)
        data[index] = this.Color;
      texture2D.SetData<Color>(data);
      this.Texture = texture2D;
    }

    public PRects()
    {
    }

    public PRects(float w, float h, Color color)
    {
      this.W = w;
      this.H = h;
      this.Color = color;
      this.Coords = new float[8];
      this.CreateTexture();
    }

    public void SetTextureData()
    {
      Color[] data = new Color[(int) this.W * (int) this.H];
      for (int index = 0; index < data.Length; ++index)
        data[index] = this.Color;
      this.Texture.SetData<Color>(data);
    }
  }
}
