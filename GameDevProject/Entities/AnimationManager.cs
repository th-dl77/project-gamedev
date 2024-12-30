using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Entities
{
    public class AnimationManager
    {
        private readonly Dictionary<string, Animation> animations;
        private Animation currentAnimation;
        private SpriteEffects currentFlipEffect;

        public AnimationManager(Dictionary<string, Animation> animations)
        {
            this.animations = animations;
            currentAnimation = this.animations["idle"];
        }

        public void PlayAnimation(string animationKey)
        {
            if (!animations.ContainsKey(animationKey))
                return;

            currentAnimation = animations[animationKey];
        }

        public void FlipAnimation(bool isFlipped)
        {
            currentFlipEffect = isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public void Update(GameTime gameTime)
        {
            currentAnimation?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 1f, bool isVisible = true)
        {
            if (isVisible)
            {
                currentAnimation?.Draw(spriteBatch, position, currentFlipEffect);
            }
        }
    }
}
