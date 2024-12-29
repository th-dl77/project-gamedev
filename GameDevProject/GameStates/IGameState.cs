using Microsoft.Xna.Framework;

namespace GameDevProject.GameStates
{
    public interface IGameState
    {
        void Update(Game1 game, GameTime gameTime);
        void Draw(Game1 game, GameTime gameTime);
    }
}
