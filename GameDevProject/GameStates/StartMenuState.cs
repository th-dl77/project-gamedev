using GameDevProject.Assets;
using GameDevProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevProject.GameStates
{
    public class StartMenuState : IGameState
    {
        private GameAssets gameAssets;
        private Texture2D backgroundTexture;
        private Rectangle playButtonHitBox;
        private Rectangle exitButtonHitBox;
        private UIManager uiManager;

        public StartMenuState(GameAssets gameAssets, Texture2D backgroundTexture)
        {
            this.gameAssets = gameAssets;
            this.backgroundTexture = backgroundTexture;
            uiManager = new UIManager(gameAssets.GetTexture("buttonTexture"), gameAssets.GetFont("font"));

            playButtonHitBox = new Rectangle(235, 220, 280, 140);
            exitButtonHitBox = new Rectangle(235, 420, 280, 140);
        }
        public void Update(Game1 game, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (uiManager.IsButtonClicked(playButtonHitBox, mouseState))
            {
                game.gameStateManager.ChangeGameState(new PlayingState(game, gameAssets));
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
            _spriteBatch.Draw(gameAssets.GetTexture("mainMenuBackground"), new Rectangle(0, 0, 800, 800), Color.White);
            _spriteBatch.DrawString(gameAssets.GetFont("font"), "Monster Slayer", new Vector2(50, 50), Color.DarkRed, 0f, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0f);
            Vector2 buttonPosition1 = new Vector2(215, 200);
            _spriteBatch.Draw(gameAssets.GetTexture("buttonTexture"), buttonPosition1, new Rectangle(0, 0, 200, 200), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(gameAssets.GetFont("font"), "Play", new Vector2(310, 225), Color.Black);
            Vector2 buttonPosition2 = new Vector2(215, 400);
            _spriteBatch.Draw(gameAssets.GetTexture("buttonTexture"), buttonPosition2, new Rectangle(0, 0, 200, 200), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(gameAssets.GetFont("font"), "Exit", new Vector2(300, 425), Color.Black);
            /*
            _spriteBatch.Draw(gameAssets.GetTexture("debug"), playButtonHitBox, Color.Green);
            _spriteBatch.Draw(gameAssets.GetTexture("debug"), exitButtonHitBox, Color.Red);
            */
            _spriteBatch.End();
        }
    }
}
