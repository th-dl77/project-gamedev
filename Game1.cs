using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 movementVector = new Vector2(0,0);
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        private Texture2D _airplaneTexture;
        private Texture2D _bulletTexture;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _airplaneTexture = Content.Load<Texture2D>("airplaneSprite");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                movementVector.X++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movementVector.X--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                movementVector.Y--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                movementVector.Y++;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_airplaneTexture, movementVector, Color.AntiqueWhite);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
