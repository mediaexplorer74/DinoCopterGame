// GameManager.Utils.KeyHelper

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

#nullable disable
namespace GameManager.Utils
{
  public static class KeyHelper
  {
    public static InputHelper InputHelper;
    public static Dictionary<AbsKey, State> KeysState = new Dictionary<AbsKey, State>()
    {
      {
        AbsKey.Ok,
        State.Up
      },
      {
        AbsKey.Up,
        State.Up
      },
      {
        AbsKey.Down,
        State.Up
      },
      {
        AbsKey.Left,
        State.Up
      },
      {
        AbsKey.Right,
        State.Up
      },
      {
        AbsKey.LSK,
        State.Up
      },
      {
        AbsKey.RSK,
        State.Up
      }
    };
    private static readonly Dictionary<AbsKey, List<Keys>> KeysLists = new Dictionary<AbsKey, List<Keys>>()
    {
      {
        AbsKey.Up,
        new List<Keys>() { Keys.Up, Keys.W, Keys.NumPad8 }
      },
      {
        AbsKey.Down,
        new List<Keys>() { Keys.Down, Keys.S, Keys.NumPad5 }
      },
      {
        AbsKey.Ok,
        new List<Keys>()
        {
          Keys.Space,
          Keys.LeftControl,
          Keys.NumPad0
        }
      },
      {
        AbsKey.Left,
        new List<Keys>() { Keys.Left, Keys.A, Keys.NumPad4 }
      },
      {
        AbsKey.Right,
        new List<Keys>() { Keys.Right, Keys.D, Keys.NumPad6 }
      },
      {
        AbsKey.LSK,
        new List<Keys>() { Keys.Enter, Keys.NumPad0 }
      },
      {
        AbsKey.RSK,
        new List<Keys>() { Keys.Back, Keys.Escape }
      }
    };
    private static readonly Dictionary<AbsKey, List<Buttons>> ButtonsLists = new Dictionary<AbsKey, List<Buttons>>()
    {
      {
        AbsKey.Up,
        new List<Buttons>()
        {
          Buttons.DPadUp,
          Buttons.LeftThumbstickUp
        }
      },
      {
        AbsKey.Down,
        new List<Buttons>()
        {
          Buttons.DPadDown,
          Buttons.LeftThumbstickDown
        }
      },
      {
        AbsKey.Left,
        new List<Buttons>()
        {
          Buttons.DPadLeft,
          Buttons.LeftThumbstickLeft
        }
      },
      {
        AbsKey.Right,
        new List<Buttons>()
        {
          Buttons.DPadRight,
          Buttons.LeftThumbstickRight
        }
      },
      {
        AbsKey.Ok,
        new List<Buttons>() { Buttons.A, Buttons.X }
      },
      {
        AbsKey.LSK,
        new List<Buttons>() { Buttons.Start }
      },
      {
        AbsKey.RSK,
        new List<Buttons>() { Buttons.Back, Buttons.B }
      }
    };

    public static void UpdateKeys(float gameTime)
    {
      KeyHelper.InputHelper.Update(gameTime);
      Dictionary<AbsKey, State> dictionary = new Dictionary<AbsKey, State>(KeyHelper.KeysState.Count);
      foreach (KeyValuePair<AbsKey, State> keyValuePair in KeyHelper.KeysState)
        dictionary[keyValuePair.Key] = KeyHelper.InputHelper.IsNewKeyPress(KeyHelper.KeysLists[keyValuePair.Key]) || KeyHelper.InputHelper.IsNewButtonPress(KeyHelper.ButtonsLists[keyValuePair.Key]) ? State.Pressed : (KeyHelper.InputHelper.IsNewKeyRelease(KeyHelper.KeysLists[keyValuePair.Key]) || KeyHelper.InputHelper.IsNewButtonRelease(KeyHelper.ButtonsLists[keyValuePair.Key]) ? State.Released : (KeyHelper.InputHelper.IsKeyDown(KeyHelper.KeysLists[keyValuePair.Key]) || KeyHelper.InputHelper.IsButtonDown(KeyHelper.ButtonsLists[keyValuePair.Key]) ? State.Down : State.Up));
      KeyHelper.KeysState = dictionary;
    }
  }
}
