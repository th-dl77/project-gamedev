using GameDevProject.Enemies;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevProject.GameStates
{
    public interface IGameStateManager
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void ChangeGameState(IGameState newGameState);
        bool AllEnemiesDead(List<IEnemy> enemies);
    }

}
