// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.ILayeredEntity
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;
using System.Collections.Generic;


namespace Steamworks.Engine.Graphics
{
  public interface ILayeredEntity : IEntity, IUpdateable, IPositionable
  {
    List<ILayerEntity> GetLayerEntities();
  }
}
