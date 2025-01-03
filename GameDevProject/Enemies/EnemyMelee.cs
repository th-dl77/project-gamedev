using System;
using System.Collections.Generic;
using GameDevProject.Animations;
using GameDevProject.Assets;
using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Enemies
{
    public class EnemyMelee : Enemy
    {
        private SpriteEffects flip = SpriteEffects.None;
        public GameAssets gameAssets;
        public float DeathTimer { get; set; } = 0f;
        protected float swingTimer = 0f;
        protected const float swingDuration = 1f;
        public EnemyMelee(Dictionary<string, Animation> animations, Vector2 startPosition, float speed, GameAssets gameAssets = null) : base(animations, startPosition, speed)
        {
            this.gameAssets = gameAssets;
        }
        public override void Update(GameTime gameTime, Player player)
        {
            if (IsAlive)
            {
                HandleMovement(gameTime, player);
                CheckRange(player, gameTime);   
                CollidableObject.Bounds = GetBounds();
            }
            else
            {
                HandleDeathTimer(gameTime);
            }
            currentAnimation.Update(gameTime);
        }
        public override void HandleMovement(GameTime gameTime, Player player)
        {
            if (!isHitting)
            {
                Vector2 playerPosition = player.Position;
                Vector2 direction = playerPosition - Position;
                direction.Normalize();
                flip = direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentAnimation = animations["walk"];
            }
        }
        public override void HandleDeathTimer(GameTime gameTime)
        {
            DeathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (DeathTimer >= 5)
            {
                IsVisible = false;
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                currentAnimation.Draw(spriteBatch, Position, flip);
            }
        }
    }
}