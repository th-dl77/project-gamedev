using GameDevProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameStates
{
    public class GameOverState : IGameState
    {
        private Texture2D buttonTexture;
        private SpriteFont font;
        private Rectangle resetButtonHitBox;
        private UIManager uiManager;

        public GameOverState(Texture2D buttonTexture, SpriteFont font)
        {
            this.buttonTexture = buttonTexture;
            this.font = font;
            uiManager = new UIManager(buttonTexture, font);

            resetButtonHitBox = new Rectangle(215, 285, 300, 140);

        }
        public void Update(Game1 game, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (uiManager.IsButtonClicked(resetButtonHitBox,mouseState))
            {
                Debug.Write("Reset game...");
            }
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            SpriteBatch spriteBatch = game._spriteBatch;
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "You Died!", new Vector2(300,200), Color.DarkRed);
            spriteBatch.Draw(buttonTexture, new Vector2(215, 485), new Rectangle(0, 0, 200, 200), Color.White);
            spriteBatch.DrawString(font, "Reset!", new Vector2(300, 200), Color.Black);
            spriteBatch.End();
                
        }
    }
}
