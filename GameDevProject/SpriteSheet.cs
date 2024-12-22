using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject
{
    public class SpriteSheet
    {
        public Texture2D _texture;
        public int FrameWidth;
        public int FrameHeight;
        private int yOffset;

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight, int yOffset = 0)
        {
            this._texture = texture;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.yOffset = yOffset;
        }

        public void DrawFrame(SpriteBatch spriteBatch, int index, Vector2 position, SpriteEffects flip = SpriteEffects.None)
        {
            int columns = _texture.Width / FrameWidth;
            Rectangle sourceRectangle = new Rectangle(
                (index % columns) * FrameWidth,
                (index / columns) * FrameHeight + yOffset,
                FrameWidth,
                FrameHeight
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
