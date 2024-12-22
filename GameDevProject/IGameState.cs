using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    public interface IGameState
    {
        void Update(Game1 game, GameTime gameTime);
        void Draw(Game1 game, GameTime gameTime);
    }
}
