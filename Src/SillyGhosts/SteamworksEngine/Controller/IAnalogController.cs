// Steamworks.Engine.Controller.IAnalogController


using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;


namespace Steamworks.Engine.Controller
{
  public interface IAnalogController : IEntity, IUpdateable, IPositionable
  {
    Vector2 GetState();
  }
}
