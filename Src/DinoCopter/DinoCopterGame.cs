// GameManager.OldGame1 (DinoCopterGame)

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;


#nullable disable
namespace GameManager
{
    public class OldGame1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Vector2 baseScreenSize = new Vector2(800, 600);

        private Matrix globalTransformation;
        int backbufferWidth, backbufferHeight;



        //private CreateDb createdb = new CreateDb();
        private OldGame1.GameState currentGameState;
        private bool paused;
        private bool canInitialize = true;
        private Rectangle pausedRectangle = new Rectangle(0, 0, 1600, 900);
        private Button btnStartGame;
        private Button btnContinue;
        private Button btnExit;
        private Button btnMainMenu;
        private Button btnSave;
        private Button btnLoad;
        private Button btnCreateProfile;
        private Button btnLoadProfile;
        private Song mainMenuSong;
        //private LevelBuilder levelBuilder = new LevelBuilder();
        private int level = 1;


        private static Vector2 playerPos;

        private static OldGame1 instance;

        private static float deltaTime;
        //private character player;
        private bool profileExisting;
        public static Dictionary<int, GameObject> spawnList = new Dictionary<int, GameObject>();
        private static List<GameObject> objectToAdd = new List<GameObject>();
        private static List<GameObject> objectsToRemove = new List<GameObject>();
        private static List<GameObject> gameObjects = new List<GameObject>();

        public OldGame1()
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

        public static List<GameObject> GameObjects
        {
            get => OldGame1.gameObjects;
            set => OldGame1.gameObjects = value;
        }

        /*
        public List<Collider> Colliders
        {
            get
            {
                List<Collider> colliders = new List<Collider>();
                foreach (GameObject gameObject in OldGame1.GameObjects)
                    colliders.Add(gameObject.GetComponent<Collider>());
                return colliders;
            }
        }
        */

        public static OldGame1 Instance
        {
            get
            {
                if (OldGame1.instance == null)
                    OldGame1.instance = new OldGame1();
                return OldGame1.instance;
            }
        }

        public static float DeltaTime => OldGame1.deltaTime;

        public static Vector2 PlayerPos
        {
            get => OldGame1.playerPos;
            set => OldGame1.playerPos = value;
        }

        public static List<GameObject> ObjectsToRemove
        {
            get => OldGame1.objectsToRemove;
            set => OldGame1.objectsToRemove = value;
        }

        public static List<GameObject> ObjectToAdd
        {
            get => OldGame1.objectToAdd;
            set => OldGame1.objectToAdd = value;
        }

        protected override void Initialize()
        {
            //this.createdb.CreateDatabase();
            base.Initialize();
        }

        public void Choselvl()
        {
            /*
            using (Connection connection = new Connection())
            {
                connection.OpenCon();
                this.player = connection.GetAllRows<character>().Last<character>();
                if (this.currentGameState == OldGame1.GameState.Playing)
                {
                    if (this.player.Level == 1)
                        this.levelBuilder.LevelOne();
                    if (this.player.Level == 2)
                        this.levelBuilder.LevelTwo();
                    if (this.player.Level == 3)
                        this.levelBuilder.LevelThree();
                    if (this.player.Level == 4)
                        this.levelBuilder.LevelOne();
                    this.IsMouseVisible = false;
                }
                connection.Dispose();
            }
            */
        }

        protected override void LoadContent()
        {
            this.mainMenuSong = this.Content.Load<Song>("Sounds/music");//("MainMenuSong");

            // Create a new SpriteBatch, which can be used to draw textures.

            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);


            // !
            ScalePresentationArea();



