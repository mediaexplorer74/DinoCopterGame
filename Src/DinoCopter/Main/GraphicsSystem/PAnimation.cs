// Decompiled with JetBrains decompiler
// Type: GameManager.GraphicsSystem.PAnimation
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

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
