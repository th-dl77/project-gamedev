using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject
{
    internal class Background : Sprite
    {
        public Background(Rectangle rectangle, Vector2 position) : base(rectangle, position)
        {
        }
        public override void Draw(SpriteBatch _spritebatch)
        {
            if (_texture != null)
            {
                _spritebatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero,1.5f,SpriteEffects.None,0f);
            }
        }
    }
}
