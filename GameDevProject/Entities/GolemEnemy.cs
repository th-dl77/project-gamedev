using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace GameDevProject.Entities
{
    public class GolemEnemy : PatrollingEnemy
    {
        private List<Vector2> patrolPoints;
        public GolemEnemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed, List<Vector2> patrolPoints) : base(animations, startPosition, speed, patrolPoints)
        {
            this.patrolPoints = patrolPoints;
        }

        public override void CheckRange(Player player, GameTime gameTime)
        {
            float distanceToPlayer = Vector2.Distance(player.Position, this.Position);
            if (distanceToPlayer < 80 && !player.IsDead)
            {
                if (!isHitting)
                {
                    isHitting = true;
                    swingTimer = 0f;
                    _currentAnimation = _animations["fight"];
                }
                else
                {
                    swingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (swingTimer >= swingDuration)
                    {
                        player.TakeHit(2, gameTime); //let the player take damage
                        swingTimer = 0f;
                    }
                }
            }
            else
            {
                isHitting = false;
                swingTimer = 0f;
            }
        }
    }
}
