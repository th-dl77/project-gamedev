﻿

using GameDevProject.Entities;
using GameDevProject.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameDevProject.GameStates
{
    public class VictoryState : IGameState
    {
        private Texture2D buttonTexture;
        private SpriteFont font;
        private Player newPlayer;
        private Rectangle resetButtonHitBox;
        private Texture2D backGround;
        private UIManager uiManager;
        private GameStateManager gameStateManager;
        private GameResetHandler gameResetHandler;

        private Texture2D _debugTexture;

        public VictoryState(Texture2D buttonTexture, SpriteFont font, Texture2D backGround, GameStateManager gameStateManager, GameResetHandler gameResetHandler)
        {
            this.buttonTexture = buttonTexture;
            this.font = font;
            this.backGround = backGround;
            this.gameStateManager = gameStateManager;
            this.gameResetHandler = gameResetHandler;
            uiManager = new UIManager(buttonTexture, font);

            resetButtonHitBox = new Rectangle(250, 310, 300, 140);

        }
        public void Update(Game1 game, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (uiManager.IsButtonClicked(resetButtonHitBox, mouseState))
            {
                newPlayer = gameResetHandler.ResetGame();
                game.player = newPlayer;
                gameStateManager.ChangeGameState(new PlayingState(game));
            }
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            SpriteBatch spriteBatch = game._spriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Rectangle(0, 0, 800, 800), Color.White);
            spriteBatch.DrawString(font, "Victory!", new Vector2(285, 200), Color.DarkRed);
            spriteBatch.Draw(buttonTexture, new Vector2(250, 310), new Rectangle(0, 0, 200, 200), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Reset", new Vector2(310, 340), Color.Black);
            spriteBatch.End();

        }
    }
}