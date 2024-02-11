// GameManager.Utils.InputHelper

using GameManager.GraphicsSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

#nullable disable
namespace GameManager.Utils
{
  public class InputHelper
  {
    private readonly List<GestureSample> _gestures = new List<GestureSample>();
    private GamePadState _currentGamePadState;
    private KeyboardState _currentKeyboardState;
    private MouseState _currentMouseState;
    private GamePadState _currentVirtualState;
    private GamePadState _lastGamePadState;
    private KeyboardState _lastKeyboardState;
    private MouseState _lastMouseState;
    private GamePadState _lastVirtualState;
    private bool _handleVirtualStick;
    private Vector2 _cursor;
    private bool _cursorIsValid;
    private bool _cursorIsVisible;
    private bool _cursorMoved;
    private Viewport _viewport;

    public InputHelper()
    {
      this._currentKeyboardState = new KeyboardState();
      this._currentGamePadState = new GamePadState();
      this._currentMouseState = new MouseState();
      this._currentVirtualState = new GamePadState();
      this._lastKeyboardState = new KeyboardState();
      this._lastGamePadState = new GamePadState();
      this._lastMouseState = new MouseState();
      this._lastVirtualState = new GamePadState();
      this._cursorIsVisible = false;
      this._cursorMoved = false;
      this._cursorIsValid = false;
      this._cursor = Vector2.Zero;
      this._handleVirtualStick = false;
    }

    public GamePadState GamePadState => this._currentGamePadState;

    public KeyboardState KeyboardState => this._currentKeyboardState;

    public MouseState MouseState => this._currentMouseState;

    public GamePadState VirtualState => this._currentVirtualState;

    public GamePadState PreviousGamePadState => this._lastGamePadState;

    public KeyboardState PreviousKeyboardState => this._lastKeyboardState;

    public MouseState PreviousMouseState => this._lastMouseState;

    public GamePadState PreviousVirtualState => this._lastVirtualState;

    public bool ShowCursor
    {
      get => this._cursorIsVisible && this._cursorIsValid;
      set => this._cursorIsVisible = value;
    }

    public bool EnableVirtualStick
    {
      get => this._handleVirtualStick;
      set => this._handleVirtualStick = value;
    }

    public Vector2 Cursor => this._cursor;

    public bool IsCursorMoved => this._cursorMoved;

    public bool IsCursorValid => this._cursorIsValid;

    public void LoadContent() => this._viewport = DispManager.GraphicsDev.Viewport;

    public void Update(float gameTime)
    {
      this._lastKeyboardState = this._currentKeyboardState;
      this._lastGamePadState = this._currentGamePadState;
      this._lastMouseState = this._currentMouseState;
      if (this._handleVirtualStick)
        this._lastVirtualState = this._currentVirtualState;
      this._currentKeyboardState = Keyboard.GetState();
      this._currentGamePadState = GamePad.GetState(PlayerIndex.One);
      this._currentMouseState = Mouse.GetState();
      if (this._handleVirtualStick)
        this._currentVirtualState = this.HandleVirtualStickWP7();
      this._gestures.Clear();
      while (TouchPanel.IsGestureAvailable)
        this._gestures.Add(TouchPanel.ReadGesture());
      Vector2 cursor = this._cursor;
      if (this._currentGamePadState.IsConnected && this._currentGamePadState.ThumbSticks.Left != Vector2.Zero)
      {
        this._cursor += this._currentGamePadState.ThumbSticks.Left * new Vector2(300f, -300f) * gameTime / 1000f;
        Mouse.SetPosition((int) this._cursor.X, (int) this._cursor.Y);
      }
      else
      {
        this._cursor.X = (float) this._currentMouseState.X;
        this._cursor.Y = (float) this._currentMouseState.Y;
      }
      this._cursor.X = MathHelper.Clamp(this._cursor.X, 0.0f, (float) this._viewport.Width);
      this._cursor.Y = MathHelper.Clamp(this._cursor.Y, 0.0f, (float) this._viewport.Height);
      this._cursorMoved = this._cursorIsValid && cursor != this._cursor;
      this._cursorIsValid = this._currentMouseState.LeftButton == ButtonState.Pressed;
    }

