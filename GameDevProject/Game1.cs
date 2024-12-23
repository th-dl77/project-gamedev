using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Input;
using GameDevProject.Map;
using System.Collections;
using GameDevProject.GameStates;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public SpriteFont font;
        public Texture2D buttonTexture;
        public Texture2D mainMenuBackground;
        public Texture2D deathScreenBackground;

        private Texture2D _debugTexture;

        public Camera _camera;
        public string[,] tileMap;
        public const int TILE_SIZE = 32;
        public Texture2D[] tiles;

        public CollisionManager _collisionManager;
        private CollisionLoader collisionLoader;

        public List<IEntity> entities;
        private EnemyFactory enemyFactory;

        public GameStateManager gameStateManager;

        public Player player;

        #region debug
        //private Texture2D _debugTexture;
        #endregion
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            _camera = new Camera(GraphicsDevice.Viewport);
            _collisionManager = new CollisionManager();
            //mapLoader = new TextFileMapLoader();

            IMapReader tilemapReader = new FileTilemapReader();
            TextFileMapLoader loader = new TextFileMapLoader(tilemapReader);
            tileMap = loader.Load("Content/Tilemap.txt");
            collisionLoader = new CollisionLoader(_collisionManager, 32);
            collisionLoader.LoadCollidables(tileMap);
            entities = new List<IEntity>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Font1");
            tiles = new Texture2D[5];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = Content.Load<Texture2D>("tileMapTextures" + (i));
            }

            enemyFactory = new EnemyFactory(Content);
            for (int i = 0; i < 1000; i += 100)
            {
                entities.Add(enemyFactory.CreateEnemy(new Vector2(300 + i, 400 + i)));
            }

            Texture2D spriteSheetTexture = Content.Load<Texture2D>("char_red_1");

            IInputStrategy inputStrategy = new KeyboardInputStrategy();

            player = PlayerFactory.CreatePlayer(inputStrategy, spriteSheetTexture, new Vector2(200, 200));

            buttonTexture = Content.Load<Texture2D>("buttonTemplate");

            mainMenuBackground = Content.Load<Texture2D>("backgroundMenu");
            deathScreenBackground = Content.Load<Texture2D>("deathScreen");
            gameStateManager = new GameStateManager(this);
            player.OnDeath += () => gameStateManager.ChangeGameState(new GameOverState(buttonTexture,font,deathScreenBackground,gameStateManager));

            #region debug 
            _debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            _debugTexture.SetData(new[] { Color.White });
            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gameStateManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //clear screen
            GraphicsDevice.Clear(Color.Gray);
            gameStateManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
