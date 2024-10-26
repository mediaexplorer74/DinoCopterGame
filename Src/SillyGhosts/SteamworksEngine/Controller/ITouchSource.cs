// Steamworks.Engine.Controller.ITouchSource

using Steamworks.Engine.Common;
using System.Collections.Generic;


namespace Steamworks.Engine.Controller
{
  public interface ITouchSource
  {
    List<Vector2> GetStateWithMovement();

    List<Vector2> GetState(bool Move, bool Up, bool Down);

    float Scale { get; set; }
  }
}
