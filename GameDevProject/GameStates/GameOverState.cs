using GameDevProject.Assets;
using GameDevProject.Entities;
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
        private Player newPlayer;
        private Rectangle resetButtonHitBox;
        private Texture2D deathScreenBackground;
        private UIManager uiManager;
        private GameStateManager gameStateManager;
        private GameResetHandler gameResetHandler;

        private GameAssets gameAssets;

        private Texture2D _debugTexture;

        public GameOverState(GameAssets gameAssets,GameStateManager gameStateManager, GameResetHandler gameResetHandler)
        {
            this.gameAssets = gameAssets;
            this.gameStateManager = gameStateManager;
            this.gameResetHandler = gameResetHandler;
            uiManager = new UIManager(buttonTexture, font);

            resetButtonHitBox = new Rectangle(250, 310, 300, 140);

        }
        public void Update(Game1 game, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (uiManager.IsButtonClicked(resetButtonHitBox,mouseState))
            {
                newPlayer = gameResetHandler.ResetGame();
                game.player = newPlayer;
                gameStateManager.ChangeGameState(new PlayingState(game, gameAssets));
            }
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            SpriteBatch spriteBatch = game._spriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(gameAssets.DeathScreenBackground, new Rectangle(0,0,800,800), Color.White);
            spriteBatch.DrawString(gameAssets.Font, "You Died!", new Vector2(285,200), Color.DarkRed);
            spriteBatch.Draw(gameAssets.ButtonTexture, new Vector2(250,310), new Rectangle(0, 0, 200, 200), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Reset", new Vector2(310, 340), Color.Black);
            spriteBatch.End();
                
        }
    }
}
