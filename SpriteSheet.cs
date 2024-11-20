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
        public Texture2D Texture { get; }
        private int frameWidth;
        private int frameHeight;

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight)
        {
            this.Texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
        }

        public Rectangle GetFrame(int index)
        {
            int columns = Texture.Width / frameWidth;
            int x = (index % columns) * frameWidth;
            int y = (index / columns) * frameHeight;
            return new Rectangle(x, y, frameWidth, frameHeight);
        }
    }
}
