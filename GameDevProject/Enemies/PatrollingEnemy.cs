using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameDevProject.Assets;
using GameDevProject.Animations;
using GameDevProject.Entities;

namespace GameDevProject.Enemies
{
    public class PatrollingEnemy : EnemyMelee, IEnemy
    {
        public SpriteEffects flip = SpriteEffects.None;
        private List<Vector2> patrolPoints;
        private int currentPatrolIndex;
        private float patrolThreshold = 10f;
        public PatrollingEnemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed, List<Vector2> patrolPoints) : base(animations, startPosition, speed)
        {
            currentPatrolIndex = 0;
            this.patrolPoints = patrolPoints;
        }
        public override void Update(GameTime gameTime, Player player)
        {
            if (IsAlive)
            {
                Vector2 targetPoint = patrolPoints[currentPatrolIndex];
                Vector2 direction = Vector2.Zero;
                direction = targetPoint - Position;
                direction.Normalize();
                flip = direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Vector2.Distance(Position, targetPoint) < patrolThreshold)
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                if (!isHitting)
                    currentAnimation = animations["walk"];
                CheckRange(player, gameTime);
            }
            else
            {
                DeathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (DeathTimer >= 5)
                    IsVisible = false;
            }
            currentAnimation.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                currentAnimation.Draw(spriteBatch, Position, flip);
            }
        }
    }
}
