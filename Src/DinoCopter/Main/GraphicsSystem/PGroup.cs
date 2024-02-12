// GameManager.GraphicsSystem.PGroup

using GameManager.Utils;
using System.Collections.Generic;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class PGroup
  {
    public List<Pair<Paintable, Point>> Elements;
    public bool AutoResize;

    public float W { get; set; }

    public float H { get; set; }

    public PGroup() => this.Elements = new List<Pair<Paintable, Point>>();
  }
}
