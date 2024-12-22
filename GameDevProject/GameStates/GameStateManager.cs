using Microsoft.Xna.Framework;

namespace GameDevProject.GameStates
{
    public class GameStateManager
    {
        private Game1 game;
        private IGameState currentGameState;

        public GameStateManager(Game1 game)
        {
            this.game = game;
            currentGameState = new StartMenuState(game.buttonTexture, game.font, game.mainMenuBackground);
        }
        public void Update(GameTime gameTime)
        {
            currentGameState.Update(game, gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            currentGameState.Draw(game, gameTime);
        }
    }
}
