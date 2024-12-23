using GameDevProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevProject.GameStates
{
    public class GameOverState : IGameState
    {
        private Texture2D buttonTexture;
        private SpriteFont font;
        private Rectangle resetButtonHitBox;
        private Texture2D deathScreenBackground;
        private UIManager uiManager;
        private GameStateManager gameStateManager;

        private Texture2D _debugTexture;

        public GameOverState(Texture2D buttonTexture, SpriteFont font,Texture2D deathScreenBackground, GameStateManager gameStateManager)
        {
            this.buttonTexture = buttonTexture;
            this.font = font;
            this.deathScreenBackground = deathScreenBackground;
            this.gameStateManager = gameStateManager;
            uiManager = new UIManager(buttonTexture, font);

            resetButtonHitBox = new Rectangle(250, 310, 300, 140);

        }
        public void Update(Game1 game, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (uiManager.IsButtonClicked(resetButtonHitBox,mouseState))
            {
                gameStateManager.ChangeGameState(new PlayingState(game));
            }
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            SpriteBatch spriteBatch = game._spriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(deathScreenBackground, new Rectangle(0,0,800,800), Color.White);
            spriteBatch.DrawString(font, "You Died!", new Vector2(285,200), Color.DarkRed);
            spriteBatch.Draw(buttonTexture, new Vector2(250,310), new Rectangle(0, 0, 200, 200), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Reset", new Vector2(310, 340), Color.Black);
            spriteBatch.End();
                
        }
    }
}
