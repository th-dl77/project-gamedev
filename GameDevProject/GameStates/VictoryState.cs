using GameDevProject.Entities;
using GameDevProject.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using GameDevProject.Assets;

namespace GameDevProject.GameStates
{
    public class VictoryState : IGameState
    {
        private GameAssets gameAssets;
        private Player newPlayer;
        private Rectangle resetButtonHitBox;
        private Texture2D backGround;
        private UIManager uiManager;
        private GameStateManager gameStateManager;
        private GameResetHandler gameResetHandler;

        public VictoryState(GameAssets gameAssets, Texture2D backGround, GameStateManager gameStateManager, GameResetHandler gameResetHandler)
        {
            this.gameAssets = gameAssets;
            this.backGround = backGround;
            this.gameStateManager = gameStateManager;
            this.gameResetHandler = gameResetHandler;
            uiManager = new UIManager(gameAssets.GetTexture("buttonTexture"), gameAssets.GetFont("font"));

            resetButtonHitBox = new Rectangle(250, 310, 300, 140);

        }
        public void Update(Game1 game, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (uiManager.IsButtonClicked(resetButtonHitBox, mouseState))
            {
                newPlayer = gameResetHandler.ResetGame();
                game.player = newPlayer;
                gameStateManager.ChangeGameState(new PlayingState(game,gameAssets));
            }
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            SpriteBatch spriteBatch = game._spriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Rectangle(0, 0, 800, 800), Color.White);
            spriteBatch.DrawString(gameAssets.GetFont("font"), "Victory!", new Vector2(285, 200), Color.DarkRed);
            spriteBatch.Draw(gameAssets.GetTexture("buttonTexture"), new Vector2(250, 310), new Rectangle(0, 0, 200, 200), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(gameAssets.GetFont("font"), "Reset", new Vector2(310, 340), Color.Black);
            spriteBatch.End();

        }
    }
}
