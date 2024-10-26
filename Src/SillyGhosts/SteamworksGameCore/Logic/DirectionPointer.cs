// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.DirectionPointer
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine.Common;
using Steamworks.Games.Game.Core.Interfaces;


namespace Steamworks.Games.Game.Core.Logic
{
  public class DirectionPointer : IDirectionPointer, IUpdateable, IVisibleObject, IPositionable
  {
    public float VisibilityDistance;
    private float _sourceX;
    private float _sourceY;
    private float _targetX;
    private float _targetY;
    private float _distanceFromSource;
    private float _x;
    private float _y;
    private float _width;
    private float _height;
    private bool _isVisible;

    public float SourceX
    {
      get => this._sourceX;
      set => this._sourceX = value;
    }

    public float SourceY
    {
      get => this._sourceY;
      set => this._sourceY = value;
    }

    public float TargetX
    {
      get => this._targetX;
      set => this._targetX = value;
    }

    public float TargetY
    {
      get => this._targetY;
      set => this._targetY = value;
    }

    public float DistanceFromSource
    {
      get => this._distanceFromSource;
      set => this._distanceFromSource = value;
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

    public float Width
    {
      get => this._width;
      set => this._width = value;
    }

    public float Height
    {
      get => this._height;
      set => this._height = value;
    }

    public void Update(float elapsedtime_s, float totaltime_s)
    {
      Vector2 vector2 = new Vector2((double) this.TargetX, (double) this.TargetY).Minus(new Vector2((double) this.SourceX, (double) this.SourceY));
      vector2 = vector2.Clamp(this.DistanceFromSource);
      this.X = this.SourceX + vector2.X;
      this.Y = this.SourceY + vector2.Y;
    }

    public bool IsVisible
    {
      get => this._isVisible;
      set => this._isVisible = value;
    }

    public float Rotation
    {
      get
      {
        return new Vector2((double) this.TargetX, (double) this.TargetY).Minus(new Vector2((double) this.SourceX, (double) this.SourceY)).Angle();
      }
      set
      {
      }
    }
  }
}
