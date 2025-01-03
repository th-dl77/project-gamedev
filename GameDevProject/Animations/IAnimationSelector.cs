using Microsoft.Xna.Framework;

namespace GameDevProject.Animations
{
    public interface IAnimationSelector
    {
        (string AnimationKey, bool IsFlipped) SelectAnimation(bool isDead, bool isFighting, Vector2 inputDirection, ref bool wasLastDirectionLeft);
    }
}
