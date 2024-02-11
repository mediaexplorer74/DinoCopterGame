// Decompiled with JetBrains decompiler
// Type: GameManager.GraphicsSystem.PModulate
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

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
