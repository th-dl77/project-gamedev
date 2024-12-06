using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GameDevProject.PlayerFiles;
using GameDevProject.Collisions;
using MonoGame.Extended;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player;

        private Camera _camera;

        private CollisionManager _collisionManager;

        private RenderingManager _renderingManager;

        private Texture2D _debugTexture;

        private TiledMapRenderer _tiledMapRenderer;
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

            _debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            _debugTexture.SetData(new[] { Color.White });

            //animations
            SpriteSheet spriteSheetRunning = new SpriteSheet(spriteSheetTexture, 56, 56, 112);
            SpriteSheet spriteSheetIdle = new SpriteSheet(spriteSheetTexture, 56, 56);
            SpriteSheet spriteSheetFighting = new SpriteSheet(spriteSheetTexture, 56, 56, 56);
            Animation idle = new Animation(spriteSheetIdle, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation running = new Animation(spriteSheetRunning, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation fighting = new Animation(spriteSheetFighting, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 },0.2f);

            player = new Player(running, idle, fighting, new Vector2(200,200), _camera);
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
            /* Debug draw for collisions
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
