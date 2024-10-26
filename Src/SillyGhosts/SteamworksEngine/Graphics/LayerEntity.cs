// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.LayerEntity
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;


namespace Steamworks.Engine.Graphics
{
  public class LayerEntity : Entity, ILayerEntity, IEntity, IUpdateable, IPositionable
  {
    private float _parallax;

    public float Parallax
    {
      get => this._parallax;
      set => this._parallax = value;
    }
  }
}
