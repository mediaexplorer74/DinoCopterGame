// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Particles.RainParticleSystem
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Graphics;


namespace Steamworks.Physics.Particles
{
  public class RainParticleSystem : ParticleSystem
  {
    public RainParticleSystem(TextureInfo ti, int maxParticles)
      : base(ti, maxParticles)
    {
    }

    protected override void InitializeConstants()
    {
      this.minInitialSpeed = 500f;
      this.maxInitialSpeed = 550f;
      this.minLifetime = 1f;
      this.maxLifetime = 3f;
      this.minNumParticles = 100f;
      this.maxNumParticles = 500f;
      this.minAngle = 1.57079637f;
      this.maxAngle = 1.57079637f;
    }
  }
}
