using GameDevProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevProject.GameStates
{
    public class StartMenuState : IGameState
    {
        private Texture2D buttonTexture;
        private SpriteFont font;
        private Texture2D backgroundTexture;
        private Rectangle playButtonHitBox;
        private Rectangle exitButtonHitBox;
        private UIManager uiManager;

        public StartMenuState(Texture2D buttonTexture, SpriteFont font, Texture2D backgroundTexture)
        {
            this.buttonTexture = buttonTexture;
            this.font = font;
            this.backgroundTexture = backgroundTexture;
            uiManager = new UIManager(buttonTexture, font);

            playButtonHitBox = new Rectangle(215, 285, 300, 140);
            exitButtonHitBox = new Rectangle(215, 485, 300, 140);
        }
        public void Update(Game1 game, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (uiManager.IsButtonClicked(playButtonHitBox, mouseState))
            {
                game.gameStateManager.ChangeGameState(new PlayingState(game));
            }
            if (uiManager.IsButtonClicked(exitButtonHitBox, mouseState))
            {
                game.Exit();
            }
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            SpriteBatch _spriteBatch = game._spriteBatch;
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 800, 800), Color.White);
            _spriteBatch.DrawString(font, "Monster Slayer", new Vector2(50, 50), Color.Gold, 0f, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0f);
            Vector2 buttonPosition1 = new Vector2(215, 200);
            _spriteBatch.Draw(buttonTexture, buttonPosition1, new Rectangle(0, 0, 200, 200), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(font, "Play", new Vector2(310, 225), Color.Black);
            Vector2 buttonPosition2 = new Vector2(215, 400);
            _spriteBatch.Draw(buttonTexture, buttonPosition2, new Rectangle(0, 0, 200, 200), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(font, "Exit", new Vector2(300, 425), Color.Black);

            /*debug hitboxes
            Rectangle button1Hitbox = new Rectangle((int)buttonPosition1.X, (int)buttonPosition1.Y, 300, 140);
            Rectangle button2Hitbox = new Rectangle((int)buttonPosition2.X, (int)buttonPosition2.Y, 300, 140);
            _spriteBatch.Draw(_debugTexture, button1Hitbox, Color.Green);
            _spriteBatch.Draw(_debugTexture, button2Hitbox, Color.Red);
            */
            _spriteBatch.End();
        }
    }
}
