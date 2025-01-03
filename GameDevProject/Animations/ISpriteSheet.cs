using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameDevProject.Animations
{
    public interface ISpriteSheet
    {
        void DrawFrame(SpriteBatch spriteBatch, int index, Vector2 position, SpriteEffects flip = SpriteEffects.None);
    }

}
