// Steamworks.Engine.Controller.MouseSource


using Microsoft.Xna.Framework.Input;
using Steamworks.Engine.Common;
using System.Collections.Generic;


namespace Steamworks.Engine.Controller
{
  public class MouseSource : ITouchSource
  {
    private bool _leftWasPressed;

    public List<Vector2> GetStateWithMovement()
    {
        return this.GetState(true, true, true);
    }

    public List<Vector2> GetState(bool Move, bool Up, bool Down)
    {
      List<Vector2> state1 = new List<Vector2>();
      MouseState state2 = Mouse.GetState();

      if
      (
        ((this._leftWasPressed
            ? 0
            : (state2.LeftButton == ButtonState.Pressed ? 1 : 0)) & (Down ? 1 : 0)) != 0
      )
      {
        state1.Add(new Vector2((double)state2.Position.X /
            (double)this.Scale, (double)state2.Position.Y / (double)this.Scale));
      }
      
      this._leftWasPressed = state2.LeftButton == ButtonState.Pressed;
      return state1;
    }

    public float Scale { get; set; } = 1f;
  }
}
