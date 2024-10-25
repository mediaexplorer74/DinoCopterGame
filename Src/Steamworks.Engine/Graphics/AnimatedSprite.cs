// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.AnimatedSprite
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;
using Steamworks.Shared.Graphics;

#nullable disable
namespace Steamworks.Engine.Graphics
{
  public class AnimatedSprite : Sprite, IAnimatedEntity, IEntity, IUpdateable, IPositionable
  {
    public int CurrentFrame;
    public double TimeCounter;
    public Animation CurrentAnimation;
    private bool _isPlaying;
    public bool IsLoop;

    public bool IsPlaying
    {
      get => this._isPlaying;
      set => this._isPlaying = value;
    }

    public string CurrentAnimationName
    {
      get => this.CurrentAnimation != null ? this.CurrentAnimation.Name : (string) null;
    }

    public AnimatedSprite(TextureInfo textureInfo)
      : base(textureInfo)
    {
      this.SourceRectangle = textureInfo.SourceRectangle;
    }

    public void PlayOnce(string name)
    {
      this.IsLoop = false;
      this.Play(name);
    }

    private void Play(string name)
    {
      if (this.CurrentAnimation == this.SpriteTextureInfo.GetAnimation(name) && this.IsPlaying)
        return;
      this.CurrentAnimation = this.SpriteTextureInfo.GetAnimation(name);
      this.IsPlaying = true;
      this.TimeCounter = 0.0;
      this.CurrentFrame = 0;
    }

    public void Stop()
    {
      this.Pause();
      this.CurrentFrame = 0;
    }

    public void Pause() => this.IsPlaying = false;

    public override void Update(float elapsedTime_s, float totalTime_s)
    {
      base.Update(elapsedTime_s, totalTime_s);
      if (!this.IsPlaying)
        return;
      this.TimeCounter += (double) elapsedTime_s;
      if (this.TimeCounter <= 1.0 / (double) this.CurrentAnimation.FPS)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame >= this.CurrentAnimation.TotalFrames)
      {
        --this.CurrentFrame;
        if (this.IsLoop)
          this.CurrentFrame = 0;
        else
          this.IsPlaying = false;
      }
      this.TimeCounter = 0.0;
    }

    public override RectangleF SourceRectangle
    {
      get
      {
        if (this.CurrentAnimation != null)
          this._sourceRectangle = new RectangleF((float) ((this.CurrentAnimation.Start + this.CurrentFrame) % this.CurrentAnimation.InRow) * this._sourceRectangle.Width, (float) ((this.CurrentAnimation.Start + this.CurrentFrame) / this.CurrentAnimation.InRow) * this._sourceRectangle.Height, this._sourceRectangle.Width, this._sourceRectangle.Height);
        else
          this._sourceRectangle = new RectangleF(0.0f, 0.0f, this._sourceRectangle.Width, this._sourceRectangle.Height);
        return this._sourceRectangle;
      }
      set => base.SourceRectangle = value;
    }

    public void PlayLoop(string Name)
    {
      this.IsLoop = true;
      this.Play(Name);
    }
  }
}
