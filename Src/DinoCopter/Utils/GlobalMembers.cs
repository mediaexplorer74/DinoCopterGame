// GameManager.Utils.GlobalMembers

using GameManager.GameElements;
using GameManager.GameLogic;
using GameManager.GraphicsSystem;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

#nullable disable
namespace GameManager.Utils
{
  public static class GlobalMembers
  {
    public const double Me = 2.7182818284590451;
    public const double MLog2E = 1.4426950408889634;
    public const double MLog10E = 0.43429448190325182;
    public const double MLn2 = 0.69314718055994529;
    public const double MLn10 = 2.3025850929940459;
    public const double MPi = 3.1415926535897931;
    public const double MPi2 = 1.5707963267948966;
    public const double MPi4 = 0.78539816339744828;
    public const double M1Pi = 0.31830988618379069;
    public const double M2Pi = 0.63661977236758138;
    public const double M2Sqrtpi = 1.1283791670955126;
    public const double MSqrt2 = 1.4142135623730951;
    public const double MSqrt12 = 0.70710678118654757;
    public const int BmfCharId = 0;
    public const int BmfCharX = 1;
    public const int BmfCharY = 2;
    public const int BmfCharWidth = 3;
    public const int BmfCharHeight = 4;
    public const int BmfCharXoffset = 5;
    public const int BmfCharYoffset = 6;
    public const int BmfCharXadvance = 7;
    public const int BmfCharPage = 8;
    public const int BmfCharChnl = 9;
    public const float ChaserCautionTime = 3f;
    public const float ChaserInjuredTime = 10f;
    public const float ChaserChaseSpeed = 3f;
    public const float PushForce = 20f;
    public const int RandMax = 32767;
    public const int PressEventsNum = 100;
    public const int EventPress = 0;
    public const int EventMove = 1;
    public const int EventRelease = 2;
    public const float UpdateStep = 0.01f;
    public const int ClipStackSize = 100;
    public const int AdsNone = 0;
    public const int AdsAdmob = 1;
    public const int AdsInneractive = 2;
    public const float DustAngleChangeDelay = 2f;
    public const float DustSpeed = 0.5f;
    public const float DustAngleChangeVariation = 1f;
    public const float DustAngleVariation = 3.14159274f;
    public const float DustSpawnDelay = 3f;
    public const float DustSpawnVariation = 1f;
    public const int TilesBlockRow = 16;
    public const float LightSize = 3f;
    public const float LightSizeVariety = 3f;
    public const float LightSizeDelay = 4.5f;
    public const float LightSizeDelayVariety = 1.5f;
    public const int GameLayerBg = 0;
    public const int GameLayerTerrain = 1;
    public const int GameLayerStone = 2;
    public const int GameLayerEnemis = 3;
    public const int GameLayerPassenger = 4;
    public const int GameLayerPlayer = 5;
    public const int GameLayerFx = 6;
    public const int GameLayerWind = 7;
    public const int GameLayers = 8;
    public const int MaxSpawnsNum = 10;
    public const int StationsAnimNum = 6;
    public const int FoodNum = 5;
    public const int WaterFramesNum = 2;
    public const float EnergyDegradation = 0.0045f;
    public const float EnergyFruitBonus = 0.25f;
    public const float WindForce = 5.5f;
    public const int PassengersNum = 3;
    public const int DeathReasonHit = 0;
    public const int DeathReasonEnergyEnd = 1;
    public const int DeathReasonPassengerDrown = 2;
    public const int DeathReasonPterodactylHit = 3;
    public const int TutorialControls = 0;
    public const int TutorialTransport = 1;
    public const int TutorialCrash = 2;
    public const int TutorialEnergy = 3;
    public const int TutorialPterodactyl = 4;
    public const int TutorialTriceratops = 5;
    public const int TutorialSleeper = 6;
    public const int TutorialSink = 7;
    public const int TutorialNum = 8;
    public const int ScrollSpeed = 80;
    public const float MenuScrollTime = 0.5f;
    public const float MenuVdelay = 3f;
    public const int MenuTypeTextDisp = 0;
    public const int MenuTypeDebugLevelSelect = 1;
    public const int MenuTypeSplash = 6;
    public const int MenuTypeLogo = 7;
    public const int MenuTypeLite = 8;
    public const int MenuTypeAds = 9;
    public const int MenuTypeRateThisApp = 10;
    public const int MenuTypeMainMenu = 11;
    public const int MenuTypeOptions = 12;
    public const int MenuTypeLevelSelect = 2;
    public const int MenuTypePause = 3;
    public const int MenuTypeRetry = 4;
    public const int MenuTypeWin = 5;
    public const int MenuTypeTutorial = 13;
    public const int MenuTypeTutorialHelp = 14;
    public const int MenuTypeBuy = 15;
    public const int MenuTypeBuyFinished = 16;
    public const int MenuTypeTapjoy = 17;
    public const int MenuTypeBuyMoreLevels = 18;
    public const int MenuTypeLogo2 = 112;
    public const string PRODUCT_ID_LEVELS_21_TO_30 = "DC_levels_21_to_30";
    public const float SplashShowTime = 3f;
    public const float SplashFadeTime = 1.5f;
    public const float PassengerShowTime = 1f;
    public const float PassengerHideTime = 1f;
    public const float PassengerSinkTime = 15f;
    public const float PassengerTellStation = 3f;
    public const float PassengerMoveSpeed = 2f;
    public const float PassengerUnderWaterFloatPart = 0.5f;
    public const float DieSpeed = 5f;
    public const float WaterForce = 2000f;
    public const float WhirlDuration = 30000f;
    public const float CopterDuration = 1900f;
    public const float PterodactylMoveSpeed = 2f;
    public const float SpawnDelay = 15f;
    public const float SpawnNumDelay = 5f;
    public const int SpriteFx = 0;
    public const int SpritePlayer = 1;
    public const int SpritePassenger = 2;
    public const int SpriteTriceratops = 3;
    public const int SpritePterodactyl = 4;
    public const int SpriteSleeper = 5;
    public const int SpriteStone = 6;
    public const int SpriteTree = 7;
    public const int SpriteFruit = 8;
    public const int SpriteDustSpawner = 9;
    public const int SpriteLava = 10;
    public const int SpriteWindLeft = 100;
    public const int SpriteWindRight = 101;
    public const int SpriteBg = -1;
    public const int SpriteTerrain = -2;
    public const int SpriteMiLabel = -10001;
    public const int SpriteMiScrollable = -10002;
    public const int SpriteMiButton = -10003;
    public const int SpriteMiSlide = -10004;
    public const int CollisionNone = 0;
    public const int CollisionRight = 1;
    public const int CollisionDown = 2;
    public const int CollisionLeft = 4;
    public const int CollisionUp = 8;
    public const int MaxCollisions = 100;
    public const int LevelsNum = 30;
    public const float SleeperBreathPhase = 3f;
    public const float TreeRegrowTime = 12f;
    public static float ScreenWidth;
    public static float ScreenHeight;
    public static DispManager Manager;
    public static int[,] PressEvents = new int[100, 4];
    public static int CurrentPressEvent = 0;
    public static bool WasAddShow = false;
    public static bool IsSingleTouchDown = false;
    public static int OldBlitDirection;
    public static GlobalMembers.Shape[] Shapes = new GlobalMembers.Shape[256]
    {
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapePlatform,
      GlobalMembers.Shape.ShapePlatform,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeNone,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect,
      GlobalMembers.Shape.ShapeFullRect
    };
    public static Game Game;
    public static GameSave Save;
    public static string[] TutorialImgs = new string[8]
    {
      "tutorial_1",
      "tutorial_2",
      "tutorial_3",
      "tutorial_4",
      "tutorial_5",
      "tutorial_6",
      "tutorial_7",
      "tutorial_8"
    };
    public static float MENU_SCROLL_TIME = 0.5f;
    public static float MENU_VDELAY = 3f;
    public static string LinkFacebook = "http://www.facebook.com/pages/Ferns-Blossom/221608111245731";
    public static string LinkFunappMarketplace = "FunApp sp. z o.o.";
    public static string LinkBuy = "http://windowsphone.com/s?appid=c204afde-3111-4873-abcd-cf939cc9793f";
    public static string LinkRate = "http://windowsphone.com/s?appid=c204afde-3111-4873-abcd-cf939cc9793f";
    public static int ItemsLayer = 0;
    public static ResManLoader ResManLoader;
    public static Dictionary<Rh, string> Images = new Dictionary<Rh, string>()
    {
      {
        Rh.ananas,
        "ananas"
      },
      {
        Rh.arbuz,
        "arbuz"
      },
      {
        Rh.arrow,
        "arrow"
      },
      {
        Rh.arrow_select,
        "arrow_select"
      },
      {
        Rh.background,
        "background"
      },
      {
        Rh.backgroundPion,
        "backgroundPion"
      },
      {
        Rh.banan,
        "banan"
      },
      {
        Rh.bg,
        "bg"
      },
      {
        Rh.btn_big_select,
        "btn_big_select"
      },
      {
        Rh.btn_big_unselect,
        "btn_big_unselect"
      },
      {
        Rh.btn_medium_select,
        "btn_medium_select"
      },
      {
        Rh.btn_medium_unselect,
        "btn_medium_unselect"
      },
      {
        Rh.button,
        "button"
      },
      {
        Rh.button_select,
        "button_select"
      },
      {
        Rh.buy_icon,
        "buy_icon"
      },
      {
        Rh.copter,
        "copter"
      },
      {
        Rh.dino1,
        "dino1"
      },
      {
        Rh.dust,
        "dust"
      },
      {
        Rh.dymek,
        "dymek"
      },
      {
        Rh.eat_1,
        "eat_1"
      },
      {
        Rh.eat_2,
        "eat_2"
      },
      {
        Rh.eat_3,
        "eat_3"
      },
      {
        Rh.eat_4,
        "eat_4"
      },
      {
        Rh.facebook,
        "facebook"
      },
      {
        Rh.facebook_icon,
        "facebook_icon"
      },
      {
        Rh.fly_arrow,
        "fly_arrow"
      },
      {
        Rh.funapp_logo,
        "funapp_logo"
      },
      {
        Rh.funapp_icon,
        "funapp_icon"
      },
      {
        Rh.game_logo,
        "game_logo"
      },
      {
        Rh.i,
        "i"
      },
      {
        Rh.ii,
        "ii"
      },
      {
        Rh.iii,
        "iii"
      },
      {
        Rh.Image1,
        "Image1"
      },
      {
        Rh.indicator_1,
        "indicator_1"
      },
      {
        Rh.indicator_2,
        "indicator_2"
      },
      {
        Rh.indicator_3,
        "indicator_3"
      },
      {
        Rh.injured_1,
        "injured_1"
      },
      {
        Rh.injured_2,
        "injured_2"
      },
      {
        Rh.IV,
        "IV"
      },
      {
        Rh.jablko,
        "jablko"
      },
      {
        Rh.joy_galka,
        "joy_galka"
      },
      {
        Rh.level_complete_sign,
        "level_complete_sign"
      },
      {
        Rh.level_inactive_icon,
        "level_inactive_icon"
      },
      {
        Rh.level_select_button_select,
        "level_select_button_select"
      },
      {
        Rh.level_select_button_unselect,
        "level_select_button_unselect"
      },
      {
        Rh.little_stars_31,
        "little_stars_31"
      },
      {
        Rh.little_stars_32,
        "little_stars_32"
      },
      {
        Rh.little_stars_33,
        "little_stars_33"
      },
      {
        Rh.loading,
        "loading"
      },
      {
        Rh.logo_color_anim_glow,
        "logo_color_anim_glow"
      },
      {
        Rh.logo_color_anim_logo,
        "logo_color_anim_logo"
      },
      {
        Rh.look,
        "look"
      },
      {
        Rh.lose_skull,
        "lose_skull"
      },
      {
        Rh.ludzik,
        "ludzik"
      },
      {
        Rh.miecho,
        "miecho"
      },
      {
        Rh.music_frame,
        "music_frame"
      },
      {
        Rh.openfaint_icon,
        "openfaint_icon"
      },
      {
        Rh.passanger2_sink_1,
        "passanger2_sink_1"
      },
      {
        Rh.passanger2_sink_2,
        "passanger2_sink_2"
      },
      {
        Rh.passanger2_sink_3,
        "passanger2_sink_3"
      },
      {
        Rh.passanger2_speak_1,
        "passanger2_speak_1"
      },
      {
        Rh.passanger2_speak_2,
        "passanger2_speak_2"
      },
      {
        Rh.passanger2_speak_3,
        "passanger2_speak_3"
      },
      {
        Rh.passanger2_stand_1,
        "passanger2_stand_1"
      },
      {
        Rh.passanger2_stand_2,
        "passanger2_stand_2"
      },
      {
        Rh.passanger2_stand_3,
        "passanger2_stand_3"
      },
      {
        Rh.passanger2_swim_1,
        "passanger2_swim_1"
      },
      {
        Rh.passanger2_swim_2,
        "passanger2_swim_2"
      },
      {
        Rh.passanger2_swim_3,
        "passanger2_swim_3"
      },
      {
        Rh.passanger2_swim_4,
        "passanger2_swim_4"
      },
      {
        Rh.passanger2_swim_5,
        "passanger2_swim_5"
      },
      {
        Rh.passanger2_walk_1,
        "passanger2_walk_1"
      },
      {
        Rh.passanger2_walk_2,
        "passanger2_walk_2"
      },
      {
        Rh.passanger2_walk_3,
        "passanger2_walk_3"
      },
      {
        Rh.passanger2_walk_4,
        "passanger2_walk_4"
      },
      {
        Rh.passanger2_walk_5,
        "passanger2_walk_5"
      },
      {
        Rh.passanger2_walk_6,
        "passanger2_walk_6"
      },
      {
        Rh.passanger2_walk_7,
        "passanger2_walk_7"
      },
      {
        Rh.passanger2_walk_8,
        "passanger2_walk_8"
      },
      {
        Rh.passanger3_sink_1,
        "passanger3_sink_1"
      },
      {
        Rh.passanger3_sink_2,
        "passanger3_sink_2"
      },
      {
        Rh.passanger3_sink_3,
        "passanger3_sink_3"
      },
      {
        Rh.passanger3_speak_1,
        "passanger3_speak_1"
      },
      {
        Rh.passanger3_speak_2,
        "passanger3_speak_2"
      },
      {
        Rh.passanger3_speak_3,
        "passanger3_speak_3"
      },
      {
        Rh.passanger3_stand_1,
        "passanger3_stand_1"
      },
      {
        Rh.passanger3_stand_2,
        "passanger3_stand_2"
      },
      {
        Rh.passanger3_stand_3,
        "passanger3_stand_3"
      },
      {
        Rh.passanger3_swim_1,
        "passanger3_swim_1"
      },
      {
        Rh.passanger3_swim_2,
        "passanger3_swim_2"
      },
      {
        Rh.passanger3_swim_3,
        "passanger3_swim_3"
      },
      {
        Rh.passanger3_swim_4,
        "passanger3_swim_4"
      },
      {
        Rh.passanger3_swim_5,
        "passanger3_swim_5"
      },
      {
        Rh.passanger3_walk_1,
        "passanger3_walk_1"
      },
      {
        Rh.passanger3_walk_2,
        "passanger3_walk_2"
      },
      {
        Rh.passanger3_walk_3,
        "passanger3_walk_3"
      },
      {
        Rh.passanger3_walk_4,
        "passanger3_walk_4"
      },
      {
        Rh.passanger3_walk_5,
        "passanger3_walk_5"
      },
      {
        Rh.passanger3_walk_6,
        "passanger3_walk_6"
      },
      {
        Rh.passanger3_walk_7,
        "passanger3_walk_7"
      },
      {
        Rh.passanger3_walk_8,
        "passanger3_walk_8"
      },
      {
        Rh.passenger1_sink_1,
        "passenger1_sink_1"
      },
      {
        Rh.passenger1_sink_2,
        "passenger1_sink_2"
      },
      {
        Rh.passenger1_sink_3,
        "passenger1_sink_3"
      },
      {
        Rh.passenger1_speak_1,
        "passenger1_speak_1"
      },
      {
        Rh.passenger1_speak_2,
        "passenger1_speak_2"
      },
      {
        Rh.passenger1_speak_3,
        "passenger1_speak_3"
      },
      {
        Rh.passenger1_stand_1,
        "passenger1_stand_1"
      },
      {
        Rh.passenger1_stand_2,
        "passenger1_stand_2"
      },
      {
        Rh.passenger1_stand_3,
        "passenger1_stand_3"
      },
      {
        Rh.passenger1_swim_1,
        "passenger1_swim_1"
      },
      {
        Rh.passenger1_swim_2,
        "passenger1_swim_2"
      },
      {
        Rh.passenger1_swim_3,
        "passenger1_swim_3"
      },
      {
        Rh.passenger1_swim_4,
        "passenger1_swim_4"
      },
      {
        Rh.passenger1_swim_5,
        "passenger1_swim_5"
      },
      {
        Rh.passenger1_walk_1,
        "passenger1_walk_1"
      },
      {
        Rh.passenger1_walk_2,
        "passenger1_walk_2"
      },
      {
        Rh.passenger1_walk_3,
        "passenger1_walk_3"
      },
      {
        Rh.passenger1_walk_4,
        "passenger1_walk_4"
      },
      {
        Rh.passenger1_walk_5,
        "passenger1_walk_5"
      },
      {
        Rh.passenger1_walk_6,
        "passenger1_walk_6"
      },
      {
        Rh.passenger1_walk_7,
        "passenger1_walk_7"
      },
      {
        Rh.passenger1_walk_8,
        "passenger1_walk_8"
      },
      {
        Rh.passenger2_sink_1,
        "passenger2_sink_1"
      },
      {
        Rh.passenger2_sink_2,
        "passenger2_sink_2"
      },
      {
        Rh.passenger2_sink_3,
        "passenger2_sink_3"
      },
      {
        Rh.passenger2_speak_1,
        "passenger2_speak_1"
      },
      {
        Rh.passenger2_speak_2,
        "passenger2_speak_2"
      },
      {
        Rh.passenger2_speak_3,
        "passenger2_speak_3"
      },
      {
        Rh.passenger2_stand_1,
        "passenger2_stand_1"
      },
      {
        Rh.passenger2_stand_2,
        "passenger2_stand_2"
      },
      {
        Rh.passenger2_stand_3,
        "passenger2_stand_3"
      },
      {
        Rh.passenger2_swim_1,
        "passenger2_swim_1"
      },
      {
        Rh.passenger2_swim_2,
        "passenger2_swim_2"
      },
      {
        Rh.passenger2_swim_3,
        "passenger2_swim_3"
      },
      {
        Rh.passenger2_swim_4,
        "passenger2_swim_4"
      },
      {
        Rh.passenger2_swim_5,
        "passenger2_swim_5"
      },
      {
        Rh.passenger2_walk_1,
        "passenger2_walk_1"
      },
      {
        Rh.passenger2_walk_2,
        "passenger2_walk_2"
      },
      {
        Rh.passenger2_walk_3,
        "passenger2_walk_3"
      },
      {
        Rh.passenger2_walk_4,
        "passenger2_walk_4"
      },
      {
        Rh.passenger2_walk_5,
        "passenger2_walk_5"
      },
      {
        Rh.passenger2_walk_6,
        "passenger2_walk_6"
      },
      {
        Rh.passenger2_walk_7,
        "passenger2_walk_7"
      },
      {
        Rh.passenger2_walk_8,
        "passenger2_walk_8"
      },
      {
        Rh.passenger3_sink_1,
        "passenger3_sink_1"
      },
      {
        Rh.passenger3_sink_2,
        "passenger3_sink_2"
      },
      {
        Rh.passenger3_sink_3,
        "passenger3_sink_3"
      },
      {
        Rh.passenger3_speak_1,
        "passenger3_speak_1"
      },
      {
        Rh.passenger3_speak_2,
        "passenger3_speak_2"
      },
      {
        Rh.passenger3_speak_3,
        "passenger3_speak_3"
      },
      {
        Rh.passenger3_stand_1,
        "passenger3_stand_1"
      },
      {
        Rh.passenger3_stand_2,
        "passenger3_stand_2"
      },
      {
        Rh.passenger3_stand_3,
        "passenger3_stand_3"
      },
      {
        Rh.passenger3_swim_1,
        "passenger3_swim_1"
      },
      {
        Rh.passenger3_swim_2,
        "passenger3_swim_2"
      },
      {
        Rh.passenger3_swim_3,
        "passenger3_swim_3"
      },
      {
        Rh.passenger3_swim_4,
        "passenger3_swim_4"
      },
      {
        Rh.passenger3_swim_5,
        "passenger3_swim_5"
      },
      {
        Rh.passenger3_walk_1,
        "passenger3_walk_1"
      },
      {
        Rh.passenger3_walk_2,
        "passenger3_walk_2"
      },
      {
        Rh.passenger3_walk_3,
        "passenger3_walk_3"
      },
      {
        Rh.passenger3_walk_4,
        "passenger3_walk_4"
      },
      {
        Rh.passenger3_walk_5,
        "passenger3_walk_5"
      },
      {
        Rh.passenger3_walk_6,
        "passenger3_walk_6"
      },
      {
        Rh.passenger3_walk_7,
        "passenger3_walk_7"
      },
      {
        Rh.passenger3_walk_8,
        "passenger3_walk_8"
      },
      {
        Rh.pause_btn,
        "pause_btn"
      },
      {
        Rh.pause_btn_select,
        "pause_btn_select"
      },
      {
        Rh.platform_arrow,
        "platform_arrow"
      },
      {
        Rh.play_icon,
        "play_icon"
      },
      {
        Rh.prawy_dolny,
        "prawy_dolny"
      },
      {
        Rh.prowadnica,
        "prowadnica"
      },
      {
        Rh.ptero1,
        "ptero1"
      },
      {
        Rh.ptero2,
        "ptero2"
      },
      {
        Rh.ptero3,
        "ptero3"
      },
      {
        Rh.ptero4,
        "ptero4"
      },
      {
        Rh.ptero5,
        "ptero5"
      },
      {
        Rh.ptero6,
        "ptero6"
      },
      {
        Rh.ramka_cala,
        "ramka_cala"
      },
      {
        Rh.ramka_rog,
        "ramka_rog"
      },
      {
        Rh.scroll_down,
        "scroll_down"
      },
      {
        Rh.scroll_mid,
        "scroll_mid"
      },
      {
        Rh.scroll_up,
        "scroll_up"
      },
      {
        Rh.selecton_music_sound,
        "selecton_music_sound"
      },
      {
        Rh.sleep_1,
        "sleep_1"
      },
      {
        Rh.sleep_10,
        "sleep_10"
      },
      {
        Rh.sleep_11,
        "sleep_11"
      },
      {
        Rh.sleep_2,
        "sleep_2"
      },
      {
        Rh.sleep_3,
        "sleep_3"
      },
      {
        Rh.sleep_4,
        "sleep_4"
      },
      {
        Rh.sleep_5,
        "sleep_5"
      },
      {
        Rh.sleep_6,
        "sleep_6"
      },
      {
        Rh.sleep_7,
        "sleep_7"
      },
      {
        Rh.sleep_8,
        "sleep_8"
      },
      {
        Rh.sleep_9,
        "sleep_9"
      },
      {
        Rh.sound_frame,
        "sound_frame"
      },
      {
        Rh.stand,
        "stand"
      },
      {
        Rh.star_active,
        "star_active"
      },
      {
        Rh.star_unactive,
        "star_unactive"
      },
      {
        Rh.stone,
        "stone"
      },
      {
        Rh.stone_down,
        "stone_down"
      },
      {
        Rh.stone_down_select,
        "stone_down_select"
      },
      {
        Rh.stone_up,
        "stone_up"
      },
      {
        Rh.stone_up_select,
        "stone_up_select"
      },
      {
        Rh.stop1,
        "stop1"
      },
      {
        Rh.stop1_small,
        "stop1_small"
      },
      {
        Rh.stop2,
        "stop2"
      },
      {
        Rh.stop2_small,
        "stop2_small"
      },
      {
        Rh.stop3,
        "stop3"
      },
      {
        Rh.stop3_small,
        "stop3_small"
      },
      {
        Rh.stop4,
        "stop4"
      },
      {
        Rh.stop4_small,
        "stop4_small"
      },
      {
        Rh.stop5,
        "stop5"
      },
      {
        Rh.stop5_small,
        "stop5_small"
      },
      {
        Rh.stop6,
        "stop6"
      },
      {
        Rh.stop6_small,
        "stop6_small"
      },
      {
        Rh.suwak,
        "suwak"
      },
      {
        Rh.taxi_counter,
        "taxi_counter"
      },
      {
        Rh.tiles,
        "tiles"
      },
      {
        Rh.tree_1,
        "tree_1"
      },
      {
        Rh.tree_2,
        "tree_2"
      },
      {
        Rh.tree_3,
        "tree_3"
      },
      {
        Rh.tree_4,
        "tree_4"
      },
      {
        Rh.tutorial_1,
        "tutorial_1"
      },
      {
        Rh.tutorial_2,
        "tutorial_2"
      },
      {
        Rh.tutorial_3,
        "tutorial_3"
      },
      {
        Rh.tutorial_4,
        "tutorial_4"
      },
      {
        Rh.tutorial_5,
        "tutorial_5"
      },
      {
        Rh.tutorial_6,
        "tutorial_6"
      },
      {
        Rh.tutorial_7,
        "tutorial_7"
      },
      {
        Rh.tutorial_8,
        "tutorial_8"
      },
      {
        Rh.tutorial_sign,
        "tutorial_sign"
      },
      {
        Rh.up_belka,
        "up_belka"
      },
      {
        Rh.up_wypelnienie,
        "up_wypelnienie"
      },
      {
        Rh.v,
        "v"
      },
      {
        Rh.vi,
        "vi"
      },
      {
        Rh.walk_1,
        "walk_1"
      },
      {
        Rh.walk_2,
        "walk_2"
      },
      {
        Rh.walk_3,
        "walk_3"
      },
      {
        Rh.walk_4,
        "walk_4"
      },
      {
        Rh.walk_5,
        "walk_5"
      },
      {
        Rh.walk_6,
        "walk_6"
      },
      {
        Rh.water_1,
        "water_1"
      },
      {
        Rh.water_2,
        "water_2"
      },
      {
        Rh.whirl,
        "whirl"
      }
    };

