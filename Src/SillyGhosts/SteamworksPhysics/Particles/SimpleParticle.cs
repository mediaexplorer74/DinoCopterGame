// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Particles.SimpleParticle
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;


namespace Steamworks.Physics.Particles
{
  public class SimpleParticle : Particle
  {
    private Vector2 _velocity;
    private float _rotationSpeed;

    public override void Initialize(
      Vector2 where,
      Vector2 velocity,
      Vector2 acceleration,
      float scale,
      float rotation,
      float rotationSpeed)
    {
      base.Initialize(where, velocity, acceleration, scale, rotation, rotationSpeed);
      this._velocity = velocity;
      this._rotationSpeed = rotationSpeed;
    }

    protected override void UpdateParticle(float elapsedTime_s, float totalTime_s)
    {
      this.X += this._velocity.X * elapsedTime_s;
      this.Y += this._velocity.Y * elapsedTime_s;
      this.Rotation += this._rotationSpeed * elapsedTime_s;
    }
  }
}
