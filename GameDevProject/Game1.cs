using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Map;
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

        public EnemySpawner enemySpawner;

        private IMapLoader mapLoader;

        private Texture2D _debugTexture;

        public Camera _camera;
        public string[,] tileMap;
        public const int TILE_SIZE = 32;
        public Texture2D[] tiles;
        public GameResetHandler gameResetHandler;

        public CollisionManager _collisionManager;
        private CollisionLoader collisionLoader;

        public HealthRenderer healthRenderer;

        public List<IEntity> entities;
        private EnemyFactory enemyFactory;

        public GameStateManager gameStateManager;
        private PlayerFactory playerFactory;

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

            enemySpawner = new EnemySpawner(Content);
            _camera = new Camera(GraphicsDevice.Viewport);
            _collisionManager = new CollisionManager();
            //mapLoader = new TextFileMapLoader();
            IMapReader tilemapReader = new FileTilemapReader();
            mapLoader = new TextFileMapLoader(tilemapReader);
            tileMap = mapLoader.Load("Content/Tilemap.txt");
            collisionLoader = new CollisionLoader(_collisionManager, 32);
            collisionLoader.LoadCollidables(tileMap);
            entities = new List<IEntity>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D heartTextureFull = Content.Load<Texture2D>("heartFull");
            Texture2D heartTextureEmpty = Content.Load<Texture2D>("heartEmpty");

            font = Content.Load<SpriteFont>("Font1");
            tiles = new Texture2D[5];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = Content.Load<Texture2D>("tileMapTextures" + (i));
            }

            entities = enemySpawner.Spawn(1);

            Texture2D spriteSheetTexture = Content.Load<Texture2D>("char_red_1");
            playerFactory = new PlayerFactory();
            player = playerFactory.CreatePlayer(spriteSheetTexture, new Vector2(200, 200));

            buttonTexture = Content.Load<Texture2D>("buttonTemplate");

            mainMenuBackground = Content.Load<Texture2D>("backgroundMenu");
            deathScreenBackground = Content.Load<Texture2D>("deathScreen");
            healthRenderer = new HealthRenderer(heartTextureFull, heartTextureEmpty, new Vector2(10,10));
            gameStateManager = new GameStateManager(this);
            gameResetHandler = new GameResetHandler(this, spriteSheetTexture, mapLoader, _collisionManager, collisionLoader, playerFactory,enemyFactory,buttonTexture,font,deathScreenBackground,gameStateManager, enemySpawner, entities);
            player.OnDeath += () => gameStateManager.ChangeGameState(new GameOverState(buttonTexture,font,deathScreenBackground,gameStateManager,gameResetHandler));

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
