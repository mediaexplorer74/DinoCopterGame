// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.Entity
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework.Content;
using Steamworks.Engine.Common;
using System;
using System.Collections.Generic;


namespace Steamworks.Engine.Graphics
{
  public class Entity : IDisposable, IEntity, IUpdateable, IPositionable
  {
    private object _tag;
    private FlipDirection _flip;
    private List<IEntity> Children;
    private bool _alphaFadeComplete;
    private IEntity _parent;
    private float _x;
    private float _y;
    private Vector2 _position = Vector2.Zero;
    private Vector2 _center = Vector2.Zero;
    private float _width;
    private float _height;
    protected bool WidthSet;
    protected bool HeightSet;
    private bool _visible = true;
    protected bool ScaleSet;
    protected float _scale = 1f;
    private float _rotation;
    private float _rotationVelocity;
    protected bool disposed;
    private float _alpha = 1f;
    private float AlphaFrom = 1f;
    private float AlphaTo = 1f;
    private float AlphaFadeTime;

    public object Tag
    {
      get => this._tag;
      set => this._tag = value;
    }

    public FlipDirection Flip
    {
      get => this._flip;
      set => this._flip = value;
    }

    public bool AlphaFadeComplete
    {
      get => this._alphaFadeComplete;
      set => this._alphaFadeComplete = value;
    }

    public bool HasParent => this.Parent != null;

    public IEntity Parent
    {
      get => this._parent;
      set => this._parent = value;
    }

    public Vector2 Center
    {
      get => this._center;
      set => this._center = value;
    }

    public float Y
    {
      get => !this.HasParent ? this._y : this.Parent.Y + this._y;
      set => this._y = value;
    }

    public float X
    {
      get => !this.HasParent ? this._x : this.Parent.X + this._x;
      set => this._x = value;
    }

    public Vector2 Position
    {
      get
      {
        this._position.X = this.X;
        this._position.Y = this.Y;
        return this._position;
      }
    }

    public Vector2 CenterPosition
    {
      get
      {
        this._position.X = this.X + this._center.X * this.Scale;
        this._position.Y = this.Y + this._center.Y * this.Scale;
        return this._position;
      }
    }

    public float Width
    {
      get => this._width;
      set
      {
        this._width = value;
        this._center.X = this._width / 2f;
        this.WidthSet = true;
      }
    }

    public float Height
    {
      get => this._height;
      set
      {
        this._height = value;
        this._center.Y = this._height / 2f;
        this.HeightSet = true;
      }
    }

    public float WidthScaled => this._width * this.Scale;

    public float HeightScaled => this._height * this.Scale;

    public bool Visible
    {
      get => this._visible;
      set => this._visible = value;
    }

    public float Scale
    {
      get => !this.HasParent ? this._scale : this._scale * this.Parent.Scale;
      set
      {
        this._scale = value;
        this.ScaleSet = true;
      }
    }

    public float Rotation
    {
      get => !this.HasParent ? this._rotation : this._rotation + this.Parent.Rotation;
      set => this._rotation = value;
    }

    public float RotationVelocity
    {
      get => this._rotationVelocity;
      set => this._rotationVelocity = value;
    }

    public void SetParent(IEntity entity) => this.Parent = entity;

    public virtual void ClearChildren()
    {
      if (this.Children == null)
        return;
      List<IEntity> entityList = new List<IEntity>();
      entityList.AddRange((IEnumerable<IEntity>) this.Children);
      foreach (IEntity child in entityList)
        this.DetachChild(child);
    }

    public void AttachChild(IEntity child)
    {
      if (child.HasParent)
        throw new Exception("Child already have parent!");
      if (this.Children == null)
        this.Children = new List<IEntity>();
      if (this.Children.IndexOf(child) >= 0)
        return;
      child.SetParent((IEntity) this);
      this.Children.Add(child);
    }

    public void DetachChild(IEntity child)
    {
      if (this.Children == null || this.Children.IndexOf(child) < 0)
        return;
      child.SetParent((IEntity) null);
      this.Children.RemoveAt(this.Children.IndexOf(child));
    }

    public void CenterX(float ScreenWidth)
    {
      this.X = (float) ((double) ScreenWidth / 2.0 - (double) this.Width / 2.0);
    }

    public virtual void Update(float elapsedTime_s, float totalTime_s)
    {
      this.UpdateAlpha(elapsedTime_s);
      if (this.Children == null)
        return;
      foreach (Entity child in this.Children)
        child.Update(elapsedTime_s, totalTime_s);
    }

    private void UpdateAlpha(float elapsedTime_s)
    {
      float num1 = this.AlphaTo - this.AlphaFrom;
      float num2 = elapsedTime_s / this.AlphaFadeTime;
      if ((double) num1 != 0.0)
        this.Alpha += num1 * num2;
      if ((double) this.Alpha > 1.0)
        this.Alpha = 1f;
      if ((double) this.Alpha < 0.0)
        this.Alpha = 0.0f;
      if ((double) this.Alpha != (double) this.AlphaTo)
        return;
      this.AlphaFadeComplete = true;
    }

    public virtual void Draw(ISpriteBatch spriteBatch)
    {
      if (!this.Visible)
        return;
      this.OnDraw(spriteBatch);
      if (this.Children == null)
        return;
      foreach (Entity child in this.Children)
        child.Draw(spriteBatch);
    }

    public virtual void OnDraw(ISpriteBatch spriteBatch)
    {
    }

    public void CenterAtLocation(float PosX, float PosY)
    {
      this.X -= this.Width / 2f;
      this.Y -= this.Height / 2f;
    }

    public virtual bool IsTouched(Vector2 Position)
    {
      return (double) Position.X > (double) this.X && (double) Position.X < (double) this.X + (double) this.Width && (double) Position.Y > (double) this.Y && (double) Position.Y < (double) this.Y + (double) this.Height;
    }

    public virtual void LoadContent(ContentManager content)
    {
      if (this.Children == null)
        return;
      foreach (Entity child in this.Children)
        child.LoadContent(content);
    }

    ~Entity() => this.Dispose(false);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      int num = this.disposed ? 1 : 0;
      this.disposed = true;
    }

    public float Alpha
    {
      get => this._alpha;
      set => this._alpha = value;
    }

    public bool HasChildren => this.Children != null && this.Children.Count > 0;

    public bool HasChild(Entity childEntity)
    {
      return this.HasChildren && this.Children.IndexOf((IEntity) childEntity) >= 0;
    }

    public void FadeAlpha(float from, float to, float totaltime_s)
    {
      this.AlphaFadeComplete = false;
      this.Alpha = from;
      this.AlphaFrom = from;
      this.AlphaTo = to;
      this.AlphaFadeTime = totaltime_s;
      this.FadeAlphaChildren(from, to, totaltime_s);
    }

    protected virtual void FadeAlphaChildren(float from, float to, float totaltime_s)
    {
      if (this.Children == null)
        return;
      foreach (Entity child in this.Children)
        child.FadeAlpha(from, to, totaltime_s);
    }

    public void FadeIn(float totaltime_s) => this.FadeAlpha(0.0f, 1f, totaltime_s);

    public void FadeOut(float totaltime_s) => this.FadeAlpha(1f, 0.0f, totaltime_s);
  }
}
