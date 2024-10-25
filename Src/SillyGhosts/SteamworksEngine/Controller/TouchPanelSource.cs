// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Controller.TouchPanelSource
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework.Input.Touch;
using Steamworks.Engine.Common;
using System.Collections.Generic;

#nullable disable
namespace Steamworks.Engine.Controller
{
  public class TouchPanelSource : ITouchSource
  {
    public List<Vector2> GetStateWithMovement() => this.GetState(true, true, true);

    public List<Vector2> GetState(bool Move, bool Up, bool Down)
    {
      List<Vector2> state = new List<Vector2>();
      foreach (TouchLocation touchLocation in TouchPanel.GetState())
      {
        if (touchLocation.State == TouchLocationState.Moved & Move || touchLocation.State == TouchLocationState.Pressed & Down || touchLocation.State == TouchLocationState.Released & Up)
          state.Add(new Vector2((double) touchLocation.Position.X, (double) touchLocation.Position.Y));
      }
      return state;
    }

    public float Scale { get; set; }
  }
}
