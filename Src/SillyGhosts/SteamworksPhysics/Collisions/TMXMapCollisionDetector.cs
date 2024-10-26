// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Collisions.TMXMapCollisionDetector
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;
using Steamworks.Shared.Map;
using System;
using System.Collections.Generic;


namespace Steamworks.Physics.Collisions
{
  public class TMXMapCollisionDetector : ICollisionDetector
  {
    private TMXMapData Map;

    public TMXMapCollisionDetector(TMXMapData Map)
    {
      this.Map = Map != null ? Map : throw new ArgumentNullException(nameof (Map));
    }

    public List<Collision> GetCollision(RectangleF Bounds)
    {
      List<Collision> collision = new List<Collision>();
      TMXLayer tmxLayer = (TMXLayer) null;
      foreach (TMXLayer layer in this.Map.layers)
      {
        if (layer.BlockingLayer)
        {
          tmxLayer = layer;
          break;
        }
      }
      if (tmxLayer != null)
      {
        int num1 = (int) Math.Floor((double) Bounds.Left / (double) this.Map.tilewidth);
        int num2 = (int) Math.Ceiling((double) Bounds.Right / (double) this.Map.tilewidth);
        int num3 = (int) Math.Floor((double) Bounds.Top / (double) this.Map.tileheight);
        int num4 = (int) Math.Ceiling((double) Bounds.Bottom / (double) this.Map.tileheight);
        for (int x = num1; x < num2; ++x)
        {
          for (int y = num3; y < num4; ++y)
          {
            if (tmxLayer.ContainsTileAt(x, y) && tmxLayer.GetTile(x, y).gid > 0)
            {
              RectangleF tileBounds = this.Map.GetTileBounds(x, y);
              Collision collisionForBounds = Bounds.GetCollisionForBounds(tileBounds);
              if (collisionForBounds.Direction != CollisionDirection.None)
                collision.Add(collisionForBounds);
            }
          }
        }
      }
      return collision;
    }
  }
}
