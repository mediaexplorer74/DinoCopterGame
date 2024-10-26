// Steamworks.Engine.Controller.MultiMouseSource


using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Steamworks.Engine.Common;
using System.Collections.Generic;


namespace Steamworks.Engine.Controller
{
  public class MultiMouseSource : ITouchSource
  {
    private bool _leftWasPressed;

    public List<Vector2> GetStateWithMovement()
    {
        return this.GetState(true, true, true);
    }

    public List<Vector2> GetState(bool Move, bool Up, bool Down)
    {
      List<Vector2> state = new List<Vector2>();
      //List<Vector2> state1 = new List<Vector2>();
      MouseState state2 = Mouse.GetState();

      // mouse events handling... 
      if (((this._leftWasPressed 
      ? 0 
      : (state2.LeftButton == ButtonState.Pressed ? 1 : 0)) & (Down ? 1 : 0)) != 0 )
      {
        state.Add(new Vector2((double)state2.Position.X /
            (double)this.Scale, (double)state2.Position.Y / (double)this.Scale));
      }

      //touch panel events handling...
      foreach (TouchLocation touchLocation in TouchPanel.GetState())
      {
            if ( touchLocation.State == TouchLocationState.Moved & Move
            || touchLocation.State == TouchLocationState.Pressed & Down
            || touchLocation.State == TouchLocationState.Released & Up )
            {
                state.Add
                (
                    //RnD: Scale
                    new Vector2(
                                 (double)touchLocation.Position.X /
                                           (double)this.Scale,
                                 (double)touchLocation.Position.Y /
                                    (double)this.Scale
                               )
                );
            }
      }

      this._leftWasPressed = state2.LeftButton == ButtonState.Pressed;
      return state;
    }

    //public float Scale { get; set; } = 1f; 
    public float Scale { get; set; } // ?
    }
}
