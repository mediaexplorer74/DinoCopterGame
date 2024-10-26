// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Particles.SnowParticleSystem
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Graphics;


namespace Steamworks.Physics.Particles
{
  public class SnowParticleSystem : ParticleSystem
  {
    public SnowParticleSystem(TextureInfo ti, int maxParticles)
      : base(ti, maxParticles)
    {
    }

    protected override void InitializeConstants()
    {
      this.minInitialSpeed = 100f;
      this.maxInitialSpeed = 150f;
      this.minLifetime = 3f;
      this.maxLifetime = 6f;
      this.minNumParticles = 10f;
      this.maxNumParticles = 20f;
      this.minAngle = 1.57079637f;
      this.maxAngle = 1.57079637f;
    }
  }
}
