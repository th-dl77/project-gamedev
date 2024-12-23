using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Entities
{
    public class GolemEnemy : EnemyMelee
    {
        private SpriteEffects flip = SpriteEffects.None;
        protected float swingTimer = 0f;
        protected const float swingDuration = 1f;
        private List<Vector2> patrolPoints;
        private int currentPatrolIndex;
        private bool detected;
        private float patrolThreshold = 10f;
        public GolemEnemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed, List<Vector2> patrolPoints) : base(animations, startPosition, speed)
        {
            this.currentPatrolIndex = 0;
            this.patrolPoints = patrolPoints;
        }
        public override void Update(GameTime gameTime, Player player)
        {
            if (IsAlive)
            {
                if (!isHitting)
                {
                    Vector2 targetPoint = patrolPoints[currentPatrolIndex];
                    Vector2 playerPosition = player.Position;
                    Vector2 direction = Vector2.Zero;
                    float distanceToPlayer = Vector2.Distance(player.Position, this.Position);
                    if (distanceToPlayer < 100 || detected)
                    {
                        detected = true;
                        direction = playerPosition - Position;
                    }
                    else
                    {
                        direction = targetPoint - Position;
                    }
                    if (direction.Length() > 0)
                    {
                        direction.Normalize();
                    }
                    if (direction.X < 0)
                    {
                        flip = SpriteEffects.FlipHorizontally; // Player is moving left
                    }
                    else if (direction.X > 0)
                    {
                        flip = SpriteEffects.None; // Player is moving right
                    }
                    Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Vector2.Distance(Position, targetPoint) < patrolThreshold)
                    {
                        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count; 
                    }
                    _currentAnimation = _animations["walk"];
                }
                this.CheckRange(player, gameTime);
            }
            _currentAnimation.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _currentAnimation.Draw(spriteBatch, Position, flip);
        }
    }
}
