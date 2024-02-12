// GameManager.GameLogic.MenuItem

using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace GameManager.GameLogic
{
  public class MenuItem : Sprite
  {
    public static Paintable ScrollBarTop = 
            Paintable.CreateFilledRect(2f, 10f, Util.ConvertIntToColor(2164228351U));

    public static Paintable ScrollBarBottom = 
            Paintable.CreateFilledRect(2f, 10f, Util.ConvertIntToColor(2155937791U));

    public static Paintable ScrollBarMid = 
            Paintable.CreateFilledRect(2f, 10f, Util.ConvertIntToColor(4278190335U));

    public static Paintable ScrollSelector = 
            Paintable.CreateFilledRect(4f, 4f, Util.ConvertIntToColor(16711935U));

    private readonly string _label;
    public int UserData;
    private Paintable _focused;
    private Paintable _formated;
    private float _height;
    private bool _isDownPressed;
    private bool _isNavigable;
    private bool _isResizeable;
    private bool _isSelected;
    private bool _isUpPressed;
    private float _minHeight;
    private float _padH;
    private float _padV;
    private float _preferredHeight;
    private Paintable _pressed;
    private float _progress;
    private float _py;
    private Paintable _unFocused;
    private bool _wasAdded;
    private float _width;
    private float _yOffset;

    public MenuItem(int type, string label)
      : base(type, new GameManager.GraphicsSystem.Point(0.0f, 0.0f))
    {
      this._label = label;
      this._wasAdded = false;
      this._preferredHeight = 0.0f;
      this._minHeight = 0.0f;
      this._yOffset = 0.0f;
      this._py = 0.0f;
      this._padV = 0.0f;
      this._padH = 0.0f;
      this._width = 0.0f;
      this._height = 0.0f;
      this._isUpPressed = false;
      this._isDownPressed = false;
    }

    public static Sprite CreateSlide(float progress)
    {
      MenuItem slide = new MenuItem(-10004, "")
      {
        _isResizeable = false,
        _isNavigable = false,
        _progress = progress
      };
      slide.Ref = (Sprite) slide;
      return (Sprite) slide;
    }

    public static Sprite CreateLabel()
    {
      MenuItem label = new MenuItem(-10001, "");
      label.Ref = (Sprite) label;
      label._isResizeable = label._isNavigable = false;
      return (Sprite) label;
    }

    public static Sprite CreateScrollable(string label)
    {
      MenuItem scrollable = new MenuItem(-10002, label);
      scrollable.Ref = (Sprite) scrollable;
      scrollable._isResizeable = scrollable._isNavigable = true;
      return (Sprite) scrollable;
    }

    public static Sprite CreateButton(string label)
    {
      return (Sprite) new MenuItem(-10003, label)
      {
        _isResizeable = false,
        _isNavigable = true
      };
    }

    public string GetLabel() => this._label;

    public void SetImg(Paintable img)
    {
      this.Img = img;
      if (this.TypeId != -10001 && this.TypeId != -10004)
        return;
      this._width = Math.Max(this._width, img.GetWidth());
      this._height = Math.Max(this._height, img.GetHeight());
    }

    public void SetUnFocused(Paintable img)
    {
      this._unFocused = img;
      this._width = Math.Max(this._width, img.GetWidth());
      this._height = Math.Max(this._height, img.GetHeight());
      this.Img = img;
    }

    public void SetFocused(Paintable img)
    {
      this._focused = img;
      this._width = Math.Max(this._width, img.GetWidth());
      this._height = Math.Max(this._height, img.GetHeight());
      if ((double) this._preferredHeight != 0.0 || this.TypeId != -10002)
        return;
      this._preferredHeight = img.GetHeight();
    }

    public void SetPressed(Paintable img)
    {
      this._pressed = img;
      this._width = Math.Max(this._width, img.GetWidth());
      this._height = Math.Max(this._height, img.GetHeight());
    }

    public void SetFormated(Paintable img)
    {
      this._formated = img;
      if (this.TypeId == -10001)
      {
        this._width = Math.Max(this._width, img.GetWidth());
        this._height = Math.Max(this._height, img.GetHeight());
      }
      else
      {
        if (this.TypeId != -10002)
          return;
        this._yOffset = 0.0f;
      }
    }

    public override float GetWidth()
    {
      return this.TypeId == -10003 ? GlobalMembers.FromPx(Math.Max(this._width, (float) ((double) this._padH * 2.0 + (this._formated != null ? (double) this._formated.GetWidth() : 0.0)))) : GlobalMembers.FromPx(this._width);
    }

    public override float GetHeight()
    {
      return this.TypeId == -10003 ? GlobalMembers.FromPx(Math.Max(this._height, (float) ((double) this._padV * 2.0 + (this._formated != null ? (double) this._formated.GetHeight() : 0.0)))) : GlobalMembers.FromPx(this._height);
    }

    public override void Update(float time)
    {
      base.Update(time);
      if (this.TypeId != -10002)
        return;
      if (this._isUpPressed)
        this._yOffset = Math.Max(0.0f, this._yOffset - time * 80f);
      if (!this._isDownPressed)
        return;
      this._yOffset = Math.Min((float) ((double) this._formated.GetHeight() - (double) this.Img.GetHeight() + 2.0 * (double) this._padV), this._yOffset + time * 80f);
    }

    public bool KeyPressed(AbsKey key)
    {
      if (this.TypeId == -10003)
      {
        if (key == AbsKey.Ok && this._pressed != null)
          this.Img = this._pressed;
      }
      else if (this.TypeId == -10002)
      {
        switch (key)
        {
          case AbsKey.Up:
            if ((double) this._yOffset == 0.0)
              return false;
            this._isUpPressed = true;
            return true;
          case AbsKey.Down:
            if ((double) this.Img.GetHeight() < (double) GlobalMembers.ToPx(this.GetHeight()) || (double) this._yOffset >= (double) this._formated.GetHeight() - (double) this.Img.GetHeight() + 2.0 * (double) this._padV)
              return false;
            this._isDownPressed = true;
            return true;
        }
      }
      else if (this.TypeId == -10004 && (key == AbsKey.Left || key == AbsKey.Right))
      {
        this._progress = MathHelper.Clamp(this._progress + (key == AbsKey.Left ? -0.05f : 0.05f), 0.0f, 1f);
        GlobalMembers.SfxButtonHoverInstance.Play();
      }
      return false;
    }

    public bool KeyReleased(AbsKey key)
    {
      if (this.TypeId == -10003)
      {
        if (key == AbsKey.Ok)
          this.Img = this._focused != null ? this._unFocused : this._focused;
      }
      else if (this.TypeId == -10002)
      {
        if (key == AbsKey.Down)
        {
          this._isDownPressed = false;
          return true;
        }
        if (key == AbsKey.Up)
        {
          this._isUpPressed = false;
          return true;
        }
      }
      return false;
    }

    public override void OnAdd() => this._wasAdded = true;

    public float GetMinimumHeight() => this.TypeId == -10002 ? this._minHeight : this.GetHeight();

    public float GetPreferredHeight()
    {
      return this.TypeId == -10002 ? this.Img.GetWidth() + this._padV * 2f : this.GetHeight();
    }

    public float GetMaximumHeight()
    {
      return this.TypeId == -10002 ? this.Img.GetWidth() + this._padV * 2f : this.GetHeight();
    }

    public void SetMinHeight(float minHeight) => this._minHeight = minHeight;

    public void SetHeight(float height)
    {
      if (this.TypeId != -10002)
        return;
      this._height = height;
      this._yOffset = 0.0f;
      MenuItem.RescaleLook(this._width, this._height, this._focused);
      MenuItem.RescaleLook(this._width, this._height, this._unFocused);
    }

    public void Load()
    {
      this.Img.AddToLoad();
      if (this._formated != null)
        this._formated.AddToLoad();
      if (this._focused != null)
        this._focused.AddToLoad();
      if (this._unFocused != null)
        this._unFocused.AddToLoad();
      if (this._pressed != null)
        this._pressed.AddToLoad();
      MenuItem.ScrollBarBottom.AddToLoad();
      MenuItem.ScrollBarMid.AddToLoad();
      MenuItem.ScrollBarTop.AddToLoad();
      MenuItem.ScrollSelector.AddToLoad();
    }

    public void SetIsSelected(bool _isSelected)
    {
      this._isSelected = _isSelected;
      if (this.TypeId == -10002)
      {
        if (this._isSelected && this._focused != null)
          this.Img = this._focused;
        else
          this.Img = this._unFocused;
      }
      if (this.TypeId != -10003)
        return;
      GlobalMembers.SfxButtonHoverInstance.Play();
      if (this._isSelected && this._focused != null)
        this.Img = this._focused;
      else
        this.Img = this._unFocused;
    }

    public void PointerEntered(GameManager.GraphicsSystem.Point pos)
    {
    }

    public void PointerLeft(GameManager.GraphicsSystem.Point pos)
    {
    }

    public void PointerPressed(GameManager.GraphicsSystem.Point p)
    {
      if (this.TypeId == -10002)
        this._py = p.Y;
      if (this.TypeId != -10004)
        return;
      this._progress = (float) (((double) p.X - (double) GlobalMembers.ToPx(this.Pos.X) - (double) this._formated.GetWidth() / 2.0) / ((double) GlobalMembers.ToPx(this.GetWidth()) - (double) this._formated.GetWidth() / 2.0));
      GlobalMembers.SfxButtonHoverInstance.Play();
    }

    public void PointerReleased(GameManager.GraphicsSystem.Point p)
    {
    }

    public void PointerMoved(GameManager.GraphicsSystem.Point p)
    {
      if (this.TypeId == -10002)
      {
        this._yOffset = Math.Min((float) ((double) this._formated.GetHeight() - (double) this.Img.GetHeight() + 2.0 * (double) this._padV), Math.Max(0.0f, this._yOffset + (float) (((double) p.Y - (double) this._py) * 5.0)));
        this._py = this.Pos.Y;
      }
      if (this.TypeId != -10004)
        return;
      this._progress = (float) (((double) p.X - (double) GlobalMembers.ToPx(this.Pos.X) - (double) this._formated.GetWidth() / 2.0) / ((double) GlobalMembers.ToPx(this.GetWidth()) - (double) this._formated.GetWidth() / 2.0));
    }

    public void CopySizeParams(MenuItem b)
    {
      b._isNavigable = this._isNavigable;
      b._isResizeable = this._isResizeable;
      b._padH = this._padH;
      b._padV = this._padV;
    }

    public static Paintable RescaleLook(float w, float h, Paintable p)
    {
      if (p.PType != PType.Group)
      {
        Paintable group = Paintable.CreateGroup(true);
        group.AddElement(0.0f, 0.0f, p);
        p = group;
      }
      for (int index = 0; index < p.GetElements().Count; ++index)
      {
        Paintable first = p.GetElements()[index].First;
        switch (first.PType)
        {
          case PType.Unloaded:
          case PType.Image:
          case PType.Group:
            w = Math.Max(w, first.GetWidth());
            h = Math.Max(h, first.GetHeight());
            break;
        }
      }
      p.SetWidth(w);
      p.SetHeight(h);
      for (int index = 0; index < p.GetElements().Count; ++index)
      {
        Paintable first = p.GetElements()[index].First;
        first.SetWidth(w);
        first.SetHeight(h);
      }
      return p;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
      if (this.TypeId == -10003 || this.TypeId == -10001)
      {
        this.Img.Paint(GlobalMembers.ToPx(this.Pos.X + this.GetWidth() / 2f), GlobalMembers.ToPx(this.Pos.Y + this.GetHeight() / 2f), 18, spriteBatch);
        if (this._formated == null)
          return;
        this._formated.Paint(GlobalMembers.ToPx(this.Pos.X + this.GetWidth() / 2f), GlobalMembers.ToPx(this.Pos.Y + this.GetHeight() / 2f) + 4f, 18, spriteBatch);
      }
      else if (this.TypeId == -10002)
      {
        this.Img.Paint(GlobalMembers.ToPx(this.Pos.X), GlobalMembers.ToPx(this.Pos.Y), 12, spriteBatch);
        GlobalMembers.Manager.PushClip(spriteBatch);
        GlobalMembers.Manager.SetClip(GlobalMembers.ToPx(this.GetPos().X) + this._padH, GlobalMembers.ToPx(this.GetPos().Y) + this._padV, GlobalMembers.ToPx(this.GetWidth()) - this._padH * 2f, GlobalMembers.ToPx(this.GetHeight()) - this._padV * 2f, spriteBatch);
        this._formated.Paint(GlobalMembers.ToPx(this.Pos.X) + this._padH, GlobalMembers.ToPx(this.Pos.Y) + this.Img.GetHeight() + this._padV + this._yOffset, 9, spriteBatch);
        GlobalMembers.Manager.PopClip(spriteBatch);
        if ((double) this._formated.GetHeight() <= (double) GlobalMembers.ToPx(this.GetHeight()) - (double) this._padV * 2.0 + 1.0)
          return;
        MenuItem.PaintScroll(GlobalMembers.ToPx(this.Pos.X + this.GetWidth()) - (float) ((double) MenuItem.ScrollBarTop.GetWidth() * 3.0 / 2.0), GlobalMembers.ToPx(this.Pos.Y) + this._padV, GlobalMembers.ToPx(this.GetHeight()) - this._padV * 2f, this._yOffset / (float) ((double) this._formated.GetHeight() - (double) this.Img.GetHeight() + 2.0 * (double) this._padV), MenuItem.ScrollBarTop, MenuItem.ScrollBarBottom, MenuItem.ScrollBarMid, MenuItem.ScrollSelector, spriteBatch);
      }
      else
      {
        if (this.TypeId != -10004)
          return;
        this.Img.Paint(GlobalMembers.ToPx(this.Pos.X), GlobalMembers.ToPx(this.Pos.Y), 12, spriteBatch);
        this._formated.Paint(GlobalMembers.ToPx(this.Pos.X + (this.GetWidth() - GlobalMembers.FromPx(this._formated.GetWidth())) * this._progress), GlobalMembers.ToPx(this.Pos.Y + this.GetHeight() / 2f), 10, spriteBatch);
      }
    }

    public static void PaintScroll(
      float x,
      float y,
      float h,
      float state,
      Paintable scrollBarTop,
      Paintable scrollBarBottom,
      Paintable scrollBarMid,
      Paintable scrollSelector,
      SpriteBatch spriteBatch)
    {
      float num = h - scrollBarTop.GetHeight() - scrollBarBottom.GetHeight();
      GlobalMembers.Manager.PushClip(spriteBatch);
      GlobalMembers.Manager.SetClip(0.0f, 0.0f, GlobalMembers.ScreenWidth, GlobalMembers.ScreenHeight, spriteBatch);
      scrollBarTop.Paint(x, (float) ((double) y + (double) h - 1.0), 9, spriteBatch);
      GlobalMembers.Manager.SetClip(0.0f, 0.0f, GlobalMembers.ScreenWidth, GlobalMembers.ScreenHeight, spriteBatch);
      for (int index = 0; (double) index < ((double) num + (double) scrollBarMid.GetHeight() - 1.0) / (double) scrollBarMid.GetHeight(); ++index)
        scrollBarMid.Paint(x, (float) ((double) y + (double) scrollBarBottom.GetHeight() + (double) index * (double) scrollBarMid.GetHeight()), 12, spriteBatch);
      GlobalMembers.Manager.SetClip(0.0f, 0.0f, GlobalMembers.ScreenWidth, GlobalMembers.ScreenHeight, spriteBatch);
      scrollBarBottom.Paint(x, y, 12, spriteBatch);
      GlobalMembers.Manager.PopClip(spriteBatch);
      scrollSelector.Paint(x + scrollBarMid.GetWidth() / 2f, (float) ((double) y + (double) scrollSelector.GetHeight() / 2.0 + ((double) h - (double) scrollSelector.GetHeight()) * (1.0 - (double) state)), 18, spriteBatch);
    }

    public MenuItem Copy()
    {
      MenuItem b = new MenuItem(this.TypeId, this.GetLabel());
      this.CopySizeParams(b);
      if (this.Img != null)
        b.SetImg(new Paintable(this.Img));
      if (this._formated != null)
        b.SetFormated(new Paintable(this._formated));
      if (this._unFocused != null)
        b.SetUnFocused(new Paintable(this._unFocused));
      if (this._focused != null)
        b.SetFocused(new Paintable(this._focused));
      if (this._pressed != null)
        b.SetPressed(new Paintable(this._pressed));
      b._minHeight = this._minHeight;
      b._isNavigable = this._isNavigable;
      b._isResizeable = this._isResizeable;
      b._isNavigable = this._isNavigable;
      b._isResizeable = this._isResizeable;
      b._progress = this._progress;
      return b;
    }

    public float GetProgress() => this._progress;

    public void SetProgress(float progress) => this._progress = progress;

    public bool GetIsResizeable() => this._isResizeable;

    public bool GetIsNavigable() => this._isNavigable;

    public bool GetIsSelected() => this._isSelected;
  }
}
