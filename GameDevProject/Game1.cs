﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Input;
using GameDevProject.Map;
using System.Collections;

public enum GameStates { MainMenu, Playing, Gameover}
namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont font;
        private Texture2D buttonTexture;

        private Camera _camera;
        private string[,] tileMap;
        private const int TILE_SIZE = 32;
        private Texture2D[] tiles;

        private CollisionManager _collisionManager;
        private CollisionLoader collisionLoader;

        private List<IEntity> entities;
        private EnemyFactory enemyFactory;

        public GameStates currentGameState;

        private Player player;

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

            buttonTexture = new Texture2D(GraphicsDevice, 1, 1);
            buttonTexture.SetData(new[] { Color.Black });

            #region debug 
            //_debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            //_debugTexture.SetData(new[] { Color.White });
            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (currentGameState)
            {
                case GameStates.MainMenu:
                    break;
                case GameStates.Playing:
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
                    break;
                case GameStates.Gameover:
                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //clear screen
            GraphicsDevice.Clear(Color.Gray);

            switch (currentGameState)
            {
                case GameStates.MainMenu:
                    _spriteBatch.Begin();
                    _spriteBatch.DrawString(font, "Main menu", new Vector2(100, 50), Color.White, 0f,new Vector2(0,0),2f,SpriteEffects.None,0f);
                    _spriteBatch.Draw(buttonTexture, new Vector2(100, 100), Color.Black);
                    _spriteBatch.DrawString(font, "Play", new Vector2(10, 10), Color.White);
                    _spriteBatch.End();
                    break;
                case GameStates.Playing:
                    _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, _camera.Transform);

                    for (int y = 0; y < tileMap.GetLength(1); y++)
                    {
                        for (int x = 0; x < tileMap.GetLength(0); x++)
                        {
                            string tileValue = tileMap[x, y];
                            int tileIndex = int.Parse(tileValue);

                            if (tileIndex >= 0 && tileIndex < tiles.Length)
                            {
                                _spriteBatch.Draw(tiles[tileIndex], new Rectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE), Color.White);
                            }
                        }
                    }
                    player.Draw(_spriteBatch);
                    foreach (var entity in entities)
                    {
                        entity.Draw(_spriteBatch);
                    }
                    _spriteBatch.End();
                    break;
                case GameStates.Gameover:
                    break;
                default:
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
