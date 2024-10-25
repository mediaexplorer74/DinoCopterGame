// Game1

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using steamworks.games.game.opengl;
using Steamworks.Engine;
using Steamworks.Engine.Common;
using Steamworks.Engine.Controller;
using Steamworks.Engine.Graphics;
using Steamworks.Engine.XNA;
using Steamworks.Games.Game.Core.Interfaces;
using Steamworks.Games.Game.Core.Logic;
using System;

#nullable disable
namespace steamworks.games.game.core
{
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    private float _baseScale = 1f;
    private bool _escWasUp;
    private float _viewPortScale = 1f;
    private SceneCreator Creator;
    private GameDifficulty CurrentDifficulty;
    private XNAEngine Engine;
    private GraphicsDeviceManager graphics;
    private bool IsTrial;
    private SceneManager Manager;
    private SpriteBatch spriteBatch;
    private /*readonly*/ IDataUtils _dataUtils;
    private /*readonly*/ IPersister _persister;
    private bool _enterWasUp;

        /*
    public Game1(IDataUtils dataUtils, IPersister persister)
    {
      this.graphics = new GraphicsDeviceManager((Microsoft.Xna.Framework.Game) this);
      
      this.Window.Title = "Silly Ghosts";

      //RnD
      this.Window.AllowAltF4 = true;
      this.Window.AllowUserResizing = true;
      
      this.Content.RootDirectory = "Content";
      this.TargetElapsedTime = TimeSpan.FromTicks(333333L);
      this.InactiveSleepTime = TimeSpan.FromSeconds(1.0);
      this._dataUtils = dataUtils;
      this._persister = persister;
    }
        */

    private void Window_ClientSizeChanged(object sender, EventArgs e)
    {
      //RnD
      this.ViewportChanged((double) this.Window.ClientBounds.Height, 
          (double) this.Window.ClientBounds.Width);
    }

    public Game1()
    {
        IDataUtils dataUtils = (IDataUtils)new OpenGlDataUtils();
        IPersister persister = (IPersister)new OpenGlPersister();

        this._dataUtils = dataUtils;
        this._persister = persister;

        this.graphics = new GraphicsDeviceManager((Game)this);

        this.Window.Title = "Silly Ghosts";

        //RnD
        this.Window.AllowAltF4 = true;
        this.Window.AllowUserResizing = true;

        this.Content.RootDirectory = "Content";
        this.TargetElapsedTime = TimeSpan.FromTicks(333333L);
        this.InactiveSleepTime = TimeSpan.FromSeconds(1.0);

        //RnD
        //this.graphics.GraphicsProfile = GraphicsProfile.HiDef;
        this.graphics.GraphicsProfile = GraphicsProfile.Reach;

        this.Content.RootDirectory = "Content";

        //this.graphics.PreferredBackBufferWidth = 800;//400;//500;
        //this.graphics.PreferredBackBufferHeight = 1280;//640;//840;


        //RnD
        this.graphics.IsFullScreen = false;//false;
      
    }

    protected override void Initialize()
    {
      this.IsMouseVisible = true;
      this.graphics.PreferredBackBufferHeight = 580;
      this.graphics.PreferredBackBufferWidth = 800;

      //this.graphics.ApplyChanges();
      
      if (this.Engine == null)
      {
        DataLoader Loader = new DataLoader(this._dataUtils);

        this.Engine = new XNAEngine((IDataLoader) Loader, 
            (IGameSettings) new GameSettings(this._persister));
        
        this.Engine.ScreenHeight = 580f;
        this.Engine.ScreenWidth = 800f;
        
        this.Window.ClientSizeChanged 
                    += new EventHandler<EventArgs>(this.Window_ClientSizeChanged);
        //RnD
        this.ViewportChanged((double) this.Window.ClientBounds.Height, 
            (double) this.Window.ClientBounds.Width);
        
        this.Engine.TouchSource = (ITouchSource) new MouseSource();
        this.Engine.IsTrial = this.IsTrial;
        this.Creator = new SceneCreator();
        this.Creator.Context = (EngineBase) this.Engine;
        this.Creator.PassangerHeight = 32f;
        this.Creator.CurrentGameProgress = new GameProgress(this._persister);
        this.Engine.Device = this.GraphicsDevice;
        this.Manager = new SceneManager((EngineBase) this.Engine);
        this.Engine.SceneManager = (ISceneManager) this.Manager;
        this.Manager.CurrentGameProgress = this.Creator.CurrentGameProgress;
        this.Manager.ManagerDataLoader = (ICaveCabDataLoader) Loader;
        this.Manager.Creator = this.Creator;
        this.Manager.SwitchToFirstScene(this.CurrentDifficulty);
      }
      base.Initialize();
    }

    protected override void LoadContent()
    {
      this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
      this.Engine.SpriteBatch = (ISpriteBatch) new XNASpriteBatch(this.spriteBatch);
      this.Engine.LoadXNAResources(this.Content);
      base.LoadContent();
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
    {
      KeyboardState state = Keyboard.GetState();
      if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || this._escWasUp && state.IsKeyDown(Keys.Escape)) && !this.Manager.Back())
        this.Exit();
      this._escWasUp = state.IsKeyUp(Keys.Escape);
      this._enterWasUp = state.IsKeyUp(Keys.Enter);
      base.Update(gameTime);
      
      //RnD
      this.Manager.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
    }

    protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
    {
      this.GraphicsDevice.Clear(
          Microsoft.Xna.Framework.Color.FromNonPremultiplied(66, 32, 140, (int) byte.MaxValue));
      base.Draw(gameTime);

            //RnD
      this.Manager.Draw();
    }

   
    protected override void OnDeactivated(object sender, EventArgs args)
    {
      base.OnDeactivated(sender, args);
    }


    protected override void OnActivated(object sender, EventArgs args)
    {
      base.OnActivated(sender, args);
      this.DetermineIsTrail();
      this.CurrentDifficulty = (GameDifficulty) null;
    }

    
    private void DetermineIsTrail()
    {
    }

    public void ViewportChanged(double height, double width)
    {
      float num = this.Engine.ScreenWidth / this.Engine.ScreenHeight;
      this._viewPortScale = width / height <= (double) num
                ? (float) width / this.Engine.ScreenWidth : (float) height / this.Engine.ScreenHeight;
      this.UpdateScaling();
    }

    public void BaseScaleChanged(float compositionScaleX, float compositionScaleY)
    {
      this._baseScale = Math.Min(compositionScaleX, compositionScaleY);
      this.UpdateScaling();
    }

    private void UpdateScaling()
    {
        this.Engine.Scale = this._viewPortScale * this._baseScale;
    }

  }
}
