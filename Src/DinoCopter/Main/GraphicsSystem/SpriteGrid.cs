// GameManager.GraphicsSystem.SpriteGrid

using GameManager.Utils;
using System;
using System.Collections.Generic;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class SpriteGrid
  {
    public List<Sprite> OutOfGrid;

    public int W { get; private set; }

    public int H { get; private set; }

    public Point Size { get; private set; }

    public Point TileSize { get; private set; }

    public List<Sprite>[][] Buckets { get; set; }

    public SpriteGrid()
      : this(new Point(), 0, 0)
    {
      this.TileSize = new Point();
      this.OutOfGrid = new List<Sprite>();
    }

    public SpriteGrid(Point _size, int _w, int _h)
    {
      this.Size = _size;
      this.W = _w;
      this.H = _h;
      this.TileSize = new Point(this.Size.X / (float) this.W, this.Size.Y / (float) this.H);
      this.Buckets = new List<Sprite>[this.W][];
      for (int index1 = 0; index1 < this.W; ++index1)
      {
        this.Buckets[index1] = new List<Sprite>[this.H];
        for (int index2 = 0; index2 < this.H; ++index2)
          this.Buckets[index1][index2] = new List<Sprite>();
      }
      this.OutOfGrid = new List<Sprite>();
    }

    public void AddSprite(Sprite s)
    {
      Point pos = s.GetPos();
      Point size = s.GetSize();
      if ((double) pos.X < 0.0 || (double) pos.Y < 0.0 || (double) pos.X + (double) size.X > (double) this.Size.X || (double) pos.Y + (double) size.Y > (double) this.Size.Y)
        this.OutOfGrid.Add(s);
      if ((double) pos.X > (double) this.Size.X || (double) pos.Y > (double) this.Size.Y || (double) pos.X + (double) size.X < 0.0 || (double) pos.Y + (double) size.Y < 0.0)
        return;
      Point point1 = pos / this.TileSize;
      int num1 = Math.Max(0, (int) point1.X);
      int num2 = Math.Max(0, (int) point1.Y);
      Point point2 = (pos + size) / this.TileSize;
      int num3 = Math.Min(this.W - 1, (int) point2.X);
      int num4 = Math.Min(this.H - 1, (int) point2.Y);
      for (int index1 = num1; index1 <= num3; ++index1)
      {
        for (int index2 = num2; index2 <= num4; ++index2)
          this.Buckets[index1][index2].Add(s);
      }
    }

    public void RemoveSprite(Sprite s)
    {
      Point pos = s.GetPos();
      Point size = s.GetSize();
      if ((double) pos.X < 0.0 || (double) pos.Y < 0.0 || (double) pos.X + (double) size.X > (double) this.Size.X || (double) pos.Y + (double) size.Y > (double) this.Size.Y)
        this.OutOfGrid.Remove(s);
      if ((double) pos.X > (double) this.Size.X || (double) pos.Y > (double) this.Size.Y || (double) pos.X + (double) size.X < 0.0 || (double) pos.Y + (double) size.Y < 0.0)
        return;
      Point point1 = pos / this.TileSize;
      int num1 = Math.Max(0, (int) point1.X);
      int num2 = Math.Max(0, (int) point1.Y);
      Point point2 = (pos + size) / this.TileSize;
      int num3 = Math.Min(this.W - 1, (int) point2.X);
      int num4 = Math.Min(this.H - 1, (int) point2.Y);
      for (int index1 = num1; index1 <= num3; ++index1)
      {
        for (int index2 = num2; index2 <= num4; ++index2)
          this.Buckets[index1][index2].Remove(s);
      }
    }

    public void SpritesOverlaping(
      Sprite s,
      Sprite[] collisions,
      ref int collisionsNum,
      Sprite[] checks,
      ref int checksNum)
    {
      Point pos1 = s.GetPos();
      Point size = s.GetSize();
      if ((double) pos1.X < 0.0 || (double) pos1.Y < 0.0 || (double) pos1.X + (double) size.X > (double) this.Size.X || (double) pos1.Y + (double) size.Y > (double) this.Size.Y)
      {
        foreach (Sprite sprite in this.OutOfGrid)
        {
          checks[checksNum] = sprite;
          if (s != checks[checksNum])
          {
            Point pos2 = checks[checksNum].Pos;
            float width = checks[checksNum].GetWidth();
            float height = checks[checksNum].GetHeight();
            if (Util.RectsOverlaps(pos2.X, pos2.Y, pos2.X + width, pos2.Y + height, s.Pos.X, s.Pos.Y, s.Pos.X + s.GetWidth(), s.Pos.Y + s.GetHeight()))
              collisions[collisionsNum++] = checks[checksNum];
            checks[checksNum++].WasCollisionChecked = true;
          }
        }
      }
      if ((double) pos1.X > (double) this.Size.X || (double) pos1.Y > (double) this.Size.Y || (double) pos1.X + (double) size.X < 0.0 || (double) pos1.Y + (double) size.Y < 0.0)
        return;
      Point point1 = pos1 / this.TileSize;
      int num1 = Math.Max(0, (int) point1.X);
      int num2 = Math.Max(0, (int) point1.Y);
      Point point2 = (pos1 + size) / this.TileSize;
      int num3 = Math.Min(this.W - 1, (int) point2.X);
      int num4 = Math.Min(this.H - 1, (int) point2.Y);
      for (int index1 = num1; index1 <= num3; ++index1)
      {
        for (int index2 = num2; index2 <= num4; ++index2)
        {
          foreach (Sprite sprite in this.Buckets[index1][index2])
          {
            checks[checksNum] = sprite;
            Point pos3 = checks[checksNum].Pos;
            float width = checks[checksNum].GetWidth();
            float height = checks[checksNum].GetHeight();
            if (!checks[checksNum].WasCollisionChecked && Util.RectsOverlaps(pos3.X, pos3.Y, pos3.X + width, pos3.Y + height, s.Pos.X, s.Pos.Y, s.Pos.X + s.GetWidth(), s.Pos.Y + s.GetHeight()))
              collisions[collisionsNum++] = checks[checksNum];
            checks[checksNum++].WasCollisionChecked = true;
          }
        }
      }
    }

    public void Dispose() => this.Buckets = (List<Sprite>[][]) null;
  }
}
