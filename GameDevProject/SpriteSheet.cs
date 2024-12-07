using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    public class SpriteSheet
    {
        public Texture2D _texture;
        private int frameWidth;
        private int frameHeight;
        private int yOffset;

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight, int yOffset = 0)
        {
            this._texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.yOffset = yOffset;
        }

        public void DrawFrame(SpriteBatch spriteBatch, int index, Vector2 position, SpriteEffects flip = SpriteEffects.None)
        {
            int columns = _texture.Width / frameWidth;
            Rectangle sourceRectangle = new Rectangle(
                (index % columns) * frameWidth,
                (index / columns) * frameHeight + yOffset,
                frameWidth,
                frameHeight
            );


            spriteBatch.Draw(
                _texture,
                position,
                sourceRectangle,
                Color.White,
                0f,
                new Vector2(0, 0),
                new Vector2(2f,2f),
                flip,
                0f
            );
        }
    }
}
