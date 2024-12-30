using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameDevProject.Entities
{
    public class FightingHitboxHandler
    {
        private AnimationManager animationManager;
        private MovementHandler movementHandler;

        public FightingHitboxHandler(AnimationManager animationManager, MovementHandler movementHandler)
        {
            this.animationManager = animationManager;
            this.movementHandler = movementHandler;
        }
        public Rectangle GetSwordHitbox()
        {
            int swordHeight = 60;
            int swordWidth = 30;

            Rectangle swordHitbox = new Rectangle();

            if (animationManager.currentFlipEffect == SpriteEffects.FlipHorizontally)
            {
                swordHitbox = new Rectangle((int)movementHandler.Position.X + 25 - swordWidth, (int)movementHandler.Position.Y + 45, swordWidth, swordHeight);
            }
            else
            {
                swordHitbox = new Rectangle((int)movementHandler.Position.X + 25 + movementHandler.Bounds.Width, (int)movementHandler.Position.Y + 45, swordWidth, swordHeight);
            }

            return swordHitbox;
        }
    }
}
