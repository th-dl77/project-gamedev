using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Assets
{
    public class FontAsset : IAsset
    {
        public SpriteFont Font { get; private set; }

        public FontAsset(SpriteFont font)
        {
            Font = font;
        }
    }
}
