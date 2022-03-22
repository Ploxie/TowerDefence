using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System;
using TowerDefence.Editor;

namespace TowerDefence
{
    public class Game1 : Game
    {
        public static Random Random = new Random();

        private GraphicsDeviceManager graphicsDevice;
        private SpriteBatch spriteBatch;

        private GameState gameState;
        private IngameState ingameState;

        private Player player;
        //private Level level;
        private LevelEditor editor;

        private HUD hud;

        public static PenumbraComponent Penumbra;


        private Texture2D texture;

        public StartInfo startInfo;

        public Game1(StartInfo startInfo)
        {
            graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            this.startInfo = startInfo;

            

            //Components.Add(penumbra);
            Penumbra = new PenumbraComponent(this);
        }
        
        protected override void LoadContent()
        {
            graphicsDevice.PreferredBackBufferWidth = 1280;
            graphicsDevice.PreferredBackBufferHeight = 768;
            graphicsDevice.ApplyChanges();

            Penumbra.Initialize();
            Penumbra.AmbientColor = new Color(192, 192, 192, 192);            

            spriteBatch = new SpriteBatch(GraphicsDevice);

            /*graphicsDevice.PreferMultiSampling = true;
            graphicsDevice.GraphicsDevice.PresentationParameters.MultiSampleCount = 16;
            graphicsDevice.ApplyChanges();*/

            TextureLoader.Initialize(Content, GraphicsDevice);

            texture = TextureLoader.CreateFilledRectangleTexture(Window.ClientBounds.Width, Window.ClientBounds.Height, Color.Red);

            SpriteFont font = Content.Load<SpriteFont>("font");

            Wave.CreateWaves();

            Player player = new Player()
            {
                Gold = 15,
                Lives = 30
            };

            //level = new Level(LevelData.Load("Level1.lvl"), GraphicsDevice, Window, spriteBatch);
            //level.Player = player;                        

            int shopWidth = 200;
            Rectangle shopBar = new Rectangle(Window.ClientBounds.Width - shopWidth - 5, 5, shopWidth, Window.ClientBounds.Height - 10);
            Rectangle topBar = new Rectangle(10, 5, Window.ClientBounds.Width-20 - shopWidth, 30);
            hud = new HUD(topBar, shopBar, font);
            hud.ShopMenu = new ShopMenu(player,hud, shopBar, font);
            hud.Player = player;
            //hud.Level = level;
            //level.Hud = hud;

            editor = new LevelEditor();
            //editor.CreateLevel(40, 24, 64, "road_tiles3");
            //editor.LoadLevel(LevelData.Load("Level1.lvl"));


            ingameState = new IngameState(player, Window, spriteBatch)
            {
                Hud = hud
            };
            

            if(startInfo.StartEditor)
            {
                this.gameState = editor;
            }
            else
            {
                this.gameState = ingameState;
            }

        }

        protected override void Update(GameTime gameTime)
        {
            Input.PreUpdate();

            gameState.Update(gameTime);
                                    
            Input.PostUpdate();            
        }

        protected override void Draw(GameTime gameTime)
        {

           

            gameState.Draw(gameTime, spriteBatch);

        }
    }
}
