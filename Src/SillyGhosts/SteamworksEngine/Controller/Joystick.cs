// Steamworks.Engine.Controller.Joystick

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;


namespace Steamworks.Engine.Controller
{
  public class Joystick : Sprite, IAnalogController, IEntity, IUpdateable, IPositionable
  {
    public double oldX = 0;
    public double oldY = 0;

    public ITouchSource TouchSource;

    public Joystick(ITouchSource touchSource)
    {
        this.TouchSource = touchSource;
    }

    public Joystick(TextureInfo textureInfo, ITouchSource touchSource)
      : base(textureInfo)
    {
      this.TouchSource = touchSource;
    }

    public Vector2 GetState()
    {

      float X = 0.0f;
      float Y = 0.0f;

       // keyboard events handling
       KeyboardState state = Keyboard.GetState();
      

      if (state.IsKeyDown(Keys.Up))
        Y = -1f;
      if (state.IsKeyDown(Keys.Down))
        Y = 1f;
      if (state.IsKeyDown(Keys.Left))
        X = -1f;
      if (state.IsKeyDown(Keys.Right))
        X = 1f;

            //-----------------------------------------------
            foreach (TouchLocation touchLocation in TouchPanel.GetState())
            {
                if
                (
                    touchLocation.State == TouchLocationState.Moved ||
                    touchLocation.State == TouchLocationState.Pressed ||
                    touchLocation.State == TouchLocationState.Released 
                )
                {
                    double newX = (double)touchLocation.Position.X;
                    double newY = (double)touchLocation.Position.Y;
                    if (newY < oldY)
                        Y = -1f;
                    if (newY > oldY)
                        Y = 1f;
                    if (newX < oldX)
                        X = -1f;
                    if (newX > oldX)
                        X = 1f;
                    oldX = newX;
                    oldY = newY;
                }
            }
            //-----------------------------------------------

            return new Vector2((double) X, (double) Y);
    }
  }
}
