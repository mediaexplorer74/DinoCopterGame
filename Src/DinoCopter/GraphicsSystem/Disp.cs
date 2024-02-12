// GameManager.GraphicsSystem.Disp

using GameManager.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class Disp : Game
  {
    protected SpriteGrid[] Grids;
    public Disp Ref;

    public Point Translate { get; protected set; }

    public float Width { get; protected set; }

    public float Height { get; protected set; }

    public float GameTime { get; protected set; }

    public int LayersNum { get; protected set; }

    public List<Sprite>[] Sprites { get; protected set; }

    public Point ViewPos { get; protected set; }

    public Point ViewSize { get; protected set; }

    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    Vector2 baseScreenSize = new Vector2(800, 600);

    private Matrix globalTransformation;
    int backbufferWidth, backbufferHeight;

        public Disp()
        {
            this.graphics = new GraphicsDeviceManager((Game)this);
            this.Content.RootDirectory = "Content";

#if WINDOWS_PHONE
            TargetElapsedTime = TimeSpan.FromTicks(333333);
#endif
            graphics.IsFullScreen = false; //set true for W10M


            //this.graphics.HardwareModeSwitch = true;

            this.graphics.PreferredBackBufferWidth = /*1600*/800;
            this.graphics.PreferredBackBufferHeight = /*900*/600;

            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft
               | DisplayOrientation.LandscapeRight;

        }
        
       
    protected Disp(int layersNum)
    {
            this.graphics = new GraphicsDeviceManager((Game)this);
            this.Content.RootDirectory = "Content";

#if WINDOWS_PHONE
            TargetElapsedTime = TimeSpan.FromTicks(333333);
#endif
            graphics.IsFullScreen = false; //set true for W10M


            //this.graphics.HardwareModeSwitch = true;

            this.graphics.PreferredBackBufferWidth = /*1600*/800;
            this.graphics.PreferredBackBufferHeight = /*900*/600;

            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft
               | DisplayOrientation.LandscapeRight;



            this.Translate = new Point();
      this.GameTime = 0.0f;
      this.LayersNum = layersNum;
      this.Sprites = new List<Sprite>[layersNum];
      for (int index = 0; index < this.Sprites.Length; ++index)
        this.Sprites[index] = new List<Sprite>();

            this.Width = 800;//(float) GlobalMembers.Manager.GetWidth();
            this.Height = 480;// (float) GlobalMembers.Manager.GetHeight();
      
      this.Grids = new SpriteGrid[layersNum];
      for (int index = 0; index < layersNum; ++index)
        this.Grids[index] = (SpriteGrid) null;
      this.ViewSize = new Point(GlobalMembers.FromPx(this.Width), GlobalMembers.FromPx(this.Height));
    }

        protected override void LoadContent()
        {
            //this.mainMenuSong = this.Content.Load<Song>("Sounds/music");//("MainMenuSong");

            // Create a new SpriteBatch, which can be used to draw textures.

            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);


            // !
            ScalePresentationArea();


            /*
            switch (this.currentGameState)
            {
                case Game1.GameState.MainMenu:
                    MediaPlayer.Play(this.mainMenuSong);
                    MediaPlayer.IsRepeating = true;
                    this.IsMouseVisible = true;

                    //this.btnCreateProfile = new Button(this.Content.Load<Texture2D>("CreateProfileOff"),
                    //    new Vector2(100f, 300f), "CreateProfileOff", "CreateProfileOn", 300, 100);

                    //this.btnStartGame = new Button(this.Content.Load<Texture2D>("ContinueOff"),
                    //    new Vector2(100f, 500f), "ContinueOff", "ContinueOn", 200, 100);

                    //this.btnExit = new Button(this.Content.Load<Texture2D>("ExitOff"),
                    //    new Vector2(100f, 700f), "ExitOff", "ExitOn", 200, 100);
                    break;
                case Game1.GameState.Playing:
                    if (this.paused)
                        break;
                    using (List<GameObject>.Enumerator enumerator = Game1.gameObjects.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                            enumerator.Current.LoadContent(this.Content);
                        break;
                    }
            }
            */
        }

        protected override void UnloadContent()
        {
        }


        // ScalePresentationArea
        public void ScalePresentationArea()
        {
            //Work out how much we need to scale our graphics to fill the screen
            backbufferWidth = GraphicsDevice.PresentationParameters.BackBufferWidth - 0;
            backbufferHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

            float horScaling = backbufferWidth / baseScreenSize.X;
            float verScaling = backbufferHeight / baseScreenSize.Y;

            Vector3 screenScalingFactor = new Vector3(horScaling, verScaling, 1);

            globalTransformation = Matrix.CreateScale(screenScalingFactor);

            System.Diagnostics.Debug.WriteLine("Screen Size - Width["
                + GraphicsDevice.PresentationParameters.BackBufferWidth + "] " +
                "Height [" + GraphicsDevice.PresentationParameters.BackBufferHeight + "]");
        }//ScalePresentationArea



        protected override void Update(GameTime gameTime)
        {

            //Confirm the screen has not been resized by the user
            if (backbufferHeight != GraphicsDevice.PresentationParameters.BackBufferHeight ||
                backbufferWidth != GraphicsDevice.PresentationParameters.BackBufferWidth)
            {
                //TODO: fix it!
                //ScalePresentationArea();
            }

            // TODO: Add your update logic here



            KeyboardState state1 = Keyboard.GetState();
            MouseState state2 = Mouse.GetState();
            //Game1.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            /*
            switch (this.currentGameState)
            {

                case Game1.GameState.MainMenu:
                    Game1.objectsToRemove.AddRange((IEnumerable<GameObject>)Game1.gameObjects);
                    Game1.spawnList.Clear();
                    foreach (GameObject gameObject in Game1.ObjectToAdd)
                    {
                        gameObject.LoadContent(this.Content);
                        Game1.gameObjects.Add(gameObject);
                    }
                    Game1.objectToAdd.Clear();
                    foreach (GameObject gameObject in Game1.objectsToRemove)
                        Game1.gameObjects.Remove(gameObject);
                    Game1.objectsToRemove.Clear();

                    this.btnStartGame.Update(this.Content, state2);
                    this.btnExit.Update(this.Content, state2);
                    this.btnCreateProfile.Update(this.Content, state2);
                    
                    if (this.btnStartGame.isClicked)
                        this.currentGameState = Game1.GameState.Playing;

                    if (this.btnExit.isClicked)
                        this.Exit();
                    
                    using (Connection connection = new Connection())
                    {
                        if (this.btnCreateProfile.isClicked)
                        {
                            if (!this.profileExisting)
                            {
                                connection.OpenCon();
                                connection.InsertRow<character>(new character()
                                {
                                    Level = 1,
                                    health = 1,
                                    PetID = 1,
                                    spellID = 1,
                                    name = "ib"
                                });
                                connection.Dispose();
                                this.profileExisting = true;
                                break;
                            }
                            break;
                        }
                        break;
                    }

                case Game1.GameState.Playing:
                    if (!this.paused)
                    {
                        MediaPlayer.Stop();
                        this.profileExisting = false;
                        if (this.canInitialize)
                        {
                            this.Choselvl();
                            this.canInitialize = false;
                        }
                        this.IsMouseVisible = false;
                        foreach (GameObject gameObject in Game1.objectsToRemove)
                            Game1.gameObjects.Remove(gameObject);
                        Game1.objectsToRemove.Clear();
                        if (state1.IsKeyDown(Keys.Escape) || state1.IsKeyDown(Keys.P))
                            this.paused = true;
                        foreach (GameObject gameObject in Game1.objectToAdd)
                            gameObject.LoadContent(this.Content);
                        Game1.GameObjects.AddRange((IEnumerable<GameObject>)Game1.ObjectToAdd);
                        Game1.objectToAdd.Clear();
                        foreach (GameObject gameObject in Game1.gameObjects)
                            gameObject.Update();

                        using (Connection connection = new Connection())
                        {
                            connection.OpenCon();
                            //this.player = connection.GetAllRows<character>().Last<character>();
                            this.player.Level = this.level;
                            connection.Dispose();
                        }

                        using (Connection connection = new Connection())
                        {
                            if ((double)Game1.playerPos.X > 1550.0)
                            {
                                if ((double)Game1.playerPos.Y > 750.0)
                                {
                                    if (this.level == 3)
                                    {
                                        connection.OpenCon();
                                        this.player.Level = 4;
                                        connection.UpdateRow<character>(this.player);
                                        Game1.objectsToRemove.AddRange((IEnumerable<GameObject>)Game1.gameObjects);
                                        this.level = 4;
                                        this.canInitialize = true;
                                        connection.Dispose();
                                    }
                                }
                            }
                        }
                        using (Connection connection = new Connection())
                        {
                            if ((double)Game1.playerPos.X > 1550.0)
                            {
                                if ((double)Game1.playerPos.Y > 750.0)
                                {
                                    if (this.level == 2)
                                    {
                                        connection.OpenCon();
                                        this.player.Level = 3;
                                        connection.UpdateRow<character>(this.player);
                                        Game1.objectsToRemove.AddRange(
                                            (IEnumerable<GameObject>)Game1.gameObjects);
                                        Game1.spawnList.Clear();
                                        this.level = 3;
                                        this.canInitialize = true;
                                        connection.Dispose();
                                    }
                                }
                            }
                        }
                        using (Connection connection = new Connection())
                        {
                            if ((double)Game1.playerPos.X > 1550.0)
                            {
                                if ((double)Game1.playerPos.Y > 750.0)
                                {
                                    if (this.level == 1)
                                    {
                                        connection.OpenCon();
                                        this.player.Level = 2;
                                        connection.UpdateRow<character>(this.player);
                                        Game1.objectsToRemove.AddRange(
                                            (IEnumerable<GameObject>)Game1.gameObjects);
                                        Game1.spawnList.Clear();
                                        this.level = 2;
                                        this.canInitialize = true;
                                        connection.Dispose();
                                    }
                                }
                            }
                        }
                    }
                    if (this.paused)
                    {
                        this.IsMouseVisible = true;
                        //this.btnContinue = new Button(this.Content.Load<Texture2D>("ContinueOff"),
                        //    new Vector2(700f, 300f), "ContinueOff", "ContinueOn", 200, 100);

                        //this.btnSave = new Button(this.Content.Load<Texture2D>("SaveOff"),
                        //    new Vector2(700f, 400f), "SaveOff", "SaveOn", 200, 100);

                        //this.btnMainMenu = new Button(this.Content.Load<Texture2D>("MainMenuOff"),
                        //    new Vector2(700f, 500f), "MainMenuOff", "MainMenuOn", 300, 100);

                        
                        this.btnMainMenu.Update(this.Content, state2);
                        this.btnContinue.Update(this.Content, state2);
                        this.btnSave.Update(this.Content, state2);
                        if (this.btnContinue.isClicked)
                            this.paused = false;
                        if (this.btnMainMenu.isClicked)
                        {
                            this.paused = false;
                            this.canInitialize = true;
                            MediaPlayer.Play(this.mainMenuSong);
                            this.currentGameState = Game1.GameState.MainMenu;
                            break;
                        }
                        
                        break;
                    }
                    break;
            }
            */
            base.Update(gameTime);
        }

        public void AddGameObject(GameObject go)
        {
            //Game1.objectToAdd.Add(go);
        }

        public void RemoveGameObject(GameObject go)
        {
            //Game1.objectsToRemove.Add(go);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Coral);

            //this.spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
                globalTransformation);

            /*
            switch (this.currentGameState)
            {
                case Game1.GameState.MainMenu:
                    this.spriteBatch.Draw(this.Content.Load<Texture2D>("480x800/background"),
                        new Rectangle(0, 0, 1600, 900), Color.White);
                    //this.btnExit.Draw(this.spriteBatch);
                    //this.btnStartGame.Draw(this.spriteBatch);
                    //this.btnCreateProfile.Draw(this.spriteBatch);
                    break;
                case Game1.GameState.Playing:
                    if (!this.paused)
                    {
                        foreach (GameObject gameObject in Game1.gameObjects)
                        {
                            gameObject.Draw(this.spriteBatch);
                        }
                    }
                    if (this.paused)
                    {
                        foreach (GameObject gameObject in OldGame1.gameObjects)
                            gameObject.Draw(this.spriteBatch);
                        this.spriteBatch.Draw(this.Content.Load<Texture2D>("Paused"),
                            this.pausedRectangle, Color.White);

                        //this.btnMainMenu.Draw(this.spriteBatch);
                        //this.btnContinue.Draw(this.spriteBatch);
                        break;
                    }
                    break;
            }
            */
            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        private enum GameState
        {
            MainMenu,
            Playing,
        }




        public void Display()
        {
            GlobalMembers.Manager.SetNextDisp(this.Ref);
        }

        public float GetWidth() => this.Width;

    public float GetHeight() => this.Height;

    public int GetSpriteLayersNum() => this.LayersNum;

    public virtual void PointerPressed(Point pos, int id)
    {
    }

    public virtual void PointerDragged(Point pos, int id)
    {
    }

    public virtual void PointerReleased(Point pos, int id)
    {
    }

    public virtual void OnShow()
    {
    }

    public virtual void OnHide()
    {
    }

    public virtual void Load()
    {
    }

    public void AddSprite(Sprite sprite, int layer)
    {
      this.Sprites[layer].Add(sprite);
      sprite.Parent = this.Ref;
      sprite.Layer = layer;
      sprite.OnAdd();
    }

    public virtual void Update(float time)
    {
      for (int index1 = 0; index1 < this.LayersNum; ++index1)
      {
        for (int index2 = 0; index2 < this.Sprites[index1].Count; ++index2)
          this.Sprites[index1][index2].Update(time);
      }
      this.CleanupRemovedSprites();
      this.GameTime += time;
    }

        public float GetGameTime()
        {
            return this.GameTime;
        }

        public virtual void Render(SpriteBatch spriteBatch)
    {
      Point translate = GlobalMembers.Manager.GetTranslate();
      for (int index = 0; index < this.LayersNum; ++index)
      {
        foreach (Sprite sprite in this.Sprites[index])
        {
          if (Util.RectsOverlaps(GlobalMembers.FromPx(-translate.X), 
              GlobalMembers.FromPx(-translate.Y), GlobalMembers.FromPx(-translate.X) + this.ViewSize.X,
              GlobalMembers.FromPx(-translate.Y) + this.ViewSize.Y, 
              sprite.Pos.X, sprite.Pos.Y, sprite.Pos.X + sprite.GetWidth(), 
              sprite.Pos.Y + sprite.GetHeight()))
            sprite.Render(spriteBatch);
        }
      }
    }

    public Sprite SpriteAt(Point pos)
    {
      for (int index1 = this.LayersNum - 1; index1 >= 0; --index1)
      {
        for (int index2 = 0; index2 < this.Sprites[index1].Count; ++index2)
        {
          Sprite sprite = this.Sprites[index1][index2];
          if ((double) GlobalMembers.FromPx(pos.X) >= (double) sprite.GetPos().X 
          && (double) GlobalMembers.FromPx(pos.X) < (double) sprite.GetPos().X + (double) sprite.GetWidth() 
          && (double) GlobalMembers.FromPx(pos.Y) < (double) sprite.GetPos().Y + (double) sprite.GetHeight() 
          && (double) GlobalMembers.FromPx(pos.Y) >= (double) sprite.GetPos().Y)
            return sprite;
        }
      }
      return new Sprite();
    }

    public void CleanupRemovedSprites()
    {
      for (int index1 = 0; index1 < this.LayersNum; ++index1)
      {
        for (int index2 = 0; index2 < this.Sprites[index1].Count; ++index2)
        {
          if (this.Sprites[index1][index2].IsToRemove())
          {
            this.Sprites[index1][index2].OnRemove();
            this.Sprites[index1][index2] = this.Sprites[index1][this.Sprites[index1].Count - 1];
            this.Sprites[index1].RemoveAt(this.Sprites[index1].Count - 1);
          }
        }
      }
    }

        public SpriteGrid GetGrid(int layer)
        {
            return this.Grids[layer];
        }

        public List<Sprite> GetSpriteLayer(int layer)
        {
            return this.Sprites[layer];
        }
    }
}
