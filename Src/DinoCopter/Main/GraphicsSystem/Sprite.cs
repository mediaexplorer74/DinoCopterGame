// Decompiled with JetBrains decompiler
// Type: GameManager.GraphicsSystem.Sprite
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

using GameManager.GameElements;
using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class Sprite
  {
    protected Paintable Img;
    private static Sprite[] collisionChecked = new Sprite[100];
    private static Sprite[] collision = new Sprite[100];

    public Point Pos { get; private set; }

    public Disp Parent { get; set; }

    public Sprite Ref { get; set; }

    public int Layer { get; set; }

    public int TypeId { get; protected set; }

    public Point Speed { get; protected set; }

    public Point OldSpeed { get; set; }

    public Point Acce { get; protected set; }

    public Point OldPos { get; protected set; }

    public Point Size { get; protected set; }

    public Point Force { get; protected set; }

    public float LiveTime { get; set; }

    public bool ToRemove { get; protected set; }

    public bool Single { get; set; }

    public bool WasCollisionChecked { get; set; }

    public Sprite()
    {
    }

    protected Sprite(int typeId, Point pos)
    {
      this.WasCollisionChecked = false;
      this.TypeId = typeId;
      this.Pos = pos;
      this.OldPos = new Point();
      this.LiveTime = -1f;
      this.ToRemove = false;
      this.Speed = new Point(0.0f, 0.0f);
      this.OldSpeed = new Point(0.0f, 0.0f);
      this.Single = false;
      this.Layer = 0;
      this.SetPaintable(Paintable.CreateFilledRect(150f, 20f, Util.ConvertIntToColor(4278190335U)));
      this.Acce = new Point();
    }

    public static Sprite CreateSprite(int _typeId, Point _pos)
    {
      Sprite sprite = new Sprite(_typeId, _pos);
      sprite.Ref = sprite;
      return sprite;
    }

    public virtual void Render(SpriteBatch spriteBatch)
    {
      this.Img.Paint(GlobalMembers.ToPx(this.Pos.X), GlobalMembers.ToPx(this.Pos.Y), spriteBatch);
    }

    public virtual void Update(float time)
    {
      this.Img.Update(time);
      this.OldPos = new Point(this.Pos);
      if ((double) this.Speed.X != 0.0 || (double) this.Speed.Y != 0.0)
      {
        this.Speed += this.Acce * time;
        SpriteGrid grid = this.Parent.GetGrid(this.Layer);
        grid?.RemoveSprite(this.Ref);
        this.Pos += this.Speed * time;
        grid?.AddSprite(this.Ref);
      }
      if ((double) this.LiveTime > 0.0 && (double) this.LiveTime <= (double) this.Parent.GetGameTime() || this.Single && this.Img.GetAnimationCyclesNum() > 0)
        this.Remove();
      this.Force = (this.Speed - this.OldSpeed) / time;
      this.OldSpeed = new Point(this.Speed);
    }

    public virtual void OnAdd() => this.Parent.GetGrid(this.Layer)?.AddSprite(this.Ref);

    public virtual void OnRemove() => this.Parent.GetGrid(this.Layer)?.RemoveSprite(this.Ref);

    public virtual float GetWidth() => this.Size.X;

    public virtual float GetHeight() => this.Size.Y;

    public void SetPaintable(Paintable _img)
    {
      if (this.Parent != null)
      {
        SpriteGrid grid = this.Parent.GetGrid(this.Layer);
        if ((double) this.Img.GetWidth() != (double) _img.GetWidth() || (double) this.Img.GetHeight() != (double) _img.GetHeight())
        {
          grid?.RemoveSprite(this.Ref);
          this.Img = _img;
          this.Size = new Point(GlobalMembers.FromPx(_img.GetWidth()), GlobalMembers.FromPx(_img.GetHeight()));
          grid?.AddSprite(this.Ref);
        }
        else
        {
          this.Img = _img;
          this.Size = new Point(GlobalMembers.FromPx(_img.GetWidth()), GlobalMembers.FromPx(_img.GetHeight()));
        }
      }
      else
      {
        this.Img = _img;
        this.Size = new Point(GlobalMembers.FromPx(_img.GetWidth()), GlobalMembers.FromPx(_img.GetHeight()));
      }
    }

    public void SetSize(Point newSize)
    {
      if (this.Parent == null)
      {
        this.Size = newSize;
      }
      else
      {
        SpriteGrid grid = this.Parent.GetGrid(this.Layer);
        grid?.RemoveSprite(this.Ref);
        this.Size = newSize;
        grid?.AddSprite(this.Ref);
      }
    }

    public void SetSingle(bool _single) => this.Single = _single;

    public void SetLiveTime(float _liveTime)
    {
      this.LiveTime = this.Parent.GetGameTime() + _liveTime;
    }

    public void Remove() => this.ToRemove = true;

    public bool IsToRemove() => this.ToRemove;

    public void SetAcceleration(Point _acce) => this.Acce = _acce;

    public void SetSpeed(Point _speed) => this.Speed = _speed;

    public void SetPos(Point _pos)
    {
      if (this.Parent == null)
      {
        this.Pos = _pos;
      }
      else
      {
        SpriteGrid grid = this.Parent.GetGrid(this.Layer);
        grid?.RemoveSprite(this.Ref);
        this.Pos = _pos;
        grid?.AddSprite(this.Ref);
      }
    }

    public void SetParent(Disp _parent) => this.Parent = _parent;

    public Paintable getImg() => this.Img;

    public Point GetAcceleration() => new Point(this.Acce);

    public Point GetSpeed() => new Point(this.Speed);

    public Point GetPos() => new Point(this.Pos);

    public Point GetSize() => new Point(this.GetWidth(), this.GetHeight());

    public Point GetOldPos() => new Point(this.OldPos);

    public Point GetMove() => this.Pos - this.OldPos;

    public Point GetForce() => new Point(this.Force);

    public Paintable GetPaintable() => this.Img;

    public int GetTypeId() => this.TypeId;

    public void SetTypeId(int _typeId) => this.TypeId = _typeId;

    public void AlignTo(Sprite s, int collisionDir)
    {
      if (collisionDir == 0)
        return;
      if ((collisionDir & 2) > 0)
        this.Pos.Y = s.Pos.Y - this.GetHeight();
      if ((collisionDir & 8) > 0)
        this.Pos.Y = s.Pos.Y + s.GetHeight();
      if ((collisionDir & 4) > 0)
        this.Pos.X = s.Pos.X - this.GetWidth();
      if ((collisionDir & 1) <= 0)
        return;
      this.Pos.X = s.Pos.X + s.GetWidth();
    }

    public void CheckCollisions(int layer)
    {
      Player player = this as Player;
      Disp parent = this.Parent;
      SpriteGrid grid = parent.GetGrid(layer);
      if (grid != null)
      {
        int checksNum = 0;
        int collisionsNum = 0;
        grid.SpritesOverlaping(this.Ref, Sprite.collision, ref collisionsNum, Sprite.collisionChecked, ref checksNum);
        for (int index = 0; index < collisionsNum; ++index)
          this.OnCollision(Sprite.collision[index]);
        for (int index = 0; index < checksNum; ++index)
        {
          Sprite.collisionChecked[index].WasCollisionChecked = false;
          Sprite.collisionChecked[index] = (Sprite) null;
        }
        for (int index = 0; index < collisionsNum; ++index)
          Sprite.collisionChecked[index] = (Sprite) null;
      }
      else
      {
        List<Sprite> spriteLayer = parent.GetSpriteLayer(layer);
        for (int index = 0; index < spriteLayer.Count; ++index)
        {
          Sprite sprite = spriteLayer[index];
          if (this != sprite && Util.RectsOverlaps(this.Pos.X, this.Pos.Y, this.Pos.X + this.GetWidth(), this.Pos.Y + this.GetHeight(), sprite.Pos.X, sprite.Pos.Y, sprite.Pos.X + sprite.GetWidth(), sprite.Pos.Y + sprite.GetHeight()))
            this.OnCollision(spriteLayer[index]);
        }
      }
    }

    public virtual void OnCollision(Sprite s)
    {
    }

    public int collisionDirection(float lx, float uy, float rx, float dy, float opx, float opy)
    {
      double x = (double) this.OldPos.X;
      double y = (double) this.OldPos.Y;
      float num1 = this.Pos.X - this.OldPos.X - (lx - opx);
      float num2 = this.Pos.Y - this.OldPos.Y - (uy - opy);
      return Util.Includes(lx, rx, this.Pos.X, this.Pos.X + this.GetWidth()) ? ((double) num2 <= 0.0 ? 8 : 2) : (Util.Includes(uy, dy, this.Pos.Y, this.Pos.Y + this.GetHeight()) ? ((double) num1 <= 0.0 ? 1 : 4) : ((double) this.Pos.X >= (double) lx && (double) this.Pos.X < (double) rx ? ((double) this.Pos.Y >= (double) uy && (double) this.Pos.Y < (double) dy ? (!Util.LineIntersectHorizontalLine(this.Pos.X - num1, this.Pos.Y - num2, this.Pos.X, this.Pos.Y, dy, lx, rx) ? 1 : 8) : (!Util.LineIntersectHorizontalLine(this.Pos.X - num1, this.Pos.Y - num2 + this.GetHeight(), this.Pos.X, this.Pos.Y + this.GetHeight(), uy, lx, rx) ? 1 : 2)) : ((double) this.Pos.Y >= (double) uy && (double) this.Pos.Y < (double) dy ? (!Util.LineIntersectHorizontalLine(this.Pos.X - num1 + this.GetWidth(), this.Pos.Y - num2, this.Pos.X + this.GetWidth(), this.Pos.Y, dy, lx, rx) ? 4 : 8) : (!Util.LineIntersectHorizontalLine(this.Pos.X - num1 + this.GetWidth(), this.Pos.Y - num2 + this.GetHeight(), this.Pos.X + this.GetWidth(), this.Pos.Y + this.GetHeight(), uy, lx, rx) ? 4 : 2))));
    }
  }
}
