﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameDevProject.Assets;
using GameDevProject.Animations;
using GameDevProject.Entities;

namespace GameDevProject.Enemies
{
    public class PatrollingEnemy : EnemyMelee
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
                if (!isHitting)
                {
                    _currentAnimation = _animations["walk"];
                }
                CheckRange(player, gameTime);
            }
            _currentAnimation.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _currentAnimation.Draw(spriteBatch, Position, flip);
        }
    }
}
