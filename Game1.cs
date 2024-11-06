using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _background;
        private Vector2 bulletVector;
        private Rectangle _airplanePart;
        private Vector2 movementVector;
        Movement movement = new Movement();
        private Rectangle _backgroundRectangle;
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
            _backgroundRectangle = new Rectangle(0, 0, 720, 960);
            base.Initialize();
        }

        private Texture2D _airplaneTexture;
        private Texture2D _bulletTexture;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _airplaneTexture = Content.Load<Texture2D>("airplaneSprite2");
            _bulletTexture = Content.Load<Texture2D>("airplaneSprite2");
            _background = Content.Load<Texture2D>("skybackground");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            movementVector = movement.CheckHorizontal();
            movementVector = movement.CheckVertical();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, _backgroundRectangle, Color.White);
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
