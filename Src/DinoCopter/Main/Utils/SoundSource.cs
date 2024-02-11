// GameManager.Utils.SoundSource

using GameManager.GraphicsSystem;
using Microsoft.Xna.Framework.Audio;
using System;

#nullable disable
namespace GameManager.Utils
{
  public class SoundSource
  {
    public SoundEffectInstance sound;
    public Point pos;
    public float volume;
    public float r1;
    public float r2;

    public SoundSource(SoundEffectInstance sound, Point pos, float volume, float r1, float r2)
    {
      this.sound = sound;
      this.pos = pos;
      this.volume = volume;
      this.r1 = r1;
      this.r2 = r2;
    }

    public void Update(Point listenerPos)
    {
      float num = (listenerPos - this.pos).Len();
      if ((double) num <= (double) this.r1)
        this.sound.Volume = Math.Max(this.sound.Volume, this.volume);
      else
        this.sound.Volume = Math.Max(0.0f, this.volume * (float) (1.0 - ((double) num - (double) this.r1) / ((double) this.r2 - (double) this.r1)));
    }
  }
}
