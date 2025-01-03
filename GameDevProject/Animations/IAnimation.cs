using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Animations
{
    public interface IAnimation
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects flip);
    }
}
