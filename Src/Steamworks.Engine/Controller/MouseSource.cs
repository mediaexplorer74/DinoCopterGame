// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Controller.MouseSource
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework.Input;
using Steamworks.Engine.Common;
using System.Collections.Generic;

#nullable disable
namespace Steamworks.Engine.Controller
{
  public class MouseSource : ITouchSource
  {
    private bool _leftWasPressed;

    public List<Vector2> GetStateWithMovement() => this.GetState(true, true, true);

    public List<Vector2> GetState(bool Move, bool Up, bool Down)
    {
      List<Vector2> state1 = new List<Vector2>();
      MouseState state2 = Mouse.GetState();
      if (((this._leftWasPressed ? 0 : (state2.LeftButton == ButtonState.Pressed ? 1 : 0)) & (Down ? 1 : 0)) != 0)
        state1.Add(new Vector2((double) state2.Position.X / (double) this.Scale, (double) state2.Position.Y / (double) this.Scale));
      this._leftWasPressed = state2.LeftButton == ButtonState.Pressed;
      return state1;
    }

    public float Scale { get; set; } = 1f;
  }
}
