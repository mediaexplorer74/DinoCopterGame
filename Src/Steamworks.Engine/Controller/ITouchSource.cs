// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Controller.ITouchSource
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;
using System.Collections.Generic;

#nullable disable
namespace Steamworks.Engine.Controller
{
  public interface ITouchSource
  {
    List<Vector2> GetStateWithMovement();

    List<Vector2> GetState(bool Move, bool Up, bool Down);

    float Scale { get; set; }
  }
}