    public static bool IsLite = false;
    public static int LiteLevels = 4;
    public static bool HasAds = true;
    public static bool HasCheat = false;
    public static bool IsFreeVersion = false;
    public static Point TileSize;

    public static float[,] starsTimes = new float[30, 2]
    {
      {
        88f,
        84f
      },
      {
        120f,
        110f
      },
      {
        155f,
        145f
      },
      {
        165f,
        145f
      },
      {
        165f,
        145f
      },
      {
        165f,
        145f
      },
      {
        195f,
        180f
      },
      {
        165f,
        145f
      },
      {
        190f,
        170f
      },
      {
        295f,
        280f
      },
      {
        140f,
        125f
      },
      {
        150f,
        130f
      },
      {
        135f,
        125f
      },
      {
        240f,
        220f
      },
      {
        120f,
        100f
      },
      {
        200f,
        180f
      },
      {
        140f,
        120f
      },
      {
        145f,
        135f
      },
      {
        160f,
        145f
      },
      {
        235f,
        215f
      },
      {
        180f,
        165f
      },
      {
        157f,
        137f
      },
      {
        220f,
        190f
      },
      {
        180f,
        160f
      },
      {
        200f,
        180f
      },
      {
        150f,
        120f
      },
      {
        280f,
        (float) byte.MaxValue
      },
      {
        175f,
        155f
      },
      {
        155f,
        138f
      },
      {
        220f,
        180f
      }
    };

