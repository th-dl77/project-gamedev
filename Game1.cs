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

        private int timePressed;

        private int _tileSize = 16;
        private HashSet<int> _collidableTiles = new HashSet<int> { 1 };

        private TiledMap _tiledmap;
        private TiledMapRenderer tiledMapRenderer;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        { 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D spriteSheetTexture = Content.Load<Texture2D>("char_red_1");
            _tiledmap = Content.Load<TiledMap>("map");

            tiledMapRenderer = new TiledMapRenderer(GraphicsDevice);

            tiledMapRenderer.LoadMap(_tiledmap);

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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            Matrix cameraMatrix = Matrix.Identity;
            player.Draw(_spriteBatch);
            tiledMapRenderer.Draw();
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
