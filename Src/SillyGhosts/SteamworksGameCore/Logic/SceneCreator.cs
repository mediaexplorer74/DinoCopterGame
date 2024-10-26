// Steamworks.Games.Game.Core.Logic.SceneCreator

using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Controller;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Scenes.Game;
using Steamworks.Games.Game.Core.Scenes.Menu;
using Steamworks.Games.Game.Core.Sprites;
using Steamworks.Physics.Collisions;
using Steamworks.Physics.Particles;
using System.Collections.Generic;


namespace Steamworks.Games.Game.Core.Logic
{
  public class SceneCreator
  {
    public GameProgress CurrentGameProgress;
    public float PassangerHeight;
    public EngineBase Context;

    internal Scene CreateGameScene(CaveLevel level, GameDifficulty CurrentDifficulty)
    {
      foreach (Cave cave in level.Caves)
      {
        cave.PassangerHeight = this.PassangerHeight;
        cave.WaterLevel = level.CaveMap.height * level.CaveMap.tileheight 
                    - level.CaveMap.tileheight / 4;
      }
      GameScene scene = new GameScene(this.Context, level, this.CurrentGameProgress, CurrentDifficulty);

      scene.Map = (ILayeredEntity) new TMXMapEntity(level.CaveMap, 
          this.Context.ResourceManagers.CurrentTextureManager);
      this.CreateTimerText(scene);
      this.CreateCabSprite(scene);
      this.CreatePointerSprites(scene);
      this.CreateSignSprites(scene);
      this.CreateFog(scene, level);
      this.CreateBackground(scene, level);
      this.CreateJoystickSprite(scene);
      this.CreateProgressBarInScene(scene);
      this.CreateParticles(level, scene);
      this.CreateMenuButton(scene);
      scene.Camera = (ICamera2D) new Camera2D(new RectangleF(
          0.0f, 0.0f, this.Context.ScreenWidth, this.Context.ScreenHeight), 
          level.CaveMap.GetMapBounds());
      scene.FirstUpdate();
      return (Scene) scene;
    }

    private void CreateMenuButton(GameScene scene)
    {
      scene.Button_Menu = this.Context.SpriteFactory.GetButton("menu");
      scene.Button_Menu.X = this.Context.ScreenWidth - 42f;
      scene.Button_Menu.Y = 10f;
      scene.Button_Restart = this.Context.SpriteFactory.GetButton("restart2");
      scene.Button_Restart.X = this.Context.ScreenWidth - 84f;
      scene.Button_Restart.Y = 10f;
      scene.Button_Mute = this.Context.SpriteFactory.GetToggleButton("mute");
      scene.Button_Mute.X = this.Context.ScreenWidth - 126f;
      scene.Button_Mute.Y = 10f;
      scene.Button_Mute.IsToggled = !this.Context.ResourceManagers.CurrentSoundManager.Muted;
      scene.AddButton(scene.Button_Menu);
      scene.AddButton(scene.Button_Restart);
      scene.AddButton((Button) scene.Button_Mute);
    }

    private void CreateParticles(CaveLevel level, GameScene scene)
    {
      if (!level.Rain && !level.Wind && (!level.Ice || !level.Rain))
        return;
      if (level.Ice)
        scene.Particles = (ParticleSystem) new SnowParticleSystem(
            (this.Context.SpriteFactory.Get("snow") as Sprite).SpriteTextureInfo, 100);
      else if (level.Rain)
        scene.Particles = (ParticleSystem) new RainParticleSystem(
            (this.Context.SpriteFactory.Get("rain") as Sprite).SpriteTextureInfo, 1000);
      else if (level.Wind)
        scene.Particles = (ParticleSystem) new WindParticleSystem(
            (this.Context.SpriteFactory.Get("wind") as Sprite).SpriteTextureInfo, 10);

      if (level.Rain)
      {
        scene.Particles.Y = -64f;
        scene.Particles.X = -this.Context.ScreenWidth;
        scene.Particles.Width = this.Context.ScreenWidth * 2f;
        scene.Particles.Height = 32f;
        if (level.Wind)
          scene.Particles.Angle = 0.7853982f;
      }
      else
      {
        scene.Particles.X = -64f;
        scene.Particles.Y = 0.0f;
        scene.Particles.Width = 32f;
        scene.Particles.Height = this.Context.ScreenHeight;
        scene.Particles.Angle = 0.0f;
      }
      scene.Particles.Precompute(20f, 0.1f);
    }

