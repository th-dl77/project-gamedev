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
        private Rectangle _bulletPart;
        private int timePressed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 960;
            _graphics.PreferredBackBufferWidth = 720;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _airplanePart = new Rectangle(30, 30, 200, 200);
            _bulletPart = new Rectangle(1340, 160, 70,70);
            movementVector = new Vector2(300, 640);
            base.Initialize();
        }

        private Texture2D _airplaneTexture;
        private Texture2D _bulletTexture;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _airplaneTexture = Content.Load<Texture2D>("airplaneSprite2");
            _bulletTexture = Content.Load<Texture2D>("airplaneSprite2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (movementVector.X<550)
                {
                    movementVector.X += 5;
                }
           
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (movementVector.X > 0)
                {
                    movementVector.X -= 5;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (movementVector.Y>0)
                {
                    movementVector.Y -= 3;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (movementVector.Y<760)
                {
                    movementVector.Y += 3;
                }
            }


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
                bulletVector.Y = movementVector.Y;
                _spriteBatch.Draw(_bulletTexture, bulletVector, _bulletPart, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