            switch (this.currentGameState)
            {
                case OldGame1.GameState.MainMenu:
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
                case OldGame1.GameState.Playing:
                    if (this.paused)
                        break;
                    using (List<GameObject>.Enumerator enumerator = OldGame1.gameObjects.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                            enumerator.Current.LoadContent(this.Content);
                        break;
                    }
            }
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
            OldGame1.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            /*
            switch (this.currentGameState)
            {

                case OldGame1.GameState.MainMenu:
                    OldGame1.objectsToRemove.AddRange((IEnumerable<GameObject>)OldGame1.gameObjects);
                    OldGame1.spawnList.Clear();
                    foreach (GameObject gameObject in OldGame1.ObjectToAdd)
                    {
                        gameObject.LoadContent(this.Content);
                        OldGame1.gameObjects.Add(gameObject);
                    }
                    OldGame1.objectToAdd.Clear();
                    foreach (GameObject gameObject in OldGame1.objectsToRemove)
                        OldGame1.gameObjects.Remove(gameObject);
                    OldGame1.objectsToRemove.Clear();

                    this.btnStartGame.Update(this.Content, state2);
                    this.btnExit.Update(this.Content, state2);
                    this.btnCreateProfile.Update(this.Content, state2);
                    
                    if (this.btnStartGame.isClicked)
                        this.currentGameState = OldGame1.GameState.Playing;

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

                case OldGame1.GameState.Playing:
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
                        foreach (GameObject gameObject in OldGame1.objectsToRemove)
                            OldGame1.gameObjects.Remove(gameObject);
                        OldGame1.objectsToRemove.Clear();
                        if (state1.IsKeyDown(Keys.Escape) || state1.IsKeyDown(Keys.P))
                            this.paused = true;
                        foreach (GameObject gameObject in OldGame1.objectToAdd)
                            gameObject.LoadContent(this.Content);
                        OldGame1.GameObjects.AddRange((IEnumerable<GameObject>)OldGame1.ObjectToAdd);
                        OldGame1.objectToAdd.Clear();
                        foreach (GameObject gameObject in OldGame1.gameObjects)
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
                            if ((double)OldGame1.playerPos.X > 1550.0)
                            {
                                if ((double)OldGame1.playerPos.Y > 750.0)
                                {
                                    if (this.level == 3)
                                    {
                                        connection.OpenCon();
                                        this.player.Level = 4;
                                        connection.UpdateRow<character>(this.player);
                                        OldGame1.objectsToRemove.AddRange((IEnumerable<GameObject>)OldGame1.gameObjects);
                                        this.level = 4;
                                        this.canInitialize = true;
                                        connection.Dispose();
                                    }
                                }
                            }
                        }
                        using (Connection connection = new Connection())
                        {
                            if ((double)OldGame1.playerPos.X > 1550.0)
                            {
                                if ((double)OldGame1.playerPos.Y > 750.0)
                                {
                                    if (this.level == 2)
                                    {
                                        connection.OpenCon();
                                        this.player.Level = 3;
                                        connection.UpdateRow<character>(this.player);
                                        OldGame1.objectsToRemove.AddRange(
                                            (IEnumerable<GameObject>)OldGame1.gameObjects);
                                        OldGame1.spawnList.Clear();
                                        this.level = 3;
                                        this.canInitialize = true;
                                        connection.Dispose();
                                    }
                                }
                            }
                        }
                        using (Connection connection = new Connection())
                        {
                            if ((double)OldGame1.playerPos.X > 1550.0)
                            {
                                if ((double)OldGame1.playerPos.Y > 750.0)
                                {
                                    if (this.level == 1)
                                    {
                                        connection.OpenCon();
                                        this.player.Level = 2;
                                        connection.UpdateRow<character>(this.player);
                                        OldGame1.objectsToRemove.AddRange(
                                            (IEnumerable<GameObject>)OldGame1.gameObjects);
                                        OldGame1.spawnList.Clear();
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
                            this.currentGameState = OldGame1.GameState.MainMenu;
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
            OldGame1.objectToAdd.Add(go);
        }

        public void RemoveGameObject(GameObject go)
        {
            OldGame1.objectsToRemove.Add(go);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Coral);

            //this.spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, 
                globalTransformation);


            switch (this.currentGameState)
            {
                case OldGame1.GameState.MainMenu:
                    this.spriteBatch.Draw(this.Content.Load<Texture2D>("480x800/background")/*("MainMenu")*/,
                        new Rectangle(0, 0, 1600, 900), Color.White);
                    //this.btnExit.Draw(this.spriteBatch);
                    //this.btnStartGame.Draw(this.spriteBatch);
                    //this.btnCreateProfile.Draw(this.spriteBatch);
                    break;
                case OldGame1.GameState.Playing:
                    if (!this.paused)
                    {
                        foreach (GameObject gameObject in OldGame1.gameObjects)
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
            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        private enum GameState
        {
            MainMenu,
            Playing,
        }
    }
}
