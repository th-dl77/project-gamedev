using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GameDevProject.PlayerFiles;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D tilesheet;

        private Player player;

        private Camera camera;
        private Texture2D fogTexture;

        private int timePressed;

        private TiledMap tiledmap;
        private TiledMapRenderer tiledMapRenderer;
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
            camera = new Camera(GraphicsDevice.Viewport);  
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the tiledmap and playerchar
            Texture2D spriteSheetTexture = Content.Load<Texture2D>("char_red_1");
            tiledmap = Content.Load<TiledMap>("map");

            tiledMapRenderer = new TiledMapRenderer(GraphicsDevice);

            tiledMapRenderer.LoadMap(tiledmap);

            //animations
            SpriteSheet spriteSheetRunning = new SpriteSheet(spriteSheetTexture, 56, 56, 112);
            SpriteSheet spriteSheetIdle = new SpriteSheet(spriteSheetTexture, 56, 56);
            SpriteSheet spriteSheetFighting = new SpriteSheet(spriteSheetTexture, 56, 56, 56);
            Animation idle = new Animation(spriteSheetIdle, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation running = new Animation(spriteSheetRunning, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation fighting = new Animation(spriteSheetFighting, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 },0.2f);

            player = new Player(running, idle, fighting, new Vector2(200,200), 100f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update(gameTime);
            camera.Update(player.position,800,800);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //clear screen
            GraphicsDevice.Clear(Color.Gray);

            //draw the map
            _spriteBatch.Begin(transformMatrix: camera.Transform);
            tiledMapRenderer.Draw(camera.Transform);
            _spriteBatch.End();

            //draw the player
            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
