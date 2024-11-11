using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 bulletVector;
        private Vector2 bulletVector2;
        private Rectangle _airplanePart;
        private Vector2 position;
        private Rectangle _backgroundRectangle;
        private Rectangle _bulletPart;

        private Player _player;
        private Background _background;

        private int timePressed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 780;
            _graphics.PreferredBackBufferWidth = 780;
        }

        protected override void Initialize()
        {
            //_bulletPart = new Rectangle(1340, 160, 70,70);
            //_backgroundRectangle = new Rectangle(0, 0, 720, 960);
            _background = new Background(new Rectangle(0, 0, 700, 700), new Vector2(0, 0));
            _player = new Player(new Rectangle(30,30,200,200), new Vector2(200, 200));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _player.LoadContent(Content, "airplaneSprite2");
            _background.LoadContent(Content, "skybackground3");
            //_bulletTexture = Content.Load<Texture2D>("airplaneSprite2");
            //_bulletTexture2 = Content.Load<Texture2D>("airplaneSprite2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _background.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
