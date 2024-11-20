using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    public class GameCharacter
    {
        private Animation animation;
        private Microsoft.Xna.Framework.Vector2 position;
        public GameCharacter(Animation animation, Microsoft.Xna.Framework.Vector2 startPosition)
        {
            this.animation = animation;
            this.position = startPosition;
        }
        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }
    }
}
