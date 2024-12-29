using GameDevProject.Assets;
using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDevProject.GameStates
{
    public class GameStateManager
    {
        private Game1 game;
        public IGameState currentGameState;
        private GameAssets gameAssets;

        public GameStateManager(Game1 game, GameAssets gameAssets)
        {
            this.game = game;
            this.gameAssets = gameAssets;
            currentGameState = new StartMenuState(gameAssets, gameAssets.GetTexture("mainMenuBackground"));
        }
        public void Update(GameTime gameTime)
        {
            currentGameState.Update(game, gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            currentGameState.Draw(game, gameTime);
        }
        public void ChangeGameState(IGameState newGameState)
        {
            Debug.Write($"Changing state to {newGameState}");
            currentGameState = newGameState;
        }
        public bool AllEnemiesDead(List<IEntity> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
