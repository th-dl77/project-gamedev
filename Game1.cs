using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player;
        private Player player2;

        private int timePressed;
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

            SpriteSheet spriteSheetRunning = new SpriteSheet(spriteSheetTexture, 56, 56, 112);
            SpriteSheet spriteSheetIdle = new SpriteSheet(spriteSheetTexture, 56, 56);
            Animation idle = new Animation(spriteSheetIdle, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation running = new Animation(spriteSheetRunning, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            player = new Player(running, idle, new Vector2(200,200), 100f);
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
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
