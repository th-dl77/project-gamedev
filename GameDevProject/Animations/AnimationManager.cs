using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameDevProject.Input;

namespace GameDevProject.Animations
{
    public class AnimationManager
    {
        private readonly Dictionary<string, Animation> animations;
        private Animation currentAnimation;
        private AnimationSelector animationSelector;
        private bool wasLastDirectionLeft;
        public SpriteEffects CurrentFlipEffect { get; set; }

        public AnimationManager(Dictionary<string, Animation> animations)
        {
            this.animations = animations;
            currentAnimation = this.animations["idle"];
            animationSelector = new AnimationSelector();
        }

        public void PlayAnimation(string animationKey)
        {
            if (!animations.ContainsKey(animationKey))
                return;

            currentAnimation = animations[animationKey];
        }

        public void FlipAnimation(bool isFlipped)
        {
            CurrentFlipEffect = isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public void Update(GameTime gameTime, IInputStrategy inputStrategy, bool isDead)
        {
            Vector2 inputDirection = inputStrategy.GetMovementInput();
            bool isFighting = inputStrategy.IsActionPressed("fight");

            var (selectedAnimationKey, isFlipped) = animationSelector.SelectAnimation(isDead, isFighting, inputDirection, ref wasLastDirectionLeft);
            FlipAnimation(isFlipped);

            if (currentAnimation != animations[selectedAnimationKey])
                currentAnimation = animations[selectedAnimationKey];

            currentAnimation?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 1f, bool isVisible = true)
        {
            if (isVisible)
            {
                currentAnimation?.Draw(spriteBatch, position, CurrentFlipEffect);
            }
        }
    }
}
