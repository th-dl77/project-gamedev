using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameDevProject.Assets;
using GameDevProject.Animations;
using GameDevProject.Entities;

namespace GameDevProject.Enemies
{
    public class PatrollingEnemy : Enemy
    {
        public SpriteEffects flip = SpriteEffects.None;
        private List<Vector2> patrolPoints;
        public float DeathTimer { get; set; } = 0f;
        protected float swingTimer = 0f;
        protected const float swingDuration = 1f;

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
                HandleMovement(gameTime, player);
                CheckRange(player, gameTime);
            }
            else
            {
                HandleDeathTimer(gameTime);
            }
            currentAnimation.Update(gameTime);
        }
        public override void HandleMovement(GameTime gameTime, Player player)
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
        }
        public override void HandleDeathTimer(GameTime gameTime)
        {
            DeathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (DeathTimer >= 5)
                IsVisible = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                currentAnimation.Draw(spriteBatch, Position, flip);
            }
        }
        public override void CheckRange(Player player, GameTime gameTime)
        {
            float distanceToPlayer = Vector2.Distance(player.Position, Position);
            if (distanceToPlayer < 90 && !player.IsDead)
            {
                if (!isHitting)
                {
                    isHitting = true;
                    swingTimer = 0f;
                    currentAnimation = animations["fight"];
                }
                else
                {
                    swingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (swingTimer >= swingDuration)
                    {
                        player.TakeHit(1, gameTime); //let the player take damage
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

        public override void Die(GameTime gameTime)
        {
            currentAnimation = animations["death"];
            IsAlive = false;
        }
    }
}
