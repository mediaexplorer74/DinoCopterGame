// GameManager.Utils.Util

using System;

#nullable disable
namespace GameManager.Utils
{
  public static class Util
  {
    public const int RUnsupported = -1;
    public const int R320X480 = 1;
    public const int R640X960 = 2;
    public const int R768X1024 = 3;
    public const int R480X800 = 4;
    public const int R600X1024 = 5;
    public const int R800X1280 = 6;
    public const int Top = 1;
    public const int Vcenter = 2;
    public const int Bottom = 4;
    public const int Left = 8;
    public const int Hcenter = 16;
    public const int Right = 32;
    public static int ResolutionSet;
    public static string FilePrefix;
    public static readonly Random Random = new Random();

    public static int sgn(float a)
    {
      if ((double) a == 0.0)
        return 0;
      return (double) a >= 0.0 ? 1 : -1;
    }

    public static float Randf(float max)
    {
      return (float) Util.Random.Next((int) short.MaxValue) / (float) short.MaxValue * max;
    }

    public static bool IsWhiteChar(char c) => c == ' ' || c == '\n' || c == '\r' || c == '\t';

    public static float Halign(float width, int halign, float elementWidth)
    {
      if ((halign & 16) != 0)
        return (float) (((double) width - (double) elementWidth) / 2.0);
      return (halign & 32) != 0 ? width - elementWidth : 0.0f;
    }

    public static bool IsPointInRect(GameManager.GraphicsSystem.Point p, GameManager.GraphicsSystem.Point origin, GameManager.GraphicsSystem.Point size)
    {
      return (double) p.X > (double) origin.X && (double) p.Y > (double) origin.Y && (double) p.X < (double) origin.X + (double) size.X && (double) p.Y < (double) origin.Y + (double) size.Y;
    }

    public static bool RectsOverlaps(
      float ax1,
      float ay1,
      float ax2,
      float ay2,
      float bx1,
      float by1,
      float bx2,
      float by2)
    {
      return (double) ax2 > (double) bx1 && (double) bx2 > (double) ax1 && (double) ay2 > (double) by1 && (double) by2 > (double) ay1;
    }

    public static bool LineIntersectHorizontalLine(
      float x1,
      float y1,
      float x2,
      float y2,
      float ly,
      float lx1,
      float lx2)
    {
      if ((double) y2 < (double) y1)
      {
        float num1 = x1;
        x1 = x2;
        x2 = num1;
        float num2 = y1;
        y1 = y2;
        y2 = num2;
      }
      if ((double) ly < (double) y1 || (double) ly > (double) y2)
        return false;
      if ((double) y1 == (double) y2)
        return (double) y1 == (double) ly;
      float num = x1 + (float) (((double) x2 - (double) x1) * ((double) ly - (double) y1) / ((double) y2 - (double) y1));
      return (double) num >= (double) lx1 && (double) num <= (double) lx2;
    }

    public static bool LineIntersectVerticalLine(
      float x1,
      float y1,
      float x2,
      float y2,
      float lx,
      float ly1,
      float ly2)
    {
      if ((double) x2 < (double) x1)
      {
        float num1 = x1;
        x1 = x2;
        x2 = num1;
        float num2 = y1;
        y1 = y2;
        y2 = num2;
      }
      if ((double) lx < (double) x1 || (double) lx > (double) x2)
        return false;
      if ((double) x1 == (double) x2)
        return (double) x1 == (double) lx;
      float num = y1 + (float) (((double) y2 - (double) y1) * ((double) lx - (double) x1) / ((double) x2 - (double) x1));
      return (double) num >= (double) ly1 && (double) num <= (double) ly2;
    }

    public static bool Includes(float a1, float a2, float b1, float b2)
    {
      if ((double) a1 >= (double) b1 && (double) a2 <= (double) b2)
        return true;
      return (double) b1 >= (double) a1 && (double) b2 <= (double) a2;
    }

    public static Microsoft.Xna.Framework.Color ConvertIntToColor(uint colorInInt)
    {
      byte[] bytes = BitConverter.GetBytes(colorInInt);
      return new Microsoft.Xna.Framework.Color((int) bytes[1], (int) bytes[2], (int) bytes[3], (int) bytes[0]);
    }
  }
}