    public void Draw()
    {
    }

    private GamePadState HandleVirtualStickWin()
    {
      Vector2 zero = Vector2.Zero;
      List<Buttons> buttonsList = new List<Buttons>();
      if (this._currentKeyboardState.IsKeyDown(Keys.A))
        --zero.X;
      if (this._currentKeyboardState.IsKeyDown(Keys.S))
        --zero.Y;
      if (this._currentKeyboardState.IsKeyDown(Keys.D))
        ++zero.X;
      if (this._currentKeyboardState.IsKeyDown(Keys.W))
        ++zero.Y;
      if (this._currentKeyboardState.IsKeyDown(Keys.Space))
        buttonsList.Add(Buttons.A);
      if (this._currentKeyboardState.IsKeyDown(Keys.LeftControl))
        buttonsList.Add(Buttons.B);
      if (zero != Vector2.Zero)
        zero.Normalize();
      return new GamePadState(zero, Vector2.Zero, 0.0f, 0.0f, buttonsList.ToArray());
    }

    private GamePadState HandleVirtualStickWP7()
    {
      return new GamePadState(Vector2.Zero, Vector2.Zero, 0.0f, 0.0f, new List<Buttons>().ToArray());
    }

    public bool IsNewKeyPress(Keys key)
    {
      return this._currentKeyboardState.IsKeyDown(key) && this._lastKeyboardState.IsKeyUp(key);
    }

    public bool IsNewKeyRelease(Keys key)
    {
      return this._lastKeyboardState.IsKeyDown(key) && this._currentKeyboardState.IsKeyUp(key);
    }

    public bool IsNewButtonPress(Buttons button)
    {
      return this._currentGamePadState.IsButtonDown(button) && this._lastGamePadState.IsButtonUp(button);
    }

    public bool IsNewButtonRelease(Buttons button)
    {
      return this._lastGamePadState.IsButtonDown(button) && this._currentGamePadState.IsButtonUp(button);
    }

    public bool IsNewMouseButtonPress(MouseButtons button)
    {
      switch (button)
      {
        case MouseButtons.LeftButton:
          return this._currentMouseState.LeftButton == ButtonState.Pressed && this._lastMouseState.LeftButton == ButtonState.Released;
        case MouseButtons.MiddleButton:
          return this._currentMouseState.MiddleButton == ButtonState.Pressed && this._lastMouseState.MiddleButton == ButtonState.Released;
        case MouseButtons.RightButton:
          return this._currentMouseState.RightButton == ButtonState.Pressed && this._lastMouseState.RightButton == ButtonState.Released;
        case MouseButtons.ExtraButton1:
          return this._currentMouseState.XButton1 == ButtonState.Pressed && this._lastMouseState.XButton1 == ButtonState.Released;
        case MouseButtons.ExtraButton2:
          return this._currentMouseState.XButton2 == ButtonState.Pressed && this._lastMouseState.XButton2 == ButtonState.Released;
        default:
          return false;
      }
    }

    public bool IsNewMouseButtonRelease(MouseButtons button)
    {
      switch (button)
      {
        case MouseButtons.LeftButton:
          return this._lastMouseState.LeftButton == ButtonState.Pressed && this._currentMouseState.LeftButton == ButtonState.Released;
        case MouseButtons.MiddleButton:
          return this._lastMouseState.MiddleButton == ButtonState.Pressed && this._currentMouseState.MiddleButton == ButtonState.Released;
        case MouseButtons.RightButton:
          return this._lastMouseState.RightButton == ButtonState.Pressed && this._currentMouseState.RightButton == ButtonState.Released;
        case MouseButtons.ExtraButton1:
          return this._lastMouseState.XButton1 == ButtonState.Pressed && this._currentMouseState.XButton1 == ButtonState.Released;
        case MouseButtons.ExtraButton2:
          return this._lastMouseState.XButton2 == ButtonState.Pressed && this._currentMouseState.XButton2 == ButtonState.Released;
        default:
          return false;
      }
    }

