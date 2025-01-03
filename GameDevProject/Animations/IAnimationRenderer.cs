using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameDevProject.Animations
{
    public interface IAnimationRenderer
    {
        void Render(
            SpriteBatch spriteBatch,
            Animation animation,
            Vector2 position,
            SpriteEffects flipEffect,
            bool isVisible
        );
    }
}
