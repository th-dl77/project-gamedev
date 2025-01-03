using Microsoft.Xna.Framework;
using System;

namespace GameDevProject.Animations
{
    public class AnimationSelector : IAnimationSelector
    {
        public (string AnimationKey, bool IsFlipped) SelectAnimation(bool isDead, bool isFighting, Vector2 inputDirection, ref bool wasLastDirectionLeft)
        {
            if (isDead)
                return ("deathAnimation", false);

            if (isFighting)
                return ("fighting", false);

            if (Math.Abs(inputDirection.X) > 0 || Math.Abs(inputDirection.Y) > 0)
            {
                if (inputDirection.X < 0 && !wasLastDirectionLeft)
                    wasLastDirectionLeft = true;
                if (inputDirection.X > 0 && wasLastDirectionLeft)
                    wasLastDirectionLeft = false; // No flip when moving right

                return ("running", wasLastDirectionLeft);
            }

            return ("idle", wasLastDirectionLeft); // Default to idle with no flip
        }
    }
}
