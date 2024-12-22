using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Input;
using GameDevProject.Map;
using System.Runtime.ExceptionServices;
using System.IO;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera _camera;
        private string[,] tileMap;
        private const int MAP_WIDTH = 50;
        private const int TILE_SIZE = 32;
        private const int MAP_HEIGHT = 50;
        private Texture2D[] tiles;



        private CollisionManager _collisionManager;
        private IMapLoader mapLoader;

        private List<IEntity> entities;
        private EnemyFactory enemyFactory;

        private IEntity player;

        #region debug
        //private Texture2D _debugTexture;
        #endregion
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            _camera = new Camera(GraphicsDevice.Viewport);

            mapLoader = new TextFileMapLoader();

            entities = new List<IEntity>();

            _collisionManager = new CollisionManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            mapLoader.Load("Content/Tilemap.txt", out int mapWidth, out int mapHeight);

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

            player = PlayerFactory.CreatePlayer(inputStrategy, spriteSheetTexture, _camera, new Vector2(200, 200));

            #region debug 
            //_debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            //_debugTexture.SetData(new[] { Color.White });
            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update(gameTime, _collisionManager);
            foreach (var entity in entities)
            {
                entity.Update(gameTime, _collisionManager);
            }
            _camera.Update(player.Position, 1600, 1600);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //clear screen
            GraphicsDevice.Clear(Color.Gray);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, _camera.Transform);

            for (int y = 0; y < MAP_HEIGHT; y++)
            {
                for (int x = 0; x < MAP_WIDTH; x++)
                {
                    int tileIndex = int.Parse(tileMap[x, y]);
                    _spriteBatch.Draw(tiles[tileIndex], new Rectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE), Color.White);
                }
            }
            player.Draw(_spriteBatch);
            foreach (var entity in entities)
            {
                entity.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
