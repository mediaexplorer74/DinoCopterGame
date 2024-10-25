// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Particles.ParticleSystem
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;
using System;
using System.Collections.Generic;


#nullable disable
namespace Steamworks.Physics.Particles
{
  public abstract class ParticleSystem : Entity
  {
    private Random rand = new Random();
    protected float minNumParticles;
    protected float maxNumParticles;
    protected float minInitialSpeed;
    protected float maxInitialSpeed;
    protected float minAcceleration;
    protected float maxAcceleration;
    protected float minRotationSpeed;
    protected float maxRotationSpeed;
    protected float minLifetime;
    protected float maxLifetime;
    protected float LifetimeAreaX;
    protected float LifetimeAreaY;
    protected int InitializeX;
    protected int InitializeY;
    protected bool UseLifetimeArea;
    protected float minScale;
    protected float maxScale;
    protected float minAngle;
    protected float maxAngle;
    private float Timer_s;
    private SimpleParticle[] Particles;
    private Queue<SimpleParticle> freeParticles = new Queue<SimpleParticle>();
    private TextureInfo ParticleTextureInfo;
    private float nextCreationTime_s;

    public float Angle
    {
      set
      {
        this.minAngle = value;
        this.maxAngle = value;
      }
    }

    protected abstract void InitializeConstants();

    public ParticleSystem(TextureInfo ti, int maxParticles)
    {
      this.ParticleTextureInfo = ti;
      this.Particles = new SimpleParticle[maxParticles];
      for (int index = 0; index < maxParticles; ++index)
      {
        this.Particles[index] = new SimpleParticle();
        this.Particles[index].Init(ti);
        this.freeParticles.Enqueue(this.Particles[index]);
      }
      this.InitializeConstants();
    }

    public void AddParticles(float elapsedTime_s)
    {
      this.Timer_s += elapsedTime_s;
      if ((double) this.Timer_s <= (double) this.nextCreationTime_s)
        return;
      int num = (int) Math.Floor((double) this.Timer_s / (double) this.nextCreationTime_s);
      for (int index = 0; index < num && this.freeParticles.Count > 0; ++index)
      {
        float particleStartX = this.GetParticleStartX();
        float particleStartY = this.GetParticleStartY();
        this.InitializeParticle(this.freeParticles.Dequeue(), new Vector2((double) particleStartX, (double) particleStartY));
      }
      this.Timer_s = 0.0f;
      this.nextCreationTime_s = 1f / ((float) (this.rand.NextDouble() * ((double) this.maxNumParticles - (double) this.minNumParticles)) + this.minNumParticles);
    }

    private float GetParticleStartY()
    {
      return !this.UseLifetimeArea ? this.RandomBetween(this.Y, this.Height) : (this.InitializeY >= 0 ? (this.InitializeY != 0 ? this.LifetimeAreaY + (float) this.ParticleTextureInfo.SizeY : this.RandomBetween(0.0f, this.LifetimeAreaY)) : (float) -Math.Max(this.ParticleTextureInfo.SizeY, this.ParticleTextureInfo.SizeX));
    }

    private float GetParticleStartX()
    {
      return !this.UseLifetimeArea ? this.RandomBetween(this.X, this.Width) : (this.InitializeX >= 0 ? (this.InitializeX != 0 ? this.LifetimeAreaX + (float) this.ParticleTextureInfo.SizeX : this.RandomBetween(0.0f, this.LifetimeAreaX)) : (float) -Math.Max(this.ParticleTextureInfo.SizeY, this.ParticleTextureInfo.SizeX));
    }

    public void Precompute(float Count, float ElapsedTime)
    {
      for (float totalTime_s = 0.0f; (double) totalTime_s < (double) Count; totalTime_s += ElapsedTime)
      {
        this.AddParticles(ElapsedTime);
        this.Update(ElapsedTime, totalTime_s);
      }
    }

    protected virtual void InitializeParticle(SimpleParticle p, Vector2 where)
    {
      float angle = this.PickRandomAngle();
      Vector2 direction = this.GetDirection(angle);
      float number1 = this.RandomBetween(this.minInitialSpeed, this.maxInitialSpeed);
      float number2 = this.RandomBetween(this.minAcceleration, this.maxAcceleration);
      float num = this.RandomBetween(this.minLifetime, this.maxLifetime);
      float scale = this.RandomBetween(this.minScale, this.maxScale);
      float rotationSpeed = this.RandomBetween(this.minRotationSpeed, this.maxRotationSpeed);
      if (!this.UseLifetimeArea)
      {
        p.Initialize(where, direction.Multiply((double) number1), direction.Multiply((double) number2), scale, angle - 1.57079637f, rotationSpeed);
        p.Lifetime = num;
        p.UseLifetimeArea = false;
      }
      else
      {
        p.Initialize(where, direction.Multiply((double) number1), direction.Multiply((double) number2), scale, angle - 1.57079637f, rotationSpeed);
        p.LifetimeAreaX = this.LifetimeAreaX;
        p.LifetimeAreaY = this.LifetimeAreaY;
        p.UseLifetimeArea = true;
      }
    }

    private Vector2 GetDirection(float angle)
    {
      return new Vector2(Math.Cos((double) angle), Math.Sin((double) angle));
    }

    protected virtual float PickRandomAngle() => this.RandomBetween(this.minAngle, this.maxAngle);

    public float RandomBetween(float min, float max)
    {
      return min + (float) this.rand.NextDouble() * (max - min);
    }

    public override void Update(float elapsedTime_s, float totalTime_s)
    {
      for (int index = 0; index < this.Particles.Length; ++index)
      {
        SimpleParticle particle = this.Particles[index];
        if (particle.Active)
        {
          particle.Update(elapsedTime_s, totalTime_s);
          if (!particle.Active)
            this.freeParticles.Enqueue(particle);
        }
      }
    }

    public override void Draw(ISpriteBatch spriteBatch)
    {
      for (int index = 0; index < this.Particles.Length; ++index)
      {
        SimpleParticle particle = this.Particles[index];
        if (particle.Active)
          particle.ParticleSprite.Draw(spriteBatch);
      }
    }
  }
}
