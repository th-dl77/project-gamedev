using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDevProject.Collisions;
using GameDevProject.Input;
using System;

namespace GameDevProject.Entities
{
    public class Player
    {

        private readonly Dictionary<string, Animation> animations;
        private Animation currentAnimation;

        private SpriteEffects _currentFlipEffect = SpriteEffects.None;

        private IInputStrategy _inputStrategy;

        public int Health { get; private set; } = 5;
        public int MaxHealth { get; private set; } = 5;
        public bool IsHitting { get; private set; } = false;
        public int SpriteHeight { get; private set; } = 56;
        public int SpriteWidth { get; private set; } = 56;
        public bool isDead { get; private set; } = false;

        private float deathTimer { get; set; }
        
        public Vector2 Velocity;
        public Vector2 Position { get; set; }
        private float Speed { get; set; } = 10f;
        public float Scale { get; private set; } = 2f;

        private float delayTimer = 3f;

        private float maxSpeed = 15f;
        private float accelerationRate = 50f;
        private float decelerationRate = 20f;

        private float hitCooldownTimer = 0f;
        private float hitCooldownDuration = 2f;

        private float flickerTimer = 0f;
        private float flickerDuration = 2f;
        private float flickerInterval = 0.1f;
        private bool isVisible = true;


        public Vector2 Acceleration { get; set; } = Vector2.Zero;

        public event Action OnDeath;
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)Position.X + 25,
                    (int)Position.Y + 45,
                    (int)((SpriteWidth - 25) * Scale),
                    (int)((SpriteHeight - 20) * Scale)
                );
            }
        }

        public Player(IInputStrategy inputStrategy, Dictionary<string, Animation> animations, Vector2 startPosition)
        {
            this._inputStrategy = inputStrategy;
            this.animations = animations;
            currentAnimation = animations["idle"];
            Position = startPosition;
        }

        public void Update(GameTime gameTime, CollisionManager collisionManager, List<IEntity> entities)
        {
            if (!isDead)
            {
                HandleInput(gameTime);
                delayTimer = 3f;
            }
            else
            {
               delayTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds; // delay to let the death animation play
               if (delayTimer<=0)
                {
                    OnDeath?.Invoke();
                }
            }

            if (hitCooldownTimer > 0f)
            {
                hitCooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (flickerTimer > 0f)
            {
                flickerTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                // toggle visibility
                if (flickerTimer % flickerInterval < flickerInterval / 2)
                {
                    isVisible = true;
                }
                else
                {
                    isVisible = false;
                }
                if (flickerTimer <= 0f)
                {
                    isVisible = true; 
                }
            }
            Vector2 proposedPosition = Position + Velocity * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 resolvedPosition = collisionManager.CheckCollision(Position, proposedPosition, Bounds.Height, Bounds.Width);
            Position = resolvedPosition;
            collisionManager.ResolvePlayerCollisions(this, entities);
            currentAnimation.Update(gameTime);
        }
        private void HandleInput(GameTime gameTime)
        {
            Vector2 inputDirection = _inputStrategy.GetMovementInput();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_inputStrategy.IsActionPressed("fight"))
            {
                // stop movement while swinging
                currentAnimation = animations["fighting"];
                Velocity = Vector2.Zero; 
                Acceleration = Vector2.Zero;
                IsHitting = true;
            }
            else if (inputDirection != Vector2.Zero)
            {
                IsHitting = false;
                inputDirection.Normalize();
                Acceleration = inputDirection * accelerationRate;

                // update velocity with acceleration
                Velocity += Acceleration * deltaTime;

                //ensure velocity is not faster than maxspeed
                if (Velocity.Length() > maxSpeed)
                {
                    Velocity.Normalize();
                    Velocity *= maxSpeed;
                }
                currentAnimation = animations["running"];
                _currentFlipEffect = inputDirection.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }
            else
            {
                if (Velocity.Length() > 0.1f)
                {
                    // deceleration
                    Velocity -= Velocity * decelerationRate * deltaTime;
                }
                else
                {
                    Velocity = Vector2.Zero;
                    currentAnimation = animations["idle"];
                }
            }

            Position += Velocity * deltaTime;
        }

        public void TakeHit(int dmgAmount, GameTime gameTime)
        {
            if (Health <= 0)
            {
                Die(gameTime);
            }

            if (hitCooldownTimer > 0f)
            {
                return;
            }
            Health -= dmgAmount;

            flickerTimer = flickerDuration;
            isVisible = true;

            hitCooldownTimer = hitCooldownDuration;
        }

        public void Die(GameTime gameTime)
        {
            isDead = true;
            currentAnimation = animations["deathAnimation"];         
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                currentAnimation.Draw(spriteBatch, Position, _currentFlipEffect);
            }
        }

        public void DrawBounds(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            spriteBatch.Draw(debugTexture, Bounds, Color.Blue * 0.5f);
            spriteBatch.Draw(debugTexture, GetSwordHitbox(), Color.Red * 0.5f);
        }

        public Rectangle GetSwordHitbox()
        {
            int swordHeight = 60;
            int swordWidth = 30;

            Rectangle swordHitbox = new Rectangle();

            if (_currentFlipEffect == SpriteEffects.FlipHorizontally)
            {
                swordHitbox = new Rectangle((int)Position.X+25 - swordWidth, (int)Position.Y+45, swordWidth, swordHeight);
            }
            else
            {
                swordHitbox = new Rectangle((int)Position.X+25 + Bounds.Width, (int)Position.Y+45, swordWidth, swordHeight);
            }

            return swordHitbox;
        }
    }
}