    private void CreateBackground(GameScene scene, CaveLevel level)
    {
      float width = (float) (level.CaveMap.tilewidth * level.CaveMap.width);
      float height = (float) (level.CaveMap.tileheight * level.CaveMap.height);
      scene.Background_Close = (IEntity) new BackgroundNear(this.Context, width, height, 0.6f);
      scene.Background_Far = this.Context.SpriteFactory.Get("background_far");
      scene.Background_Close.Y = 100f;
      scene.Background_Far.Y = 100f;
    }

    private void CreateCabSprite(GameScene scene)
    {
      scene.Cab = this.Context.SpriteFactory.GetAnimated("cab");
      scene.Cab.X = 0.0f;
      scene.Cab.Y = 0.0f;
      scene.Cab.Width = 64f;
      scene.Cab.Height = 64f;
      scene.Cab.PlayLoop("idle");
    }

    private void CreateFog(GameScene scene, CaveLevel level)
    {
      scene.Bottom_Fog_Far = (IEntity) new Entity();
      for (int index = 0; index < level.CaveMap.width + 1; ++index)
      {
        IEntity child = this.Context.SpriteFactory.Get("bottom_fog_1");
        child.Alpha = 0.5f;
        child.X = (float) (index * level.CaveMap.tilewidth - 24);
        child.Y = (float) ((level.CaveMap.height - 1) * level.CaveMap.tileheight);
        scene.Bottom_Fog_Far.AttachChild(child);
      }
      scene.Bottom_Fog_Near1 = (IEntity) new Entity();
      for (int index = 0; index < level.CaveMap.width + 1; ++index)
      {
        IEntity child = this.Context.SpriteFactory.Get("bottom_fog_1");
        child.Alpha = 0.5f;
        child.X = (float) (index * level.CaveMap.tilewidth - 24);
        child.Y = (float) ((level.CaveMap.height - 1) * level.CaveMap.tileheight);
        scene.Bottom_Fog_Near1.AttachChild(child);
      }
    }

    private void CreateJoystickSprite(GameScene scene)
    {
      scene.Joystick = (IAnalogController) new Joystick(
          this.Context.ResourceManagers.CurrentTextureManager.Get("joystick"), 
          this.Context.TouchSource);

      scene.Joystick.X = 544f;
      scene.Joystick.Y = 224f;
      scene.Joystick.Width = 256f;
      scene.Joystick.Height = 256f;
    }

    private void CreatePointerSprites(GameScene scene)
    {
      scene.PointerFull = this.Context.SpriteFactory.Get("pointer");
      scene.PointerFull.Alpha = 0.0f;
      scene.PointerEmpty = this.Context.SpriteFactory.Get("pointer2");
      scene.PointerEmpty.Alpha = 0.0f;
    }

    private IEntity CreateProgressBackground()
    {
      IEntity progressBackground = this.Context.SpriteFactory.Get("progressBack");
      progressBackground.X = 96f;
      progressBackground.Y = 25f;
      progressBackground.Alpha = 0.0f;
      return progressBackground;
    }

    private void CreateProgressBarInScene(GameScene scene)
    {
      scene.CurrentProgressBar = this.CreateProgressBar();
      scene.ProgressBarBack = this.CreateProgressBackground();
    }

    private ProgressBar CreateProgressBar()
    {
      ProgressBar progressBar = this.Context.SpriteFactory.GetProgressBar("progressFront");
      progressBar.Reverse = true;
      progressBar.X = 96f;
      progressBar.Y = 25f;
      progressBar.Alpha = 0.0f;
      return progressBar;
    }

    private void CreateSignSprites(GameScene scene)
    {
      scene.CaveSignSprites = new List<IEntity>();
      for (int index = 0; index < 5; ++index)
      {
        IEntity entity = this.Context.SpriteFactory.Get("cavesign" + index.ToString());
        entity.Alpha = 0.0f;
        scene.CaveSignSprites.Add(entity);
      }
    }

    private void CreateTimerText(GameScene scene)
    {
      scene.TimerText = this.Context.SpriteFactory.GetText("", "smallfont");
      scene.TimerText.X = 700f;
    }

    public DifficultyMenuScene CreateDifficultyMenuScene() => new DifficultyMenuScene(this.Context);

    public Scene CreateLevelsScene(int levelCount, GameDifficulty CurrentDifficulty)
    {
      return (Scene) new LevelsScene(this.Context, this.CurrentGameProgress, 
          (int) CurrentDifficulty.Name, levelCount);
    }

    public MainMenuScene CreateMainMenuScene()
    {
      return new MainMenuScene(this.Context, this.CurrentGameProgress);
    }

    public Scene CreateTutorialScene(bool FromStart)
    {
      return (Scene) new TutorialScene(this.Context, this.CurrentGameProgress);
    }

    internal Scene CreateCreditsScene()
    {
        return (Scene)new CreditsScene(this.Context);
    }
  }
}
