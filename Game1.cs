using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 movementVector;
        private Vector2 bulletVector;
        private Rectangle _airplanePart;
        private int timePressed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _airplanePart = new Rectangle(30, 30, 200, 200);
            timePressed = 1;
            base.Initialize();
        }

        private Texture2D _airplaneTexture;
        private Texture2D _bulletTexture;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _airplaneTexture = Content.Load<Texture2D>("airplaneSprite2");
            _bulletTexture = Content.Load<Texture2D>("bulletSprite");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                movementVector.X+=5;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movementVector.X-=5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                movementVector.Y-=3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                movementVector.Y+=3;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_airplaneTexture, movementVector, _airplanePart, Color.AntiqueWhite);
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                bulletVector.X = movementVector.X;
                bulletVector.Y = movementVector.Y-2;
                _spriteBatch.Draw(_bulletTexture, bulletVector, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
