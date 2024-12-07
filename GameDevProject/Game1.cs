using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GameDevProject.Collisions;
using MonoGame.Extended;
using GameDevProject.Entities;
using GameDevProject.Input;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera _camera;

        private CollisionManager _collisionManager;
        private RenderingManager _renderingManager;
        private TiledMapRenderer _tiledMapRenderer;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TiledMap _tiledMap = Content.Load<TiledMap>("map");

            _collisionManager.AddCollidableObjects(CollisionLoader.LoadCollidableObjectsFromTiledMap(_tiledMap, GraphicsDevice));

            _renderingManager = new RenderingManager(GraphicsDevice, _tiledMap, _camera);

            Texture2D spriteSheetTexture = Content.Load<Texture2D>("char_red_1");

            IInputStrategy inputStrategy = new KeyboardInputStrategy();

            player = PlayerFactory.CreatePlayer(inputStrategy, spriteSheetTexture, _camera, new Vector2(200,200));

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
            _camera.Update(player.Position, 800, 800);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //clear screen
            GraphicsDevice.Clear(Color.Gray);

            //draw the map
            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            _renderingManager.DrawMap(_spriteBatch);
            /* Debug draw for collidables
            _collisionManager.DrawCollidables(_spriteBatch);*/
            _spriteBatch.End();

            //draw the player
            _spriteBatch.Begin();
            /* used to draw textures around the bounds of the playerchar
            player.DrawBounds(_spriteBatch, _debugTexture);*/
            player.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
