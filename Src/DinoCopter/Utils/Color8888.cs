// GameManager.Utils.Color8888

#nullable disable
namespace GameManager.Utils
{
  internal class Color8888
  {
    public byte A;
    public byte B;
    public byte[] C = new byte[4];
    public byte G;
    public byte R;

    public Color8888()
    {
      this.B = byte.MaxValue;
      this.G = byte.MaxValue;
      this.R = byte.MaxValue;
      this.A = byte.MaxValue;
      this.C[0] = this.B;
      this.C[1] = this.G;
      this.C[2] = this.R;
      this.C[3] = this.A;
    }

    public Color8888(byte b, byte g, byte r, byte a)
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
