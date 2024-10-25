// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.GameTime
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

#nullable disable
namespace Steamworks.Engine.Common
{
  public class GameTime
  {
    private float _totalTime_s;
    private bool _isPaused;

    public float TotalTime_s
    {
      get => this._totalTime_s;
      set => this._totalTime_s = value;
    }

    public void ResetTime()
    {
      this.Continue();
      this.TotalTime_s = 0.0f;
    }

    public void Pause() => this.IsPaused = true;

    public void Continue() => this.IsPaused = false;

    public void UpdateTime(float ElapsedTime_s)
    {
      if (this.IsPaused)
        return;
      this.TotalTime_s += ElapsedTime_s;
    }

    public bool IsPaused
    {
      get => this._isPaused;
      set => this._isPaused = value;
    }
  }
}
