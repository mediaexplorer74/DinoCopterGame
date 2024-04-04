// GameManager.GraphicsSystem.Paintable

using GameManager.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class Paintable
  {
    public static BasicEffect effect = new BasicEffect(DispManager.GraphicsDev);
    private static VertexPositionColor[] _V = new VertexPositionColor[4];

    public PType PType { get; set; }

    public PImagePart PImagePart { get; set; }

    public PRects PRects { get; set; }

    public PInvisible PInvisible { get; set; }

    public PImage PImage { get; set; }

    public PClipped PClipped { get; set; }

    public PAnimation PAnimation { get; set; }

    public PGroup PGroup { get; set; }

    public PBiPhaseAnim PBiPhaseAnim { get; set; }

    public PText PText { get; set; }

    public PModulate PModulate { get; set; }

    public PRotated PRotated { get; set; }

    public PColoredPolygon PColoredPolygon { get; set; }

    public bool Mirror { get; set; }

    public Paintable()
      : this(PType.Unloaded, false)
    {
    }

    public Paintable(PType type)
    {
      this.PType = type;
      switch (this.PType)
      {
        case PType.Unloaded:
        case PType.Image:
          this.PImage = new PImage();
          break;
        case PType.Animation:
          this.PAnimation = new PAnimation();
          break;
        case PType.BiphaseAnim:
          this.PBiPhaseAnim = new PBiPhaseAnim();
          break;
        case PType.InvisibleRect:
          this.PInvisible = new PInvisible();
          break;
        case PType.Clipped:
          this.PClipped = new PClipped();
          break;
        case PType.Rect:
        case PType.FillRect:
          this.PRects = new PRects();
          break;
        case PType.ImagePart:
          this.PImagePart = new PImagePart();
          break;
        case PType.Modulate:
          this.PModulate = new PModulate();
          break;
        case PType.ColoredPolygon:
          this.PColoredPolygon = new PColoredPolygon();
          break;
      }
    }

    private Paintable(PType type, bool mirror)
      : this(type)
    {
      this.Mirror = mirror;
    }

    public static Paintable Copy(Paintable toCopy) => new Paintable(toCopy);

    public Paintable(Paintable toCopy)
    {
      this.PType = toCopy.PType;
      this.Mirror = toCopy.Mirror;
      switch (toCopy.PType)
      {
        case PType.Unloaded:
        case PType.Image:
          this.PImage = toCopy.PImage;
          break;
        case PType.Animation:
          this.PAnimation = new PAnimation()
          {
            AnimationLength = toCopy.PAnimation.AnimationLength,
            AnimationTimeElapsed = toCopy.PAnimation.AnimationTimeElapsed,
            FramesNum = toCopy.PAnimation.FramesNum
          };
          this.PAnimation.Frames = new Paintable[this.PAnimation.FramesNum];
          for (int index = 0; index < this.PAnimation.FramesNum; ++index)
            this.PAnimation.Frames[index] = new Paintable(toCopy.PAnimation.Frames[index]);
          break;
        case PType.Group:
          this.PGroup = new PGroup()
          {
            AutoResize = toCopy.PGroup.AutoResize,
            W = toCopy.PGroup.W,
            H = toCopy.PGroup.H,
            Elements = new List<Pair<Paintable, Point>>()
          };
          using (List<Pair<Paintable, Point>>.Enumerator enumerator = toCopy.PGroup.Elements.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Pair<Paintable, Point> current = enumerator.Current;
              this.AddElement(current.Second.X, current.Second.Y, new Paintable(current.First));
            }
            break;
          }
        case PType.Text:
          this.PText = new PText()
          {
            Font = toCopy.PText.Font,
            HAlign = toCopy.PText.HAlign,
            Text = new string(toCopy.PText.Text.ToCharArray()),
            Width = toCopy.PText.Width
          };
          break;
        case PType.BiphaseAnim:
          this.PBiPhaseAnim = new PBiPhaseAnim()
          {
            Phase1 = new Paintable(toCopy.PBiPhaseAnim.Phase1),
            Phase2 = new Paintable(toCopy.PBiPhaseAnim.Phase2)
          };
          break;
        case PType.InvisibleRect:
          this.PInvisible = new PInvisible()
          {
            W = toCopy.PInvisible.W,
            H = toCopy.PInvisible.H
          };
          break;
        case PType.Clipped:
          this.PClipped = new PClipped()
          {
            X = toCopy.PClipped.X,
            Y = toCopy.PClipped.Y,
            W = toCopy.PClipped.W,
            H = toCopy.PClipped.H,
            Img = new Paintable(toCopy.PClipped.Img)
          };
          break;
        case PType.Rect:
        case PType.FillRect:
          this.PRects = new PRects(toCopy.PRects.W, toCopy.PRects.H, toCopy.PRects.Color);
          this.PRects.Coords[0] = toCopy.PRects.Coords[0];
          this.PRects.Coords[1] = toCopy.PRects.Coords[1];
          this.PRects.Coords[2] = toCopy.PRects.Coords[2];
          this.PRects.Coords[3] = toCopy.PRects.Coords[3];
          this.PRects.Coords[4] = toCopy.PRects.Coords[4];
          this.PRects.Coords[5] = toCopy.PRects.Coords[5];
          this.PRects.Coords[6] = toCopy.PRects.Coords[6];
          this.PRects.Coords[7] = toCopy.PRects.Coords[7];
          break;
        case PType.ImagePart:
          this.PImagePart = new PImagePart()
          {
            MinX = toCopy.PImagePart.MinX,
            MinY = toCopy.PImagePart.MinY,
            W = toCopy.PImagePart.W,
            H = toCopy.PImagePart.H,
            CoordsNum = toCopy.PImagePart.CoordsNum,
            Image = new Paintable(toCopy.PImagePart.Image),
            Coords = new float[toCopy.PImagePart.CoordsNum * 2],
            TextureCoords = new float[toCopy.PImagePart.CoordsNum * 2]
          };
          Array.Copy((Array) toCopy.PImagePart.Coords, (Array) this.PImagePart.Coords, toCopy.PImagePart.Coords.Length);
          Array.Copy((Array) toCopy.PImagePart.TextureCoords, (Array) this.PImagePart.TextureCoords, toCopy.PImagePart.TextureCoords.Length);
          break;
        case PType.Modulate:
          this.PModulate = new PModulate()
          {
            Duration = toCopy.PModulate.Duration,
            Img = new Paintable(toCopy.PModulate.Img),
            Time = toCopy.PModulate.Time
          };
          Array.Copy((Array) toCopy.PModulate.Start, (Array) this.PModulate.Start, toCopy.PModulate.Start.Length);
          Array.Copy((Array) toCopy.PModulate.D, (Array) this.PModulate.D, toCopy.PModulate.D.Length);
          break;
        case PType.Rotated:
          this.PRotated = new PRotated()
          {
            Img = new Paintable(toCopy.PRotated.Img),
            AxisX = toCopy.PRotated.AxisX,
            AxisY = toCopy.PRotated.AxisY,
            Angle = toCopy.PRotated.Angle
          };
          break;
        case PType.ColoredPolygon:
          this.PColoredPolygon = new PColoredPolygon()
          {
            VertexNum = toCopy.PColoredPolygon.VertexNum,
            Vertexes = new Point[toCopy.PColoredPolygon.VertexNum],
            Colors = new GameManager.Utils.Color[toCopy.PColoredPolygon.VertexNum]
          };
          Array.Copy((Array) toCopy.PColoredPolygon.Vertexes, (Array) this.PColoredPolygon.Vertexes, toCopy.PColoredPolygon.Vertexes.Length);
          Array.Copy((Array) toCopy.PColoredPolygon.Colors, (Array) this.PColoredPolygon.Colors, toCopy.PColoredPolygon.Colors.Length);
          break;
      }
    }

    public string ResourceId { get; set; }

    public Paintable GetCurrentFrame()
    {
      return this.PAnimation.FramesNum != 0 ? this.PAnimation.Frames[(double) this.PAnimation.AnimationLength == 0.0 ? 0 : (int) ((double) this.PAnimation.AnimationTimeElapsed * (double) this.PAnimation.FramesNum / (double) this.PAnimation.AnimationLength) % this.PAnimation.FramesNum] : new Paintable();
    }

    public static Paintable CreateLine(
      float height,
      string s,
      int spos,
      int epos,
      BMFont font,
      int valign,
      bool add,
      bool addLine)
    {
      Paintable group = Paintable.CreateGroup(true);
      group.PGroup.H = height;
      if (add)
      {
        if (epos == s.Length)
          epos += 45;
        ++epos;
      }
      for (int index1 = spos; index1 < epos; ++index1)
      {
        if (s[index1] == '#' && index1 < s.Length - 1 && s[index1 + 1] != '#')
        {
          Pair<int, int> pair = Paintable.LoadInt(s, index1 + 1);
          font = GlobalMembers.Fonts[pair.Second];
          index1 = pair.First - 1;
        }
        else if (s[index1] == '@' && index1 < s.Length - 1 && s[index1 + 1] != '@')
        {
          int index2 = index1 + 1;
          switch (s[index2])
          {
            case 'B':
              valign = 4;
              break;
            case 'C':
              valign = 2;
              break;
            case 'T':
              valign = 1;
              break;
          }
          index1 = index2 + 1;
        }
        else if (s[index1] == '$' && index1 < s.Length - 1 && s[index1 + 1] != '$')
        {
          index1 = Paintable.LoadInt(s, index1 + 1).First;
          addLine = true;
        }
        else
        {
          string text = "";
          for (; index1 < epos && (s[index1] != '$' || s[index1 + 1] == '$') && (s[index1] != '#' || s[index1 + 1] == '#'); ++index1)
          {
            if ((s[index1] == '$' || s[index1] == '#') && (int) s[index1] == (int) s[index1 + 1])
              ++index1;
            text += (string) (object) s[index1];
          }
          if (index1 + 1 < epos && (s[index1] == '$' && s[index1 + 1] != '$' || s[index1] == '#' && s[index1 + 1] != '#'))
            --index1;
          switch (valign)
          {
            case 1:
              group.AddElement(group.GetWidth(), 0.0f, Paintable.CreateText(text, font), 9);
              break;
            case 2:
              group.AddElement(group.GetWidth(), group.GetHeight() / 2f, Paintable.CreateText(text, font), 10);
              break;
            case 4:
              group.AddElement(group.GetWidth(), group.GetHeight(), Paintable.CreateText(text, font), 12);
              break;
          }
          addLine = true;
        }
      }
      return !addLine ? Paintable.CreateInvisibleRect(0.0f, 0.0f) : group;
    }

    public static Paintable CreateRotated(Paintable image, Point axis, float angle)
    {
      return new Paintable(PType.Rotated)
      {
        PRotated = new PRotated()
        {
          Img = new Paintable(image),
          AxisX = axis.X,
          AxisY = axis.Y,
          Angle = angle
        }
      };
    }

    public static List<Paintable> CreateTiles(Paintable tiles, int width, int height)
    {
      List<Paintable> tiles1 = new List<Paintable>();
      float w = tiles.GetWidth() / (float) width;
      float h = tiles.GetHeight() / (float) height;
      if (tiles.PType != PType.Image && tiles.PType != PType.Unloaded)
      {
        for (int index1 = 0; index1 < height; ++index1)
        {
          for (int index2 = 0; index2 < width; ++index2)
            tiles1.Add(Paintable.CreateClipped((float) index2 * w, (float) index1 * h, w, h, tiles));
        }
      }
      else
      {
        for (int index3 = 0; index3 < height; ++index3)
        {
          for (int index4 = 0; index4 < width; ++index4)
          {
            float[] coords = new float[8]
            {
              (float) index4 * w,
              (float) index3 * h,
              (float) (1 + index4) * w,
              (float) index3 * h,
              (float) index4 * w,
              (float) (1 + index3) * h,
              (float) (1 + index4) * w,
              (float) (1 + index3) * h
            };
            tiles1.Add(Paintable.CreateImagePart(tiles, coords, 4));
          }
        }
      }
      return tiles1;
    }

    public static Paintable CreateImage(int w, int h, Texture2D texture, bool rotate)
    {
      Paintable image = new Paintable(PType.Image)
      {
        PImage = {
          KeepPixelData = false
        }
      };
      image.InitImage(w, h, texture, rotate);
      return image;
    }

    public static Paintable CreateColoredPolygon(int _vertexNum, Point[] _vertexes, GameManager.Utils.Color[] colors)
    {
      Paintable coloredPolygon = new Paintable(PType.ColoredPolygon)
      {
        PColoredPolygon = new PColoredPolygon()
        {
          VertexNum = _vertexNum,
          Vertexes = new Point[_vertexNum],
          Colors = new GameManager.Utils.Color[_vertexNum]
        }
      };
      Array.Copy((Array) _vertexes, (Array) coloredPolygon.PColoredPolygon.Vertexes, _vertexes.Length);
      Array.Copy((Array) colors, (Array) coloredPolygon.PColoredPolygon.Colors, colors.Length);
      coloredPolygon.PColoredPolygon.MinX = _vertexes[0].X;
      coloredPolygon.PColoredPolygon.MinY = _vertexes[0].Y;
      coloredPolygon.PColoredPolygon.SizeX = _vertexes[0].X;
      coloredPolygon.PColoredPolygon.SizeY = _vertexes[0].Y;
      for (int index = 1; index < _vertexNum; ++index)
      {
        if ((double) coloredPolygon.PColoredPolygon.MinX > (double) _vertexes[index].X)
          coloredPolygon.PColoredPolygon.MinX = _vertexes[index].X;
        if ((double) coloredPolygon.PColoredPolygon.MinY > (double) _vertexes[index].Y)
          coloredPolygon.PColoredPolygon.MinY = _vertexes[index].Y;
        if ((double) coloredPolygon.PColoredPolygon.SizeX < (double) _vertexes[index].X)
          coloredPolygon.PColoredPolygon.SizeX = _vertexes[index].X;
        if ((double) coloredPolygon.PColoredPolygon.SizeY < (double) _vertexes[index].Y)
          coloredPolygon.PColoredPolygon.SizeY = _vertexes[index].Y;
      }
      coloredPolygon.PColoredPolygon.SizeX -= coloredPolygon.PColoredPolygon.MinX;
      coloredPolygon.PColoredPolygon.SizeY -= coloredPolygon.PColoredPolygon.MinY;
      coloredPolygon.PColoredPolygon.PointList = new VertexPositionColor[_vertexNum];
      for (int index = 0; index < _vertexNum; ++index)
      {
        coloredPolygon.PColoredPolygon.PointList[index].Position = new Vector3(_vertexes[index].X, _vertexes[index].Y, 0.0f);
        coloredPolygon.PColoredPolygon.PointList[index].Color = (double) coloredPolygon.PColoredPolygon.Colors[index].A != 0.0 ? new Microsoft.Xna.Framework.Color(76, 76, 76, 76) : Microsoft.Xna.Framework.Color.Transparent;
      }
      return coloredPolygon;
    }

    public static Paintable CreateModulate(Paintable image, GameManager.Utils.Color from, GameManager.Utils.Color to, float time)
    {
      Paintable modulate = new Paintable(PType.Modulate)
      {
        PModulate = new PModulate()
      };
      modulate.PModulate.D[0] = to.C[0] - from.C[0];
      modulate.PModulate.D[1] = to.C[1] - from.C[1];
      modulate.PModulate.D[2] = to.C[2] - from.C[2];
      modulate.PModulate.D[3] = to.C[3] - from.C[3];
      modulate.PModulate.Duration = time;
      modulate.PModulate.Img = new Paintable(image);
      modulate.PModulate.Start[0] = from.C[0];
      modulate.PModulate.Start[1] = from.C[1];
      modulate.PModulate.Start[2] = from.C[2];
      modulate.PModulate.Start[3] = from.C[3];
      modulate.PModulate.Time = 0.0f;
      return modulate;
    }

    public static Paintable CreateImagePart(Paintable image, float[] coords, int pointsNum)
    {
      Paintable imagePart = new Paintable(PType.ImagePart)
      {
        PImagePart = new PImagePart()
        {
          Image = new Paintable(image),
          Coords = new float[pointsNum * 2]
        }
      };
      Array.Copy((Array) coords, (Array) imagePart.PImagePart.Coords, coords.Length);
      float[] numArray = new float[pointsNum * 2];
      float val1_1 = coords[0];
      float val1_2 = coords[1];
      float val1_3 = coords[0];
      float val1_4 = coords[1];
      imagePart.PImagePart.CoordsNum = pointsNum;
      for (int index = 0; index < pointsNum; ++index)
      {
        numArray[index << 1] = coords[index << 1] / (float) image.PImage.TextureWidth;
        val1_1 = Math.Min(val1_1, coords[index << 1]);
        val1_3 = Math.Max(val1_3, coords[index << 1]);
        val1_2 = Math.Min(val1_2, coords[(index << 1) + 1]);
        val1_4 = Math.Max(val1_4, coords[(index << 1) + 1]);
        numArray[(index << 1) + 1] = coords[(index << 1) + 1] / (float) image.PImage.TextureHeight;
      }
      imagePart.PImagePart.TextureCoords = numArray;
      imagePart.PImagePart.MinX = val1_1;
      imagePart.PImagePart.MinY = val1_2;
      imagePart.PImagePart.W = val1_3 - val1_1;
      imagePart.PImagePart.H = val1_4 - val1_2;
      return imagePart;
    }

    public static Paintable CreateAnim(int framesNum, Paintable[] frames, float duration)
    {
      Paintable anim = new Paintable(PType.Animation)
      {
        PAnimation = new PAnimation()
        {
          AnimationLength = duration,
          AnimationTimeElapsed = 0.0f,
          FramesNum = framesNum,
          Frames = new Paintable[framesNum]
        }
      };
      for (int index = 0; index < framesNum; ++index)
        anim.PAnimation.Frames[index] = frames[index];
      return anim;
    }

    public static Paintable CreateClipped(float x, float y, float w, float h, Paintable p)
    {
      return new Paintable(PType.Clipped)
      {
        PClipped = new PClipped()
        {
          X = x,
          Y = y,
          W = w,
          H = h,
          Img = new Paintable(p)
        }
      };
    }

    public static Paintable CreateRect(float w, float h, Microsoft.Xna.Framework.Color color)
    {
      Paintable rect = new Paintable(PType.Rect)
      {
        PRects = new PRects(w, h, color)
      };
      rect.PRects.Coords[0] = 0.0f;
      rect.PRects.Coords[1] = 0.0f;
      rect.PRects.Coords[2] = w;
      rect.PRects.Coords[3] = 0.0f;
      rect.PRects.Coords[4] = w;
      rect.PRects.Coords[5] = h;
      rect.PRects.Coords[6] = 0.0f;
      rect.PRects.Coords[7] = h;
      return rect;
    }

    public static Paintable CreateText(string text, BMFont font)
    {
      return Paintable.CreateText(text, font, 0.0f, 8);
    }

    public static Paintable CreateText(string text, BMFont font, float width, int halign)
    {
      Paintable text1 = new Paintable(PType.Text)
      {
        PText = new PText()
        {
          Text = new string(text.ToCharArray())
        }
      };
      text1.PText.Width = (double) width == 0.0 ? font.GetStringWidth(text) : width;
      text1.PText.HAlign = halign;
      text1.PText.Font = font;
      return text1;
    }

    public static Paintable CreateInvisibleRect(float w, float h)
    {
      return new Paintable(PType.InvisibleRect)
      {
        PInvisible = new PInvisible() { W = w, H = h }
      };
    }

    public static Paintable CreateFilledRect(float w, float h, Microsoft.Xna.Framework.Color color)
    {
      Paintable filledRect = new Paintable(PType.FillRect)
      {
        PRects = new PRects(w, h, color)
      };
      filledRect.PRects.Coords[0] = 0.0f;
      filledRect.PRects.Coords[1] = 0.0f;
      filledRect.PRects.Coords[2] = w;
      filledRect.PRects.Coords[3] = 0.0f;
      filledRect.PRects.Coords[4] = 0.0f;
      filledRect.PRects.Coords[5] = h;
      filledRect.PRects.Coords[6] = w;
      filledRect.PRects.Coords[7] = h;
      return filledRect;
    }

    public static Paintable CreateFromResMan(string resourceId)
    {
      return GlobalMembers.ResManLoader.Resources[resourceId];
    }

    public static Paintable CreateGroup(bool autoResize)
    {
      return Paintable.CreateGroup(autoResize, 0.0f, 0.0f);
    }

    public static Paintable CreateGroup(bool autoResize, float w, float h)
    {
      return new Paintable(PType.Group)
      {
        PGroup = new PGroup()
        {
          AutoResize = autoResize,
          W = w,
          H = h,
          Elements = new List<Pair<Paintable, Point>>()
        }
      };
    }

    public static Paintable CreateBiPhaseAnim(Paintable phase1, Paintable phase2)
    {
      return new Paintable(PType.BiphaseAnim)
      {
        PBiPhaseAnim = new PBiPhaseAnim()
        {
          Phase1 = new Paintable(phase1),
          Phase2 = new Paintable(phase2)
        }
      };
    }

    public static Paintable CreateFormatedPaintable(
      string s,
      string[] inputs,
      int inputsNum,
      float width,
      int halign,
      bool fitSmallContent)
    {
      Paintable group = Paintable.CreateGroup(true);
      group.AddElement(0.0f, 0.0f, Paintable.CreateInvisibleRect(width, 0.0f));
      for (int index = 0; index < s.Length - 1; ++index)
      {
        if (s[index] == '%')
        {
          if (s[index + 1] == '%')
          {
            s = s.Substring(0, index) + s.Substring(index + 1, s.Length - index - 1);
          }
          else
          {
            Pair<int, int> pair = Paintable.LoadInt(s, index + 1);
            s = s.Substring(0, index) + inputs[pair.Second] + s.Substring(pair.First, s.Length - pair.First);
            index = pair.First - 1;
          }
        }
        else if (s[index] == '\\')
        {
          if (s[index + 1] == '\\')
            s = s.Substring(0, index) + s.Substring(index + 1, s.Length - index - 1);
          if (s[index + 1] == 'n')
            s = s.Substring(0, index) + (object) '\n' + s.Substring(index + 2, s.Length - index - 2);
        }
      }
      BMFont font1 = GlobalMembers.Fonts[0];
      BMFont font2 = GlobalMembers.Fonts[0];
      float num1 = 0.0f;
      float num2 = 0.0f;
      int num3 = 0;
      int num4 = 0;
      int valign = 2;
      bool addLine = false;
      for (int index = 0; index < s.Length; ++index)
      {
        if (s[index] == '#' && index != s.Length - 1 && s[index + 1] != '#')
        {
          num4 = 0;
          Pair<int, int> pair = Paintable.LoadInt(s, index + 1);
          font1 = GlobalMembers.Fonts[pair.Second];
          index = pair.First - 1;
        }
        else if (s[index] == '$' && index != s.Length - 1 && s[index + 1] != '$' && index < s.Length - 1 && s[index + 1] >= '0' && s[index + 1] <= '9')
        {
          num4 = 0;
          index = Paintable.LoadInt(s, index + 1).First - 1;
        }
        else if (s[index] == '@' && index != s.Length - 1 && s[index + 1] != '@')
        {
          ++index;
          num4 = 0;
          switch (s[index])
          {
            case 'B':
              valign = 4;
              continue;
            case 'C':
              valign = 2;
              continue;
            case 'T':
              valign = 1;
              continue;
            default:
              continue;
          }
        }
        else
        {
          num2 = Math.Max(font1.GetHeight(), num2);
          if ((s[index] == '$' || s[index] == '#' || s[index] == '@') && (int) s[index] == (int) s[index + 1])
            ++index;
          char c = s[index];
          if ((double) num1 + (double) font1.GetCharWidth(c) >= (double) width || c == '\n')
          {
            if (c == '\n')
              addLine = true;
            int num5 = Math.Max(0, index - num4);
            bool add = false;
            if (c != '\n')
            {
              if (num5 != num3)
              {
                for (; index > num5 && index > 0 && index > num3; --index)
                  num1 -= font1.GetCharWidth(s[index]);
              }
              else
              {
                add = true;
                for (; index > num5 && (double) num1 + (double) font1.GetCharWidth('-') > (double) width; --index)
                  num1 -= font1.GetCharWidth(s[index]);
              }
            }
            if (8 == halign)
              group.AddElement(0.0f, group.GetHeight(), Paintable.CreateLine(num2, s, num3, index, font2, valign, add, addLine), 12);
            else if (16 == halign)
              group.AddElement(width / 2f, group.GetHeight(), Paintable.CreateLine(num2, s, num3, index, font2, valign, add, addLine), 20);
            else
              group.AddElement(width, group.GetHeight(), Paintable.CreateLine(num2, s, num3, index, font2, valign, add, addLine), 36);
            num1 = 0.0f;
            font2 = font1;
            if (c == '\n')
            {
              num3 = index + 1;
            }
            else
            {
              num3 = index;
              while (num3 < s.Length && Util.IsWhiteChar(s[num3]))
                ++num3;
              num1 += font1.GetCharWidth(s[index]);
            }
            num2 = 0.0f;
            num4 = 1;
            addLine = false;
          }
          else
          {
            if (!Util.IsWhiteChar(s[index]))
              ++num4;
            else
              num4 = 0;
            num1 += font1.GetCharWidth(s[index]);
          }
        }
      }
      if (8 == halign)
        group.AddElement(0.0f, group.GetHeight(), Paintable.CreateLine(num2, s, num3, s.Length, font2, valign, false, addLine), 12);
      else if (16 == halign)
        group.AddElement(width / 2f, group.GetHeight(), Paintable.CreateLine(num2, s, num3, s.Length, font2, valign, false, addLine), 20);
      else
        group.AddElement(width, group.GetHeight(), Paintable.CreateLine(num2, s, num3, s.Length, font2, valign, false, addLine), 36);
      group.PGroup.Elements.Remove(group.PGroup.Elements[0]);
      if (fitSmallContent && group.PGroup.Elements.Count == 1)
      {
        Paintable first = group.PGroup.Elements[0].First;
        group.PGroup.W = first.GetWidth();
        group.PGroup.H = first.GetHeight();
        group.PGroup.Elements[0].Second.X = 0.0f;
        group.PGroup.Elements[0].Second.Y = 0.0f;
      }
      float height = group.GetHeight();
      foreach (Pair<Paintable, Point> element in group.PGroup.Elements)
      {
        element.Second.Y = height - element.First.GetHeight();
        height -= element.First.GetHeight();
      }
      return group;
    }

    public void InitImage(int w, int h, Texture2D texture, bool rotate)
    {
      if (this.PImage.Texture != null)
        throw new Exception("InitImage - Texture already created");
      int num1 = 1;
      while (num1 < w)
        num1 <<= 1;
      int num2 = 1;
      while (num2 < h)
        num2 <<= 1;
      this.PImage.TextureWidth = num1;
      this.PImage.TextureHeight = num2;
      this.PImage.W = (float) w;
      this.PImage.H = (float) h;
      this.PImage.Texture = texture;
      this.PImage.TextureCoords[0] = 0.0f;
      this.PImage.TextureCoords[1] = 0.0f;
      this.PImage.TextureCoords[2] = (float) w / (float) num1;
      this.PImage.TextureCoords[3] = 0.0f;
      this.PImage.TextureCoords[4] = 0.0f;
      this.PImage.TextureCoords[5] = (float) h / (float) num2;
      this.PImage.TextureCoords[6] = (float) w / (float) num1;
      this.PImage.TextureCoords[7] = (float) h / (float) num2;
      this.PImage.Coords[0] = 0.0f;
      this.PImage.Coords[1] = 0.0f;
      this.PImage.Coords[2] = (float) w;
      this.PImage.Coords[3] = 0.0f;
      this.PImage.Coords[4] = 0.0f;
      this.PImage.Coords[5] = (float) h;
      this.PImage.Coords[6] = (float) w;
      this.PImage.Coords[7] = (float) h;
    }

    public void AddElement(float x, float y, Paintable p) => this.AddElement(x, y, p, 12);

    public void AddElement(float x, float y, Paintable p, int anchor)
    {
      this.AddElement(x, y, p, anchor, -1);
    }

    public void AddElement(float x, float y, Paintable p, int anchor, int index)
    {
      x = this.TransformX(x, p.GetWidth(), anchor);
      y = this.TransformY(y, p.GetHeight(), anchor);
      if (index == -1)
        this.PGroup.Elements.Add(new Pair<Paintable, Point>(p, new Point(x, y)));
      else
        this.PGroup.Elements.Insert(index, new Pair<Paintable, Point>(p, new Point(x, y)));
      if (!this.PGroup.AutoResize)
        return;
      if ((double) x + (double) p.GetWidth() > (double) this.GetWidth())
        this.PGroup.W = x + p.GetWidth();
      if ((double) y + (double) p.GetHeight() <= (double) this.GetHeight())
        return;
      this.PGroup.H = y + p.GetHeight();
    }

    public float GetWidth()
    {
      switch (this.PType)
      {
        case PType.Unloaded:
        case PType.Image:
          return this.PImage.W;
        case PType.Animation:
          return this.GetCurrentFrame().GetWidth();
        case PType.Group:
          return this.PGroup.W;
        case PType.Text:
          return this.PText.Width;
        case PType.BiphaseAnim:
          return this.PBiPhaseAnim.Phase1.GetAnimationCyclesNum() < 1 ? this.PBiPhaseAnim.Phase1.GetWidth() : this.PBiPhaseAnim.Phase2.GetWidth();
        case PType.InvisibleRect:
          return this.PInvisible.W;
        case PType.Clipped:
          return this.PClipped.Img.GetWidth() - this.PClipped.W;
        case PType.Rect:
        case PType.FillRect:
          return this.PRects.W;
        case PType.ImagePart:
          return this.PImagePart.W;
        case PType.Modulate:
          return this.PModulate.Img.GetWidth();
        case PType.Rotated:
          return this.PRotated.Img.GetWidth();
        case PType.ColoredPolygon:
          return this.PColoredPolygon.SizeX;
        default:
          return 0.0f;
      }
    }

    public float GetHeight()
    {
      switch (this.PType)
      {
        case PType.Unloaded:
        case PType.Image:
          return this.PImage.H;
        case PType.Animation:
          return this.GetCurrentFrame().GetHeight();
        case PType.Group:
          return this.PGroup.H;
        case PType.Text:
          return this.PText.Font.GetHeight();
        case PType.BiphaseAnim:
          return this.PBiPhaseAnim.Phase1.GetAnimationCyclesNum() < 1 ? this.PBiPhaseAnim.Phase1.GetHeight() : this.PBiPhaseAnim.Phase2.GetHeight();
        case PType.InvisibleRect:
          return this.PInvisible.H;
        case PType.Clipped:
          return this.PClipped.Img.GetHeight() - this.PClipped.H;
        case PType.Rect:
        case PType.FillRect:
          return this.PRects.H;
        case PType.ImagePart:
          return this.PImagePart.H;
        case PType.Modulate:
          return this.PModulate.Img.GetHeight();
        case PType.Rotated:
          return this.PRotated.Img.GetHeight();
        case PType.ColoredPolygon:
          return this.PColoredPolygon.SizeY;
        default:
          return 0.0f;
      }
    }

    public void Paint(float x, float y, SpriteBatch spriteBatch)
    {
      this.Paint(x, y, 12, spriteBatch);
    }

    public void Paint(float x, float y, int anchor, SpriteBatch spriteBatch)
    {
      x = this.TransformX(x, this.GetWidth(), anchor);
      y = this.TransformY(y, this.GetHeight(), anchor);
      this.Paint(x, y, anchor, 0.0f, Vector2.Zero, new Rectangle?(), Microsoft.Xna.Framework.Color.White, spriteBatch);
    }

    static Paintable()
    {
      Paintable.effect.Projection = Matrix.CreateOrthographicOffCenter(0.0f, GlobalMembers.ScreenWidth, 0.0f, GlobalMembers.ScreenHeight, 0.0f, 1f);
      Paintable.effect.VertexColorEnabled = true;
    }

    public void Paint(
      float x,
      float y,
      int anchor,
      float angle,
      Vector2 origin,
      Rectangle? source,
      Microsoft.Xna.Framework.Color color,
      SpriteBatch spriteBatch)
    {
      float height = this.GetHeight();
      switch (this.PType)
      {
        case PType.Unloaded:
          if (string.IsNullOrEmpty(this.ResourceId))
            break;
          this.AddToLoad();
          GlobalMembers.ResManLoader.LoadResources(false);
          break;
        case PType.Image:
          this.Draw(spriteBatch, x, y, height, source, color, angle, origin);
          break;
        case PType.Animation:
          if (this.PAnimation.FramesNum == 0)
            break;
          Paintable currentFrame = this.GetCurrentFrame();
          currentFrame.Mirror = this.Mirror;
          currentFrame.Paint(x, y, anchor, angle, origin, source, color, spriteBatch);
          break;
        case PType.Group:
          using (List<Pair<Paintable, Point>>.Enumerator enumerator = this.PGroup.Elements.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Pair<Paintable, Point> current = enumerator.Current;
              current.First.Paint(x + current.Second.X, y + current.Second.Y, anchor, angle, origin, source, color, spriteBatch);
            }
            break;
          }
        case PType.Text:
          this.PText.Font.Write(this.PText.Text, x + Util.Halign(this.PText.Width, this.PText.HAlign, this.PText.Font.GetStringWidth(this.PText.Text)), y, 9, spriteBatch);
          break;
        case PType.BiphaseAnim:
          if (this.PBiPhaseAnim.Phase1.GetAnimationCyclesNum() == 0)
          {
            this.PBiPhaseAnim.Phase1.Paint(x, y, anchor, angle, origin, source, color, spriteBatch);
            break;
          }
          this.PBiPhaseAnim.Phase2.Paint(x, y, anchor, angle, origin, source, color, spriteBatch);
          break;
        case PType.Clipped:
          GlobalMembers.Manager.PushClip(spriteBatch);
          GlobalMembers.Manager.SetClip(x, y, this.PClipped.W, this.PClipped.H, spriteBatch);
          this.PClipped.Img.Paint(x - this.PClipped.X, y - this.PClipped.Y, spriteBatch);
          GlobalMembers.Manager.PopClip(spriteBatch);
          break;
        case PType.Rect:
          this.Draw(spriteBatch, x, y, height, source, color, angle, origin);
          break;
        case PType.FillRect:
          this.Draw(spriteBatch, x, y, height, source, color, angle, origin);
          break;
        case PType.ImagePart:
          if (this.PImagePart.CoordsNum == 4)
          {
            Rectangle rectangle = new Rectangle((int) this.PImagePart.MinX, (int) ((double) this.PImagePart.Image.GetHeight() - (double) this.PImagePart.MinY - (double) this.PImagePart.H), (int) this.PImagePart.W, (int) this.PImagePart.H);
            this.PImagePart.Image.Paint(x, y, anchor, angle, origin, new Rectangle?(rectangle), color, spriteBatch);
            break;
          }
          spriteBatch.End();
          float[] coords = this.SortCoordsByLeftCorner(new float[6]
          {
            this.PImagePart.Coords[0],
            this.PImagePart.Image.GetHeight() - this.PImagePart.Coords[1],
            this.PImagePart.Coords[2],
            this.PImagePart.Image.GetHeight() - this.PImagePart.Coords[3],
            this.PImagePart.Coords[4],
            this.PImagePart.Image.GetHeight() - this.PImagePart.Coords[5]
          });
          float num = coords[1] - (this.PImagePart.Image.GetHeight() - this.PImagePart.MinY - this.PImagePart.H);
          VertexPositionTexture[] vertexData = this.SetUpVertices(x + GlobalMembers.Manager.TranslatePos.X, GlobalMembers.ScreenHeight - (y + GlobalMembers.Manager.TranslatePos.Y) - this.PImagePart.H + num, coords, this.PImagePart.Image.GetTexture());
          foreach (EffectPass pass in DispManager.BasicEffect.CurrentTechnique.Passes)
          {
            pass.Apply();
            DispManager.GraphicsDev.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, vertexData, 0, 1, VertexPositionTexture.VertexDeclaration);
          }
          spriteBatch.Begin();
          break;
        case PType.Modulate:
          float a = (float) ((double) byte.MaxValue * ((double) this.PModulate.Start[3] + (double) Math.Min(this.PModulate.Time, this.PModulate.Duration) / (double) this.PModulate.Duration * (double) this.PModulate.D[3]));
          this.PModulate.Img.Paint(x, y, anchor, angle, origin, source, new Microsoft.Xna.Framework.Color((float) byte.MaxValue, (float) byte.MaxValue, (float) byte.MaxValue, a), spriteBatch);
          break;
        case PType.Rotated:
          this.PRotated.Img.Paint(x + this.PRotated.AxisX, y + this.PRotated.AxisY, anchor, this.PRotated.Angle, new Vector2(this.PRotated.AxisX, this.PRotated.AxisY), new Rectangle?(), Microsoft.Xna.Framework.Color.White, spriteBatch);
          break;
        case PType.ColoredPolygon:
          spriteBatch.End();
          for (int index = 0; index < this.PColoredPolygon.PointList.Length; ++index)
          {
            Paintable._V[index].Position = this.PColoredPolygon.PointList[index].Position - new Vector3(GlobalMembers.MGame.ViewPos.X * GlobalMembers.TileSize.X, GlobalMembers.MGame.ViewPos.Y * GlobalMembers.TileSize.Y, 0.0f);
            Paintable._V[index].Color = this.PColoredPolygon.PointList[index].Color;
          }
          foreach (EffectPass pass in Paintable.effect.CurrentTechnique.Passes)
          {
            pass.Apply();
            DispManager.GraphicsDev.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, Paintable._V, 0, 2, VertexPositionColor.VertexDeclaration);
          }
          spriteBatch.Begin();
          break;
      }
    }

    public void Update(float time)
    {
      switch (this.PType)
      {
        case PType.Animation:
          this.PAnimation.AnimationTimeElapsed += time;
          break;
        case PType.Group:
          using (List<Pair<Paintable, Point>>.Enumerator enumerator = this.PGroup.Elements.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.First.Update(time);
            break;
          }
        case PType.BiphaseAnim:
          if (this.PBiPhaseAnim.Phase1.GetAnimationCyclesNum() == 0)
          {
            this.PBiPhaseAnim.Phase1.Update(time);
            break;
          }
          this.PBiPhaseAnim.Phase2.Update(time);
          break;
        case PType.Modulate:
          this.PModulate.Time += time;
          break;
        case PType.Rotated:
          this.PRotated.Img.Update(time);
          break;
      }
    }

    public void AddToLoad()
    {
      switch (this.PType)
      {
        case PType.Unloaded:
        case PType.Image:
          if (string.IsNullOrEmpty(this.ResourceId))
            break;
          GlobalMembers.ResManLoader.AddToLoad(this.ResourceId);
          break;
        case PType.Animation:
          for (int index = 0; index < this.PAnimation.FramesNum; ++index)
            this.PAnimation.Frames[index].AddToLoad();
          break;
        case PType.Group:
          using (List<Pair<Paintable, Point>>.Enumerator enumerator = this.PGroup.Elements.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.First.AddToLoad();
            break;
          }
        case PType.BiphaseAnim:
          this.PBiPhaseAnim.Phase1.AddToLoad();
          this.PBiPhaseAnim.Phase2.AddToLoad();
          break;
        case PType.Clipped:
          this.PClipped.Img.AddToLoad();
          break;
        case PType.ImagePart:
          this.PImagePart.Image.AddToLoad();
          break;
      }
    }

    public string GetResourceId() => this.ResourceId;

    public PType GetPType() => this.PType;

    public int GetAnimationCyclesNum()
    {
      return this.PType == PType.Animation ? (int) ((double) this.PAnimation.AnimationTimeElapsed / (double) this.PAnimation.AnimationLength) : 1;
    }

    public List<Pair<Paintable, Point>> GetElements() => this.PGroup.Elements;

    public void SetWidth(float width)
    {
      switch (this.PType)
      {
        case PType.Group:
          this.PGroup.W = width;
          break;
        case PType.InvisibleRect:
          this.PInvisible.W = width;
          break;
        case PType.Rect:
        case PType.FillRect:
          this.PRects.W = width;
          break;
      }
    }

    public void SetHeight(float height)
    {
      switch (this.PType)
      {
        case PType.Group:
          this.PGroup.H = height;
          break;
        case PType.InvisibleRect:
          this.PInvisible.H = height;
          break;
        case PType.Rect:
        case PType.FillRect:
          this.PRects.H = height;
          break;
      }
    }

    public void SetAnimationTimeElapsed(float time) => this.PAnimation.AnimationTimeElapsed = time;

    public float GetAnimationDuration() => this.PAnimation.AnimationLength;

    public void SetAnimationDuration(float duration) => this.PAnimation.AnimationLength = duration;

    private float TransformY(float y, float height, int anchor)
    {
      if ((1 & anchor) > 0)
        return (float) ((double) y - (double) height + 1.0);
      return (2 & anchor) > 0 ? y - height / 2f : y;
    }

    private float TransformX(float x, float width, int anchor)
    {
      if ((32 & anchor) > 0)
        return x - width;
      return (16 & anchor) > 0 ? x - width / 2f : x;
    }

    private static Pair<int, int> LoadInt(string s, int pos)
    {
      int second = 0;
      char ch1;
      char ch2;
      for (; pos < s.Length && (ch2 = s[pos]) >= '0' && (ch1 = s[pos]) <= '9'; ++pos)
        second = second * 10 + (int) ch1 - 48;
      return new Pair<int, int>(pos, second);
    }

    public Texture2D GetTexture()
    {
      switch (this.PType)
      {
        case PType.Unloaded:
        case PType.Image:
          return this.PImage.Texture;
        case PType.Animation:
          return this.GetCurrentFrame().GetTexture();
        case PType.BiphaseAnim:
          return this.PBiPhaseAnim.Phase1.GetAnimationCyclesNum() != 0 ? this.PBiPhaseAnim.Phase2.GetTexture() : this.PBiPhaseAnim.Phase1.GetTexture();
        case PType.Clipped:
          return this.PClipped.Img.GetTexture();
        case PType.Rect:
        case PType.FillRect:
          return this.PRects.Texture;
        case PType.ImagePart:
          return this.PImagePart.Image.GetTexture();
        case PType.Modulate:
          return this.PModulate.Img.GetTexture();
        case PType.Rotated:
          return this.PRotated.Img.GetTexture();
        default:
          return (Texture2D) null;
      }
    }

    private float[] SortCoordsByUpperCorner(float[] coords)
    {
      float[] numArray1 = new float[coords.Length];
      List<int> intList = new List<int>(2);
      float maxValue = float.MaxValue;
      float num1 = MathHelper.Min(coords[1], MathHelper.Min(coords[3], MathHelper.Min(coords[5], maxValue)));
      for (int index = 0; index < coords.Length; index += 2)
      {
        if ((double) coords[index + 1] == (double) num1)
          intList.Add(index);
      }
      int index1;
      int index2;
      int index3;
      if (intList.Count == 2)
      {
        if ((double) coords[intList[0]] < (double) coords[intList[1]])
        {
          index1 = intList[0];
          index2 = intList[1];
        }
        else
        {
          index1 = intList[1];
          index2 = intList[0];
        }
        int num2;
        switch (index1)
        {
          case 0:
            num2 = index2 == 2 ? 4 : 2;
            break;
          case 2:
            num2 = index2 == 0 ? 4 : 0;
            break;
          default:
            num2 = index2 == 0 ? 2 : 0;
            break;
        }
        index3 = num2;
      }
      else
      {
        index1 = intList[0];
        int[] numArray2 = new int[2];
        if (intList[0] == 0)
        {
          numArray2[0] = 2;
          numArray2[1] = 4;
        }
        else if (intList[0] == 2)
        {
          numArray2[0] = 0;
          numArray2[1] = 4;
        }
        else
        {
          numArray2[0] = 0;
          numArray2[1] = 2;
        }
        if ((double) coords[numArray2[0]] > (double) coords[numArray2[1]])
        {
          index2 = numArray2[0];
          index3 = numArray2[1];
        }
        else
        {
          index2 = numArray2[1];
          index3 = numArray2[0];
        }
      }
      numArray1[0] = coords[index1];
      numArray1[1] = coords[index1 + 1];
      numArray1[2] = coords[index2];
      numArray1[3] = coords[index2 + 1];
      numArray1[4] = coords[index3];
      numArray1[5] = coords[index3 + 1];
      return numArray1;
    }

    private float[] SortCoordsByLeftCorner(float[] coords)
    {
      float[] numArray1 = new float[coords.Length];
      List<int> intList = new List<int>(2);
      float maxValue = float.MaxValue;
      float num1 = MathHelper.Min(coords[0], MathHelper.Min(coords[2], MathHelper.Min(coords[4], maxValue)));
      for (int index = 0; index < coords.Length; index += 2)
      {
        if ((double) coords[index] == (double) num1)
          intList.Add(index + 1);
      }
      int index1;
      int index2;
      int index3;
      if (intList.Count == 2)
      {
        if ((double) coords[intList[0]] < (double) coords[intList[1]])
        {
          index1 = intList[0];
          index2 = intList[1];
        }
        else
        {
          index1 = intList[1];
          index2 = intList[0];
        }
        int num2;
        switch (index1)
        {
          case 1:
            num2 = index2 == 3 ? 5 : 3;
            break;
          case 3:
            num2 = index2 == 1 ? 5 : 1;
            break;
          default:
            num2 = index2 == 1 ? 3 : 1;
            break;
        }
        index3 = num2;
      }
      else
      {
        index1 = intList[0];
        int[] numArray2 = new int[2];
        if (intList[0] == 1)
        {
          numArray2[0] = 3;
          numArray2[1] = 5;
        }
        else if (intList[0] == 3)
        {
          numArray2[0] = 1;
          numArray2[1] = 5;
        }
        else
        {
          numArray2[0] = 1;
          numArray2[1] = 3;
        }
        if ((double) coords[numArray2[0]] < (double) coords[numArray2[1]])
        {
          index3 = numArray2[0];
          index2 = numArray2[1];
        }
        else
        {
          index3 = numArray2[1];
          index2 = numArray2[0];
        }
      }
      numArray1[0] = coords[index1 - 1];
      numArray1[1] = coords[index1];
      numArray1[2] = coords[index3 - 1];
      numArray1[3] = coords[index3];
      numArray1[4] = coords[index2 - 1];
      numArray1[5] = coords[index2];
      return numArray1;
    }

    private VertexPositionTexture[] SetUpVertices(
      float x,
      float y,
      float[] coords,
      Texture2D texture)
    {
      if (coords.Length != 6)
        throw new Exception("Paintable - drawImagePart, currently only triangles can be drawn using BasicEffect");
      VertexPositionTexture[] vertexPositionTextureArray = new VertexPositionTexture[3];
      vertexPositionTextureArray[0].Position = new Vector3(x, y, 0.0f);
      vertexPositionTextureArray[0].TextureCoordinate.X = coords[0] / (float) texture.Width;
      vertexPositionTextureArray[0].TextureCoordinate.Y = coords[1] / (float) texture.Height;
      vertexPositionTextureArray[1].Position = new Vector3(x + (coords[2] - coords[0]), y + (coords[3] - coords[1]), 0.0f);
      vertexPositionTextureArray[1].TextureCoordinate.X = coords[2] / (float) texture.Width;
      vertexPositionTextureArray[1].TextureCoordinate.Y = coords[3] / (float) texture.Height;
      vertexPositionTextureArray[2].Position = new Vector3(x + (coords[4] - coords[0]), y + (coords[5] - coords[1]), 0.0f);
      vertexPositionTextureArray[2].TextureCoordinate.X = coords[4] / (float) texture.Width;
      vertexPositionTextureArray[2].TextureCoordinate.Y = coords[5] / (float) texture.Height;
      return vertexPositionTextureArray;
    }

    private void Draw(
      SpriteBatch spriteBatch,
      float x,
      float y,
      float heightOfTexture,
      Rectangle? source,
      Microsoft.Xna.Framework.Color color,
      float rotation,
      Vector2 origin)
    {
      this.Draw(spriteBatch, x + GlobalMembers.Manager.TranslatePos.X, y + GlobalMembers.Manager.TranslatePos.Y, heightOfTexture, source, color, rotation, origin, 1f, this.Mirror ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.0f);
    }

    private void Draw(
      SpriteBatch spriteBatch,
      float x,
      float y,
      float heightOfTexture,
      Rectangle? source,
      Microsoft.Xna.Framework.Color color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects spriteEffects,
      float layerDepth)
    {
      if (!source.HasValue)
        spriteBatch.Draw(this.GetTexture(), new Vector2(x, GlobalMembers.ScreenHeight - y - heightOfTexture), new Rectangle?(), color, rotation, origin, scale, spriteEffects, layerDepth);
      else
        spriteBatch.Draw(this.GetTexture(), new Vector2(x, GlobalMembers.ScreenHeight - y - (float) source.Value.Height), source, color, rotation, origin, scale, spriteEffects, layerDepth);
    }

    public static void SetFrameMirror(Paintable[] frames, bool b)
    {
      for (int index = 0; index < frames.Length; ++index)
        frames[index].Mirror = b;
    }
  }
}
