using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameDevProject.Animations
{
    public class AnimationRenderer : IAnimationRenderer
    {
        public void Render(
            SpriteBatch spriteBatch,
            Animation animation,
            Vector2 position,
            SpriteEffects flipEffect,
            bool isVisible
        )
        {
            if (!isVisible)
                return;

            animation.Draw(spriteBatch, position, flipEffect);
        }
    }
}
