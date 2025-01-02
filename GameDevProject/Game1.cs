using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Map;
using GameDevProject.GameStates;
using GameDevProject.Assets;
using GameDevProject.Enemies;
using GameDevProject.PlayerClasses;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public EnemySpawner enemySpawner;

        private IMapLoader mapLoader;

        private Texture2D _debugTexture;

        public Camera _camera;
        public string[,] tileMap;
        public const int TILE_SIZE = 32;
        public Texture2D[] tiles;
        public GameResetHandler gameResetHandler;

        public CollisionManager _collisionManager;
        public CollisionLoader collisionLoader;

        public HealthRenderer healthRenderer;
        public HealthManager healthManager;

        public GameAssets gameAssets;

        public List<IEntity> entities;

        public GameStateManager gameStateManager;
        private PlayerFactory playerFactory;
        private PlayerDeathHandler playerDeathHandler;

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

            gameAssets = new GameAssets(GraphicsDevice);
            gameAssets.LoadContent(Content);

            _camera = new Camera(GraphicsDevice.Viewport);
            _collisionManager = new CollisionManager();

            IMapReader tilemapReader = new FileTilemapReader();
            mapLoader = new TextFileMapLoader(tilemapReader);

            collisionLoader = new CollisionLoader(_collisionManager, 32);
            entities = new List<IEntity>();

            gameStateManager = new GameStateManager(this, gameAssets);
            healthRenderer = new HealthRenderer(gameAssets, new Vector2(600, 10));

            healthManager = new HealthManager(5);

            playerFactory = new PlayerFactory();
            player = playerFactory.CreatePlayer(gameAssets.GetTexture("player"), new Vector2(800, 800));
            gameResetHandler = new GameResetHandler(this, gameAssets, mapLoader, _collisionManager, collisionLoader, playerFactory, gameStateManager, enemySpawner, entities);
            playerDeathHandler = new PlayerDeathHandler(gameStateManager, gameResetHandler, gameAssets, player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            tiles = new Texture2D[5];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = Content.Load<Texture2D>("tileMapTextures" + (i));
            }
            tileMap = mapLoader.Load("Content/Tilemap.txt");
            collisionLoader.LoadCollidables(tileMap);

            playerDeathHandler.HandleDeath();

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