    public static SoundEffect SfxButtonHover;
    public static SoundEffectInstance SfxButtonHoverInstance;
    public static SoundEffect SfxButtonRelease;
    public static SoundEffectInstance SfxButtonReleaseInstance;
    public static SoundEffect SfxWin;
    public static SoundEffect SfxLose;
    public static SoundEffect SfxCopterRotor;
    public static SoundEffectInstance SfxCopterRotorInstance;
    public static SoundEffect SfxGroundHit;
    public static SoundEffectInstance SfxGroundHitInstance;
    public static SoundEffect SfxTaxiCall;
    public static SoundEffect SfxSplash;
    public static SoundEffect SfxTreeHit;
    public static SoundEffect SfxPassengerHit;
    public static SoundEffect SfxTriceratopsHit;
    public static SoundEffect[] SfxTriceratopsRoar = new SoundEffect[3];
    public static SoundEffect SfxPterodactylHit;
    public static SoundEffect SfxSnoring;
    public static Song SfxMusic;
    public static List<BMFont> Fonts = new List<BMFont>();
    public static int FontsNum;

    static GlobalMembers() => GlobalMembers.TileSize = new Point(48f, 48f);

    public static float FromPx(float a)
    {
        return a / GlobalMembers.TileSize.X;
    }

    public static float ToPx(float a)
    {
        return a * GlobalMembers.TileSize.X;
    }

    public static float GetTimeForStars(int starsNum, int level)
    {
      if (starsNum == 2)
        return GlobalMembers.starsTimes[level, 0];
      return starsNum == 3 ? GlobalMembers.starsTimes[level, 1] : 0.0f;
    }

    public static bool LeaveZuneOn { get; set; }

    public enum ChaserState
    {
      ChaserStateSleep,
      ChaserStateCaution,
      ChaserStateChase,
      ChaserStateInjured,
    }

    public enum Shape
    {
      ShapeNone,
      ShapeFullRect,
      ShapePlatform,
      ShapeVhalfrect,
    }

    public enum PassengerState
    {
      PassengerStateShow,
      PassengerStateGoToStation,
      PassengerStateWait,
      PassengerStateTellStation,
      PassengerStateGoIn,
      PassengerStateInChopper,
      PassengerStateGoOut,
      PassengerStateHide,
      PassengerStateFall,
      PassengerStateFloat,
      PassengerStateFloatTellStation,
      PassengerStateSwimTo,
      PassengerStateSink,
    }

    public enum StoneState
    {
      StoneStateNormal,
      StoneStateInChopper,
    }

    public enum TreeState
    {
      TreeStateHasFruit,
      TreeStateDontHaveFruit,
    }
  }
}
