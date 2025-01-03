using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Animations
{
    public class SpriteSheet : ISpriteSheet
    {
        public Texture2D texture;
        public int FrameWidth;
        public int FrameHeight;
        private int yOffset;

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight, int yOffset = 0)
        {
            this.texture = texture;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            this.yOffset = yOffset;
        }

        public void DrawFrame(SpriteBatch spriteBatch, int index, Vector2 position, SpriteEffects flip = SpriteEffects.None)
        {
            int columns = texture.Width / FrameWidth;
            Rectangle sourceRectangle = new Rectangle(
                index % columns * FrameWidth,
                index / columns * FrameHeight + yOffset,
                FrameWidth,
                FrameHeight
            );


            spriteBatch.Draw(
                texture,
                position,
                sourceRectangle,
                Color.White,
                0f,
                new Vector2(0, 0),
                new Vector2(2f, 2f),
                flip,
                0f
            );
        }
    }
}
