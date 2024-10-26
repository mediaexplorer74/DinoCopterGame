// Steamworks.Engine.Controller.BetterKeyboard


using Microsoft.Xna.Framework.Input;


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

        public bool IsKeyDown(Keys key)
        {
            return this._keyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return this._keyboardState.IsKeyUp(key);
        }
    }
}
