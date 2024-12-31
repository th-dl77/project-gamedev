using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevProject.Input;

namespace GameDevProject.Entities
{
    public class AnimationManager
    {
        private readonly Dictionary<string, Animation> animations;
        private Animation currentAnimation;
        public SpriteEffects currentFlipEffect { get; set; }

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

        public void Update(GameTime gameTime, IInputStrategy inputStrategy)
        {
            Vector2 inputDirection = inputStrategy.GetMovementInput();
            bool isFighting = inputStrategy.IsActionPressed("fight");
            bool isMoving = Math.Abs(inputDirection.X) > 0.1f || Math.Abs(inputDirection.Y) > 0.1f;
            if (isFighting)
            {
                PlayAnimation("fighting");
            }
            else if (isMoving)
            {
                PlayAnimation("running");
                if (inputDirection.X < 0)
                {
                    FlipAnimation(true);
                }
                else if (inputDirection.X > 0)
                {
                    FlipAnimation(false);
                }
            }
            else
            {
                PlayAnimation("idle");
            }
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
