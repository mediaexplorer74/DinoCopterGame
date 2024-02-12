// GameManager.Game1

using GameManager.GameLogic;
using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace GameManager
{
  public class Game1 : Disp
  {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Vector2 baseScreenSize = new Vector2(800, 600);

        private Matrix globalTransformation;
        int backbufferWidth, backbufferHeight;

        public static GameSerialized GameState;

    public static bool IsSerialized;
    public Paintable[] StationsCloudSigns = new Paintable[6];
    public Paintable[] StationsIndicatorSigns = new Paintable[6];
    public Paintable[] Food = new Paintable[5];
    public Paintable[] Water = new Paintable[2];
    public Spawn[] Spawns = new Spawn[10];
    public List<Paintable>[] PassengerWalk = new List<Paintable>[3];
    public Paintable[] PassengerWalkAnim = new Paintable[3];
    public List<Paintable>[] PassengerStand = new List<Paintable>[3];
    public Paintable[] PassengerStandAnim = new Paintable[3];
    public List<Paintable>[] PassengerSpeak = new List<Paintable>[3];
    public Paintable[] PassengerSpeakAnim = new Paintable[3];
    public List<Paintable>[] PassengerSwim = new List<Paintable>[3];
    public Paintable[] PassengerSwimAnim = new Paintable[3];
    public List<Paintable>[] PassengerSink = new List<Paintable>[3];
    public Paintable[] PassengerSinkAnim = new Paintable[3];
    public int LostReason;
    public List<SoundSource> SoundSources = new List<SoundSource>();
    private Paintable WaterPaintable;
    private bool CreatedWarterRectanble;

    public Sprite Player { get; private set; }

    public GameManager.GraphicsSystem.Point Gravity { get; private set; }

    public bool FirstLoad { get; private set; }

    public bool HasLose { get; private set; }

    public bool HasWon { get; private set; }

    public int PassengersDelivered { get; private set; }

    public int PassengersToDeliver { get; private set; }

    public Paintable TilesImg { get; private set; }

    public Paintable BgImg { get; private set; }

    public List<Paintable> Tiles { get; private set; }

    public List<Paintable> Copter { get; private set; }

    public List<Paintable> Whirl { get; private set; }

    public List<Paintable> TriceratopsRun { get; private set; }

    public List<Paintable> TriceratopsInjured { get; private set; }

    public List<Paintable> TriceratopsEat { get; private set; }

    public List<Paintable> TriceratopsLook { get; private set; }

    public Paintable TriceratopsEatAnim { get; private set; }

    public Paintable TriceratopsLookAnim { get; private set; }

    public Paintable TriceratopsRunAnim { get; private set; }

    public Paintable TriceratopsInjuredAnim { get; private set; }

    public List<Paintable> TreeStand { get; private set; }

    public Paintable TreeStandAnim { get; private set; }

    public Paintable TreeHitAnim { get; private set; }

    public List<Paintable> Pterodactyl { get; private set; }

    public Paintable PterodactylAnim { get; private set; }

    public List<Paintable> SleeperIn { get; private set; }

    public Paintable SleeperInAnim { get; private set; }

    public List<Paintable> SleeperOut { get; private set; }

    public Paintable SleeperOutAnim { get; private set; }

    public Paintable StoneAnim { get; private set; }

    public Paintable DustAnim { get; private set; }

    public Paintable Cloud { get; private set; }

    public Paintable ArrowImg { get; private set; }

    public Paintable Pause { get; private set; }

    public Paintable HudUp { get; private set; }

    public Paintable EnergyFill { get; private set; }

    public Paintable StoneEnabled { get; private set; }

    public Paintable StoneDisabled { get; private set; }

    public Paintable TargetCounter { get; private set; }

    public GameManager.GraphicsSystem.Point MapSize { get; private set; }

    public float Scale { get; private set; }

    public float Energy { get; private set; }

    public short[][] Bg { get; private set; }

    public short[][] Fg { get; private set; }

    public float WaterLevel { get; private set; }

    public float WaterLevelRise { get; private set; }

    public float LoseTime { get; private set; }

    public GameManager.GraphicsSystem.Point MoveDir { get; private set; }

    public int Stones { get; private set; }

    public int Level { get; private set; }

    public bool DropStone { get; private set; }

    public Paintable IndicatorWaiting { get; private set; }

    public Paintable IndicatorTarget { get; private set; }

    public Paintable IndicatorWater { get; private set; }

    public Game1()
      : base(8)
    {
      this.TriceratopsRun = new List<Paintable>();
      this.TriceratopsInjured = new List<Paintable>();
      this.TriceratopsEat = new List<Paintable>();
      this.TriceratopsLook = new List<Paintable>();
      this.TreeStand = new List<Paintable>();
      this.Pterodactyl = new List<Paintable>();
      this.SleeperIn = new List<Paintable>();
      this.SleeperOut = new List<Paintable>();
      for (int index = 0; index < 3; ++index)
      {
        this.PassengerWalk[index] = new List<Paintable>();
        this.PassengerStand[index] = new List<Paintable>();
        this.PassengerSpeak[index] = new List<Paintable>();
        this.PassengerSwim[index] = new List<Paintable>();
        this.PassengerSink[index] = new List<Paintable>();
      }
      for (int index = 0; index < 10; ++index)
        this.Spawns[index] = new Spawn();
      this.MapSize = new GameManager.GraphicsSystem.Point();
      this.Stones = 0;
      this.DropStone = false;
      this.WaterLevelRise = 0.0f;
      this.FirstLoad = true;
      this.Bg = (short[][]) null;
      this.Fg = (short[][]) null;
      this.Energy = 1f;
      this.WaterLevel = 2.3f;
      this.HasLose = false;
      this.HasWon = false;
      this.PassengersDelivered = 0;
      this.PassengersToDeliver = 0;
    }

    public void ApplyShape(Sprite s, GlobalMembers.Shape sh)
    {
      if (sh != GlobalMembers.Shape.ShapePlatform)
        return;

      GameManager.GraphicsSystem.Point pos = s.GetPos();

      s.SetPos(new GameManager.GraphicsSystem.Point(pos.X, pos.Y + s.GetHeight() * 3f));
    }

    public Paintable CreateView(int blockId)
    {
      float[] coords = new float[8];
      float num1 = GlobalMembers.Game.TilesImg.GetWidth() / 16f;
      float num2 = num1 * (float) (blockId % 16);
      float num3 = num1 * (float) (blockId / 16);
      switch (Game1.GetShape(blockId))
      {
        case GlobalMembers.Shape.ShapePlatform:
          coords[0] = num2;
          coords[1] = num3 + (float) ((double) num1 * 3.0 / 4.0);
          coords[2] = num2 + num1;
          coords[3] = num3 + (float) ((double) num1 * 3.0 / 4.0);
          coords[4] = num2;
          coords[5] = num3 + num1;
          coords[6] = num2 + num1;
          coords[7] = num3 + num1;
          return Paintable.CreateImagePart(GlobalMembers.Game.TilesImg, coords, 4);
        case GlobalMembers.Shape.ShapeVhalfrect:
          coords[0] = num2;
          coords[1] = num3;
          coords[2] = num2 + num1;
          coords[3] = num3;
          coords[4] = num2;
          coords[5] = num3 + num1 / 2f;
          coords[6] = num2 + num1;
          coords[7] = num3 + num1 / 2f;
          return Paintable.CreateImagePart(GlobalMembers.Game.TilesImg, coords, 4);
        default:
          return this.Tiles[blockId];
      }
    }

    public void PaintIndicators(SpriteBatch spriteBatch)
    {
      int x = (int) this.ViewPos.X;
      int y = (int) this.ViewPos.Y;
      int num1 = (int) ((double) this.ViewPos.X + (double) this.ViewSize.X);
      int num2 = (int) ((double) this.ViewPos.Y + (double) this.ViewSize.Y);
      for (int index = 0; index < 10; ++index)
      {
        Sprite passenger1 = this.Spawns[index].GetPassenger();
        if (passenger1 != null)
        {
          Passenger passenger2 = passenger1 as Passenger;
          if ((this.Player as GameManager.Player).GetPassengersIn() == 0 
                        && ((double) passenger2.GetPos().Y
                        + (double) passenger2.GetHeight() / 2.0 > (double) num2
                        || (double) passenger2.GetPos().Y + (double) passenger2.GetHeight() / 2.0 < (double) y 
                        || (double) passenger2.GetPos().X + (double) passenger2.GetWidth() / 2.0 > (double) num1
                        || (double) passenger2.GetPos().X + (double) passenger2.GetWidth() / 2.0 < (double) x)
                        && (passenger2.GetState() == GlobalMembers.PassengerState.PassengerStateFloat 
                        || passenger2.GetState() == GlobalMembers.PassengerState.PassengerStateWait))
            this.PaintIndicator
            (
                passenger2.GetPos() + passenger2.GetSize() / 2f 
                - this.ViewPos - this.ViewSize / 2f, 
                passenger2.GetState() == GlobalMembers.PassengerState.PassengerStateFloat
                ? Paintable.CreateInvisibleRect(1f, 1f) 
                : this.StationsCloudSigns[passenger2.GetFromStation()], 
                passenger2.GetState() == GlobalMembers.PassengerState.PassengerStateFloat
                ? this.IndicatorWater
                : this.IndicatorWaiting, spriteBatch 
            );
        }
      }
    }

    public void PaintIndicator(GameManager.GraphicsSystem.Point vec, Paintable p,
        Paintable ind, SpriteBatch spriteBatch)
    {
      if ((double) Math.Abs(vec.X) > (double) this.ViewSize.X / 2.0)
        vec *= this.ViewSize.X * 0.5f / Math.Abs(vec.X);
      if ((double) Math.Abs(vec.Y) > (double) this.ViewSize.Y / 2.0)
        vec *= this.ViewSize.Y * 0.5f / Math.Abs(vec.Y);
      float num = vec.Angle();
      float x = ind.GetWidth() - ind.GetHeight() / 2f;
      if ((double) Math.Abs(vec.X - this.ViewSize.X / 2f) < 1.0 / 1000.0)
      {
        float px = (float) (int) GlobalMembers.ToPx(this.ViewSize.Y / 2f + vec.Y);
        Paintable.CreateRotated(ind, 
            new GameManager.GraphicsSystem.Point(ind.GetWidth() / 2f, 
            ind.GetHeight() / 2f), 0.0f).Paint(GlobalMembers.ToPx(this.ViewSize.X), px - 25f, 34, spriteBatch);

        p.Paint(GlobalMembers.ToPx(this.ViewSize.X) - x, px, 18, spriteBatch);
      }
      else if ((double) Math.Abs(vec.X + this.ViewSize.X / 2f) < 1.0 / 1000.0)
      {
        float px = (float) (int) GlobalMembers.ToPx(this.ViewSize.Y / 2f + vec.Y);
        Paintable.CreateRotated(ind,
            new GameManager.GraphicsSystem.Point(ind.GetWidth() / 2f, ind.GetHeight() / 2f),
            3.14159274f).Paint(0.0f, px - 25f, 10, spriteBatch);
        p.Paint(x, px, 18, spriteBatch);
      }
      else if (Math.Abs((double) num - Math.PI / 2.0) < 0.78539818525314331)
      {
        Paintable.CreateRotated(ind, 
            new GameManager.GraphicsSystem.Point(ind.GetWidth() / 2f, ind.GetHeight() / 2f),
            -1.57079637f).Paint(GlobalMembers.ToPx(this.ViewSize.X / 2f + vec.X),
            GlobalMembers.ToPx(this.ViewSize.Y) - 27f, 17, spriteBatch);

        p.Paint(GlobalMembers.ToPx(this.ViewSize.X / 2f + vec.X), 
            (float) ((double) GlobalMembers.ToPx(this.ViewSize.Y) - (double) x + (double) ind.GetHeight() / 4.0), 
            18, spriteBatch);
      }
      else
      {
        if (Math.Abs((double) num - 3.0 * Math.PI / 2.0) >= 0.78539818525314331)
          return;
        Paintable.CreateRotated(ind, 
            new GameManager.GraphicsSystem.Point(ind.GetWidth() / 2f, ind.GetHeight() / 2f),
            1.57079637f).Paint(GlobalMembers.ToPx(this.ViewSize.X / 2f + vec.X), -23f, 20, spriteBatch);
        p.Paint(GlobalMembers.ToPx(this.ViewSize.X / 2f + vec.X), x - ind.GetHeight() / 4f, 18, spriteBatch);
      }
    }

    public void IncreaseStones() => ++this.Stones;

    public void DecreaseStones() => --this.Stones;

    public bool ShouldDropStone() => this.DropStone;

    public GameManager.GraphicsSystem.Point GetMoveDir() => new GameManager.GraphicsSystem.Point(this.MoveDir);

    public Spawn[] GetSpawns() => this.Spawns;

    public int GetLevel() => this.Level;

    public Sprite GetPlayer() => this.Player;

    public Disp CreateGame(int _levelNum)
    {
      Game1 game = new Game1();
      game.Level = _levelNum;
      GlobalMembers.Game = game;
      Disp next = (Disp) game;
      next.Ref = next;
      return GlobalMembers.HasAds && _levelNum > 0 ? Menu.CreateAdsMenu(next) : next;
    }

    public static GlobalMembers.Shape GetShape(int id)
    {
      return id >= 0 ? GlobalMembers.Shapes[id] : GlobalMembers.Shape.ShapeNone;
    }

    public float GetWaterLevel() => this.WaterLevel;

    public override void Render(SpriteBatch spriteBatch)
    {
      float num1 = 1f;
      float num2 = 1f;
      GameManager.GraphicsSystem.Point p = new GameManager.GraphicsSystem.Point(this.ViewSize);

      this.ViewSize = new GameManager.GraphicsSystem.Point(this.ViewSize.X * num1, this.ViewSize.Y * num2);
      this.ViewPos = this.Player.GetPos() + this.Player.GetSize() / 2f - this.ViewSize * 0.5f;
      if ((double) this.ViewPos.X < 0.0)
        this.ViewPos.X = 0.0f;
      if ((double) this.ViewPos.Y < 0.0)
        this.ViewPos.Y = 0.0f;
      if ((double) this.ViewPos.X > (double) this.MapSize.X - (double) this.ViewSize.X)
        this.ViewPos.X = this.MapSize.X - this.ViewSize.X;
      if ((double) this.ViewPos.Y > (double) this.MapSize.Y - (double) this.ViewSize.Y)
        this.ViewPos.Y = this.MapSize.Y - this.ViewSize.Y;
      this.BgImg.Paint(-this.ViewPos.X * Math.Min(GlobalMembers.ToPx(0.3f), (float) (((double) this.BgImg.GetWidth() - (double) GlobalMembers.ScreenWidth) / ((double) this.MapSize.X - (double) this.ViewSize.X))), -this.ViewPos.Y * Math.Min(GlobalMembers.ToPx(0.3f), (float) (((double) this.BgImg.GetHeight() - (double) GlobalMembers.ScreenHeight) / ((double) this.MapSize.Y - (double) this.ViewSize.Y))), spriteBatch);
      GlobalMembers.Manager.Translate((float) (int) -(double) GlobalMembers.ToPx(this.ViewPos.X), (float) (int) -(double) GlobalMembers.ToPx(this.ViewPos.Y));
      base.Render(spriteBatch);
      int y1 = (int) ((double) this.WaterLevel * (double) GlobalMembers.TileSize.Y);
      float height = (float) ((double) y1 - (double) this.Water[0].GetHeight() + 1.0);
      if ((double) height > 0.0 && this.CreatedWarterRectanble)
      {
        this.WaterPaintable.SetHeight(height);
        this.WaterPaintable.Paint(0.0f, 0.0f, spriteBatch);
      }
      if (!this.CreatedWarterRectanble)
      {
        this.WaterPaintable = Paintable.CreateFilledRect(this.MapSize.X * GlobalMembers.TileSize.X, GlobalMembers.ScreenHeight, new Microsoft.Xna.Framework.Color(0, 52, 90, 128));
        this.CreatedWarterRectanble = true;
      }
      int num3 = (int) ((double) GlobalMembers.ToPx(this.ViewPos.X) / (double) this.Water[0].GetWidth());
      int num4 = (int) ((double) GlobalMembers.ToPx(this.ViewPos.X + this.ViewSize.X) / (double) this.Water[0].GetWidth());
      for (int index = num3; index <= num4; ++index)
        this.Water[(index + (int) ((double) this.GameTime / 0.20000000298023224)) % 2].Paint((float) index * this.Water[0].GetWidth(), (float) y1, 9, spriteBatch);
      GlobalMembers.Manager.Translate((float) (int) GlobalMembers.ToPx(this.ViewPos.X), (float) (int) GlobalMembers.ToPx(this.ViewPos.Y));
      this.ViewSize = new GameManager.GraphicsSystem.Point(p);
      Dictionary<int, GameManager.GraphicsSystem.Point> dictionary = new Dictionary<int, GameManager.GraphicsSystem.Point>();
      bool flag = true;
      if (flag)
      {
        float width = Paintable.CreateFromResMan("arrow").GetWidth();
        this.MoveDir = new GameManager.GraphicsSystem.Point();
        dictionary = GlobalMembers.Manager.GetPointersCurrent();
        if (this == GlobalMembers.Manager.GetDisp())
        {
          foreach (int key in dictionary.Keys)
          {
            GameManager.GraphicsSystem.Point point = dictionary[key];
            if ((double) point.Y < (double) width)
            {
              if ((double) point.X < (double) width)
                this.MoveDir.Y = -1f;
              if ((double) point.X > (double) this.Width - (double) width)
                this.MoveDir.X = 1f;
              else if ((double) point.X > (double) this.Width - (double) width * 2.0)
                this.MoveDir.X = -1f;
            }
            if ((double) point.Y > (double) width && (double) point.Y < 2.0 * (double) width && (double) point.X < (double) width)
              this.MoveDir.Y = 1f;
          }
        }
        Paintable.CreateRotated(Paintable.CreateFromResMan((double) this.MoveDir.Y < 0.0 ? "arrow_select" : "arrow"), new GameManager.GraphicsSystem.Point(width / 2f, width / 2f), 1.57f).Paint(0.0f, -width, spriteBatch);
        Paintable.CreateRotated(Paintable.CreateFromResMan((double) this.MoveDir.Y > 0.0 ? "arrow_select" : "arrow"), new GameManager.GraphicsSystem.Point(width / 2f, width / 2f), -1.57f).Paint(-1f, 0.0f, spriteBatch);
        Paintable.CreateRotated(Paintable.CreateFromResMan((double) this.MoveDir.X < 0.0 ? "arrow_select" : "arrow"), new GameManager.GraphicsSystem.Point(width / 2f, width / 2f), 3.14f).Paint(this.Width - width, (float) -((double) width + 2.0), 36, spriteBatch);
        Paintable.CreateFromResMan((double) this.MoveDir.X > 0.0 ? "arrow_select" : "arrow").Paint(this.Width, 0.0f, 36, spriteBatch);
      }
      GameManager.GraphicsSystem.Point point1 = new GameManager.GraphicsSystem.Point(this.Width - (float) ((double) this.StoneEnabled.GetWidth() * 4.0 / 6.0), (float) ((double) this.StoneEnabled.GetHeight() * 4.0 / 6.0));
      if (flag)
        point1.Y += (float) ((double) this.Height / 4.0 + (double) this.StoneEnabled.GetHeight() / 2.0);
      (this.Stones > 0 ? this.StoneEnabled : this.StoneDisabled).Paint(point1.X, point1.Y, 18, spriteBatch);
      this.DropStone = false;
      foreach (int key in dictionary.Keys)
      {
        if ((double) (dictionary[key] - point1).Len() < (double) this.StoneEnabled.GetWidth() / 2.0)
        {
          this.DropStone = true;
          break;
        }
      }
      this.TargetCounter.Paint(this.Width / 2f, (float) ((double) this.Height - 2.0 - (double) this.HudUp.GetHeight() - (double) this.StationsIndicatorSigns[0].GetHeight() * 4.0 / 5.0), 20, spriteBatch);
      this.HudUp.Paint(this.Width / 2f, this.Height - 2f, 17, spriteBatch);
      for (int index = 0; index < 10; ++index)
      {
        Sprite passenger1 = this.Spawns[index].GetPassenger();
        if (passenger1 != null)
        {
          Passenger passenger2 = passenger1 as Passenger;
          if (passenger2.GetState() == GlobalMembers.PassengerState.PassengerStateInChopper)
          {
            float y2 = (float) ((double) this.Height - 2.0 - (double) this.HudUp.GetHeight() - (double) this.StationsCloudSigns[0].GetHeight() * 1.0 / 9.0);
            if (Util.ResolutionSet == 2 || Util.ResolutionSet == 3)
              y2 = (float) ((double) this.Height - 2.0 - (double) this.HudUp.GetHeight() - (double) this.StationsIndicatorSigns[0].GetHeight() * 4.0 / 5.0 + (double) this.TargetCounter.GetHeight() / 2.0);
            this.StationsIndicatorSigns[passenger2.GetTargetStation()].Paint(this.Width / 2f, y2, 18, spriteBatch);
          }
        }
      }
      Paintable.CreateClipped(0.0f, 0.0f, this.EnergyFill.GetWidth() * this.Energy, this.EnergyFill.GetHeight(), this.EnergyFill).Paint((float) (((double) this.Width - (double) this.EnergyFill.GetWidth()) / 2.0), this.Height - this.HudUp.GetHeight() / 2f, 10, spriteBatch);
      string s1 = string.Format("{0}/{1}", (object) this.PassengersDelivered, (object) this.PassengersToDeliver);
      GlobalMembers.Fonts[1].Write(s1, (float) (((double) this.Width + (double) this.HudUp.GetWidth()) / 2.0 - (double) this.HudUp.GetWidth() / 12.0), this.Height - this.HudUp.GetHeight() / 2f, 34, spriteBatch);
      string s2 = string.Format("{0}:{1}", (object) ((int) this.GameTime / 60), (object) ((int) this.GameTime % 60).ToString("D2"));
      GlobalMembers.Fonts[1].Write(s2, (float) (((double) this.Width - (double) this.HudUp.GetWidth()) / 2.0 + (double) this.HudUp.GetWidth() / 12.0), this.Height - this.HudUp.GetHeight() / 2f, 10, spriteBatch);
      this.PaintIndicators(spriteBatch);
      this.Pause.Paint(this.Width - this.Pause.GetWidth() / 2f, this.Height - this.Pause.GetHeight() / 2f, 33, spriteBatch);
      if (this == GlobalMembers.Manager.GetDisp())
      {
        Dictionary<int, GameManager.GraphicsSystem.Point> pointersPressed = GlobalMembers.Manager.GetPointersPressed();
        foreach (int key in pointersPressed.Keys)
        {
          GameManager.GraphicsSystem.Point point2 = pointersPressed[key];
          if ((double) point2.X > (double) this.Width - (double) this.Pause.GetWidth() * 2.0 && (double) point2.Y > (double) this.Height - (double) this.Pause.GetHeight() * 2.0)
            Menu.CreatePause().Display();
        }
      }
      GlobalMembers.Manager.Translate((float) (int) -(double) GlobalMembers.ToPx(this.ViewPos.X), (float) (int) -(double) GlobalMembers.ToPx(this.ViewPos.Y));
      foreach (Sprite sprite in this.Sprites[4])
        (sprite as Passenger).RenderCloud(spriteBatch);
      GlobalMembers.Manager.Translate((float) (int) GlobalMembers.ToPx(this.ViewPos.X), (float) (int) GlobalMembers.ToPx(this.ViewPos.Y));
      if (!this.HasLose)
        return;
      for (int index = 0; index < this.SoundSources.Count; ++index)
      {
        if (this.SoundSources[index].sound.State == SoundState.Playing)
          this.SoundSources[index].sound.Stop();
        this.SoundSources.Clear();
      }
      Paintable.CreateFilledRect(this.Width, this.Height, Util.ConvertIntToColor((uint) Math.Min((int) byte.MaxValue, (int) ((double) byte.MaxValue * ((double) this.GameTime - (double) this.LoseTime))))).Paint(0.0f, 0.0f, spriteBatch);
      Paintable.CreateFromResMan("lose_skull").Paint(this.Width / 2f, this.Height / 2f, 18, spriteBatch);
      string s3 = "";
      switch (this.LostReason)
      {
        case 0:
          s3 = Texts.TextDeathReasonHit;
          break;
        case 1:
          s3 = Texts.TextDeathReasonEnergyEnd;
          break;
        case 2:
          s3 = Texts.TextDeathReasonDrown;
          break;
        case 3:
          s3 = Texts.TextDeathReasonPterodactylHit;
          break;
      }
      GlobalMembers.Fonts[0].Write(s3, this.Width / 2f, (float) (((double) this.Height - (double) Paintable.CreateFromResMan("lose_skull").GetHeight()) / 2.0 - 40.0), 17, spriteBatch);
    }

    public override void Update(float time)
    {
      this.EnterTutorial(0);
      this.EnterTutorial(1);
      base.Update(time);
      for (int index = 0; index < 10; ++index)
        this.Spawns[index].Update(time);
      this.WaterLevel += time * this.WaterLevelRise;
      this.Energy -= time * 0.0045f;
      if ((double) this.Energy < 0.20000000298023224)
        GlobalMembers.Game.EnterTutorial(3);
      if ((double) this.Energy <= 0.0)
      {
        this.Lose(1);
        this.Energy = 0.0f;
      }
      foreach (SoundSource soundSource in this.SoundSources)
        soundSource.Update(this.Player.GetPos() + this.Player.GetSize() / 2f);
    }

    public override void OnShow()
    {
    }

    public override void Load()
    {
      GlobalMembers.SfxGroundHitInstance.Volume = GlobalMembers.Save.GetSoundVolume();
      GlobalMembers.SfxCopterRotorInstance.Volume = 0.0f;
      GlobalMembers.SfxCopterRotorInstance.Play();
      if (this.FirstLoad)
      {
        this.ArrowImg = Paintable.CreateFromResMan("fly_arrow");
        this.TargetCounter = Paintable.CreateFromResMan("taxi_counter");
        if (this.TriceratopsRun.Count < 6)
        {
          this.TriceratopsRun.Add(Paintable.CreateFromResMan("walk_1"));
          this.TriceratopsRun.Add(Paintable.CreateFromResMan("walk_2"));
          this.TriceratopsRun.Add(Paintable.CreateFromResMan("walk_3"));
          this.TriceratopsRun.Add(Paintable.CreateFromResMan("walk_4"));
          this.TriceratopsRun.Add(Paintable.CreateFromResMan("walk_5"));
          this.TriceratopsRun.Add(Paintable.CreateFromResMan("walk_6"));
        }
        Paintable[] frames1 = new Paintable[this.TriceratopsRun.Count];
        for (int index = 0; index < this.TriceratopsRun.Count; ++index)
          frames1[index] = this.TriceratopsRun[index];
        this.TriceratopsRunAnim = Paintable.CreateAnim(this.TriceratopsRun.Count, frames1, 1.5f);
        if (this.TriceratopsEat.Count < 6)
        {
          this.TriceratopsEat.Add(Paintable.CreateFromResMan("eat_1"));
          this.TriceratopsEat.Add(Paintable.CreateFromResMan("eat_2"));
          this.TriceratopsEat.Add(Paintable.CreateFromResMan("eat_3"));
          this.TriceratopsEat.Add(Paintable.CreateFromResMan("eat_4"));
          this.TriceratopsEat.Add(Paintable.CreateFromResMan("eat_3"));
          this.TriceratopsEat.Add(Paintable.CreateFromResMan("eat_4"));
          this.TriceratopsEat.Add(Paintable.CreateFromResMan("eat_3"));
          this.TriceratopsEat.Add(Paintable.CreateFromResMan("eat_4"));
        }
        Paintable[] frames2 = new Paintable[this.TriceratopsEat.Count];
        for (int index = 0; index < this.TriceratopsEat.Count; ++index)
          frames2[index] = this.TriceratopsEat[index];
        this.TriceratopsEatAnim = Paintable.CreateAnim(this.TriceratopsEat.Count, frames2, 1.5f);
        if (this.TriceratopsInjured.Count < 2)
        {
          this.TriceratopsInjured.Add(Paintable.CreateFromResMan("injured_1"));
          this.TriceratopsInjured.Add(Paintable.CreateFromResMan("injured_2"));
        }
        Paintable[] frames3 = new Paintable[this.TriceratopsInjured.Count];
        for (int index = 0; index < this.TriceratopsInjured.Count; ++index)
          frames3[index] = this.TriceratopsInjured[index];
        this.TriceratopsInjuredAnim = Paintable.CreateAnim(this.TriceratopsInjured.Count, frames3, 0.5f);
        if (this.TriceratopsLook.Count < 2)
        {
          this.TriceratopsLook.Add(Paintable.CreateFromResMan("stand"));
          this.TriceratopsLook.Add(Paintable.CreateFromResMan("look"));
        }
        Paintable[] frames4 = new Paintable[this.TriceratopsLook.Count];
        for (int index = 0; index < this.TriceratopsLook.Count; ++index)
          frames4[index] = this.TriceratopsLook[index];
        this.TriceratopsLookAnim = Paintable.CreateAnim(this.TriceratopsLook.Count, frames4, 1f);
        if (this.Pterodactyl.Count < 10)
        {
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero1"));
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero2"));
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero3"));
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero4"));
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero5"));
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero6"));
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero5"));
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero4"));
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero3"));
          this.Pterodactyl.Add(Paintable.CreateFromResMan("ptero2"));
        }
        Paintable[] frames5 = new Paintable[this.Pterodactyl.Count];
        for (int index = 0; index < this.Pterodactyl.Count; ++index)
          frames5[index] = this.Pterodactyl[index];
        this.PterodactylAnim = Paintable.CreateAnim(this.Pterodactyl.Count, frames5, 1.5f);
        this.PterodactylAnim.Mirror = true;
        this.StoneAnim = Paintable.CreateFromResMan("stone");
        this.BgImg = Paintable.CreateFromResMan("bg");
        if (this.TreeStand.Count < 2)
        {
          this.TreeStand.Add(Paintable.CreateFromResMan("tree_1"));
          this.TreeStand.Add(Paintable.CreateFromResMan("tree_2"));
        }
        Paintable[] frames6 = new Paintable[10];
        frames6[0] = this.TreeStand[1];
        for (int index = 1; index < 10; ++index)
          frames6[index] = this.TreeStand[0];
        this.TreeStandAnim = Paintable.CreateAnim(10, frames6, 8f);
        this.TreeHitAnim = Paintable.CreateAnim(1, new Paintable[1]
        {
          Paintable.CreateFromResMan("tree_3")
        }, 0.3f);
        this.Food[0] = Paintable.CreateFromResMan("ananas");
        this.Food[1] = Paintable.CreateFromResMan("arbuz");
        this.Food[2] = Paintable.CreateFromResMan("banan");
        this.Food[3] = Paintable.CreateFromResMan("jablko");
        this.Food[4] = Paintable.CreateFromResMan("miecho");
        this.DustAnim = Paintable.CreateFromResMan("dust");
        if (this.SleeperIn.Count < 5)
        {
          this.SleeperIn.Add(Paintable.CreateFromResMan("sleep_1"));
          this.SleeperIn.Add(Paintable.CreateFromResMan("sleep_2"));
          this.SleeperIn.Add(Paintable.CreateFromResMan("sleep_3"));
          this.SleeperIn.Add(Paintable.CreateFromResMan("sleep_4"));
          this.SleeperIn.Add(Paintable.CreateFromResMan("sleep_5"));
        }
        Paintable[] frames7 = new Paintable[this.SleeperIn.Count];
        for (int index = 0; index < this.SleeperIn.Count; ++index)
          frames7[index] = this.SleeperIn[index];
        this.SleeperInAnim = Paintable.CreateAnim(this.SleeperIn.Count, frames7, 1.5f);
        this.SleeperInAnim.Mirror = true;
        if (this.SleeperOut.Count < 5)
        {
          this.SleeperOut.Add(Paintable.CreateFromResMan("sleep_6"));
          this.SleeperOut.Add(Paintable.CreateFromResMan("sleep_7"));
          this.SleeperOut.Add(Paintable.CreateFromResMan("sleep_8"));
          this.SleeperOut.Add(Paintable.CreateFromResMan("sleep_9"));
          this.SleeperOut.Add(Paintable.CreateFromResMan("sleep_10"));
          this.SleeperOut.Add(Paintable.CreateFromResMan("sleep_11"));
        }
        Paintable[] frames8 = new Paintable[this.SleeperOut.Count];
        for (int index = 0; index < this.SleeperOut.Count; ++index)
          frames8[index] = this.SleeperOut[index];
        this.SleeperOutAnim = Paintable.CreateAnim(this.SleeperOut.Count, frames8, 1.5f);
        this.SleeperOutAnim.Mirror = true;
        for (int index1 = 0; index1 < 3; ++index1)
        {
          for (int index2 = 0; index2 < 8; ++index2)
          {
            string resourceId = string.Format("passenger{0}_walk_{1}", (object) (index1 + 1), (object) (index2 + 1));
            this.PassengerWalk[index1].Add(Paintable.CreateFromResMan(resourceId));
          }
          Paintable[] frames9 = new Paintable[this.PassengerWalk[index1].Count];
          for (int index3 = 0; index3 < this.PassengerWalk[index1].Count; ++index3)
            frames9[index3] = this.PassengerWalk[index1][index3];
          this.PassengerWalkAnim[index1] = Paintable.CreateAnim(this.PassengerWalk[index1].Count, frames9, 1f);
          this.PassengerWalkAnim[index1].Mirror = true;
          for (int index4 = 0; index4 < 3; ++index4)
          {
            string resourceId = string.Format("passenger{0}_stand_{1}", (object) (index1 + 1), (object) (index4 + 1));
            this.PassengerStand[index1].Add(Paintable.CreateFromResMan(resourceId));
          }
          Paintable[] frames10 = new Paintable[this.PassengerStand[index1].Count * 2 - 2];
          for (int index5 = 0; index5 < this.PassengerStand[index1].Count; ++index5)
            frames10[index5] = this.PassengerStand[index1][index5];
          for (int index6 = 0; index6 < this.PassengerStand[index1].Count - 2; ++index6)
            frames10[this.PassengerStand[index1].Count + index6] = this.PassengerStand[index1][this.PassengerStand[index1].Count - 2 - index6];
          this.PassengerStandAnim[index1] = Paintable.CreateAnim(this.PassengerStand[index1].Count * 2 - 2, frames10, 0.5f);
          this.PassengerStandAnim[index1].Mirror = true;
          for (int index7 = 0; index7 < 3; ++index7)
          {
            string resourceId = string.Format("passenger{0}_speak_{1}", (object) (index1 + 1), (object) (index7 + 1));
            this.PassengerSpeak[index1].Add(Paintable.CreateFromResMan(resourceId));
          }
          Paintable[] frames11 = new Paintable[7]
          {
            this.PassengerSpeak[index1][0],
            this.PassengerSpeak[index1][1],
            this.PassengerSpeak[index1][2],
            this.PassengerSpeak[index1][1],
            this.PassengerSpeak[index1][2],
            this.PassengerSpeak[index1][1],
            this.PassengerSpeak[index1][0]
          };
          this.PassengerSpeakAnim[index1] = Paintable.CreateAnim(7, frames11, 0.5f);
          this.PassengerSpeakAnim[index1].Mirror = true;
          for (int index8 = 0; index8 < 5; ++index8)
          {
            string resourceId = string.Format("passenger{0}_swim_{1}", (object) (index1 + 1), (object) (index8 + 1));
            this.PassengerSwim[index1].Add(Paintable.CreateFromResMan(resourceId));
          }
          Paintable[] frames12 = new Paintable[this.PassengerSwim[index1].Count];
          for (int index9 = 0; index9 < this.PassengerSwim[index1].Count; ++index9)
            frames12[index9] = this.PassengerSwim[index1][index9];
          this.PassengerSwimAnim[index1] = Paintable.CreateAnim(this.PassengerSwim[index1].Count, frames12, 1f);
          this.PassengerSwimAnim[index1].Mirror = true;
          for (int index10 = 0; index10 < 3; ++index10)
          {
            string resourceId = string.Format("passenger{0}_sink_{1}", (object) (index1 + 1), (object) (index10 + 1));
            this.PassengerSink[index1].Add(Paintable.CreateFromResMan(resourceId));
          }
          Paintable[] frames13 = new Paintable[this.PassengerSink[index1].Count * 2 - 2];
          for (int index11 = 0; index11 < this.PassengerSink[index1].Count; ++index11)
            frames13[index11] = this.PassengerSink[index1][index11];
          for (int index12 = 0; index12 < this.PassengerSink[index1].Count - 2; ++index12)
            frames13[this.PassengerSink[index1].Count + index12] = this.PassengerSink[index1][this.PassengerSink[index1].Count - 2 - index12];
          this.PassengerSinkAnim[index1] = Paintable.CreateAnim(this.PassengerSink[index1].Count * 2 - 2, frames13, 0.5f);
          this.PassengerSinkAnim[index1].Mirror = true;
        }
        this.Water[0] = Paintable.CreateFromResMan("water_1");
        this.Water[1] = Paintable.CreateFromResMan("water_2");
        this.Cloud = Paintable.CreateFromResMan("dymek");
        this.IndicatorWaiting = Paintable.CreateFromResMan("indicator_1");
        this.IndicatorTarget = Paintable.CreateFromResMan("indicator_3");
        this.IndicatorWater = Paintable.CreateFromResMan("indicator_2");
        this.StationsIndicatorSigns[0] = Paintable.CreateFromResMan("stop1");
        this.StationsIndicatorSigns[1] = Paintable.CreateFromResMan("stop2");
        this.StationsIndicatorSigns[2] = Paintable.CreateFromResMan("stop3");
        this.StationsIndicatorSigns[3] = Paintable.CreateFromResMan("stop4");
        this.StationsIndicatorSigns[4] = Paintable.CreateFromResMan("stop5");
        this.StationsIndicatorSigns[5] = Paintable.CreateFromResMan("stop6");
        this.StationsCloudSigns[0] = Paintable.CreateFromResMan("stop1_small");
        this.StationsCloudSigns[1] = Paintable.CreateFromResMan("stop2_small");
        this.StationsCloudSigns[2] = Paintable.CreateFromResMan("stop3_small");
        this.StationsCloudSigns[3] = Paintable.CreateFromResMan("stop4_small");
        this.StationsCloudSigns[4] = Paintable.CreateFromResMan("stop5_small");
        this.StationsCloudSigns[5] = Paintable.CreateFromResMan("stop6_small");
        this.Pause = Paintable.CreateFromResMan("pause_btn");
        this.HudUp = Paintable.CreateFromResMan("up_belka");
        this.EnergyFill = Paintable.CreateFromResMan("up_wypelnienie");
        this.StoneEnabled = Paintable.CreateFromResMan("stone_down");
        this.StoneDisabled = Paintable.CreateFromResMan("stone_up");
      }
      this.TargetCounter.AddToLoad();
      GlobalMembers.ResManLoader.AddToLoad("arrow");
      GlobalMembers.ResManLoader.AddToLoad("arrow_select");
      this.Pause.AddToLoad();
      this.HudUp.AddToLoad();
      this.EnergyFill.AddToLoad();
      this.StoneEnabled.AddToLoad();
      this.StoneDisabled.AddToLoad();
      for (int index = 0; index < GlobalMembers.FontsNum; ++index)
        GlobalMembers.Fonts[index].Img.AddToLoad();
      for (int index = 0; index < 5; ++index)
        this.Food[index].AddToLoad();
      for (int index = 0; index < 2; ++index)
        this.Water[index].AddToLoad();
      for (int index = 0; index < 6; ++index)
      {
        this.StationsCloudSigns[index].AddToLoad();
        this.StationsIndicatorSigns[index].AddToLoad();
      }
      for (int index = 0; index < 3; ++index)
        this.PassengerWalkAnim[index].AddToLoad();
      for (int index = 0; index < 3; ++index)
        this.PassengerStandAnim[index].AddToLoad();
      for (int index = 0; index < 3; ++index)
        this.PassengerSinkAnim[index].AddToLoad();
      for (int index = 0; index < 3; ++index)
        this.PassengerSwimAnim[index].AddToLoad();
      for (int index = 0; index < 3; ++index)
        this.PassengerSpeakAnim[index].AddToLoad();
      this.Cloud.AddToLoad();
      this.TilesImg = Paintable.CreateFromResMan("tiles");
      this.TilesImg.AddToLoad();
      this.BgImg.AddToLoad();
      this.IndicatorTarget.AddToLoad();
      this.IndicatorWaiting.AddToLoad();
      this.IndicatorWater.AddToLoad();
      this.ArrowImg.AddToLoad();
      GlobalMembers.ResManLoader.AddToLoad("whirl");
      GlobalMembers.ResManLoader.AddToLoad("lose_skull");
      GlobalMembers.ResManLoader.AddToLoad("copter");
      this.TreeStandAnim.AddToLoad();
      this.TreeHitAnim.AddToLoad();
      this.DustAnim.AddToLoad();
      this.StoneAnim.AddToLoad();
      this.TriceratopsEatAnim.AddToLoad();
      this.TriceratopsInjuredAnim.AddToLoad();
      this.TriceratopsRunAnim.AddToLoad();
      this.TriceratopsLookAnim.AddToLoad();
      this.PterodactylAnim.AddToLoad();
      this.SleeperInAnim.AddToLoad();
      this.SleeperOutAnim.AddToLoad();
      GlobalMembers.ResManLoader.LoadResources(true);
      if (this.FirstLoad)
      {
        this.Tiles = Paintable.CreateTiles(this.TilesImg, 16, 16);
        this.Copter = Paintable.CreateTiles(Paintable.CreateFromResMan("copter"), 6, 1);
        this.Whirl = Paintable.CreateTiles(Paintable.CreateFromResMan("whirl"), 8, 1);
        this.Scale = 1f;
        this.ViewSize = new GameManager.GraphicsSystem.Point(GlobalMembers.FromPx(this.Width), GlobalMembers.FromPx(this.Height));
        this.Gravity = new GameManager.GraphicsSystem.Point(0.0f, (float) (-(double) GlobalMembers.FromPx(GlobalMembers.TileSize.Y) * 8.0));
        Stream input = TitleContainer.OpenStream(string.Format("Content/Levels/level{0:00}.dt", (object) (this.Level + 1)));
        BinaryReader binaryReader = new BinaryReader(input);
        this.PassengersToDeliver = binaryReader.ReadInt32();
        this.PassengersToDeliver = Math.Min(this.PassengersToDeliver, 7);
        this.MapSize.X = (float) binaryReader.ReadInt32();
        this.MapSize.Y = (float) binaryReader.ReadInt32();
        GameManager.GraphicsSystem.Point point1 = new GameManager.GraphicsSystem.Point(this.MapSize);
        if ((double) this.MapSize.Y < (double) this.Height / (double) GlobalMembers.TileSize.Y)
          this.MapSize.Y = (float) ((int) ((double) this.Height / (double) GlobalMembers.TileSize.Y) + 1);
        this.Bg = new short[(int) this.MapSize.X][];
        this.Fg = new short[(int) this.MapSize.X][];
        for (int index = 0; (double) index < (double) this.MapSize.X; ++index)
        {
          this.Bg[index] = new short[(int) this.MapSize.Y];
          this.Fg[index] = new short[(int) this.MapSize.Y];
        }
        this.Grids[1] = new SpriteGrid(new GameManager.GraphicsSystem.Point(this.MapSize.X, this.MapSize.Y), 20, 20);
        for (int y = (int) point1.Y - 1; y >= 0; --y)
        {
          for (int x = 0; x < (int) point1.X; ++x)
          {
            short index = binaryReader.ReadInt16();
            this.Bg[x][y] = index;
            if (index != (short) -1)
            {
              Sprite sprite = Sprite.CreateSprite(-1, new GameManager.GraphicsSystem.Point((float) x, (float) y));
              this.AddSprite(sprite, 0);
              sprite.SetPaintable(this.Tiles[(int) index]);
            }
          }
        }
        for (int y = (int) point1.Y - 1; y >= 0; --y)
        {
          for (int x = 0; x < (int) point1.X; ++x)
          {
            short num = binaryReader.ReadInt16();
            this.Fg[x][y] = num;
            if (num != (short) -1)
            {
              int layer = Game1.GetShape((int) num) == GlobalMembers.Shape.ShapeNone ? 0 : 1;
              Sprite sprite = !Lava.IsLava((int) num) ? (!this.IsKilling((int) num) ? Sprite.CreateSprite(-2, new GameManager.GraphicsSystem.Point((float) x, (float) y)) : Lava.CreateLava(new GameManager.GraphicsSystem.Point((float) x, (float) y), (int) num)) : Lava.CreateLava(new GameManager.GraphicsSystem.Point((float) x, (float) y), (int) num);
              this.AddSprite(sprite, layer);
              sprite.SetPaintable(this.CreateView((int) num));
              this.ApplyShape(sprite, Game1.GetShape((int) num));
            }
          }
        }
        for (int y = 0; y < (int) point1.Y; ++y)
        {
          for (int x = 0; x < (int) point1.X; ++x)
          {
            if (this.IsCaveEntry((int) this.Fg[x][y]))
            {
              Spawn spawn = new Spawn(new GameManager.GraphicsSystem.Point((float) x, (float) y), this);
              this.Spawns[spawn.GetStation()] = spawn;
            }
            if (this.IsDustSpawner((int) this.Fg[x][y]))
              this.AddSprite(DustSpawner.CreateDustSpawner(new GameManager.GraphicsSystem.Point((float) x + 0.5f, (float) y + 0.5f)), 6);
          }
        }
        int num1 = binaryReader.ReadInt32();
        for (int index = 0; index < num1; ++index)
        {
          int num2 = binaryReader.ReadInt32();
          GameManager.GraphicsSystem.Point point2 = new GameManager.GraphicsSystem.Point()
          {
            X = binaryReader.ReadSingle(),
            Y = binaryReader.ReadSingle()
          };
          point2.Y = (float) ((double) point1.Y - (double) point2.Y - 1.0);
          switch (num2)
          {
            case 1:
              this.Player = GameManager.Player.CreatePlayer();
              this.AddSprite(this.Player, 5);
              this.Player.SetPos(point2);
              if (this.Level == 13)
              {
                this.Player.Pos.X = 6f;
                this.Player.Pos.Y = 7f;
                break;
              }
              break;
            case 3:
              this.AddSprite(Chaser.CreateChaser(point2), 3);
              break;
            case 4:
              this.AddSprite(GameManager.Pterodactyl.CreatePterodactyl(point2), 3);
              break;
            case 5:
              bool right = binaryReader.ReadBoolean();
              Sprite sleeper = Sleeper.CreateSleeper(point2, right);
              this.AddSprite(sleeper, 3);
              sleeper.SetPos(point2);
              break;
            case 6:
              if (!Game1.IsSerialized)
              {
                this.AddSprite(Stone.CreateStone(point2), 2);
                break;
              }
              break;
            case 7:
              this.AddSprite(Tree.CreateTree(point2), 2);
              break;
          }
        }
        this.WaterLevel = binaryReader.ReadSingle();
        this.WaterLevelRise = binaryReader.ReadSingle();
        int num3 = binaryReader.ReadInt32();
        
        for (int index = 0; index < num3; ++index)
          this.Spawns[binaryReader.ReadInt32() - 1].SetMaxPassengersSpawned(binaryReader.ReadInt32());
        
        binaryReader.Dispose();
        input.Dispose();

        bool flag = true;
        for (int index = 0; (double) index < (double) this.MapSize.X; ++index)
        {
          if (this.Bg[index][(int) this.MapSize.Y - 1] == (short) -1)
            flag = false;
        }
        if (flag)
        {
          float num4 = -4.5f;
          while ((double) num4 < (double) this.MapSize.X + 3.0)
          {
            num4 += 4.5f + Util.Randf(1.5f);
            float a1 = 3f + Util.Randf(3f);
            Sprite sprite1 = Sprite.CreateSprite(0, new GameManager.GraphicsSystem.Point(num4, this.MapSize.Y - a1));
            this.AddSprite(sprite1, 6);
            float ww = GlobalMembers.ToPx(a1) * (0.2f + Util.Randf(0.2f));
            sprite1.SetPaintable(this.CreateLight(GlobalMembers.ToPx(a1), GlobalMembers.ToPx(a1), 0.3f, 0.0f, ww, (int) GlobalMembers.ToPx(num4), (int) GlobalMembers.ToPx(this.MapSize.Y - a1)));
            if ((double) a1 - 3.0 > 0.75)
            {
              float num5 = num4 + (a1 - GlobalMembers.FromPx(ww / 3f + Util.Randf((float) ((double) ww * 2.0 / 3.0))));
              float a2 = 3f + Util.Randf(3f);
              num4 = num5 - a2;
              Sprite sprite2 = Sprite.CreateSprite(0, new GameManager.GraphicsSystem.Point(num4, this.MapSize.Y - a2));
              this.AddSprite(sprite2, 6);
              sprite2.SetPaintable(this.CreateLight(GlobalMembers.ToPx(a2), GlobalMembers.ToPx(a2), 0.3f, 0.0f, GlobalMembers.ToPx(a2 / 3f) * (0.2f + Util.Randf(0.2f)), (int) GlobalMembers.ToPx(num4), (int) GlobalMembers.ToPx(this.MapSize.Y - a2)));
            }
          }
        }
        this.FirstLoad = false;
      }
      Sprite sprite3 = Sprite.CreateSprite(-2, new GameManager.GraphicsSystem.Point(-1f, 0.0f));
      sprite3.SetPaintable(Paintable.CreateInvisibleRect(GlobalMembers.ToPx(1f), GlobalMembers.ToPx(this.MapSize.Y)));
      this.AddSprite(sprite3, 1);
      Sprite sprite4 = Sprite.CreateSprite(-2, new GameManager.GraphicsSystem.Point(this.MapSize.X, 0.0f));
      sprite4.SetPaintable(Paintable.CreateInvisibleRect(GlobalMembers.ToPx(1f), GlobalMembers.ToPx(this.MapSize.Y)));
      this.AddSprite(sprite4, 1);
      Sprite sprite5 = Sprite.CreateSprite(-2, new GameManager.GraphicsSystem.Point(0.0f, this.MapSize.Y));
      sprite5.SetPaintable(Paintable.CreateInvisibleRect(GlobalMembers.ToPx(this.MapSize.X), GlobalMembers.ToPx(1f)));
      this.AddSprite(sprite5, 1);
      if (!Game1.IsSerialized)
        return;
      this.GameFromSerialization();
      Game1.IsSerialized = false;
    }

    public override void OnHide()
    {
      if (GlobalMembers.SfxCopterRotorInstance.State == SoundState.Playing)
        GlobalMembers.SfxCopterRotorInstance.Pause();
      if (GlobalMembers.Manager.Next == null || !(GlobalMembers.Manager.Next is Menu) || (GlobalMembers.Manager.Next as Menu).GetType() == 13 || (GlobalMembers.Manager.Next as Menu).GetType() == 3)
        return;
      for (int index = 0; index < this.SoundSources.Count; ++index)
      {
        if (this.SoundSources[index].sound.State == SoundState.Playing)
          this.SoundSources[index].sound.Stop();
        this.SoundSources.Clear();
      }
    }

    public void EnterTutorial(int tutNum)
    {
      if (GlobalMembers.Manager.Next != null || GlobalMembers.Save.WasTutorial(tutNum))
        return;
      Menu.CreateTutorial(false, tutNum).Display();
      GlobalMembers.Save.UsedTutorial(tutNum);
    }

    public void AddEnergy(float a)
    {
      this.Energy += a;
      if ((double) this.Energy < 0.0)
        this.Energy = 0.0f;
      if ((double) this.Energy <= 1.0)
        return;
      this.Energy = 1f;
    }

    public GameManager.GraphicsSystem.Point GetGravity(Sprite s) => new GameManager.GraphicsSystem.Point(this.Gravity);

    public bool IsCaveEntry(int tile) => tile == 137 || tile == 138 || tile == 139;

    public bool IsLandindPlatform(int tile)
    {
      return tile == 168 || tile == 169 || tile == 170 || tile == 171;
    }

    public bool IsDustSpawner(int tile)
    {
      return tile == 161 || tile == 144 || tile == 128 || tile == 129 || tile == 104 || tile == 105 || tile == 102 || tile == 88;
    }

    public bool IsKilling(int tile)
    {
      return tile == 72 || tile == 73 || tile == 74 || tile == 75 || tile == 78 || tile == 79 || tile == 56 || tile == 57 || tile == 58 || tile == 59 || tile == 60 || tile == 61 || tile == 40 || tile == 41 || tile == 42 || tile == 43;
    }

    public GameManager.GraphicsSystem.Point GetPlatformSize(int x, int y)
    {
      GameManager.GraphicsSystem.Point platformSize = new GameManager.GraphicsSystem.Point((float) x, (float) x);
      while ((double) platformSize.X > 0.0 && Game1.GetShape((int) this.Fg[(int) platformSize.X - 1][y - 1]) != GlobalMembers.Shape.ShapeNone && Game1.GetShape((int) this.Fg[(int) platformSize.X - 1][y]) == GlobalMembers.Shape.ShapeNone)
        --platformSize.X;
      while ((double) platformSize.Y < (double) this.MapSize.X - 1.0 && Game1.GetShape((int) this.Fg[(int) platformSize.Y + 1][y - 1]) != GlobalMembers.Shape.ShapeNone && Game1.GetShape((int) this.Fg[(int) platformSize.Y + 1][y]) == GlobalMembers.Shape.ShapeNone)
        ++platformSize.Y;
      return platformSize;
    }

    public GameManager.GraphicsSystem.Point GetMapSize() => new GameManager.GraphicsSystem.Point(this.MapSize);

    public int GetPlatformId(int tile) => tile < 118 || tile > 123 ? -1 : tile - 118;

    public override void PointerPressed(GameManager.GraphicsSystem.Point pos, int id)
    {
      if (this.HasLose && (double) this.GameTime - (double) this.LoseTime > 1.0)
        Menu.CreateRetry().Display();
      if (!GlobalMembers.HasCheat || (double) pos.X >= 10.0 || (double) pos.Y >= 10.0)
        return;
      this.Win();
    }

    ~Game1()
    {
      this.Bg = (short[][]) null;
      this.Fg = (short[][]) null;
    }

    public Paintable CreateLight(
      float w,
      float h,
      float lightMax,
      float lightMin,
      float ww,
      int x,
      int y)
    {
      Paintable group = Paintable.CreateGroup(false, w, h);
      double num1 = (double) Util.Randf(0.2f);
      GameManager.GraphicsSystem.Point[] _vertexes1 = new GameManager.GraphicsSystem.Point[4];
      GameManager.Utils.Color[] colors1 = new GameManager.Utils.Color[4];
      _vertexes1[0] = new GameManager.GraphicsSystem.Point(0.0f, ww);
      _vertexes1[2] = new GameManager.GraphicsSystem.Point(0.0f, 0.0f);
      _vertexes1[1] = new GameManager.GraphicsSystem.Point(w - ww, h);
      _vertexes1[3] = new GameManager.GraphicsSystem.Point(w, h);
      for (int index = 0; index < _vertexes1.Length; ++index)
      {
        _vertexes1[index].X += (float) x;
        _vertexes1[index].Y += (float) y;
      }
      colors1[0] = new GameManager.Utils.Color(1f, 1f, 1f, lightMin);
      colors1[2] = new GameManager.Utils.Color(1f, 1f, 1f, lightMin);
      colors1[1] = new GameManager.Utils.Color(1f, 1f, 1f, lightMax);
      colors1[3] = new GameManager.Utils.Color(1f, 1f, 1f, lightMax);
      group.AddElement(0.0f, 0.0f, Paintable.CreateColoredPolygon(4, _vertexes1, colors1));
      float num2 = 0.075f;
      GameManager.GraphicsSystem.Point[] _vertexes2 = new GameManager.GraphicsSystem.Point[4];
      GameManager.Utils.Color[] colors2 = new GameManager.Utils.Color[4];
      _vertexes2[0] = new GameManager.GraphicsSystem.Point(0.0f, ww + ww / 4f);
      _vertexes2[2] = new GameManager.GraphicsSystem.Point(0.0f, ww);
      _vertexes2[1] = new GameManager.GraphicsSystem.Point((float) ((double) w - (double) ww - (double) ww / 4.0), h);
      _vertexes2[3] = new GameManager.GraphicsSystem.Point(w - ww, h);
      for (int index = 0; index < _vertexes2.Length; ++index)
      {
        _vertexes2[index].X += (float) x;
        _vertexes2[index].Y += (float) y;
      }
      colors2[0] = new GameManager.Utils.Color(1f, 1f, 1f, lightMin);
      colors2[2] = new GameManager.Utils.Color(1f, 1f, 1f, lightMin);
      colors2[1] = new GameManager.Utils.Color(1f, 1f, 1f, lightMin);
      colors2[3] = new GameManager.Utils.Color(1f, 1f, 1f, 0.5f);
      group.AddElement(0.0f, ww, Paintable.CreateColoredPolygon(4, _vertexes2, colors2));
      GameManager.GraphicsSystem.Point[] _vertexes3 = new GameManager.GraphicsSystem.Point[4];
      GameManager.Utils.Color[] colors3 = new GameManager.Utils.Color[4];
      _vertexes3[0] = new GameManager.GraphicsSystem.Point(0.0f, 0.0f);
      _vertexes3[2] = new GameManager.GraphicsSystem.Point(w * num2, 0.0f);
      _vertexes3[1] = new GameManager.GraphicsSystem.Point(w, h);
      _vertexes3[3] = new GameManager.GraphicsSystem.Point(w + w * num2, h);
      for (int index = 0; index < _vertexes3.Length; ++index)
      {
        _vertexes3[index].X += (float) x;
        _vertexes3[index].Y += (float) y;
      }
      colors3[0] = new GameManager.Utils.Color(1f, 1f, 1f, lightMin);
      colors3[2] = new GameManager.Utils.Color(1f, 1f, 1f, lightMin);
      colors3[1] = new GameManager.Utils.Color(1f, 1f, 1f, 0.7f);
      colors3[3] = new GameManager.Utils.Color(1f, 1f, 1f, lightMin);
      group.AddElement(0.0f, 0.0f, Paintable.CreateColoredPolygon(4, _vertexes3, colors3));
      return group;
    }

    public void Win()
    {
      if (this.HasWon || this.HasLose)
        return;
      this.HasWon = true;
      DispManager.StopMusic();
      GlobalMembers.Manager.PlaySound(GlobalMembers.SfxWin);
      GlobalMembers.Save.SetTime(this.Level, this.GameTime);
      Menu.CreateWin(this.Level, this.GameTime).Display();
    }

    public void Lose(int _lostReason)
    {
      if (this.HasWon || this.HasLose)
        return;
      this.LostReason = _lostReason;
      this.HasLose = true;
      DispManager.StopMusic();
      GlobalMembers.Manager.PlaySound(GlobalMembers.SfxLose);
      this.LoseTime = this.GameTime;
    }

    public void PassengerDelivered()
    {
      ++this.PassengersDelivered;
      if (this.PassengersDelivered < this.PassengersToDeliver)
        return;
      this.Win();
    }

    public void GameFromSerialization()
    {
      this.FirstLoad = Game1.GameState.FirstLoad;
      this.HasLose = Game1.GameState.HasLose;
      this.HasWon = Game1.GameState.HasWon;
      this.PassengersDelivered = Game1.GameState.PassengersDelivered;
      this.PassengersToDeliver = Game1.GameState.PassengersToDeliver;
      this.Energy = Game1.GameState.Energy;
      this.WaterLevel = Game1.GameState.WaterLevel;
      this.WaterLevelRise = Game1.GameState.WaterLevelRise;
      this.GameTime = Game1.GameState.GameTime;
      this.MoveDir = Game1.GameState.MoveDir;
      this.Stones = Game1.GameState.Stones;
      this.Level = Game1.GameState.Level;
      this.DropStone = Game1.GameState.DropStone;
      this.LostReason = Game1.GameState.LostReason;

      for (int index = 0; index < Game1.GameState.StonePos.Count; ++index)
      {
        Sprite stone = Stone.CreateStone(Game1.GameState.StonePos[index]);
        (stone as Stone).OnGround = Game1.GameState.StoneOnGround[index];
        (stone as Stone).State = Game1.GameState.StoneState[index];
        this.AddSprite(stone, 2);
      }

      for (int index = 0; index < Game1.GameState.FruitPos.Count; ++index)
      {
        Sprite fruit = Fruit.CreateFruit(Game1.GameState.FruitPos[index]);
        (fruit as Fruit).OnGround = Game1.GameState.FruitOnGround[index];
        (fruit as Fruit).Type = Game1.GameState.FruitType[index];
        (fruit as Fruit).SetPaintable(Paintable.Copy(GlobalMembers.Game.Food[Game1.GameState.FruitType[index]]));
        this.AddSprite(fruit, 2);
      }
      int index1 = 0;

      for (int index2 = 0; index2 < this.GetSpriteLayer(3).Count; ++index2)
      {
        if (this.GetSpriteLayer(3)[index2].TypeId == 3)
        {
          Chaser chaser = this.GetSpriteLayer(3)[index2] as Chaser;
          chaser.SetState(Game1.GameState.ChaserState[index1]);
          chaser.StateChangeTime = Game1.GameState.ChaserStateChangeTime[index1];
          chaser.WereHit = Game1.GameState.ChaserWereHit[index1++];
        }
      }
      
      this.Player.SetPos(Game1.GameState.PlayerPos);
      (this.Player as GameManager.Player).FirstCrash = Game1.GameState.FirstCrash;
      (this.Player as GameManager.Player).CopterAnimPhase = Game1.GameState.CopterAnimPhase;
      (this.Player as GameManager.Player).WhirlAnimPhase = Game1.GameState.WhirlAnimPhase;
      (this.Player as GameManager.Player).OnGround = Game1.GameState.OnGround;
      (this.Player as GameManager.Player).WaterFloatEnergyStart = Game1.GameState.WaterFloatEnergyStart;
      (this.Player as GameManager.Player).WaterFloatStart = Game1.GameState.WaterFloatStart;
      (this.Player as GameManager.Player).FloatsOnWater = Game1.GameState.FloatsOnWater;
      (this.Player as GameManager.Player).PassengersIn = Game1.GameState.PassengersIn;
      (this.Player as GameManager.Player).MaxPassengers = Game1.GameState.MaxPassengers;
      
      for (int index3 = 0; index3 < 10; ++index3)
      {
        if (Game1.GameState.ThereIsPassenger[index3])
        {
          this.Spawns[index3].IsPassangerAlive = Game1.GameState.IsPassangerAlive[index3];
          this.Spawns[index3].PassengersSpawned = Game1.GameState.PassengersSpawned[index3];
          this.Spawns[index3].MaxPassengersSpawned = Game1.GameState.MaxPassengersSpawned[index3];
          GameManager.GraphicsSystem.Point passengerPo = Game1.GameState.PassengerPos[index3];
          if (this.Spawns[index3].Passenger == null && Game1.GameState.PassengerState[index3] != GlobalMembers.PassengerState.PassengerStateHide)
          {
            this.Spawns[index3].Passenger = Passenger.CreatePassenger(new GameManager.GraphicsSystem.Point(passengerPo.X, passengerPo.Y), Game1.GameState.PassengerType[index3], Game1.GameState.FromStation[index3]);
            this.AddSprite(this.Spawns[index3].Passenger, 4);
            this.Spawns[index3].Passenger.SetPos(Game1.GameState.PassengerPos[index3]);
            (this.Spawns[index3].Passenger as Passenger).PassengerType = Game1.GameState.PassengerType[index3];
            (this.Spawns[index3].Passenger as Passenger).SetState(Game1.GameState.PassengerState[index3]);
            (this.Spawns[index3].Passenger as Passenger).StateChangeTime = Game1.GameState.StateChangeTime[index3];
            (this.Spawns[index3].Passenger as Passenger).FromStation = Game1.GameState.FromStation[index3];
            (this.Spawns[index3].Passenger as Passenger).TargetStation = Game1.GameState.TargetStation[index3];
            (this.Spawns[index3].Passenger as Passenger).FloatStart = Game1.GameState.FloatStart[index3];
          }
        }
      }
    }
  }
}
