// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Particles.Particle
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using Steamworks.Physics.Body;
using System;


namespace Steamworks.Physics.Particles
{
  public abstract class Particle : IPositionable
  {
    public bool Active;
    public float Lifetime;
    public float LifetimeAreaX;
    public float LifetimeAreaY;
    public PositionableMediator Mediator;
    public Sprite ParticleSprite;
    public float TimeSinceStart;
    public bool UseLifetimeArea;
    private float _height;
    private float _rotation;
    private float _width;
    private float _x;
    private float _y;

    public float Height
    {
      get => this._height;
      set => this._height = value;
    }

    public float Rotation
    {
      get => this._rotation;
      set => this._rotation = value;
    }

    public float Width
    {
      get => this._width;
      set => this._width = value;
    }

    public float X
    {
      get => this._x;
      set => this._x = value;
    }

    public float Y
    {
      get => this._y;
      set => this._y = value;
    }

    public virtual void Init(TextureInfo ti)
    {
      this.ParticleSprite = new Sprite(ti);
      this.Width = (float) ti.SizeX;
      this.Height = (float) ti.SizeY;
      this.Mediator = new PositionableMediator((IPositionable) this, (IPositionable) this.ParticleSprite);
    }

    public virtual void Initialize(
      Vector2 where,
      Vector2 velocity,
      Vector2 acceleration,
      float scale,
      float rotation,
      float rotationSpeed)
    {
      this.Active = true;
      this.TimeSinceStart = 0.0f;
      this.X = where.X;
      this.Y = where.Y;
      this.Rotation = rotation;
      this.Mediator.UpdateTarget();
    }

    public void Update(float elapsedTime_s, float totalTime_s)
    {
      this.UpdateParticle(elapsedTime_s, totalTime_s);
      this.Mediator.UpdateTarget();
      this.CheckActive(elapsedTime_s);
    }

    protected abstract void UpdateParticle(float elapsedTime_s, float totalTime_s);

    private void CheckActive(float elapsedTime_s)
    {
      this.TimeSinceStart += elapsedTime_s;
      if (!this.UseLifetimeArea)
      {
        if ((double) this.TimeSinceStart <= (double) this.Lifetime)
          return;
        this.Active = false;
      }
      else
      {
        float num = Math.Max(this.Width, this.Height);
        if ((double) this.X >= 0.0 - (double) num && (double) this.X <= (double) this.LifetimeAreaX + (double) num / 2.0 && (double) this.Y >= 0.0 - (double) num && (double) this.Y <= (double) this.LifetimeAreaY + (double) num / 2.0)
          return;
        this.Active = false;
      }
    }
  }
}
