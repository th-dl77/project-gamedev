using System.Collections.Generic;
using GameDevProject.Animations;
using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameDevProject.Enemies
{
    public class GolemEnemy : PatrollingEnemy, IEnemy
    {
        private List<Vector2> patrolPoints;
        private bool detected;
        private int currentPatrolIndex;
        private float patrolThreshold = 10f;

        public GolemEnemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed, List<Vector2> patrolPoints) : base(animations, startPosition, speed, patrolPoints)
        {
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
                    float distanceToPlayer = Vector2.Distance(player.Position, Position);
                    if (distanceToPlayer < 300 || detected)
                    {
                        detected = true;
                        direction = playerPosition - Position;
                    }
                    else
                        direction = targetPoint - Position;
                    direction.Normalize();
                    flip = direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Vector2.Distance(Position, targetPoint) < patrolThreshold)
                        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                    _currentAnimation = _animations["walk"];
                }
                CheckRange(player, gameTime);
            }
            else
            {
                deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (deathTimer >= 5)
                    IsVisible = false;
            }
            _currentAnimation.Update(gameTime);
        }
        public override void CheckRange(Player player, GameTime gameTime)
        {
            float distanceToPlayer = Vector2.Distance(player.Position, Position);
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
