// GameManager.GraphicsSystem.PAnimation

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class PAnimation
  {
    public Paintable[] Frames { get; set; }

    public int FramesNum { get; set; }

    public float AnimationLength { get; set; }

    public float AnimationTimeElapsed { get; set; }
  }
}
