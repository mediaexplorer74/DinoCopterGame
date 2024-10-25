// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Particles.RigidBodyParticle
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using Steamworks.Physics.Body;

#nullable disable
namespace Steamworks.Physics.Particles
{
  public class RigidBodyParticle : Particle
  {
    public RigidBody SourceBody;
    private PositionableMediator RigidBodyMediator;

    public override void Init(TextureInfo ti)
    {
      base.Init(ti);
      this.SourceBody = new RigidBody();
      this.SourceBody.Width = (float) ti.SizeX;
      this.SourceBody.Height = (float) ti.SizeY;
      this.RigidBodyMediator = new PositionableMediator((IPositionable) this.SourceBody, (IPositionable) this);
    }

    public override void Initialize(
      Vector2 where,
      Vector2 velocity,
      Vector2 acceleration,
      float scale,
      float rotation,
      float rotationSpeed)
    {
      base.Initialize(where, velocity, acceleration, scale, rotation, rotationSpeed);
      this.SourceBody.Position = where;
      this.SourceBody.Velocity = velocity;
      this.SourceBody.Rotation = rotation;
      this.SourceBody.RotationSpeed = rotationSpeed;
    }

    protected override void UpdateParticle(float elapsedTime_s, float totalTime_s)
    {
      this.SourceBody.Update(elapsedTime_s, totalTime_s);
      this.RigidBodyMediator.UpdateTarget();
    }
  }
}
