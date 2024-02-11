// Decompiled with JetBrains decompiler
// Type: GameManager.GraphicsSystem.PGroup
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

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
