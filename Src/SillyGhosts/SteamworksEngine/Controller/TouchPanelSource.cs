// Steamworks.Engine.Controller.TouchPanelSource

using Microsoft.Xna.Framework.Input.Touch;
using Steamworks.Engine.Common;
using System.Collections.Generic;


namespace Steamworks.Engine.Controller
{
  public class TouchPanelSource : ITouchSource
  {
    public List<Vector2> GetStateWithMovement()
    {
        return this.GetState(true, true, true);
    }

    public List<Vector2> GetState(bool Move, bool Up, bool Down)
    {
      List<Vector2> state = new List<Vector2>();

      foreach (TouchLocation touchLocation in TouchPanel.GetState())
      {
        if 
        ( 
            touchLocation.State == TouchLocationState.Moved & Move ||
            touchLocation.State == TouchLocationState.Pressed & Down ||
            touchLocation.State == TouchLocationState.Released & Up
        )
        {
            state.Add(new Vector2((double)touchLocation.Position.X,
                (double)touchLocation.Position.Y));
        }
      }
      return state;
    }

    public float Scale { get; set; }
  }
}
