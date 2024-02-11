// Decompiled with JetBrains decompiler
// Type: GameManager.GraphicsSystem.PRects
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

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
