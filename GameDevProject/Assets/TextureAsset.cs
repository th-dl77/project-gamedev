using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Assets
{
    public class TextureAsset : IAsset
    {
        public Texture2D Texture { get; private set; }

        public TextureAsset(Texture2D texture)
        {
            Texture = texture;
        }
    }
}
