// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.TMXMapEntity
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;
using Steamworks.Shared.Map;
using System.Collections.Generic;


namespace Steamworks.Engine.Graphics
{
  public class TMXMapEntity : Entity, ILayeredEntity, IEntity, IUpdateable, IPositionable
  {
    private TMXMapData tMXMap;
    private List<ILayerEntity> Layers = new List<ILayerEntity>();

    public List<ILayerEntity> GetLayerEntities() => this.Layers;

    public TMXMapEntity(TMXMapData tMXMap, ITextureManager texManager)
    {
      this.tMXMap = tMXMap;
      foreach (TMXLayer layer in tMXMap.layers)
      {
        LayerEntity layerEntity = new LayerEntity();
        layerEntity.Parallax = layer.Parallax;
        this.Layers.Add((ILayerEntity) layerEntity);
        for (int x = 0; x < tMXMap.width; ++x)
        {
          for (int y = 0; y < tMXMap.height; ++y)
          {
            TMXLayerTile tile = layer.GetTile(x, y);
            if (tile.gid > 0)
            {
              TMXTileset tileset = tMXMap.GetTileset(tile);
              Sprite child = new Sprite(texManager.Get(tileset.image.source));
              child.X = (float) (x * tMXMap.tilewidth);
              child.Y = (float) (y * tMXMap.tileheight);
              child.Width = 48f;
              child.Height = 48f;
              child.SourceRectangle = this.GetSourceRectangle(tile, tileset);
              layerEntity.AttachChild((IEntity) child);
            }
          }
        }
      }
    }

    private RectangleF GetSourceRectangle(TMXLayerTile tile, TMXTileset tileset)
    {
      int num1 = tile.gid - tileset.firstgid;
      int num2 = tileset.width / tileset.tilewidth;
      int num3 = num1 % num2;
      int num4 = num1 / num2;
      return new RectangleF((float) (num3 * tileset.tilewidth), (float) (num4 * tileset.tileheight), (float) tileset.tilewidth, (float) tileset.tileheight);
    }
  }
}
