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
        private const int TILE_SIZE = 32;
        private Texture2D[] tiles;



        private CollisionManager _collisionManager;
        private CollisionLoader collisionLoader;
        private IMapLoader mapLoader;

        private List<IEntity> entities;
        private EnemyFactory enemyFactory;

        private Player player;

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
                entity.Update(gameTime, player);
            }
            _camera.Update(player.Position, 1600, 1600);
            if (player.IsHitting)
            {
                Rectangle swordHitbox = player.GetSwordHitbox();

                foreach (var entity in entities)
                {
                    if (swordHitbox.Intersects(entity.Bounds))
                    {
                        entity.Die(gameTime); // Apply damage or trigger other effects
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //clear screen
            GraphicsDevice.Clear(Color.Gray);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, _camera.Transform);

            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    string tileValue = tileMap[x, y];
                    int tileIndex = int.Parse(tileValue);

                    if (tileIndex >= 0 && tileIndex < tiles.Length)
                    {
                        _spriteBatch.Draw(tiles[tileIndex], new Rectangle(x*TILE_SIZE,y*TILE_SIZE,TILE_SIZE,TILE_SIZE), Color.White);
                    }
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
