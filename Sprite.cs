using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    internal class Sprite
    {
        protected Texture2D _texture;
        public Vector2 Position;
        public Rectangle sourceRectangle;


        public Sprite(Rectangle rectangle, Vector2 position)
        {
            sourceRectangle = rectangle;
            Position = position;
        }

        public virtual void LoadContent(ContentManager content)
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch _spritebatch)
        {
            if (_texture != null)
            {
                _spritebatch.Draw(_texture, Position, sourceRectangle, Color.White);
            }
        }

	}
}
