// GameManager.GraphicsSystem.Disp

using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class Disp
  {
    protected SpriteGrid[] Grids;
    public Disp Ref;

    public Point Translate { get; protected set; }

    public float Width { get; protected set; }

    public float Height { get; protected set; }

    public float GameTime { get; protected set; }

    public int LayersNum { get; protected set; }

    public List<Sprite>[] Sprites { get; protected set; }

    public Point ViewPos { get; protected set; }

    public Point ViewSize { get; protected set; }

    public Disp()
    {
    }

    protected Disp(int layersNum)
    {
      this.Translate = new Point();
      this.GameTime = 0.0f;
      this.LayersNum = layersNum;
      this.Sprites = new List<Sprite>[layersNum];
      for (int index = 0; index < this.Sprites.Length; ++index)
        this.Sprites[index] = new List<Sprite>();
      this.Width = (float) GlobalMembers.Manager.GetWidth();
      this.Height = (float) GlobalMembers.Manager.GetHeight();
      this.Grids = new SpriteGrid[layersNum];
      for (int index = 0; index < layersNum; ++index)
        this.Grids[index] = (SpriteGrid) null;
      this.ViewSize = new Point(GlobalMembers.FromPx(this.Width), GlobalMembers.FromPx(this.Height));
    }

        public void Display()
        {
            GlobalMembers.Manager.SetNextDisp(this.Ref);
        }

        public float GetWidth() => this.Width;

    public float GetHeight() => this.Height;

    public int GetSpriteLayersNum() => this.LayersNum;

    public virtual void PointerPressed(Point pos, int id)
    {
    }

    public virtual void PointerDragged(Point pos, int id)
    {
    }

    public virtual void PointerReleased(Point pos, int id)
    {
    }

    public virtual void OnShow()
    {
    }

    public virtual void OnHide()
    {
    }

    public virtual void Load()
    {
    }

    public void AddSprite(Sprite sprite, int layer)
    {
      this.Sprites[layer].Add(sprite);
      sprite.Parent = this.Ref;
      sprite.Layer = layer;
      sprite.OnAdd();
    }

    public virtual void Update(float time)
    {
      for (int index1 = 0; index1 < this.LayersNum; ++index1)
      {
        for (int index2 = 0; index2 < this.Sprites[index1].Count; ++index2)
          this.Sprites[index1][index2].Update(time);
      }
      this.CleanupRemovedSprites();
      this.GameTime += time;
    }

        public float GetGameTime()
        {
            return this.GameTime;
        }

        public virtual void Render(SpriteBatch spriteBatch)
    {
      Point translate = GlobalMembers.Manager.GetTranslate();
      for (int index = 0; index < this.LayersNum; ++index)
      {
        foreach (Sprite sprite in this.Sprites[index])
        {
          if (Util.RectsOverlaps(GlobalMembers.FromPx(-translate.X), 
              GlobalMembers.FromPx(-translate.Y), GlobalMembers.FromPx(-translate.X) + this.ViewSize.X,
              GlobalMembers.FromPx(-translate.Y) + this.ViewSize.Y, 
              sprite.Pos.X, sprite.Pos.Y, sprite.Pos.X + sprite.GetWidth(), 
              sprite.Pos.Y + sprite.GetHeight()))
            sprite.Render(spriteBatch);
        }
      }
    }

    public Sprite SpriteAt(Point pos)
    {
      for (int index1 = this.LayersNum - 1; index1 >= 0; --index1)
      {
        for (int index2 = 0; index2 < this.Sprites[index1].Count; ++index2)
        {
          Sprite sprite = this.Sprites[index1][index2];
          if ((double) GlobalMembers.FromPx(pos.X) >= (double) sprite.GetPos().X 
          && (double) GlobalMembers.FromPx(pos.X) < (double) sprite.GetPos().X + (double) sprite.GetWidth() 
          && (double) GlobalMembers.FromPx(pos.Y) < (double) sprite.GetPos().Y + (double) sprite.GetHeight() 
          && (double) GlobalMembers.FromPx(pos.Y) >= (double) sprite.GetPos().Y)
            return sprite;
        }
      }
      return new Sprite();
    }

    public void CleanupRemovedSprites()
    {
      for (int index1 = 0; index1 < this.LayersNum; ++index1)
      {
        for (int index2 = 0; index2 < this.Sprites[index1].Count; ++index2)
        {
          if (this.Sprites[index1][index2].IsToRemove())
          {
            this.Sprites[index1][index2].OnRemove();
            this.Sprites[index1][index2] = this.Sprites[index1][this.Sprites[index1].Count - 1];
            this.Sprites[index1].RemoveAt(this.Sprites[index1].Count - 1);
          }
        }
      }
    }

        public SpriteGrid GetGrid(int layer)
        {
            return this.Grids[layer];
        }

        public List<Sprite> GetSpriteLayer(int layer)
        {
            return this.Sprites[layer];
        }
    }
}
