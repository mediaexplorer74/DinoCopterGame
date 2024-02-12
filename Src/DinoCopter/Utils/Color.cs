// GameManager.Utils.Color

#nullable disable
namespace GameManager.Utils
{
  public class Color
  {
    public float B;
    public float G;
    public float R;
    public float A;
    public float[] C = new float[4];

    public Color()
    {
      this.B = 1f;
      this.G = 1f;
      this.R = 1f;
      this.A = 1f;
      this.C[0] = this.B;
      this.C[1] = this.G;
      this.C[2] = this.R;
      this.C[3] = this.A;
    }

    public Color(float b, float g, float r, float a)
    {
      this.B = b;
      this.G = g;
      this.R = r;
      this.A = a;
      this.C[0] = this.B;
      this.C[1] = this.G;
      this.C[2] = this.R;
      this.C[3] = this.A;
    }
  }
}
