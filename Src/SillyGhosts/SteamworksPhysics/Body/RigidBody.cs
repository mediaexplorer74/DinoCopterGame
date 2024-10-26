// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Body.RigidBody
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;
using Steamworks.Physics.Collisions;
using System;
using System.Collections.Generic;


namespace Steamworks.Physics.Body
{
  public class RigidBody : IUpdateable, IPositionable, IRigidBody
  {
    public Vector2 Acceleration = Vector2.Zero;
    public float AirDensity;
    public List<Vector2> ConstantForces = new List<Vector2>();
    public List<Vector2> DynamicForces = new List<Vector2>();
    public Vector2 ForcesSum = Vector2.Zero;
    public float Gravity;
    public float Mass = 1f;
    public float MaximumBounceVelocity;
    public Vector2 Position = Vector2.Zero;
    public Vector2 Size = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;
    public float WaterDensity;
    public float WaterDragDensityDown;
    public float WaterDragDensityUp;
    public Vector2 WaterForce = Vector2.Zero;
    private RectangleF _boundingBox;
    private bool _bumped;
    private bool _checkForPlatformCollisions = true;
    private ICollisionDetector _currentCollisionDetector;
    private bool _fellInWater;
    private bool _isCrashed;
    private bool _isInWater;
    private bool _isOnGroundOrWater;
    private float _rotation;
    private float _rotationSpeed;
    private float _waterLevel;
    private Vector2 ForceDrag = Vector2.Zero;
    private bool waterLevelSet;

    public RigidBody() => this.BoundingBox = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);

    public RectangleF BoundingBox
    {
      get => this._boundingBox;
      set => this._boundingBox = value;
    }

    public RectangleF Bounds
    {
      get
      {
        return new RectangleF(this.Position.X + this.BoundingBox.X, this.Position.Y + this.BoundingBox.Y, this.BoundingBox.Width, this.BoundingBox.Height);
      }
    }

    public bool Bumped
    {
      get => this._bumped;
      set => this._bumped = value;
    }

    public Vector2 Center => this.Position.Plus(this.Size.Divide(2.0));

    public bool CheckForPlatformCollisions
    {
      get => this._checkForPlatformCollisions;
      set => this._checkForPlatformCollisions = value;
    }

    public ICollisionDetector CurrentCollisionDetector
    {
      get => this._currentCollisionDetector;
      set => this._currentCollisionDetector = value;
    }

    public bool FellInWater
    {
      get => this._fellInWater;
      set => this._fellInWater = value;
    }

    public float FinalDensityX => this.AirDensity;

    public float FinalDensityY
    {
      get
      {
        float num1 = 0.0f;
        if (this.waterLevelSet)
          num1 = this.SubmergedHeight / this.Height;
        float num2 = this.WaterDragDensityDown;
        if ((double) this.Velocity.Y < 0.0)
          num2 = this.WaterDragDensityUp;
        return (float) ((1.0 - (double) num1) * (double) this.AirDensity + (double) num1 * (double) num2);
      }
    }

    public float Height
    {
      get => this.Size.Y;
      set => this.Size.Y = value;
    }

    public bool IsCrashed
    {
      get => this._isCrashed;
      set => this._isCrashed = value;
    }

    public bool IsInWater
    {
      get => this._isInWater;
      set => this._isInWater = value;
    }

    public bool IsOnGroundOrWater
    {
      get => this._isOnGroundOrWater;
      set => this._isOnGroundOrWater = value;
    }

    public float Rotation
    {
      get => this._rotation;
      set => this._rotation = value;
    }

    public float RotationSpeed
    {
      get => this._rotationSpeed;
      set => this._rotationSpeed = value;
    }

    public float SubmergedHeight
    {
      get
      {
        float submergedHeight = 0.0f;
        if ((double) this.Bounds.Bottom > (double) this.WaterLevel)
          submergedHeight = Math.Abs(this.Bounds.Bottom - this.WaterLevel);
        if ((double) submergedHeight > (double) this.Bounds.Height)
          submergedHeight = this.Bounds.Height;
        return submergedHeight;
      }
    }

    public float WaterLevel
    {
      get => this._waterLevel;
      set
      {
        this._waterLevel = value;
        this.waterLevelSet = true;
      }
    }

    public float Width
    {
      get => this.Size.X;
      set => this.Size.X = value;
    }

    public float X
    {
      get => this.Position.X;
      set => this.Position.X = value;
    }

    public float Y
    {
      get => this.Position.Y;
      set => this.Position.Y = value;
    }

