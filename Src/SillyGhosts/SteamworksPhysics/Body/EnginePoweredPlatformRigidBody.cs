// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Body.EnginePoweredPlatformRigidBody
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;
using Steamworks.Physics.Collisions;
using System;

#nullable disable
namespace Steamworks.Physics.Body
{
  public class EnginePoweredPlatformRigidBody : 
    RigidBody,
    IEnginePoweredPlatformRigidBody,
    IRigidBody
  {
    public Vector2 EngineForce = Vector2.Zero;
    public float EnginePower;
    public Vector2 EngineDirection = Vector2.Zero;
    public float VelocityDecreaseOnBump;
    public float VelocityDecreaseOnBumpPerpendicular;

    private bool HasLandingVelocity
    {
      get
      {
        return (double) this.Velocity.Length() < (double) this.MaximumBounceVelocity * 0.20000000298023224;
      }
    }

    private bool HasCrashVelocityX
    {
      get => (double) Math.Abs(this.Velocity.X) > (double) this.MaximumBounceVelocity;
    }

    private bool HasCrashVelocityY
    {
      get => (double) Math.Abs(this.Velocity.Y) > (double) this.MaximumBounceVelocity;
    }

    public bool IsEngineRunning => (double) this.EngineForce.Length() > 0.0;

    public override void Update(float elapsedtime_ms, float totaltime_ms)
    {
      this.CalculateEngineForce();
      this.DynamicForces.Add(this.EngineForce);
      base.Update(elapsedtime_ms, totaltime_ms);
    }

    private void CalculateEngineForce()
    {
      this.EngineForce = this.EngineDirection.Multiply((double) this.EnginePower);
      this.EngineDirection = Vector2.Zero;
    }

    protected override void CalculateCollisionResponse(Collision collision)
    {
      if (collision.Direction == CollisionDirection.Bottom || collision.Direction == CollisionDirection.Top)
      {
        if (this.HasLandingVelocity)
        {
          this.Velocity.Y = 0.0f;
          this.IsOnGroundOrWater = collision.Direction == CollisionDirection.Bottom;
        }
        else
        {
          if (this.HasCrashVelocityY)
            this.IsCrashed = true;
          else if ((double) Math.Abs(this.Velocity.Y) > 0.800000011920929)
            this.Bumped = true;
          this.Velocity.Y -= this.Velocity.Y * this.VelocityDecreaseOnBump;
          this.Velocity.Y = -this.Velocity.Y;
        }
        this.Velocity.X -= this.Velocity.X * this.VelocityDecreaseOnBumpPerpendicular;
        this.Position.Y -= collision.Intersection.Y;
      }
      if (collision.Direction != CollisionDirection.Right && collision.Direction != CollisionDirection.Left)
        return;
      if (this.HasLandingVelocity)
      {
        this.Velocity.X = 0.0f;
      }
      else
      {
        if (this.HasCrashVelocityX)
          this.IsCrashed = true;
        else if ((double) Math.Abs(this.Velocity.X) > 0.800000011920929)
          this.Bumped = true;
        this.Velocity.X -= this.Velocity.X * this.VelocityDecreaseOnBump;
        this.Velocity.X = -this.Velocity.X;
      }
      this.Velocity.Y -= this.Velocity.Y * this.VelocityDecreaseOnBumpPerpendicular;
      this.Position.X -= collision.Intersection.X;
    }

    protected override bool ShouldSolveCollision(Collision collision) => true;
  }
}
