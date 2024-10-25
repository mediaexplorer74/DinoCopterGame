// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Controller.BetterKeyboard
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework.Input;

#nullable disable
namespace Steamworks.Engine.Controller
{
  public class BetterKeyboard
  {
    private KeyboardState _keyboardState;
    private KeyboardState _oldState;

    public BetterKeyboard()
    {
      this._oldState = Keyboard.GetState();
      this._keyboardState = Keyboard.GetState();
    }

    public void Update()
    {
      this._oldState = this._keyboardState;
      this._keyboardState = Keyboard.GetState();
    }

    public bool WasPressed(Keys key)
    {
      return this._oldState.IsKeyUp(key) && this._keyboardState.IsKeyDown(key);
    }

    public bool IsKeyDown(Keys key) => this._keyboardState.IsKeyDown(key);

    public bool IsKeyUp(Keys key) => this._keyboardState.IsKeyUp(key);
  }
}
