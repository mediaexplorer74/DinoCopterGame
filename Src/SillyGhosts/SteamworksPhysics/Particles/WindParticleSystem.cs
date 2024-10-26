// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Particles.WindParticleSystem
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Graphics;


namespace Steamworks.Physics.Particles
{
  public class WindParticleSystem : ParticleSystem
  {
    public WindParticleSystem(TextureInfo ti, int maxParticles)
      : base(ti, maxParticles)
    {
    }

    protected override void InitializeConstants()
    {
      this.minInitialSpeed = 30f;
      this.maxInitialSpeed = 75f;
      this.minLifetime = 20f;
      this.maxLifetime = 20f;
      this.minNumParticles = 0.3f;
      this.maxNumParticles = 0.6f;
      this.minAngle = 1.57079637f;
      this.maxAngle = 1.57079637f;
      this.minRotationSpeed = 0.7853982f;
      this.maxRotationSpeed = 1.57079637f;
    }
  }
}
