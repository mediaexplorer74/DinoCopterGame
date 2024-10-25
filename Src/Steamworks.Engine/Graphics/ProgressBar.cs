// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.ProgressBar
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;

#nullable disable
namespace Steamworks.Engine.Graphics
{
  public class ProgressBar : Sprite
  {
    private RectangleF OriginalRectangle;
    private float _progress;
    private bool _reverse;
    private float _max;
    private float _min;

    public float Progress
    {
      get => this._progress;
      private set => this._progress = value;
    }

    public bool Reverse
    {
      get => this._reverse;
      set => this._reverse = value;
    }

    public ProgressBar(TextureInfo textureInfo, float start, float finish, float value)
      : base(textureInfo)
    {
      this.Min = start;
      this.Max = finish;
      this.Value = value;
      this.OriginalRectangle = this.SourceRectangle;
    }

    public override void OnDraw(ISpriteBatch spriteBatch)
    {
      this.SourceRectangle = new RectangleF(this.OriginalRectangle.X, this.OriginalRectangle.Y, (float) (int) ((double) this.OriginalRectangle.Width * (double) this.Progress), this.OriginalRectangle.Height);
      base.OnDraw(spriteBatch);
    }

    public float Max
    {
      get => this._max;
      set => this._max = value;
    }

    public float Min
    {
      get => this._min;
      set => this._min = value;
    }

    public float Value
    {
      set
      {
        this.Progress = (float) (((double) value - (double) this.Min) / ((double) this.Max - (double) this.Min));
        if (this.Reverse)
          this.Progress = 1f - this.Progress;
        if ((double) this.Progress < 0.0)
          this.Progress = 0.0f;
        if ((double) this.Progress <= 1.0)
          return;
        this.Progress = 1f;
      }
    }
  }
}