    public bool IsMouseButtonDown(MouseButtons button)
    {
      switch (button)
      {
        case MouseButtons.LeftButton:
          return this._currentMouseState.LeftButton == ButtonState.Pressed && this._lastMouseState.LeftButton == ButtonState.Pressed;
        case MouseButtons.MiddleButton:
          return this._currentMouseState.MiddleButton == ButtonState.Pressed && this._lastMouseState.MiddleButton == ButtonState.Pressed;
        case MouseButtons.RightButton:
          return this._currentMouseState.RightButton == ButtonState.Pressed && this._lastMouseState.RightButton == ButtonState.Pressed;
        case MouseButtons.ExtraButton1:
          return this._currentMouseState.XButton1 == ButtonState.Pressed && this._lastMouseState.XButton1 == ButtonState.Pressed;
        case MouseButtons.ExtraButton2:
          return this._currentMouseState.XButton2 == ButtonState.Pressed && this._lastMouseState.XButton2 == ButtonState.Pressed;
        default:
          return false;
      }
    }

    public bool IsNewKeyPress(List<Keys> list)
    {
      foreach (Keys key in list)
      {
        if (this.IsNewKeyPress(key))
          return true;
      }
      return false;
    }

    public bool IsNewButtonPress(List<Buttons> list)
    {
      foreach (Buttons button in list)
      {
        if (this.IsNewButtonPress(button))
          return true;
      }
      return false;
    }

    public bool IsNewKeyRelease(List<Keys> keysList)
    {
      foreach (Keys keys in keysList)
      {
        if (this.IsNewKeyRelease(keys))
          return true;
      }
      return false;
    }

    public bool IsNewButtonRelease(List<Buttons> buttonsList)
    {
      foreach (Buttons buttons in buttonsList)
      {
        if (this.IsNewButtonRelease(buttons))
          return true;
      }
      return false;
    }

    public bool IsKeyDown(List<Keys> keysList)
    {
      foreach (Keys keys in keysList)
      {
        if (this.KeyboardState.IsKeyDown(keys))
          return true;
      }
      return false;
    }

    public bool IsButtonDown(List<Buttons> buttonsList)
    {
      foreach (Buttons buttons in buttonsList)
      {
        if (this.GamePadState.IsButtonDown(buttons))
          return true;
      }
      return false;
    }

    public bool IsMenuSelect()
    {
      return this.IsNewKeyPress(Keys.Space) || this.IsNewKeyPress(Keys.Enter) || this.IsNewButtonPress(Buttons.A) || this.IsNewButtonPress(Buttons.Start) || this.IsNewMouseButtonPress(MouseButtons.LeftButton);
    }

    public bool IsMenuPressed()
    {
      return this._currentKeyboardState.IsKeyDown(Keys.Space) || this._currentKeyboardState.IsKeyDown(Keys.Enter) || this._currentGamePadState.IsButtonDown(Buttons.A) || this._currentGamePadState.IsButtonDown(Buttons.Start) || this._currentMouseState.LeftButton == ButtonState.Pressed;
    }

    public bool IsMenuReleased()
    {
      return this.IsNewKeyRelease(Keys.Space) || this.IsNewKeyRelease(Keys.Enter) || this.IsNewButtonRelease(Buttons.A) || this.IsNewButtonRelease(Buttons.Start) || this.IsNewMouseButtonRelease(MouseButtons.LeftButton);
    }

    public bool IsMenuCancel()
    {
      return this.IsNewKeyPress(Keys.Escape) || this.IsNewButtonPress(Buttons.Back);
    }
  }
}