    public void CalculateCollisionsResponse()
    {
      List<Collision> collision = this.CurrentCollisionDetector.GetCollision(this.Bounds);
      if (collision.Count <= 0)
        return;
      for (List<Collision> collisionList = this.FilterAndSortCollisions(collision); collisionList.Count > 0; collisionList = this.FilterAndSortCollisions(this.CurrentCollisionDetector.GetCollision(this.Bounds)))
        this.CalculateCollisionResponse(collisionList[0]);
    }

    public void CalculateWaterForce()
    {
      this.WaterForce = Vector2.Zero;
      bool isInWater = this.IsInWater;
      this.IsInWater = false;
      this.FellInWater = false;
      if ((double) this.SubmergedHeight <= 0.0)
        return;
      this.WaterForce = new Vector2(0.0, (double) (-this.Gravity * this.WaterDensity * this.Width * this.Height * this.SubmergedHeight));
      this.IsInWater = true;
      this.IsOnGroundOrWater = true;
      if (!this.IsInWater || isInWater)
        return;
      this.FellInWater = true;
      this.FellToWater();
    }

    protected virtual void FellToWater()
    {
    }

    public virtual void Update(float elapsedtime_ms, float totaltime_ms)
    {
      this.IsOnGroundOrWater = false;
      this.Bumped = false;
      this.CalculateForces();
      this.CalculateAcceleration();
      this.CalculateVelocity((double) elapsedtime_ms);
      this.CalculatePosition((double) elapsedtime_ms);
      this.CalculateRotation(elapsedtime_ms);
      if (this.CurrentCollisionDetector == null)
        return;
      this.CalculateCollisionsResponse();
    }

    protected virtual void CalculateCollisionResponse(Collision collision)
    {
      if (!this.CheckForPlatformCollisions || collision.Direction != CollisionDirection.Bottom)
        return;
      this.IsOnGroundOrWater = true;
      this.Position.Y -= collision.Intersection.Y;
      this.Velocity.Y = 0.0f;
    }

    protected virtual void CalculateForces()
    {
      if (this.waterLevelSet)
        this.CalculateWaterForce();
      this.CalculateDrag();
      this.ForcesSum = Vector2.Zero;
      foreach (Vector2 constantForce in this.ConstantForces)
        this.ForcesSum = this.ForcesSum.Plus(constantForce);
      foreach (Vector2 dynamicForce in this.DynamicForces)
        this.ForcesSum = this.ForcesSum.Plus(dynamicForce);
      this.ForcesSum = this.ForcesSum.Plus(this.WaterForce);
      this.ForcesSum = this.ForcesSum.Plus(this.ForceDrag);
      this.DynamicForces.Clear();
    }

    protected virtual bool ShouldSolveCollision(Collision collision)
    {
      return collision.Direction == CollisionDirection.Bottom && this.CheckForPlatformCollisions;
    }

    private void CalculateAcceleration()
    {
      this.Acceleration = Vector2.Zero;
      this.Acceleration = this.Acceleration.Plus(new Vector2(0.0, (double) this.Gravity));
      this.Acceleration = this.Acceleration.Plus(this.ForcesSum.Divide((double) this.Mass));
    }

    private void CalculateDrag()
    {
      this.ForceDrag = new Vector2((double) (-1 * this.Sign(this.Velocity.X)) * 0.5 * (double) this.FinalDensityX * (double) this.Velocity.X * (double) this.Velocity.X * (double) this.Width * (double) this.Height * 1.0499999523162842, (double) (-1 * this.Sign(this.Velocity.Y)) * 0.5 * (double) this.FinalDensityY * (double) this.Velocity.Y * (double) this.Velocity.Y * (double) this.Width * (double) this.Height * 1.0499999523162842);
    }

    private void CalculatePosition(double elapsedTime)
    {
      this.Position = this.Position.Plus(this.Velocity.Multiply(elapsedTime));
    }

    private void CalculateRotation(float elapsedtime_ms)
    {
      this.Rotation += this.RotationSpeed * elapsedtime_ms;
    }

    private void CalculateVelocity(double elapsedTime)
    {
      this.Velocity = this.Velocity.Plus(this.Acceleration.Multiply(elapsedTime));
    }

    private List<Collision> FilterAndSortCollisions(List<Collision> collisions)
    {
      List<Collision> collisionList = new List<Collision>();
      foreach (Collision collision in collisions)
      {
        if (this.ShouldSolveCollision(collision))
          collisionList.Add(collision);
      }
      return collisionList;
    }

    private int Sign(float p) => (double) p > 0.0 ? 1 : -1;
  }
}
