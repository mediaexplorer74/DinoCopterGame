// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Particles.CloudParticleSystem
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Graphics;


namespace Steamworks.Physics.Particles
{
  public class CloudParticleSystem : ParticleSystem
  {
    public CloudParticleSystem(float AreaX, float AreaY, TextureInfo ti, int maxParticles)
      : base(ti, maxParticles)
    {
      this.LifetimeAreaX = AreaX;
      this.LifetimeAreaY = AreaY;
      this.Precompute(1000f, 1f);
    }

    protected override void InitializeConstants()
    {
      this.minInitialSpeed = 5f;
      this.maxInitialSpeed = 13f;
      this.minNumParticles = 0.1f;
      this.maxNumParticles = 0.2f;
      this.minAngle = 0.0f;
      this.maxAngle = 0.0f;
      this.UseLifetimeArea = true;
      this.InitializeX = -1;
      this.InitializeY = 0;
    }
  }
}
