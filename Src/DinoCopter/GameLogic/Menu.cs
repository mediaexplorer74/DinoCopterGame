// GameManager.GameLogic.Menu

using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace GameManager.GameLogic
{
  public class Menu : Disp
  {
    private List<Sprite> Elements = new List<Sprite>();
    private int SelectedPos;
    private float Ty;
    protected float ChangeTime;
    protected float OffsetY;
    protected float NewOffsetY;
    protected int Valign;
    protected bool FixedSize;
    protected Paintable Bg;
    protected Sprite Header;
    protected Sprite Footer;
    protected Sprite LeftNavi;
    protected Sprite RightNavi;
    protected Disp Next = new Disp();
    protected Sprite SelectedByPointer;
    protected Sprite SelectedByPointerPress;
    protected Point PressPoint;
    protected Point ActualPoint;
    protected Point ReleasePoint;
    protected bool LeftSoftKeyPressedByPointer;
    protected bool RightSoftKeyPressedByPointer;
    protected Disp Prev;
    protected Paintable LeftBg;
    protected bool FirstShow;
    protected Paintable Bg2;
    protected float WidthForContent;
    protected float HeightForContent;
    protected Point ContentStartPos;
    protected int Episode;
    protected int FromColor;
    protected int ToColor;
    protected int Level;
    protected float Time;
    protected string ProductId;
    public static Point MainContentStartPos;
    public static Point BgPos;
    public static Paintable ScrollUp;
    public static Paintable ScrollMid;
    public static Paintable ScrollDown;
    public static Paintable ScrollBar;

    public int Type { get; set; }

    public int GetType() => this.Type;

    public void SetType(int _type) => this.Type = _type;

    public Menu()
      : base(1)
    {
      this.Type = -1;
      this.FixedSize = false;
      this.Valign = 1;
      this.SelectedPos = 0;
      this.ChangeTime = 0.0f;
      this.OffsetY = 0.0f;
      this.NewOffsetY = 0.0f;
      this.Ty = 0.0f;
      this.LeftSoftKeyPressedByPointer = false;
      this.RightSoftKeyPressedByPointer = false;
      this.PressPoint = (Point) null;
      this.ActualPoint = (Point) null;
      this.ReleasePoint = (Point) null;
      this.FirstShow = true;
      this.WidthForContent = (float) ((double) GlobalMembers.ScreenWidth * 9.0 / 10.0);
      this.HeightForContent = (float) ((double) GlobalMembers.ScreenHeight * 9.0 / 10.0);
      this.ContentStartPos = Menu.MainContentStartPos;
    }

    protected static bool IsMenuItem(Sprite s)
    {
      if (s == null)
        return false;
      return s.GetTypeId() == -10004 || s.GetTypeId() == -10003 
                || s.GetTypeId() == -10001 || s.GetTypeId() == -10002;
    }

    protected bool IsPointerInElementsRect(Point pos)
    {
      return Util.IsPointInRect(new Point((pos - this.GetContentStartPos()).X, 
          (pos - this.Ty).Y), new Point(), new Point(this.GetWidthForContent(), this.GetHeightForContent()));
    }

    public static Disp CreateBuyMenu(string description, string productId, Disp next)
    {
      Menu menu = new Menu();
      Disp buyMenu = (Disp) menu;
      menu.SetValign(2);
      menu.Next = next;
      menu.Ref = buyMenu;
      menu.SetBg(Paintable.CreateFromResMan("background"));
      menu.ProductId = productId;
      menu.Type = 15;
      menu.HeightForContent = menu.Bg.GetHeight();
      menu.WidthForContent = menu.Bg.GetWidth();
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), 0.0f);
      menu.SetLeftNavi(menu.CreateNaviLabel(Texts.Buy));
      menu.SetRightNavi(menu.CreateNaviLabel(Texts.Back));
      menu.HeightForContent = menu.Bg.GetHeight() - GlobalMembers.ToPx(menu.RightNavi.GetHeight());
      menu.WidthForContent = (float) ((double) menu.Bg.GetWidth() * 4.0 / 5.0);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), GlobalMembers.ToPx(menu.RightNavi.GetHeight()));
      menu.AddItem(menu.CreateButton(Texts.Restore, Texts.Restore));
      menu.AddItem(menu.CreateLabel(Texts.RestoreDesc));
      menu.AddItem(menu.CreateButton(Texts.UnlockForFree, Texts.UnlockForFree));
      menu.AddItem(menu.CreateLabel(Texts.UnlockForFreeDesc));
      menu.AddItem(menu.CreateLabel(description));
      return buyMenu;
    }

    public static Disp CreateBuyMoreLevels(Disp prev)
    {
      Menu menu = new Menu();
      Disp buyMoreLevels = (Disp) menu;
      menu.SetValign(2);
      menu.Prev = prev;
      menu.Ref = buyMoreLevels;
      menu.SetBg(Paintable.CreateFromResMan("background"));
      menu.Type = 18;
      menu.HeightForContent = menu.Bg.GetHeight();
      menu.WidthForContent = menu.Bg.GetWidth();
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), 0.0f);
      menu.SetLeftNavi(menu.CreateNaviLabel(Texts.Unlock));
      menu.SetRightNavi(menu.CreateNaviLabel(Texts.Back));
      menu.HeightForContent = menu.Bg.GetHeight() - GlobalMembers.ToPx(menu.RightNavi.GetHeight());
      menu.WidthForContent = (float) ((double) menu.Bg.GetWidth() * 4.0 / 5.0);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), GlobalMembers.ToPx(menu.RightNavi.GetHeight()));
      menu.AddItem(menu.CreateButton(Texts.Buy, Texts.Buy));
      menu.AddItem(menu.CreateLabel(Texts.BuyFullVersion));
      menu.AddItem(menu.CreateLabel(Texts.Shells));
      menu.AddItem(menu.CreateLabel("Test"));
      menu.AddItem(menu.CreateLabel(Texts.YouHave));
      menu.AddItem(menu.CreateButton(Texts.CheckACtions, Texts.CheckACtions));
      menu.AddItem(menu.CreateLabel(Texts.UnlockMoreLevels));
      return buyMoreLevels;
    }

    public static Disp CreateTapJoy(Disp prev)
    {
      Menu menu = new Menu();
      Disp tapJoy = (Disp) menu;
      menu.SetValign(2);
      menu.Prev = prev;
      menu.Ref = tapJoy;
      menu.SetBg(Paintable.CreateFromResMan("background"));
      menu.Type = 17;
      menu.HeightForContent = menu.Bg.GetHeight();
      menu.WidthForContent = menu.Bg.GetWidth();
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), 0.0f);
      menu.SetLeftNavi(menu.CreateNaviLabel(Texts.Unlock));
      menu.SetRightNavi(menu.CreateNaviLabel(Texts.Back));
      menu.HeightForContent = menu.Bg.GetHeight() - GlobalMembers.ToPx(menu.RightNavi.GetHeight());
      menu.WidthForContent = (float) ((double) menu.Bg.GetWidth() * 4.0 / 5.0);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), GlobalMembers.ToPx(menu.RightNavi.GetHeight()));
      menu.AddItem(menu.CreateLabel(Texts.Shells));
      menu.AddItem(menu.CreateLabel("Test"));
      menu.AddItem(menu.CreateLabel(Texts.YouHave));
      menu.AddItem(menu.CreateButton(Texts.CheckACtions, Texts.CheckACtions));
      menu.AddItem(menu.CreateLabel(Texts.TapjoyDesc));
      return tapJoy;
    }

    public static Disp CreateRateMenu(Disp next)
    {
      Menu menu = new Menu();
      Disp rateMenu = (Disp) menu;
      menu.SetValign(2);
      menu.Ref = rateMenu;
      menu.Next = next;
      menu.SetBg(Paintable.CreateFilledRect(GlobalMembers.ScreenWidth, GlobalMembers.ScreenHeight, Util.ConvertIntToColor(uint.MaxValue)));
      menu.HeightForContent = menu.Bg.GetHeight();
      menu.WidthForContent = (float) ((double) menu.Bg.GetWidth() * 4.0 / 5.0);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), 0.0f);
      menu.Type = 10;
      menu.SetLeftNavi(menu.CreateNaviLabel("Skip"));
      menu.SetRightNavi(menu.CreateNaviLabel("Rate"));
      menu.AddItem(menu.CreateLabel("#1If you enjoy playing, please rate this app. If people will like this game we will release more levels."));
      return rateMenu;
    }

    public static Disp CreateLogo(Disp next, int fromColor, int toColor)
    {
      Disp textDisplayerMenu = Menu.CreateTextDisplayerMenu(new Paintable(), "", next, new Paintable(), "");
      Menu menu = (Menu) textDisplayerMenu;
      menu.FromColor = fromColor;
      menu.ToColor = toColor;
      menu.Type = 7;
      menu.SetBg(Paintable.CreateFilledRect(GlobalMembers.ScreenWidth, GlobalMembers.ScreenHeight, Util.ConvertIntToColor((uint) byte.MaxValue)));
      return textDisplayerMenu;
    }

    public static Disp CreateSplash(Paintable img, Disp next, int fromColor, int toColor)
    {
      Disp textDisplayerMenu = Menu.CreateTextDisplayerMenu(new Paintable(), "", next, new Paintable(), "");
      Menu menu = (Menu) textDisplayerMenu;
      menu.FromColor = fromColor;
      menu.ToColor = toColor;
      menu.Type = 6;
      Paintable group = Paintable.CreateGroup(false, textDisplayerMenu.Width, textDisplayerMenu.Height);
      group.AddElement(0.0f, 0.0f, Paintable.CreateFilledRect(group.GetWidth(), group.GetHeight(), Util.ConvertIntToColor((uint) byte.MaxValue)));
      group.AddElement(group.GetWidth() / 2f, group.GetHeight() / 2f, img, 18);
      menu.SetBg(group);
      return textDisplayerMenu;
    }

    public static Disp CreateAdsMenu(Disp next)
    {
      Menu menu = new Menu();
      Disp adsMenu = (Disp) menu;
      menu.Ref = adsMenu;
      menu.Next = next;
      menu.SetValign(2);
      Paintable bg = menu.CreateBg(false);
      menu.SetBg(bg);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), 0.0f);
      menu.HeightForContent = menu.Bg.GetHeight();
      menu.WidthForContent = menu.Bg.GetWidth() * 0.9f;
      if (GlobalMembers.HasAds)
        GlobalMembers.Manager.ShowAds();
      menu.AddItem(menu.CreateButton(Texts.Buy, Texts.Buy));
      menu.AddItem(menu.CreateLabel("#1To remove ads buy full version"));
      menu.AddItem(menu.CreateLabel("#1 This app is free, thanks to players who click ads."));
      menu.Type = 9;
      menu.SetNaviLabelsBg();
      return adsMenu;
    }

    public static Disp CreateLiteMenu()
    {
      Menu menu = new Menu();
      Disp liteMenu = (Disp) menu;
      menu.Ref = liteMenu;
      Paintable filledRect1 = Paintable.CreateFilledRect(GlobalMembers.ScreenWidth, GlobalMembers.ScreenHeight, Util.ConvertIntToColor(uint.MaxValue));
      Paintable group = Paintable.CreateGroup(false, filledRect1.GetWidth(), filledRect1.GetHeight());
      group.AddElement(0.0f, 0.0f, filledRect1);
      Paintable filledRect2 = Paintable.CreateFilledRect(GlobalMembers.ScreenWidth / 2f, GlobalMembers.ScreenHeight / 2f, Util.ConvertIntToColor(uint.MaxValue));
      group.AddElement(group.GetWidth() / 2f, group.GetHeight() / 2f, filledRect2, 18);
      Paintable formatedPaintable = Paintable.CreateFormatedPaintable("#1You have finished lite version. To enjoy rest of levels buy full version.", (string[]) null, 0, group.GetWidth(), 16, true);
      group.AddElement(group.GetWidth() / 2f, group.GetHeight(), Paintable.CreateFilledRect(group.GetWidth(), 5f + formatedPaintable.GetHeight(), Util.ConvertIntToColor((uint) byte.MaxValue)), 17);
      group.AddElement(group.GetWidth() / 2f, group.GetHeight() - 3f, formatedPaintable, 17);
      menu.AddItem(menu.CreateLabel(""));
      menu.SetBg(group);
      menu.Type = 8;
      if (DispManager.IsNetworkAvailable())
        menu.SetLeftNavi(menu.CreateNaviLabel("Buy"));
      menu.SetRightNavi(menu.CreateNaviLabel("Back"));
      return liteMenu;
    }

    public static Disp CreateDebugLevelSelect()
    {
      Menu menu = new Menu();
      Disp debugLevelSelect = (Disp) menu;
      menu.SetValign(4);
      menu.Ref = debugLevelSelect;
      menu.SetBg(menu.CreateBg());
      menu.HeightForContent = (float) ((double) menu.Bg.GetHeight() - (double) Paintable.CreateFromResMan("game_logo").GetHeight() - 100.0);
      menu.WidthForContent = menu.Bg.GetWidth() / 2f;
      menu.ContentStartPos = new Point(0.0f, 75f);
      menu.Type = 1;
      for (int index = 0; index < 30; ++index)
      {
        string str = string.Format("{0}", (object) string.Format("Level {0:D}", (object) (index + 1)));
        menu.AddItem(menu.CreateButton(str, str));
      }
      return debugLevelSelect;
    }

    public static Disp CreateLevelSelect()
    {
      Menu menu = new Menu();
      Disp levelSelect = (Disp) menu;
      menu.Ref = levelSelect;
      menu.SetBg(menu.CreateBg(false));
      menu.ContentStartPos = new Point((float) (((double) menu.Width - (double) menu.WidthForContent) / 2.0), menu.Height - menu.HeightForContent);
      if (GlobalMembers.IsFreeVersion)
      {
        menu.SetLeftNavi(menu.CreateNaviLabel(Texts.GetMore));
        menu.LeftNavi.SetPos(new Point(GlobalMembers.FromPx(menu.Width) - menu.LeftNavi.GetWidth(), GlobalMembers.FromPx(menu.Height) - menu.LeftNavi.GetHeight()));
      }
      menu.SetRightNavi(menu.CreateNaviLabel(Texts.Back));
      menu.RightNavi.SetPos(new Point(GlobalMembers.FromPx(menu.Width / 2f) - menu.RightNavi.GetWidth() / 2f, GlobalMembers.FromPx(menu.Height / 20f)));
      float px = GlobalMembers.ToPx(menu.RightNavi.GetPos().Y + menu.RightNavi.GetHeight());
      menu.HeightForContent = (float) ((double) menu.Bg.GetHeight() * 4.0 / 5.0);
      menu.WidthForContent = (float) ((double) menu.Width * 9.0 / 10.0);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), px);
      menu.SetValign(2);
      menu.Type = 2;
      Paintable fromResMan1 = Paintable.CreateFromResMan("level_select_button_unselect");
      Paintable fromResMan2 = Paintable.CreateFromResMan("level_select_button_select");
      Paintable fromResMan3 = Paintable.CreateFromResMan("level_inactive_icon");
      Paintable fromResMan4 = Paintable.CreateFromResMan("play_icon");
      Paintable[] paintableArray = new Paintable[3]
      {
        Paintable.CreateFromResMan("little_stars_31"),
        Paintable.CreateFromResMan("little_stars_32"),
        Paintable.CreateFromResMan("little_stars_33")
      };
      float w = Math.Max(fromResMan1.GetWidth(), paintableArray[0].GetWidth());
      float num1 = fromResMan1.GetHeight() + paintableArray[0].GetHeight();
      float num2 = fromResMan1.GetWidth() / 8f;
      float num3 = paintableArray[0].GetWidth() / 4f;
      int num4 = (int) (((double) menu.WidthForContent - (double) num2) / ((double) w + (double) num2));
      int num5 = (30 + num4 - 1) / num4;
      for (int index1 = 0; index1 < num5; ++index1)
      {
        int num6;
        if (index1 == 0)
        {
          num6 = 30 % num4;
          if (num6 == 0)
            num6 = num4;
        }
        else
          num6 = num4;
        float a = (float) (((double) menu.WidthForContent - (double) w - ((double) w + (double) num2) * (double) (num6 - 1)) / 2.0) + menu.ContentStartPos.X;
        for (int index2 = 0; index2 < num6; ++index2)
        {
          int level = (num5 - 1 - index1) * num4 + index2;
          Paintable group1 = Paintable.CreateGroup(false, w, num1);
          group1.AddElement(0.0f, num1, fromResMan1, 9);
          if (Menu.IsLevelToBoughtIcon(level))
            group1.AddElement(fromResMan1.GetWidth() / 2f, num1 - fromResMan1.GetHeight() / 2f, Paintable.CreateFromResMan("buy_icon"), 18);
          else if (level < GlobalMembers.Save.CurrentLevel())
          {
            group1.AddElement(0.0f, 0.0f, paintableArray[GlobalMembers.Save.GetStarsNum(level, GlobalMembers.Save.GetTime(level)) - 1]);
            string text = string.Format("{0}", (object) string.Format("{0:D}", (object) (level + 1)));
            group1.AddElement(fromResMan1.GetWidth() / 2f, num1 - fromResMan1.GetHeight() / 2f, Paintable.CreateText(text, GlobalMembers.Fonts[1]), 18);
          }
          else if (level == GlobalMembers.Save.CurrentLevel())
            group1.AddElement(fromResMan1.GetWidth() / 2f, num1 - fromResMan1.GetHeight() / 2f, fromResMan4, 18);
          else if (level > GlobalMembers.Save.CurrentLevel())
            group1.AddElement(fromResMan1.GetWidth() / 2f, num1 - fromResMan1.GetHeight() / 2f, fromResMan3, 18);
          Paintable group2 = Paintable.CreateGroup(false, w, num1);
          group2.AddElement(0.0f, num1, fromResMan2, 9);
          if (Menu.IsLevelToBoughtIcon(level))
            group2.AddElement(fromResMan1.GetWidth() / 2f, num1 - fromResMan1.GetHeight() / 2f, Paintable.CreateFromResMan("buy_icon"), 18);
          else if (level < GlobalMembers.Save.CurrentLevel())
          {
            group2.AddElement(0.0f, 0.0f, paintableArray[GlobalMembers.Save.GetStarsNum(level, GlobalMembers.Save.GetTime(level)) - 1]);
            string text = string.Format("{0}", (object) string.Format("{0:D}", (object) (level + 1)));
            group2.AddElement(fromResMan1.GetWidth() / 2f, num1 - fromResMan1.GetHeight() / 2f, Paintable.CreateText(text, GlobalMembers.Fonts[1]), 18);
          }
          else if (level == GlobalMembers.Save.CurrentLevel())
            group2.AddElement(fromResMan1.GetWidth() / 2f, num1 - fromResMan1.GetHeight() / 2f, fromResMan4, 18);
          else if (level > GlobalMembers.Save.CurrentLevel())
            group2.AddElement(fromResMan1.GetWidth() / 2f, num1 - fromResMan1.GetHeight() / 2f, fromResMan3, 18);
          Sprite button = MenuItem.CreateButton("");
          MenuItem menuItem = (MenuItem) button;
          menuItem.UserData = level;
          menuItem.SetPressed(group2);
          menuItem.SetFocused(group2);
          menuItem.SetUnFocused(group1);
          menu.AddItem(button);
          button.SetPos(new Point(GlobalMembers.FromPx(a), GlobalMembers.FromPx((float) index1 * (num1 + num3))));
          a += w + num2;
        }
      }
      return levelSelect;
    }

    public static Disp CreateOptionsMenu()
    {
      Menu menu = new Menu();
      Disp optionsMenu = (Disp) menu;
      menu.Ref = optionsMenu;
      menu.SetBg(Paintable.CreateFromResMan("ramka_cala"));
      menu.SetRightNavi(menu.CreateNaviLabel(Texts.Back));
      menu.HeightForContent = (float) ((double) menu.Bg.GetHeight() * 4.0 / 5.0) - GlobalMembers.ToPx(menu.Header != null ? menu.Header.GetHeight() : 0.0f);
      menu.WidthForContent = (float) ((double) menu.Width * 3.0 / 5.0);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), GlobalMembers.ToPx(menu.RightNavi != null ? menu.RightNavi.GetPos().Y + menu.RightNavi.GetHeight() : 0.0f));
      menu.SetValign(2);
      menu.Type = 12;
      menu.AddItem(menu.CreateButton(Texts.Music + " " + (DispManager.IsMusicPlaying() ? Texts.On : Texts.Off), Texts.Music));
      menu.AddItem(menu.CreateButton(Texts.Sound + " " + ((double) GlobalMembers.Save.GetSoundVolume() > 0.0 ? Texts.On : Texts.Off), Texts.Sound));
      return optionsMenu;
    }

    public void CenterOn(Sprite mi)
    {
      float num = this.GetContentHeight() - this.GetHeightForContent();
      if ((double) GlobalMembers.MENU_SCROLL_TIME == 0.0)
      {
        this.OffsetY = GlobalMembers.ToPx(mi.GetPos().Y + mi.GetHeight() / 2f) - this.GetHeightForContent() / 2f;
        if ((double) this.OffsetY > (double) num)
          this.OffsetY = num;
        if ((double) this.OffsetY < 0.0)
          this.OffsetY = 0.0f;
        this.NewOffsetY = this.OffsetY;
      }
      else
      {
        this.SetOffset();
        this.NewOffsetY = GlobalMembers.ToPx(mi.GetPos().Y + mi.GetHeight() / 2f) - this.GetHeightForContent() / 2f;
        if ((double) this.NewOffsetY > (double) num)
          this.NewOffsetY = num;
        if ((double) this.NewOffsetY < 0.0)
          this.NewOffsetY = 0.0f;
        this.ChangeTime = this.GameTime;
      }
    }

    public void SetFixedSize(bool fixedSize)
    {
      fixedSize = this.FixedSize;
      if (!this.FixedSize)
        return;
      this.FitSize();
    }

    public bool IsFixedSize() => this.FixedSize;

    public void FitSize()
    {
      if (this.GetItemsNum() <= 0)
        return;
      float num = this.GetContentHeight() - this.GetHeightForContent();
      if ((double) num <= 0.0)
        return;
      for (int i = 0; i < this.GetItemsNum(); ++i)
      {
        MenuItem itemAsMenuItem = this.GetItemAsMenuItem(i);
        if ((double) itemAsMenuItem.GetHeight() > (double) itemAsMenuItem.GetPreferredHeight())
        {
          float height = itemAsMenuItem.GetHeight();
          itemAsMenuItem.SetHeight(itemAsMenuItem.GetPreferredHeight());
          num -= height - itemAsMenuItem.GetHeight();
        }
      }
      for (int i = 0; i < this.GetItemsNum(); ++i)
      {
        MenuItem itemAsMenuItem = this.GetItemAsMenuItem(i);
        if (itemAsMenuItem.GetIsResizeable())
        {
          float height = itemAsMenuItem.GetHeight();
          itemAsMenuItem.SetHeight(Math.Max(itemAsMenuItem.GetHeight() - num, itemAsMenuItem.GetMinimumHeight()));
          num -= height - itemAsMenuItem.GetHeight();
        }
      }
      this.SelectedPos = 0;
      List<Sprite> spriteList = new List<Sprite>();
      for (int i = 0; i < this.GetItemsNum(); ++i)
        spriteList.Add(this.GetItem(i));
      for (int i = this.GetItemsNum() - 1; i >= 0; --i)
        this.RemoveItem(i);
      this.CleanupRemovedSprites();
      for (int index = 0; index < spriteList.Count; ++index)
      {
        Point pos = spriteList[index].GetPos();
        spriteList[index].SetPos(new Point(pos.X, 0.0f));
        this.AddItem(spriteList[index]);
      }
    }

    public void SetBg(Paintable bg)
    {
      this.Bg = bg;
      if (this.Bg == null)
        return;
      this.Width = this.Bg.GetWidth();
      this.Height = this.Bg.GetHeight();
    }

    public void KeyPressed(int keyCode)
    {
      if (!this.GetItemAsMenuItem(this.SelectedPos).KeyPressed((AbsKey) keyCode))
      {
        int selectedPos = this.SelectedPos;
        switch (keyCode)
        {
          case 1:
            do
            {
              this.SelectedPos = ++this.SelectedPos % this.GetItemsNum();
            }
            while (this.SelectedPos != selectedPos && !this.GetItemAsMenuItem(this.SelectedPos).GetIsNavigable());
            if (selectedPos >= this.SelectedPos)
            {
              this.SelectedPos = selectedPos;
              if ((double) this.GetContentHeight() > (double) this.GetHeightForContent())
              {
                this.SetOffset();
                this.ChangeTime = this.GameTime;
                this.NewOffsetY = Math.Min(this.OffsetY + this.Height / 4f, Math.Max(0.0f, this.GetItem(this.GetItemsNum() - 1).GetPos().Y - GlobalMembers.FromPx(this.GetHeightForContent())));
                break;
              }
              break;
            }
            break;
          case 2:
            do
            {
              this.SelectedPos = (this.SelectedPos + this.GetItemsNum() - 1) % this.GetItemsNum();
            }
            while (this.SelectedPos != selectedPos && !this.GetItemAsMenuItem(this.SelectedPos).GetIsNavigable());
            if (selectedPos <= this.SelectedPos)
            {
              this.SetOffset();
              this.SelectedPos = selectedPos;
              this.ChangeTime = this.GameTime;
              this.NewOffsetY = Math.Max(0.0f, this.OffsetY - this.Height / 4f);
              break;
            }
            break;
          default:
            this.ProceedKeyPressed(keyCode);
            break;
        }
        if (selectedPos != this.SelectedPos)
        {
          this.GetItemAsMenuItem(selectedPos).SetIsSelected(false);
          this.GetItemAsMenuItem(this.SelectedPos).SetIsSelected(true);
          this.CenterOn(this.GetItem(selectedPos));
        }
      }
      this.SetNaviLabelsBg();
    }

    public void KeyReleased(int keyCode)
    {
      if (!this.GetItemAsMenuItem(this.SelectedPos).KeyReleased((AbsKey) keyCode))
      {
        if (keyCode == 0)
        {
          this.OnChoose(this.GetItemAsMenuItem(this.SelectedPos).GetLabel(), this.GetItem(this.SelectedPos));
          return;
        }
        this.ProceedKeyReleased(keyCode);
      }
      this.SetNaviLabelsBg();
    }

    public void SetValign(int valign) => this.Valign = valign;

    public void SetHeader(Sprite header)
    {
      this.Header = header;
      this.Header.SetPos(new Point(GlobalMembers.FromPx(this.Width - this.GetWidthForContent()) + (float) (((double) GlobalMembers.FromPx(this.GetWidthForContent()) - (double) this.Header.GetWidth()) / 2.0), this.Header.GetHeight()));
    }

    public void SetFooter(Sprite footer)
    {
      this.Footer = footer;
      this.Footer.SetPos(new Point(GlobalMembers.FromPx((float) (((double) this.Width - (double) this.Footer.GetWidth()) / 2.0)), GlobalMembers.FromPx(this.Height)));
    }

    public void SetLeftNavi(Sprite leftNavi)
    {
      this.LeftNavi = leftNavi;
      this.SetNaviLabelsBg();
      this.LeftNavi.SetPos(new Point(0.0f, 0.0f));
    }

    public void SetRightNavi(Sprite rightNavi)
    {
      this.RightNavi = rightNavi;
      this.SetNaviLabelsBg();
      if (this.IsFramed())
        this.RightNavi.SetPos(new Point(GlobalMembers.FromPx(this.Width / 2f) - this.RightNavi.GetWidth() / 2f, 0.0f));
      else
        this.RightNavi.SetPos(new Point(GlobalMembers.FromPx(this.Width) - this.RightNavi.GetWidth(), 0.0f));
    }

    public float GetHeaderHeight()
    {
      return this.Header == null ? 0.0f : GlobalMembers.ToPx(this.Header.GetHeight());
    }

    public float GetFooterHeight()
    {
      float num = 0.0f;
      if (this.Footer != null)
        num = Math.Max(this.Footer.GetHeight() + this.Footer.GetPos().Y, num);
      if (this.LeftNavi != null && (!GlobalMembers.IsFreeVersion || this.Type != 2))
        num = Math.Max(this.LeftNavi.GetHeight() + this.LeftNavi.GetPos().Y, num);
      if (this.RightNavi != null)
        num = Math.Max(this.RightNavi.GetHeight() + this.RightNavi.GetPos().Y, num);
      return GlobalMembers.ToPx(num);
    }

    public float GetContentHeight()
    {
      return this.GetItemsNum() == 0 ? 0.0f : GlobalMembers.ToPx(this.GetItem(this.GetItemsNum() - 1).GetPos().Y + this.GetItem(this.GetItemsNum() - 1).GetHeight());
    }

    public Sprite GetItem(int i) => this.Elements[i];

    public MenuItem GetItemAsMenuItem(int i) => (MenuItem) this.GetItem(i);

    public int GetItemsNum() => this.Elements.Count;

    public void RemoveItem(Sprite mi)
    {
      for (int i = 0; i < this.Elements.Count; ++i)
      {
        if (mi == this.GetItem(i))
          this.RemoveItem(i);
      }
    }

    public void RemoveItem(int i)
    {
      Sprite sprite1 = this.GetItem(i);
      sprite1.Remove();
      float num = sprite1.GetPos().Y - sprite1.GetHeight();
      this.Elements[i].Remove();
      for (int i1 = i; i1 < this.Elements.Count; ++i1)
      {
        Sprite sprite2 = this.GetItem(i1);
        sprite2.SetPos(new Point(sprite2.GetPos().X, num + sprite2.GetHeight()));
        num += sprite2.GetHeight() + GlobalMembers.FromPx(GlobalMembers.MENU_VDELAY);
      }
      this.Elements.RemoveAt(this.Elements.Count - 1);
    }

    public void ProceedWithPointerPosition(Point pos)
    {
      Sprite s = this.SpriteAt(pos);
      if (s == this.SelectedByPointer)
        return;
      if (Menu.IsMenuItem(this.SelectedByPointer))
        ((MenuItem) this.SelectedByPointer).SetIsSelected(false);
      if (Menu.IsMenuItem(s))
      {
        for (int i = 0; i < this.Elements.Count; ++i)
        {
          if (s == this.GetItem(i))
          {
            if (this.GetItemAsMenuItem(this.SelectedPos).GetIsSelected())
              this.GetItemAsMenuItem(this.SelectedPos).SetIsSelected(false);
            this.SelectedPos = i;
            this.GetItemAsMenuItem(this.SelectedPos).SetIsSelected(true);
          }
        }
        this.SelectedByPointer = s;
      }
      else
      {
        if (s != null)
          return;
        this.SelectedByPointer = new Sprite();
      }
    }

    public override void PointerPressed(Point pos, int id)
    {
      if ((double) this.GameTime < 1.0 && this.Type == 5)
        return;
      if (this.Type == 6 || this.Type == 7 || this.Type == 112)
      {
        if (this.Next == null)
          return;
        this.Next.Display();
      }
      else
      {
        pos -= this.Translate;
        this.ActualPoint = new Point(pos);
        if (this.IsPointerInElementsRect(pos))
        {
          pos.Y -= this.Ty - Math.Max(0.0f, this.GetContentHeight() - this.GetHeightForContent()) + this.OffsetY;
          this.ProceedWithPointerPosition(pos);
          Sprite s = this.SpriteAt(pos);
          this.SelectedByPointerPress = s;
          if (Menu.IsMenuItem(s))
            ((MenuItem) s).PointerPressed(pos);
        }
        this.ProceedPointerPressed(pos);
        this.SetNaviLabelsBg();
      }
    }

    public override void PointerDragged(Point pos, int id)
    {
      if ((double) this.GameTime < 1.0 && this.Type == 5)
        return;

      pos -= this.Translate;
      this.ChangeTime = this.GameTime - GlobalMembers.MENU_SCROLL_TIME;
      if (this.ActualPoint != (Point) null && (this.SelectedByPointerPress == null || this.SelectedByPointerPress.GetTypeId() != -10002))
        this.OffsetY = Math.Max(0.0f, Math.Min(this.GetContentHeight() - this.GetHeightForContent(), this.OffsetY - this.ActualPoint.Y + pos.Y));
      this.NewOffsetY = this.OffsetY;
      this.ActualPoint = new Point(pos);
      if (this.IsPointerInElementsRect(pos))
      {
        pos.Y -= this.Ty - Math.Max(0.0f, this.GetContentHeight() - this.GetHeightForContent()) + this.OffsetY;
        this.ProceedWithPointerPosition(pos);
        Sprite s = this.SpriteAt(pos);
        if (s != null && Menu.IsMenuItem(s))
        {
          MenuItem menuItem = (MenuItem) s;
          if (s != this.SelectedByPointer)
          {
            menuItem.PointerEntered(pos);
            if (Menu.IsMenuItem(this.SelectedByPointer))
              ((MenuItem) this.SelectedByPointer).PointerLeft(pos);
          }
          menuItem.PointerMoved(pos);
        }
        if (this.SelectedByPointer != null && Menu.IsMenuItem(this.SelectedByPointer) && s != this.SelectedByPointer)
          ((MenuItem) this.SelectedByPointer).PointerLeft(pos);
      }
      else if (this.SelectedByPointer != null && Menu.IsMenuItem(this.SelectedByPointer))
        ((MenuItem) this.SelectedByPointer).PointerLeft(pos);
      this.SetNaviLabelsBg();
    }

    public override void PointerReleased(Point pos, int id)
    {
      if ((double) this.GameTime < 1.0 && this.Type == 5)
        return;
      pos -= this.Translate;
      if (this.LeftNavi != null && Util.IsPointInRect(pos, new Point(GlobalMembers.ToPx(this.LeftNavi.GetPos().X), GlobalMembers.ToPx(this.LeftNavi.GetPos().Y)), new Point(GlobalMembers.ToPx(this.LeftNavi.Size.X), GlobalMembers.ToPx(this.LeftNavi.Size.Y))))
      {
        this.KeyReleased(5);
        this.LeftSoftKeyPressedByPointer = false;
        GlobalMembers.SfxButtonReleaseInstance.Play();
      }
      if (this.RightNavi != null && Util.IsPointInRect(pos, 
          new Point(GlobalMembers.ToPx(this.RightNavi.GetPos().X), GlobalMembers.ToPx(this.RightNavi.GetPos().Y)), new Point(GlobalMembers.ToPx(this.RightNavi.Size.X), GlobalMembers.ToPx(this.RightNavi.Size.Y))))
      {
        this.KeyReleased(6);
        this.RightSoftKeyPressedByPointer = false;
        GlobalMembers.SfxButtonReleaseInstance.Play();
      }
      Sprite s = this.SpriteAt(pos);
      this.ReleasePoint = new Point(pos);
      if (this.IsPointerInElementsRect(pos))
      {
        pos.Y -= this.Ty - Math.Max(0.0f, this.GetContentHeight() - this.GetHeightForContent()) + this.OffsetY;
        if (Menu.IsMenuItem(s))
          ((MenuItem) s).PointerReleased(pos);
        if (Menu.IsMenuItem(this.SelectedByPointer))
        {
          MenuItem selectedByPointer = (MenuItem) this.SelectedByPointer;
          if (!selectedByPointer.KeyReleased(AbsKey.Ok))
            this.OnChoose(selectedByPointer.GetLabel(), this.SelectedByPointer);
          selectedByPointer.SetIsSelected(false);
        }
      }
      this.SelectedByPointer = new Sprite();
      this.PressPoint = (Point) null;
      this.ActualPoint = (Point) null;
      this.SetNaviLabelsBg();
      this.ProceedPointerReleased(pos);
    }

    public virtual void ProceedPointerReleased(Point pos)
    {
    }

    public virtual void ProceedPointerPressed(Point pos)
    {
      if (this.Type == 11 && Util.IsPointInRect(pos, new Point((float) ((double) this.Width - (double) Paintable.CreateFromResMan("prawy_dolny").GetWidth() / 2.0 - (double) Paintable.CreateFromResMan("facebook").GetWidth() / 2.0), (float) ((double) this.Height / 10.0 - (double) Paintable.CreateFromResMan("facebook").GetHeight() / 2.0)), new Point(Paintable.CreateFromResMan("facebook").GetWidth(), Paintable.CreateFromResMan("facebook").GetHeight())))
        DispManager.GoToWebsite(GlobalMembers.LinkFacebook);

      if (this.Type != 11 || !Util.IsPointInRect(pos, new Point((float) ((double) this.Width - (double) Paintable.CreateFromResMan("prawy_dolny").GetWidth() / 2.0 - (double) Paintable.CreateFromResMan("funapp_icon").GetWidth() / 2.0), (float) ((double) this.Height / 10.0 + (double) Paintable.CreateFromResMan("prawy_dolny").GetHeight() + 10.0 - (double) Paintable.CreateFromResMan("funapp_icon").GetHeight() / 2.0)), new Point(Paintable.CreateFromResMan("funapp_icon").GetWidth(), Paintable.CreateFromResMan("funapp_icon").GetHeight())))
        return;
      DispManager.GoToMarketplace(GlobalMembers.LinkFunappMarketplace);
    }

    public virtual void ProceedKeyPressed(int key)
    {
      if (this.Type != 6 && this.Type != 7 && this.Type != 112 || this.Next == null)
        return;
      this.Next.Display();
    }

    public virtual void ProceedKeyReleased(int key)
    {
      if (this.Type == 9 && (double) this.GameTime >= 5.0)
        this.Next.Display();
      if (this.Type == 8)
      {
        if (key == 6)
          Menu.CreateMainMenu().Display();
        if (key == 5 && DispManager.IsNetworkAvailable())
          DispManager.GoToWebsite(GlobalMembers.LinkBuy);
      }
      if (this.Type == 10)
      {
        if (key == 5)
          this.Next.Display();
        if (key == 6 && DispManager.IsNetworkAvailable())
          DispManager.RateApp(GlobalMembers.LinkRate);
      }
      if (this.Type == 2)
      {
        if (GlobalMembers.IsFreeVersion)
        {
          if (key == 6)
            Menu.CreateMainMenu().Display();
          if (key != 5)
            ;
        }
        else if (key == 6)
          Menu.CreateMainMenu().Display();
      }
      if (this.Type == 14)
        Menu.CreateMainMenu().Display();
      if (this.Type == 12 || this.Type == 13 || this.Type == 18)
        this.Prev.Display();
      if (this.Type == 3)
        GlobalMembers.MGame.Display();
      if (this.Type == 0 && this.Next != null)
        this.Next.Display();
      if (this.Type == 16 && key == 6)
        this.Next.Display();
      if (this.Type == 18 && key == 5)
      {
        if (GlobalMembers.Save.GetTapJoyPoints() >= 1)
        {
          GlobalMembers.Save.SetTapJoyPoints(GlobalMembers.Save.GetTapJoyPoints() - 1);
          GlobalMembers.Save.SetLevelsUnlocked(GlobalMembers.Save.LevelsUnlocked + 5);
          Menu.CreateLevelSelect().Display();
        }
        else
          Menu.createFramedTextDisplayerMenu(new Paintable(), Texts.NotEnoughShells, this.Ref, 
              new Paintable(), Texts.Back).Display();
      }
      if (this.Type != 17)
        return;
      if (key == 6)
        this.Prev.Display();
      if (key != 5)
        return;
      if (GlobalMembers.Save.GetTapJoyPoints() >= 10)
      {
        GlobalMembers.Save.SetTapJoyPoints(GlobalMembers.Save.GetTapJoyPoints() - 10);
        GlobalMembers.Save.AddBought("DC_levels_21_to_30");
        Menu.CreateLevelSelect().Display();
      }
      else
        Menu.createFramedTextDisplayerMenu(new Paintable(), Texts.NotEnoughShells, 
            this.Ref, new Paintable(), Texts.Back).Display();
    }

    public bool HasLefBg() => this.LeftBg != null;

    public bool IsFramed()
    {
      return (double) this.Width != (double) GlobalMembers.Manager.Width 
                || (double) this.Height != (double) GlobalMembers.Manager.Height;
    }

    public void AddItem(Sprite mi, int index)
    {
      this.Elements.Insert(index, mi);
      this.AddSprite(mi, 0);
      Point _pos = new Point();
      MenuItem menuItem = (MenuItem) mi;
      _pos.X = GlobalMembers.FromPx(this.GetContentStartPos().X) + Util.Halign(GlobalMembers.FromPx(this.GetWidthForContent()), 16, mi.GetWidth());
      if (index > 0)
      {
        MenuItem itemAsMenuItem = this.GetItemAsMenuItem(index - 1);
        _pos.Y = itemAsMenuItem.GetPos().Y + itemAsMenuItem.GetHeight() + GlobalMembers.FromPx(GlobalMembers.MENU_VDELAY);
      }
      else
      {
        _pos.Y = 0.0f;
        if (menuItem.GetIsNavigable())
          this.SelectedPos = index;
      }
      mi.SetPos(_pos);
      float y = mi.GetPos().Y + GlobalMembers.FromPx(GlobalMembers.MENU_VDELAY) + mi.GetHeight();
      for (int i = index + 1; i < this.Elements.Count; ++i)
      {
        mi = this.GetItem(i);
        mi.SetPos(new Point(mi.GetPos().X, y));
        y += mi.GetHeight() + GlobalMembers.FromPx(GlobalMembers.MENU_VDELAY);
      }
      if (this.GetItemAsMenuItem(this.SelectedPos).GetIsNavigable() || !menuItem.GetIsNavigable())
        return;
      this.SelectedPos = index;
      this.GetItemAsMenuItem(this.SelectedPos).SetIsSelected(true);
    }

    public void AddItem(Sprite mi) => this.AddItem(mi, this.Elements.Count);

    public float GetSoftHeight() => GlobalMembers.Fonts[0].GetHeight() * 2f;

    public float GetSoftWidth()
    {
      string s = "AAAAAAAAAA";
      return GlobalMembers.Fonts[0].GetStringWidth(s);
    }

    public override void OnShow()
    {
      if (this.Prev == null && GlobalMembers.Manager.GetDisp() != this)
        this.Prev = GlobalMembers.Manager.GetDisp();
      if (this.FirstShow)
        this.FirstShow = false;
      if (this.Type == 9)
      {
        GlobalMembers.Manager.ShowAds();
        GlobalMembers.Manager.RefreshAd();
      }
      if (this.Type == 17 && this.Elements.Count == 5)
      {
        this.Level = GlobalMembers.Save.TapJoyPoints;
        string s = string.Format("{0}", (object) string.Format("#Olevel {0:D}", (object) this.Level));
        this.GetItemAsMenuItem(1).SetFormated(Paintable.CreateFormatedPaintable(s, (string[]) null, 0, this.GetWidthForContent(), 16, true));
      }
      if (this.Type != 18)
        return;
      this.Level = GlobalMembers.Save.TapJoyPoints;
      string s1 = string.Format("{0}", (object) string.Format("#Olevel {0:D}", (object) this.Level));
      this.GetItemAsMenuItem(3).SetFormated(Paintable.CreateFormatedPaintable(s1, (string[]) null, 0, this.GetWidthForContent(), 16, true));
    }

    public override void OnHide()
    {
      if (this.Type == 9)
        GlobalMembers.Manager.HideAds();
      if (GlobalMembers.Manager.Next == this.Prev)
        this.Prev = new Disp();
      if (this.Type != 6)
        return;
      GlobalMembers.ResManLoader.ReleaseResourceMemory();
    }

    public override void Load()
    {
      if (this.Type == 7)
      {
        GlobalMembers.ResManLoader.AddToLoad("logo_color_anim_glow");
        GlobalMembers.ResManLoader.AddToLoad("logo_color_anim_logo");
      }
      if (this.Type == 11)
      {
        GlobalMembers.ResManLoader.AddToLoad("facebook");
        GlobalMembers.ResManLoader.AddToLoad("prawy_dolny");
      }
      if (this.Prev != null)
        this.Prev.Load();
      for (int i = 0; i < this.GetItemsNum(); ++i)
        this.GetItemAsMenuItem(i).Load();
      if (this.Header != null)
        ((MenuItem) this.Header).Load();
      if (this.Footer != null)
        ((MenuItem) this.Footer).Load();
      if (this.LeftNavi != null)
        ((MenuItem) this.LeftNavi).Load();
      if (this.RightNavi != null)
        ((MenuItem) this.RightNavi).Load();
      if (this.Bg != null)
        this.Bg.AddToLoad();
      if (this.Bg2 != null)
        this.Bg2.AddToLoad();
      if (this.LeftBg != null)
        this.LeftBg.AddToLoad();
      GlobalMembers.Fonts[0].SetColor(1f, 1f, 1f, 1f);
      GlobalMembers.Fonts[1].SetColor(1f, 1f, 1f, 1f);
      GlobalMembers.ResManLoader.AddToLoad("btn_big_select");
      GlobalMembers.ResManLoader.LoadResources(false);
    }

    public virtual void OnChoose(string label, Sprite im)
    {
      if (label == Texts.Buy && (this.Type == 18 || this.Type == 9))
        DispManager.GoToWebsite(GlobalMembers.LinkBuy);

      if (label == Texts.UnlockForFree)
        Menu.CreateTapJoy(this.Ref).Display();
      
      int num = label == Texts.CheckACtions ? 1 : 0;
      
      if (label == Texts.Restore)
      {
        while (this.GetItemsNum() > 0)
          this.RemoveItem(0);
      }
      if (this.Type == 14)
      {
        Disp tutorial = Menu.CreateTutorial(true, (this.Level + 1) % 8);
        ((Menu) tutorial).Prev = this.Prev;
        tutorial.Display();
      }

      if (im.GetTypeId() == -10003)
        GlobalMembers.SfxButtonReleaseInstance.Play();

      if (this.Type == 2 && ((MenuItem) im).UserData <= GlobalMembers.Save.CurrentLevel())
      {
        int userData = ((MenuItem) im).UserData;
        bool flag = true;
        if (GlobalMembers.IsFreeVersion)
        {
          if (Menu.IsLevelToBoughtIcon(userData))
          {
            flag = false;
            Menu.CreateBuyMoreLevels(this.Ref).Display();
          }
        }
        else if (userData >= 20 && !GlobalMembers.Save.HasBought("DC_levels_21_to_30"))
        {
          flag = false;
          Menu.CreateBuyMenu(Texts.ProductLevels21To30Description, "DC_levels_21_to_30", this.Ref).Display();
        }

        if (!flag)
          return;

        GlobalMembers.MGame.CreateGame(((MenuItem) im).UserData).Display();
      }
      else
      {
        if (label == Texts.Menu)
          Menu.CreateMainMenu().Display();

        if (label == Texts.Replay)
        {
          this.Prev = new Disp();
          GlobalMembers.MGame.CreateGame(GlobalMembers.MGame.GetLevel()).Display();
        }

        if (label == Texts.Next)
        {
          this.Prev = new Disp();
          GlobalMembers.MGame.CreateGame(GlobalMembers.MGame.GetLevel() + 1).Display();
        }
        
        if (this.Type == 4)
        {
          if (label == Texts.Yes)
          {
            this.Prev = new Disp();
            GlobalMembers.MGame.CreateGame(GlobalMembers.MGame.GetLevel()).Display();
          }
          if (label == Texts.No)
          {
            this.Prev = new Disp();
            Menu.CreateLevelSelect().Display();
          }
        }
        
        if (this.Type == 1)
        {
          for (int index = 0; index < this.Elements.Count; ++index)
          {
            if (im == this.Elements[index])
            {
              GlobalMembers.MGame.CreateGame(index + 1).Display();
              break;
            }
          }
        }

        if (label == Texts.Music)
        {
          if (DispManager.IsMusicPlaying())
          {
            DispManager.SetMusicVolume(0.0f);
            DispManager.StopMusic();
          }
          else
          {
            DispManager.SetMusicVolume(0.5f);
            DispManager.PlayMusic(GlobalMembers.SfxMusic);
          }
          MenuItem menuItem = (MenuItem) im;
          menuItem.SetFormated(Paintable.CreateFormatedPaintable(Texts.Music + " " + (DispManager.IsMusicPlaying() ? Texts.On : Texts.Off), (string[]) null, 0, GlobalMembers.ToPx(menuItem.GetWidth()), 16, true));
        }
        else if (label == Texts.Sound)
        {
          if ((double) GlobalMembers.Save.SoundVolume > 0.0)
          {
            DispManager.SetSoundVolume(0.0f);
          }
          else
          {
            DispManager.SetSoundVolume(0.7f);
            if (GlobalMembers.MGame != null)
            {
              for (int index = 0; index < GlobalMembers.MGame.SoundSources.Count; ++index)
                GlobalMembers.MGame.SoundSources[index].sound.Volume = 0.7f;
            }
          }
          MenuItem menuItem = (MenuItem) im;
          menuItem.SetFormated(Paintable.CreateFormatedPaintable(Texts.Sound + " " + ((double) GlobalMembers.Save.SoundVolume > 0.0 ? Texts.On : Texts.Off), (string[]) null, 0, GlobalMembers.ToPx(menuItem.GetWidth()), 16, true));
        }
        else if (label == Texts.Play)
          Menu.CreateLevelSelect().Display();
        else if (label == Texts.Exit)
        {
          Menu.CreateLevelSelect().Display();
          this.Prev = new Disp();
        }
        else if (label == Texts.Options)
          Menu.CreateOptionsMenu().Display();
        else if (label == Texts.Back)
        {
          this.Prev.Display();
        }
        else
        {
          if (label == Texts.About)
          {
            Disp textDisplayerMenu = Menu.createFramedTextDisplayerMenu(new Paintable(), Texts.AboutText, this.Ref, new Paintable(), Texts.Back);
            textDisplayerMenu.Ref = textDisplayerMenu;
            textDisplayerMenu.Display();
          }
          if (label == Texts.Help)
            Menu.CreateTutorial(true, 0).Display();
          if (!(label == Texts.Yes) && !(label == Texts.No) && !(label == Texts.Menu) && !(label == Texts.Replay) && !(label == Texts.Next))
            return;
          DispManager.ResumeMusic();
        }
      }
    }

    public override void Update(float time)
    {
      if (this.Type == 17)
      {
        this.OnShow();
        if ((int) ((double) this.GameTime / 2.0) != (int) (((double) this.GameTime - (double) time) / 2.0))
          GlobalMembers.Save.RefreshTapJoyPoints();
      }
      if (this.Type == 9)
      {
        float num = 5f - this.GameTime;
        if ((double) num > 0.0)
          this.SetRightNavi(this.CreateNaviLabel(string.Format("{0}", (object) string.Format("#Wait {0:D}", (object) ((int) num + 1)))));
        else
          this.SetRightNavi(this.CreateNaviLabel("Skip"));
      }

      base.Update(time);
      
     foreach (KeyValuePair<AbsKey, GameManager.Utils.State> keyValuePair in KeyHelper.KeysState)
      {
        if (keyValuePair.Value == GameManager.Utils.State.Pressed)
          this.KeyPressed((int) keyValuePair.Key);
        if (keyValuePair.Value == GameManager.Utils.State.Released)
          this.KeyReleased((int) keyValuePair.Key);
      }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
      if (this.Prev != null && ((double) this.Width < (double) GlobalMembers.ScreenWidth || (double) this.Height < (double) GlobalMembers.ScreenHeight))
        this.Prev.Render(spriteBatch);
      this.Translate = new Point((float) (((double) GlobalMembers.Manager.GetWidth() - (double) this.Width) / 2.0), (float) (((double) GlobalMembers.Manager.GetHeight() - (double) this.Height) / 2.0));
      GlobalMembers.Manager.Translate(this.Translate.X, this.Translate.Y);
      if (this.Bg != null)
        Paintable.CreateModulate(this.Bg, new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), 1f).Paint(this.Width / 2f, this.Height / 2f, 18, spriteBatch);
      if (this.LeftBg != null)
        this.LeftBg.Paint(this.Width / 4f, this.Height / 2f, 18, spriteBatch);
      if (this.GetItemsNum() <= 0)
        return;
      this.Ty = this.GetContentStartPos().Y;
      if ((double) this.GetContentHeight() < (double) this.GetHeightForContent())
      {
        if (this.Valign == 2)
          this.Ty += (float) (((double) this.GetHeightForContent() - (double) this.GetContentHeight()) / 2.0);
        if (this.Valign == 1)
          this.Ty += this.GetHeightForContent() - this.GetContentHeight();
      }
      if (this.Header != null)
        this.Header.Render(spriteBatch);
      GlobalMembers.Manager.Translate(0.0f, this.Ty);
      float num1;
      if ((double) this.GameTime - (double) this.ChangeTime >= (double) GlobalMembers.MENU_SCROLL_TIME)
      {
        this.OffsetY = this.NewOffsetY;
        num1 = this.OffsetY;
      }
      else
        num1 = this.OffsetY + (float) (((double) this.NewOffsetY - (double) this.OffsetY) * ((double) this.GameTime - (double) this.ChangeTime)) / GlobalMembers.MENU_SCROLL_TIME;
      float y1 = Math.Max(0.0f, this.GetContentHeight() - this.GetHeightForContent()) - num1;
      GlobalMembers.Manager.PushClip(spriteBatch);
      float height = Paintable.CreateFromResMan("ramka_cala").GetHeight();
      if (this.Type == 11 || this.Type == 5)
        GlobalMembers.Manager.SetClip(0.0f, 0.0f, GlobalMembers.ScreenWidth, GlobalMembers.ScreenHeight, spriteBatch);
      else if (this.Type == 2)
        GlobalMembers.Manager.SetClip(0.0f, (float) ((double) GlobalMembers.ScreenHeight / 2.0 - (double) height / 2.0 + 45.0), GlobalMembers.ScreenWidth, height - 90f, spriteBatch);
      else
        GlobalMembers.Manager.SetClip(0.0f, (float) ((double) GlobalMembers.ScreenHeight / 2.0 - (double) height / 2.0 + 65.0), GlobalMembers.ScreenWidth, height - 140f, spriteBatch);
      GlobalMembers.Manager.Translate(0.0f, -y1);
      base.Render(spriteBatch);
      GlobalMembers.Manager.Translate(0.0f, y1);
      GlobalMembers.Manager.PopClip(spriteBatch);
      GlobalMembers.Manager.Translate(0.0f, -this.Ty);
      if (this.Footer != null)
        this.Footer.Render(spriteBatch);
      if (this.LeftNavi != null)
        this.LeftNavi.Render(spriteBatch);
      if (this.RightNavi != null)
        this.RightNavi.Render(spriteBatch);
      if (this.Type == 2 && GlobalMembers.IsFreeVersion)
      {
        this.Level = GlobalMembers.Save.TapJoyPoints;
        string s = string.Format("{0}", (object) string.Format("#Olevel {0:D}", (object) this.Level));
        float x1 = GlobalMembers.ToPx(this.LeftNavi.GetPos().X) - 2f;
        float y2 = GlobalMembers.ToPx(this.LeftNavi.GetPos().Y + this.LeftNavi.GetHeight() / 2f) - 2f;
        Paintable formatedPaintable = Paintable.CreateFormatedPaintable(Texts.Shells, (string[]) null, 0, this.Width, 8, true);
        formatedPaintable.Paint(x1, y2, 34, spriteBatch);
        float x2 = x1 - (formatedPaintable.GetWidth() + GlobalMembers.Fonts[1].GetCharWidth(' '));
        GlobalMembers.Fonts[1].Write(s, x2, y2, 34, spriteBatch);
        float x3 = x2 - (GlobalMembers.Fonts[1].GetStringWidth(s) + GlobalMembers.Fonts[1].GetCharWidth(' '));
        Paintable.CreateFormatedPaintable(Texts.YouHave, (string[]) null, 0, this.Width, 8, true).Paint(x3, y2, 34, spriteBatch);
      }
      if ((double) this.GetContentHeight() > (double) this.GetHeightForContent())
        MenuItem.PaintScroll((float) ((double) this.GetContentStartPos().X + (double) this.GetWidthForContent() + 10.0), this.GetContentStartPos().Y, this.GetHeightForContent(), this.OffsetY / (this.GetContentHeight() - this.GetHeightForContent()), Menu.ScrollUp, Menu.ScrollDown, Menu.ScrollMid, Menu.ScrollBar, spriteBatch);
      if (this.Type == 6 || this.Type == 7 || this.Type == 112)
      {
        float num2 = -1f;
        int num3 = 0;
        if ((double) this.GameTime < 1.5)
        {
          num2 = (float) (1.0 - (double) this.GameTime / 1.5);
          num3 = this.FromColor;
        }
        if ((double) this.GameTime > 4.5)
        {
          num2 = (float) (((double) this.GameTime - 1.5 - 3.0) / 1.5);
          num3 = this.ToColor;
          if ((double) num2 >= 1.0 && GlobalMembers.Manager.Next == null && this.Next != null)
          {
            this.Next.Display();
            this.Next = new Disp();
          }
          if ((double) num2 > 1.0)
            num2 = 1f;
        }
        if (this.Type == 7)
        {
          if ((double) num2 < 0.0)
          {
            float a = (float) (((double) this.GameTime - 1.5) / 1.5);
            if ((double) a >= 1.0)
              a = 2f - a;
            Paintable.CreateModulate(Paintable.CreateFromResMan("logo_color_anim_glow"), new Color(1f, 1f, 1f, a), new Color(1f, 1f, 1f, a), 1f).Paint(this.Width / 2f, this.Height / 2f, 18, spriteBatch);
          }
          Paintable.CreateFromResMan("logo_color_anim_logo").Paint(this.Width / 2f, this.Height / 2f, 18, spriteBatch);
        }
        if ((double) num2 >= 0.0)
          Paintable.CreateFilledRect(this.Width, this.Height, Util.ConvertIntToColor((uint) num3 | (uint) (int) ((double) byte.MaxValue * (double) num2))).Paint(0.0f, 0.0f, spriteBatch);
      }
      if (this.Type == 11)
      {
        Paintable fromResMan = Paintable.CreateFromResMan("prawy_dolny");
        fromResMan.Paint(this.Width, (float) ((double) this.Height / 10.0 + (double) fromResMan.GetHeight() + 10.0), 34, spriteBatch);
        Paintable.CreateFromResMan("funapp_icon").Paint(this.Width - fromResMan.GetWidth() / 2f, (float) ((double) this.Height / 10.0 + (double) fromResMan.GetHeight() + 10.0), 18, spriteBatch);
        fromResMan.Paint(this.Width, this.Height / 10f, 34, spriteBatch);
        Paintable.CreateFromResMan("facebook").Paint(this.Width - fromResMan.GetWidth() / 2f, this.Height / 10f, 18, spriteBatch);
      }
      GlobalMembers.Manager.Translate(-this.Translate.X, -this.Translate.Y);
    }

    public float GetHeightForContent() => this.HeightForContent - this.GetFooterHeight();

    public float GetWidthForContent() => this.WidthForContent;

    public Point GetContentStartPos() => new Point(this.ContentStartPos);

    public float GetTextContentWidth() => (float) ((double) this.GetWidthForContent() * 4.0 / 5.0);

    public static Disp CreateTextDisplayerMenu(
      Paintable leftBg,
      string text,
      Disp next,
      Paintable header,
      string nextText)
    {
      Disp textDisplayerMenu = (Disp) new Menu();
      Menu menu = (Menu) textDisplayerMenu;
      menu.Ref = textDisplayerMenu;
      menu.LeftBg = leftBg;
      menu.SetBg(Paintable.CreateFromResMan("ramka_cala"));
      menu.WidthForContent = (float) ((double) menu.Width * 3.0 / 5.0);
      menu.HeightForContent = menu.Height - Paintable.CreateFromResMan("btn_big_unselect").GetHeight();
      menu.ContentStartPos = new Point((float) (((double) menu.Width - (double) menu.WidthForContent) / 2.0), menu.Height - menu.HeightForContent);
      menu.SetValign(2);
      menu.Next = next;
      menu.Type = 0;
      if (header != null)
      {
        Sprite label = menu.CreateLabel("");
        ((MenuItem) label).SetFormated(header);
        menu.SetHeader(label);
      }
      if (nextText.Length > 0)
        menu.SetRightNavi(menu.CreateNaviLabel(nextText));
      menu.AddItem(menu.CreateLabel(text));
      return textDisplayerMenu;
    }

    public static Disp CreateTextDisplayerMenu(
      Paintable leftBg,
      Paintable text,
      Disp next,
      Paintable header,
      string nextText)
    {
      GlobalMembers.Manager.GetWidth();
      Disp textDisplayerMenu = (Disp) new Menu();
      Menu menu = (Menu) textDisplayerMenu;
      menu.Ref = textDisplayerMenu;
      menu.LeftBg = leftBg;
      menu.SetBg(Paintable.CreateFromResMan("ramka_cala"));
      menu.WidthForContent = (float) ((double) menu.Width * 3.0 / 5.0);
      menu.HeightForContent = menu.Height - Paintable.CreateFromResMan("btn_big_unselect").GetHeight();
      menu.ContentStartPos = new Point((float) (((double) menu.Width - (double) menu.WidthForContent) / 2.0), menu.Height - menu.HeightForContent);
      menu.SetValign(2);
      menu.Next = next;
      menu.Type = 0;
      if (header != null)
      {
        Sprite label = menu.CreateLabel("");
        ((MenuItem) label).SetFormated(header);
        menu.SetHeader(label);
      }
      if (nextText.Length > 0)
        menu.SetRightNavi(menu.CreateNaviLabel(nextText));
      Sprite label1 = menu.CreateLabel("");
      ((MenuItem) label1).SetFormated(text);
      menu.AddItem(label1);
      return textDisplayerMenu;
    }

    public static Disp createFramedTextDisplayerMenu(
      Paintable leftBg,
      string text,
      Disp next,
      Paintable header,
      string nextText)
    {
      Menu menu = new Menu();
      Disp textDisplayerMenu = (Disp) menu;
      menu.Ref = textDisplayerMenu;
      menu.SetBg(Paintable.CreateFromResMan("ramka_cala"));
      menu.ContentStartPos = new Point((float) (((double) menu.Width - (double) menu.WidthForContent) / 2.0), menu.Height - menu.HeightForContent);
      if (nextText.Length > 0)
        menu.SetRightNavi(menu.CreateNaviLabel(nextText));
      if (header != null)
      {
        Sprite label = menu.CreateLabel("");
        ((MenuItem) label).SetFormated(header);
        menu.SetHeader(label);
      }
      menu.HeightForContent = (float) ((double) menu.Bg.GetHeight() * 4.0 / 5.0) - GlobalMembers.ToPx(menu.Header != null ? menu.Header.GetHeight() : 0.0f);
      menu.WidthForContent = (float) ((double) menu.Width * 3.0 / 5.0);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), GlobalMembers.ToPx(menu.RightNavi != null ? menu.RightNavi.GetPos().Y + menu.RightNavi.GetHeight() : 0.0f));
      menu.LeftBg = leftBg;
      menu.SetValign(2);
      menu.Next = next;
      menu.Type = 0;
      Sprite label1 = menu.CreateLabel(text);
      menu.AddItem(label1);
      return textDisplayerMenu;
    }

    public void SetNaviLabelsBg()
    {
      bool flag1 = false;
      bool flag2 = false;
      Paintable fromResMan1 = Paintable.CreateFromResMan("btn_big_unselect");
      Paintable fromResMan2 = Paintable.CreateFromResMan("btn_big_select");
      if (this.LeftNavi != null)
      {
        flag1 = this.LeftNavi.getImg() != fromResMan1;
        this.LeftNavi.SetPaintable(fromResMan1);
      }
      if (this.RightNavi != null)
      {
        flag2 = this.RightNavi.getImg() != fromResMan1;
        this.RightNavi.SetPaintable(fromResMan1);
      }
      if (this.LeftNavi != null && (this.ActualPoint != (Point) null && Util.IsPointInRect(this.ActualPoint, new Point(GlobalMembers.ToPx(this.LeftNavi.GetPos().X), GlobalMembers.ToPx(this.LeftNavi.GetPos().Y)), new Point(GlobalMembers.ToPx(this.LeftNavi.GetSize().X), GlobalMembers.ToPx(this.LeftNavi.GetSize().Y))) || KeyHelper.KeysState[AbsKey.LSK] == GameManager.Utils.State.Down))
      {
        this.LeftNavi.SetPaintable(fromResMan2);
        if (!flag1)
          GlobalMembers.SfxButtonHoverInstance.Play();
      }
      else if (flag1 && this.ActualPoint != (Point) null)
        GlobalMembers.SfxButtonHoverInstance.Play();
      if (this.RightNavi != null && (this.ActualPoint != (Point) null && Util.IsPointInRect(this.ActualPoint, new Point(GlobalMembers.ToPx(this.RightNavi.GetPos().X), GlobalMembers.ToPx(this.RightNavi.GetPos().Y)), new Point(GlobalMembers.ToPx(this.RightNavi.GetSize().X), GlobalMembers.ToPx(this.RightNavi.GetSize().Y))) || KeyHelper.KeysState[AbsKey.RSK] == GameManager.Utils.State.Down))
      {
        this.RightNavi.SetPaintable(fromResMan2);
        if (flag2)
          return;
        GlobalMembers.SfxButtonHoverInstance.Play();
      }
      else
      {
        if (!flag2 || !(this.ActualPoint != (Point) null))
          return;
        GlobalMembers.SfxButtonHoverInstance.Play();
      }
    }

    public Sprite CreateButton(string formated, string label)
    {
      Sprite button = MenuItem.CreateButton(label);
      MenuItem menuItem = (MenuItem) button;
      menuItem.SetPressed(Paintable.CreateFromResMan("btn_big_select"));
      menuItem.SetFocused(Paintable.CreateFromResMan("btn_big_select"));
      menuItem.SetUnFocused(Paintable.CreateFromResMan("btn_big_unselect"));
      menuItem.SetFormated(Paintable.CreateFormatedPaintable(formated, (string[]) null, 0, GlobalMembers.ToPx(menuItem.GetWidth()), 16, true));
      return button;
    }

    public Sprite CreateMediumButton(string formated, string label)
    {
      Sprite button = MenuItem.CreateButton(label);
      MenuItem menuItem = (MenuItem) button;
      menuItem.SetPressed(Paintable.CreateFromResMan("btn_medium_select"));
      menuItem.SetFocused(Paintable.CreateFromResMan("btn_medium_select"));
      menuItem.SetUnFocused(Paintable.CreateFromResMan("btn_medium_unselect"));
      menuItem.SetFormated(Paintable.CreateFormatedPaintable(formated, (string[]) null, 0, GlobalMembers.ToPx(menuItem.GetWidth()), 16, true));
      return button;
    }

    public Sprite CreateLabel(string formated)
    {
      Sprite label = MenuItem.CreateLabel();
      MenuItem menuItem = (MenuItem) label;
      menuItem.SetImg(Paintable.CreateInvisibleRect(1f, 1f));
      menuItem.SetFormated(Paintable.CreateFormatedPaintable(formated, (string[]) null, 0, this.GetWidthForContent(), 16, true));
      return label;
    }

    public Sprite CreateNaviLabel(string text)
    {
      Sprite label = MenuItem.CreateLabel();
      MenuItem menuItem = (MenuItem) label;
      menuItem.SetImg(Paintable.CreateFromResMan("btn_big_select"));
      menuItem.SetFormated(Paintable.CreateFormatedPaintable(text, (string[]) null, 0, GlobalMembers.MGame.Width, 16, true));
      return label;
    }

    public static bool IsLevelToBoughtIcon(int level)
    {
      if (level > GlobalMembers.Save.CurrentLevel())
        return false;
      if (level >= 20 && !GlobalMembers.Save.HasBought("DC_levels_21_to_30") && !GlobalMembers.IsFreeVersion)
        return true;
      return GlobalMembers.IsFreeVersion && level >= GlobalMembers.Save.LevelsUnlocked;
    }

    public Paintable CreateBg(bool withCopter)
    {
      Paintable fromResMan = Paintable.CreateFromResMan("background");
      Paintable group = Paintable.CreateGroup(false, fromResMan.GetWidth(), fromResMan.GetHeight());
      group.AddElement(0.0f, 0.0f, fromResMan);
      if (withCopter)
      {
        group.AddElement(group.GetWidth() / 4f, group.GetHeight() - 10f, Paintable.CreateFromResMan("game_logo"), 17);
        group.AddElement((float) ((double) group.GetWidth() * 3.0 / 4.0), group.GetHeight() / 2f, Paintable.CreateFromResMan("ludzik"), 18);
      }
      return group;
    }

    public Paintable CreateBg() => this.CreateBg(true);

    private void SetOffset()
    {
      this.OffsetY += (float) (((double) this.NewOffsetY - (double) this.OffsetY) * ((double) this.GameTime - (double) this.ChangeTime)) / GlobalMembers.MENU_SCROLL_TIME;
    }

    public static Disp CreateTutorial(bool isHelp, int tutNum)
    {
      Menu menu = new Menu();
      Disp tutorial = (Disp) menu;
      menu.Ref = tutorial;
      menu.SetBg(Paintable.CreateFromResMan("ramka_cala"));
      menu.SetRightNavi(menu.CreateNaviLabel(Texts.Next));
      Paintable fromResMan = Paintable.CreateFromResMan(GlobalMembers.TutorialImgs[tutNum]);
      menu.HeightForContent = fromResMan.GetHeight() + GlobalMembers.ToPx(menu.RightNavi.GetHeight());
      menu.WidthForContent = fromResMan.GetWidth();
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), GlobalMembers.ToPx(menu.RightNavi != null ? menu.RightNavi.GetPos().Y + menu.RightNavi.GetHeight() : 0.0f));
      menu.SetValign(2);
      menu.Type = isHelp ? 14 : 13;
      Sprite label = MenuItem.CreateLabel();
      ((MenuItem) label).SetImg(fromResMan);
      menu.AddItem(label);
      menu.Level = tutNum;
      return tutorial;
    }

    public static Disp CreateWin(int level, float time)
    {
      Menu menu = new Menu();
      Disp win = (Disp) menu;
      menu.Ref = win;
      menu.Level = level;
      menu.Time = time;
      menu.SetBg(Paintable.CreateFromResMan("ramka_cala"));
      menu.Width = (float) ((double) menu.Width * 7.0 / 6.0);
      menu.HeightForContent = menu.Height;
      menu.WidthForContent = menu.Width;
      menu.ContentStartPos = new Point((float) (((double) menu.Width - (double) menu.WidthForContent) / 2.0), GlobalMembers.ToPx(menu.RightNavi != null ? menu.RightNavi.GetPos().Y + menu.RightNavi.GetHeight() : 0.0f));
      menu.SetValign(4);
      menu.Type = 5;
      Sprite mediumButton1 = menu.CreateMediumButton(Texts.Menu, Texts.Menu);
      MenuItem menuItem1 = (MenuItem) mediumButton1;
      menu.AddItem(mediumButton1);
      mediumButton1.SetPos(new Point());
      Sprite mediumButton2 = menu.CreateMediumButton(Texts.Replay, Texts.Replay);
      menuItem1 = (MenuItem) mediumButton2;
      menu.AddItem(mediumButton2);
      mediumButton2.SetPos(new Point((float) (((double) GlobalMembers.FromPx(menu.Width) - (double) mediumButton2.GetWidth()) / 2.0), 0.0f));
      Sprite mediumButton3 = menu.CreateMediumButton(Texts.Next, Texts.Next);
      menuItem1 = (MenuItem) mediumButton3;
      if (level < 29)
        menu.AddItem(mediumButton3);
      mediumButton3.SetPos(new Point(GlobalMembers.FromPx(menu.GetWidth()) - mediumButton3.GetWidth(), 0.0f));
      if (Util.ResolutionSet == 2)
      {
        Sprite label = MenuItem.CreateLabel();
        ((MenuItem) label).SetImg(Paintable.CreateInvisibleRect(1f, 40f));
        menu.AddItem(label);
      }
      Paintable[] paintableArray = new Paintable[3]
      {
        Paintable.CreateFromResMan("little_stars_31"),
        Paintable.CreateFromResMan("little_stars_32"),
        Paintable.CreateFromResMan("little_stars_33")
      };
      float num1 = paintableArray[0].GetWidth() * 0.1f;
      double height = (double) paintableArray[0].GetHeight();
      Paintable text1 = Paintable.CreateText(Texts.Time + string.Format("{0}", (object) string.Format("{0}:{1}", (object) ((int) GlobalMembers.GetTimeForStars(2, level) / 60), (object) ((int) GlobalMembers.GetTimeForStars(2, level) % 60).ToString("D2"))), GlobalMembers.Fonts[1]);
      Paintable text2 = Paintable.CreateText(Texts.Time + string.Format("{0}", (object) string.Format("{0}:{1}", (object) ((int) GlobalMembers.GetTimeForStars(3, level) / 60), (object) ((int) GlobalMembers.GetTimeForStars(3, level) % 60).ToString("D2"))), GlobalMembers.Fonts[1]);
      Sprite label1 = MenuItem.CreateLabel();
      ((MenuItem) label1).SetImg(Paintable.CreateInvisibleRect(1f, menu.Bg.GetHeight() / 10f));
      menu.AddItem(label1);
      float num2 = Math.Max(paintableArray[0].GetWidth(), Math.Max(text1.GetWidth(), text2.GetWidth()));
      Paintable group1 = Paintable.CreateGroup(false, (float) ((double) num2 * 2.0 + 2.0 * (double) num1), paintableArray[0].GetHeight() + text1.GetHeight());
      group1.AddElement(num2 / 2f, group1.GetHeight(), paintableArray[1], 17);
      group1.AddElement(num1 + (float) ((double) num2 * 3.0 / 2.0), group1.GetHeight(), paintableArray[2], 17);
      group1.AddElement(num2 / 2f, 0.0f, text1, 20);
      group1.AddElement((float) ((double) num2 * 3.0 / 2.0) + num1, 0.0f, text2, 20);
      Sprite label2 = MenuItem.CreateLabel();
      MenuItem menuItem2 = (MenuItem) label2;
      menuItem2.SetImg(Paintable.CreateInvisibleRect(1f, 1f));
      menuItem2.SetFormated(group1);
      menu.AddItem(label2);
      int starsNum = GlobalMembers.Save.GetStarsNum(level, time);
      Paintable fromResMan1 = Paintable.CreateFromResMan("star_active");
      Paintable fromResMan2 = Paintable.CreateFromResMan("star_unactive");
      float num3 = fromResMan1.GetWidth() * 0.1f;
      Paintable group2 = Paintable.CreateGroup(false, (float) ((double) fromResMan1.GetWidth() * 3.0 + 2.0 * (double) num3), fromResMan1.GetHeight());
      group2.AddElement(0.0f, 0.0f, fromResMan1);
      group2.AddElement(fromResMan1.GetWidth() + num3, 0.0f, starsNum > 1 ? fromResMan1 : fromResMan2);
      group2.AddElement((float) ((double) fromResMan1.GetWidth() * 2.0 + 2.0 * (double) num3), 0.0f, starsNum > 2 ? fromResMan1 : fromResMan2);
      Sprite label3 = MenuItem.CreateLabel();
      MenuItem menuItem3 = (MenuItem) label3;
      menuItem3.SetImg(Paintable.CreateInvisibleRect(1f, 1f));
      menuItem3.SetFormated(group2);
      menu.AddItem(label3);
      string str1 = string.Format("{0}", (object) string.Format("{0}:{1}", (object) ((int) time / 60), (object) ((int) time % 60).ToString("D2")));
      string str2 = Texts.Time + str1;
      string formated = "#1" + Texts.LevelTime + str1;
      menu.AddItem(menu.CreateLabel(formated));
      menu.AddItem(menu.CreateLabel(Texts.LevelCompleted));
      return win;
    }

    public static Disp CreateRetry()
    {
      Menu menu = new Menu();
      Disp retry = (Disp) menu;
      menu.Ref = retry;
      Paintable group = Paintable.CreateGroup(false, menu.Width, menu.Height);
      group.AddElement(0.0f, 0.0f, Paintable.CreateFilledRect(group.GetWidth(), group.GetHeight(), Util.ConvertIntToColor((uint) byte.MaxValue)));
      group.AddElement((float) (((double) menu.GetWidth() - 25.0) / 2.0), menu.GetHeight() / 2f, Paintable.CreateFromResMan("ramka_cala"), 18);
      menu.SetBg(group);
      menu.HeightForContent = (float) ((double) menu.Bg.GetHeight() * 4.0 / 5.0);
      menu.WidthForContent = (float) ((double) menu.Width * 2.0 / 5.0);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), (float) (((double) menu.Bg.GetHeight() - (double) menu.HeightForContent) / 2.0));
      menu.SetValign(2);
      menu.Type = 4;
      menu.AddItem(menu.CreateButton(Texts.No, Texts.No));
      menu.AddItem(menu.CreateButton(Texts.Yes, Texts.Yes));
      menu.AddItem(menu.CreateLabel(Texts.DoYouWantToTryAgain));
      return retry;
    }

    public static Disp CreateMainMenu()
    {
      Menu menu = new Menu();
      Disp mainMenu = (Disp) menu;
      menu.SetValign(2);
      menu.Ref = mainMenu;
      menu.SetBg(menu.CreateBg());
      menu.HeightForContent = (float) ((double) menu.Bg.GetHeight() - (double) Paintable.CreateFromResMan("btn_big_unselect").GetHeight() - 30.0);
      menu.WidthForContent = menu.Bg.GetWidth() / 2f;
      menu.ContentStartPos = new Point(0.0f, 10f);
      menu.Type = 11;
      menu.AddItem(menu.CreateButton(Texts.About, Texts.About));
      menu.AddItem(menu.CreateButton(Texts.Help, Texts.Help));
      menu.AddItem(menu.CreateButton(Texts.Options, Texts.Options));
      menu.AddItem(menu.CreateButton(Texts.Play, Texts.Play));
      return mainMenu;
    }

    public static Disp CreatePause()
    {
      Menu menu = new Menu();
      Disp pause = (Disp) menu;
      menu.Ref = pause;
      menu.SetBg(Paintable.CreateFromResMan("ramka_cala"));
      menu.SetRightNavi(menu.CreateNaviLabel(Texts.Back));
      menu.HeightForContent = (float) ((double) menu.Bg.GetHeight() * 4.0 / 5.0) - GlobalMembers.ToPx(menu.Header != null ? menu.Header.GetHeight() : 0.0f);
      menu.WidthForContent = (float) ((double) menu.Width * 3.0 / 5.0);
      menu.ContentStartPos = new Point((float) (((double) menu.Bg.GetWidth() - (double) menu.WidthForContent) / 2.0), GlobalMembers.ToPx(menu.RightNavi != null ? menu.RightNavi.GetPos().Y + menu.RightNavi.GetHeight() : 0.0f));
      menu.SetValign(2);
      menu.Type = 3;
      menu.AddItem(menu.CreateButton(Texts.Exit, Texts.Exit));
      menu.AddItem(menu.CreateButton(Texts.Options, Texts.Options));
      return pause;
    }

    public static Disp CreateFunAppSplash(Paintable img, Disp next, int fromColor, int toColor)
    {
      Disp textDisplayerMenu = Menu.CreateTextDisplayerMenu(new Paintable(), "", next, new Paintable(), "");
      Menu menu = (Menu) textDisplayerMenu;
      menu.FromColor = fromColor;
      menu.ToColor = toColor;
      menu.Type = 112;
      Paintable group = Paintable.CreateGroup(false, textDisplayerMenu.Width, textDisplayerMenu.Height);
      group.AddElement(0.0f, 0.0f, Paintable.CreateFilledRect(group.GetWidth(), group.GetHeight(), Util.ConvertIntToColor((uint) byte.MaxValue)));
      group.AddElement(group.GetWidth() / 2f, group.GetHeight() / 2f, img, 18);
      menu.SetBg(group);
      return textDisplayerMenu;
    }
  }
}
