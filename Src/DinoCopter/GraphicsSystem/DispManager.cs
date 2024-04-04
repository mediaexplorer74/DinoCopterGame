// GameManager.GraphicsSystem.DispManager

using GameManager.GameLogic;
using GameManager.Utils;
//using Microsoft.Advertising.Mobile.Xna;
//using Microsoft.Devices;
//using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class DispManager
  {
    public static bool Horizontal;
    public static bool RotateHorizontal;
    private float[] ActualClip = new float[4];
    private float[] ActualClipMatrix = new float[16];
    public float[,] ClipStack = new float[100, 4];
    public float[][] ClipMatrixStack = new float[100][];
    public Song Music;
    public Disp Next = new Disp();
    private readonly InputHelper _inputHelper;
    public static bool ExitGame;
    public static GraphicsDevice GraphicsDev;
    public static ContentManager Content;
    public static BasicEffect BasicEffect;
    public static bool AdsVisible = false;
    //private DrawableAd bannerAd;

    public static bool IsAskingToTurnOnMusic { get; set; }

    public int LastAccY { get; private set; }

    public bool NeedsRotate { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public float TimeAcc { get; private set; }

    public float ScaleX { get; private set; }

    public float ScaleY { get; private set; }

    public Point TranslatePos { get; private set; }

    public long LastUpdate { get; set; }

    public int StackPos { get; set; }

    public Dictionary<int, Point> PointersPressed { get; set; }

    public Dictionary<int, Point> PointersCurrent { get; set; }

    public Dictionary<int, Point> PointersReleased { get; set; }

    public Paintable Load { get; set; }

    public Disp Current { get; set; }

    public bool IsLoadScreen { get; set; }

    // tranformTouch
    private Point tranformTouch(Point p)
    {
      p.X = (float) (int) ((double) p.X * (double) GlobalMembers.ScreenWidth 
                / (double) Math.Max(1, DispManager.GraphicsDev.Viewport.Width));
      p.Y = (float) (int) ((double) p.Y * (double) GlobalMembers.ScreenHeight
                / (double) Math.Max(1, DispManager.GraphicsDev.Viewport.Height));
      if (this.GetManager().IsNeedsRotate())
      {
        p.X = (float) ((int) GlobalMembers.ScreenWidth - 1) - p.X;
        p.Y = (float) ((int) GlobalMembers.ScreenHeight - 1) - p.Y;
      }
      return new Point(p.X, (float) (this.GetManager().GetHeight() - 1) - p.Y);
    }//tranformTouch

    // InitAds
    public void InitAds()
    {
      Rectangle rectangle = new Rectangle((int) ((double) GlobalMembers.ScreenWidth - 480.0) / 2,
          100, 480, 80);
      DispManager.AdsVisible = false;
    }//InitAds


    // ShowAds
    public void ShowAds()
    {
      if (!GlobalMembers.HasAds)
        return;
      DispManager.AdsVisible = true;
    }//ShowAds


    // HideAds
    public void HideAds()
    {
      if (!GlobalMembers.HasAds)
        return;
      DispManager.AdsVisible = false;
    }//HideAds


        // RefreshAd
    public void RefreshAd()
    {
    }//RefreshAd


    // DestroyAds
    public void DestroyAds()
    {
    }

        public bool isNeedsHorizontalRotate()
        {
            return DispManager.RotateHorizontal;
        }

        private bool IsHorizontal()
    {
        return DispManager.Horizontal;
    }

    private bool IsNeedsRotate()
    {
        return this.NeedsRotate;
    }

    public DispManager(bool _horizontal)
    {
      this.PointersPressed = new Dictionary<int, Point>();
      this.PointersCurrent = new Dictionary<int, Point>();
      this.PointersReleased = new Dictionary<int, Point>();
      this.TranslatePos = new Point();
      for (int index = 0; index < 16; ++index)
        this.ClipMatrixStack[index] = new float[16];
      DispManager.Horizontal = _horizontal;
      DispManager.RotateHorizontal = false;
      this.LastAccY = 0;
      this.TimeAcc = 0.0f;
      this.LastUpdate = 0L;
      this.NeedsRotate = false;
      this.RegisterTouchCallbacks();

      if (DispManager.Horizontal
                && (double) GlobalMembers.ScreenWidth < (double) GlobalMembers.ScreenHeight 
                || !DispManager.Horizontal 
                && (double) GlobalMembers.ScreenHeight < (double) GlobalMembers.ScreenWidth)
      {
        float screenWidth = GlobalMembers.ScreenWidth;
        GlobalMembers.ScreenWidth = GlobalMembers.ScreenHeight;
        GlobalMembers.ScreenHeight = screenWidth;
      }
      this.Width = (int) GlobalMembers.ScreenWidth;
      this.Height = (int) GlobalMembers.ScreenHeight;
      this.ScaleX = (float) Math.Max(1, DispManager.GraphicsDev.Viewport.Width)
                / GlobalMembers.ScreenWidth;
      this.ScaleY = (float) Math.Max(1, DispManager.GraphicsDev.Viewport.Height) 
                / GlobalMembers.ScreenHeight;
      this.InitAds();
      this._inputHelper = new InputHelper();
      KeyHelper.InputHelper = this._inputHelper;
      this.CreateLoadingImage();
    }

    public int GetHeight() => this.Height;

    public int GetWidth() => this.Width;

        public Disp GetDisp()
        {
            return this.Current;
        }

        public void InitDraw(SpriteBatch spriteBatch)
    {
      this.TranslatePos.X = this.TranslatePos.Y = 0.0f;
      this.ResetClipStack(spriteBatch);
      this.PushClip(spriteBatch);
    }

    public void PaintLoad(SpriteBatch spriteBatch)
    {
      this.InitDraw(spriteBatch);
      this.Load.Paint(GlobalMembers.ScreenWidth / 2f, GlobalMembers.ScreenHeight / 2f, 18, spriteBatch);
    }

        public void SetNextDisp(Disp next)
        {
            this.Next = next;
        }

        public void Render(SpriteBatch spriteBatch)
    {
      if (this.Current == null)
      {
        this.InitDraw(spriteBatch);
      }
      else
      {
        this.InitDraw(spriteBatch);
        this.Current.Render(spriteBatch);
      }
    }

    public void Update(TimeSpan totalGameTime)
    {
      long totalMilliseconds = (long) totalGameTime.TotalMilliseconds;
      this.TimeAcc += (float) (totalMilliseconds - this.LastUpdate) / 1000f;
      if ((double) this.TimeAcc > 0.10000000149011612)
        this.TimeAcc = 0.1f;
      this.LastUpdate = totalMilliseconds;
      KeyHelper.UpdateKeys(this.TimeAcc);
      this.UpdateTouchInput();
      if (this.Next != null)
      {
        if (this.Current != null)
          this.Current.OnHide();
        this.Next.OnShow();
        this.Current = this.Next;
        this.Next.Load();
        this.Next = (Disp) null;
        this.TimeAcc = 0.0f;
      }
      if (this.Current == null)
        return;
      if (Util.ResolutionSet == -1)
        GlobalMembers.CurrentPressEvent = 0;
      for (int index = 0; index < GlobalMembers.CurrentPressEvent; ++index)
      {
        switch (GlobalMembers.PressEvents[index, 0])
        {
          case 0:
            this.PointerPress(GlobalMembers.PressEvents[index, 1], 
                (float) GlobalMembers.PressEvents[index, 2], (float) GlobalMembers.PressEvents[index, 3]);
            break;
          case 1:
            this.PointerMove(GlobalMembers.PressEvents[index, 1], 
                (float) GlobalMembers.PressEvents[index, 2], (float) GlobalMembers.PressEvents[index, 3]);
            break;
          case 2:
            this.PointerRelease(GlobalMembers.PressEvents[index, 1],
                (float) GlobalMembers.PressEvents[index, 2], (float) GlobalMembers.PressEvents[index, 3]);
            break;
        }
      }
      GlobalMembers.CurrentPressEvent = 0;
      for (; (double) this.TimeAcc > 0.0099999997764825821; this.TimeAcc -= 0.01f)
        this.Current.Update(0.01f);
    }

    public void PushClip(SpriteBatch spriteBatch)
    {
      this.ClipStack[this.StackPos, 0] = this.ActualClip[0];
      this.ClipStack[this.StackPos, 1] = this.ActualClip[1];
      this.ClipStack[this.StackPos, 2] = this.ActualClip[2];
      this.ClipStack[this.StackPos, 3] = this.ActualClip[3];
      for (int index = 0; index < 16; ++index)
        this.ClipMatrixStack[this.StackPos][index] = this.ActualClipMatrix[index];
      ++this.StackPos;
    }

    public void SetClip(float x, float y, float w, float h, SpriteBatch spriteBatch)
    {
      this.clipRect(x, y, w, h, spriteBatch);
    }

    public void PopClip(SpriteBatch spriteBatch)
    {
      if (this.StackPos <= 0)
      {
        this.StackPos = 0;
        this.clipRect(0.0f, 0.0f, GlobalMembers.ScreenWidth, GlobalMembers.ScreenHeight, spriteBatch);
      }
      else
      {
        --this.StackPos;
        this.clipRect(this.ClipStack[this.StackPos, 0], this.ClipStack[this.StackPos, 1],
            this.ClipStack[this.StackPos, 2], this.ClipStack[this.StackPos, 3], 
            this.ClipMatrixStack[this.StackPos], spriteBatch);
      }
    }

    public void PeekClip(SpriteBatch spriteBatch)
    {
      this.clipRect(this.ClipStack[this.StackPos - 1, 0], this.ClipStack[this.StackPos - 1, 1],
          this.ClipStack[this.StackPos - 1, 2], this.ClipStack[this.StackPos - 1, 3], 
          this.ClipMatrixStack[this.StackPos - 1], spriteBatch);
    }

    public void ResetClipStack(SpriteBatch spriteBatch)
    {
      this.clipRect(0.0f, 0.0f, GlobalMembers.ScreenWidth, GlobalMembers.ScreenHeight, spriteBatch);
      this.StackPos = 0;
    }

    public void Vibrate()
    {
        // VibrateController.Default.Start(TimeSpan.FromMilliseconds(1000.0));
    }

    public void Translate(float x, float y)
    {
      this.TranslatePos.X += x;
      this.TranslatePos.Y += y;
    }

    public void Translate(Point p)
    {
        this.TranslatePos += p;
    }

    public Point GetTranslate()
    {
        return new Point(this.TranslatePos);
    }

    public void Dispose()
    {
      if (this.Current != null)
      {
        this.Current.OnHide();
        this.Current = (Disp) null;
      }
      this.DestroyAds();
    }

    public void PointerPress(int id, float x, float y)
    {
      this.PointersPressed[id] = new Point(x, y);
      this.PointersCurrent[id] = new Point(x, y);
      if (this.Current == null)
        return;
      this.Current.PointerPressed(new Point(x, y), id);
    }

    public void PointerMove(int id, float x, float y)
    {
      this.PointersCurrent[id] = new Point(x, y);
      if (this.Current == null)
        return;
      this.Current.PointerDragged(new Point(x, y), id);
    }

    public void PointerRelease(int id, float x, float y)
    {
      this.PointersReleased[id] = new Point(x, y);
      this.PointersCurrent.Remove(id);
      this.PointersPressed.Remove(id);

      if (this.Current == null)
        return;

      this.Current.PointerReleased(new Point(x, y), id);
    }

    public Dictionary<int, Point> GetPointersPressed()
    {
        return this.PointersPressed;
    }

    public Dictionary<int, Point> GetPointersCurrent()
    {
        return this.PointersCurrent;
    }

    public Dictionary<int, Point> GetPointersReleased()
    {
        return this.PointersReleased;
    }

    public static void PlayMusic(Song music)
    {
      if (GlobalMembers.LeaveZuneOn)
        return;

      MediaPlayer.IsRepeating = true;
      MediaPlayer.Volume = GlobalMembers.Save.GetMusicVolume();
      MediaPlayer.Play(music);
    }

    public static void StopMusic()
    {
      if (GlobalMembers.LeaveZuneOn)
        return;
      MediaPlayer.Stop();
    }

    public static void ResumeMusic()
    {
        MediaPlayer.Resume();
    }

    public static bool IsMusicPlaying()
    {
      return !MediaPlayer.IsMuted && (double) GlobalMembers.Save.MusicVolume > 0.0;
    }

    public static void SetMusicVolume(float volume)
    {
      float vol = MathHelper.Clamp(volume, 0.0f, 1f);
      GlobalMembers.Save.SetMusicVolume(vol);
      MediaPlayer.Volume = volume;
    }

    public static void SetSoundVolume(float volume)
    {
      float vol = MathHelper.Clamp(volume, 0.0f, 1f);
      GlobalMembers.Save.SetSoundVolume(vol);
      SoundEffect.MasterVolume = vol;
    }

    public DispManager GetManager()
    {
        return GlobalMembers.Manager;
    }

    public void RegisterTouchCallbacks()
    {
        //
    }

    public void UnregisterTouchCallbacks()
    {
       //
    }

    private void clipRect(float x, float y, float w, float h, SpriteBatch spriteBatch)
    {
      this.clipRect(x, y, w, h, (float[]) null, spriteBatch);
    }

    private void clipRect(
      float x,
      float y,
      float w,
      float h,
      float[] matrix,
      SpriteBatch spriteBatch)
    {
      this.ActualClip[0] = x;
      this.ActualClip[1] = y;
      this.ActualClip[2] = w;
      this.ActualClip[3] = h;

      Rectangle rectangle = new Rectangle
        (
            (int) x, 
            (int) ((double) GlobalMembers.ScreenHeight - (double) y - (double) h),
            (int) w, 
            (int) h
        );

      spriteBatch.End();
      spriteBatch.GraphicsDevice.RasterizerState = new RasterizerState()
      {
        ScissorTestEnable = true
      };
      spriteBatch.GraphicsDevice.ScissorRectangle = rectangle;
      spriteBatch.Begin(SpriteSortMode.Immediate, (BlendState) null, (SamplerState) null,
          (DepthStencilState) null, spriteBatch.GraphicsDevice.RasterizerState);
    }

    private void CreateLoadingImage()
    {
      this.Load = new Paintable(PType.Unloaded)
      {
        PImage = new PImage(),
        ResourceId = "game_logo"
      };
      this.Load.PImage.KeepPixelData = false;
      string.Format("{0}\\{1}", (object) Util.FilePrefix, (object) this.Load.ResourceId);
      Texture2D texture = DispManager.Content.Load<Texture2D>("480x800\\game_logo");
      this.Load.InitImage(texture.Width, texture.Height, texture, false);
      this.Load.PType = PType.Image;
    }

    public void UpdateTouchInput()
    {
      foreach (TouchLocation touchLocation in TouchPanel.GetState())
      {
        if (GlobalMembers.CurrentPressEvent + 1 < 100)
        {
          switch (touchLocation.State)
          {
            case TouchLocationState.Released:
              ++GlobalMembers.CurrentPressEvent;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 0] = 2;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 1] = touchLocation.Id;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 2] = 
                                (int) touchLocation.Position.X;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 3] =
                                (int) ((double) GlobalMembers.ScreenHeight - 1.0
                                  - (double) touchLocation.Position.Y);
              continue;
            case TouchLocationState.Pressed:
              ++GlobalMembers.CurrentPressEvent;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 0] = 0;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 1] = touchLocation.Id;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 2] =
                                (int) touchLocation.Position.X;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 3] = 
                                (int) ((double) GlobalMembers.ScreenHeight - 1.0 
                                  - (double) touchLocation.Position.Y);
              continue;
            case TouchLocationState.Moved:
              ++GlobalMembers.CurrentPressEvent;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 0] = 1;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 1] = touchLocation.Id;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 2] =
                                (int) touchLocation.Position.X;
              GlobalMembers.PressEvents[GlobalMembers.CurrentPressEvent - 1, 3] =
                                (int) ((double) GlobalMembers.ScreenHeight - 1.0 
                                  - (double) touchLocation.Position.Y);
              continue;
            default:
              continue;
          }
        }
      }
    }

    public void PlaySound(SoundEffect sound)
    {
        sound.Play();
    }

    public static bool IsNetworkAvailable()
    {
        return NetworkInterface.GetIsNetworkAvailable();
    }

    public static void GoToWebsite(string url)
    {
      //new WebBrowserTask() { URL = url }.Show();
    }

        public static void RateApp(string url)
        {
            //new MarketplaceReviewTask().Show();
        }


    // GoToMarketplace
    public static void GoToMarketplace(string searchText)
    {
        //new MarketplaceSearchTask()
        //{
        //  SearchTerms = searchText
        //}.Show();
    }//GoToMarketplace


    // BuyFullVersion
    public static void BuyFullVersion()
    {
        //
    }//BuyFullVersion


    // PromptPurchase
    private static void PromptPurchase(IAsyncResult ar)
    {
      //int? nullable1 = Guide.EndShowMessageBox(ar);
      //if (!nullable1.HasValue)
      //  return;
      //int? nullable2 = nullable1;
      //if ((nullable2.GetValueOrDefault() != 0 ? 0 : (nullable2.HasValue ? 1 : 0)) == 0)
      //  return;
      //Guide.ShowMarketplace(PlayerIndex.One);
    }

    public static void AskForPermissionToTurnOffZune()
    {
      //DispManager.IsAskingToTurnOnMusic = true;
      //Guide.BeginShowMessageBox("Do you want to leave Zune music on?",
      //"Click OK to leave music on.",
      //(IEnumerable<string>) new List<string>()
      //{
      //  "Yes",
      //  "No"
      //}, 0, MessageBoxIcon.None, new AsyncCallback(DispManager.PromptMusic), (object) null);
    }

    private static void PromptMusic(IAsyncResult ar)
    {
      //DispManager.IsAskingToTurnOnMusic = false;
      //int? nullable1 = Guide.EndShowMessageBox(ar);
      //if (nullable1.HasValue)
      //{
      //  int? nullable2 = nullable1;
      //  if ((nullable2.GetValueOrDefault() != 0 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
      //  {
      //    GlobalMembers.LeaveZuneOn = true;
      //    DispManager.SetMusicVolume(0.0f);
      //    return;
      //  }
      //}
      //GlobalMembers.LeaveZuneOn = false;
      DispManager.StopMusic();
      DispManager.TurnOnStartupMusic();
    }

    public static void TurnOnStartupMusic()
    {
      DispManager.PlayMusic(GlobalMembers.SfxMusic);
      DispManager.SetSoundVolume(GlobalMembers.Save.GetSoundVolume());
    }

    public static void AskToExitTheGame()
    {
      //Guide.BeginShowMessageBox("Do you want to exit the game?", "Click OK to exit.", (IEnumerable<string>) new List<string>()
      //{
      //  "Yes",
      //  "No"
      //}, 0, MessageBoxIcon.None, new AsyncCallback(DispManager.PromptExit), (object) null);
    }

    private static void PromptExit(IAsyncResult ar)
    {
      //int? nullable1 = Guide.EndShowMessageBox(ar);
      //if (nullable1.HasValue)
      //{
      //  int? nullable2 = nullable1;
      //  if ((nullable2.GetValueOrDefault() != 0 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
      //  {
          DispManager.ExitGame = true;
          return;
      //  }
      //}
      //DispManager.ExitGame = false;
    }

    public static void GotToMainMenu()
    {
      GlobalMembers.Manager.Current = Menu.CreateMainMenu();
      GlobalMembers.Manager.Current.Display();
    }
  }
}
