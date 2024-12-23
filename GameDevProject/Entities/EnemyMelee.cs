﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Entities
{
    public class EnemyMelee : Enemy
    {
        private SpriteEffects flip = SpriteEffects.None;
        protected float swingTimer = 0f;
        protected const float swingDuration = 1f;
        public EnemyMelee(Dictionary<string, Animation> animations, Vector2 startPosition, float speed) : base(animations, startPosition, speed)
        {
        }
        public override void Update(GameTime gameTime, Player player)
        {
            if (IsAlive)
            {
                if (!isHitting)
                {
                    Vector2 playerPosition = player.Position;
                    Vector2 direction = playerPosition - Position;
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
                    _currentAnimation = _animations["walk"];
                }
                this.CheckRange(player, gameTime);
            }
            _currentAnimation.Update(gameTime);
        }
        public override void CheckRange(Player player, GameTime gameTime)
        {
            float distanceToPlayer = Vector2.Distance(player.Position,this.Position);
            if (distanceToPlayer <50 && !player.isDead)
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
                        player.TakeHit(30, gameTime); //let the player take damage
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
            _currentAnimation = _animations["death"];
            IsAlive = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _currentAnimation.Draw(spriteBatch, Position, flip);
        }
    }
}