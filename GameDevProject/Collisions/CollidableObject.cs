using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Collisions
{
    public class CollidableObject
    {
        public Rectangle Bounds { get; set; }
        public bool IsCollidable { get; set; }

        private Texture2D _texture;

        public CollidableObject(Rectangle bounds, GraphicsDevice graphicsDevice, bool isCollidable = true)
        {
            Bounds = bounds;
            IsCollidable = isCollidable;
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new[] { Color.Green });
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Bounds, Color.White);
        }
    }

}
