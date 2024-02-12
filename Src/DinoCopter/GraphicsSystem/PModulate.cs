// GameManager.GraphicsSystem.PModulate

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class PModulate
  {
    public Paintable Img { get; set; }

    public float[] Start { get; set; }

    public float[] D { get; set; }

    public float Duration { get; set; }

    public float Time { get; set; }

    public PModulate()
    {
      this.Start = new float[4];
      this.D = new float[4];
    }
  }
}
