// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Controller.Joystick
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework.Input;
using Steamworks.Engine.Common;
using Steamworks.Engine.Graphics;

#nullable disable
namespace Steamworks.Engine.Controller
{
  public class Joystick : Sprite, IAnalogController, IEntity, IUpdateable, IPositionable
  {
    public ITouchSource TouchSource;

    public Joystick(ITouchSource touchSource) => this.TouchSource = touchSource;

    public Joystick(TextureInfo textureInfo, ITouchSource touchSource)
      : base(textureInfo)
    {
      this.TouchSource = touchSource;
    }

    public Vector2 GetState()
    {
      KeyboardState state = Keyboard.GetState();
      float X = 0.0f;
      float Y = 0.0f;
      if (state.IsKeyDown(Keys.Up))
        Y = -1f;
      if (state.IsKeyDown(Keys.Down))
        Y = 1f;
      if (state.IsKeyDown(Keys.Left))
        X = -1f;
      if (state.IsKeyDown(Keys.Right))
        X = 1f;
      return new Vector2((double) X, (double) Y);
    }
  }
}
