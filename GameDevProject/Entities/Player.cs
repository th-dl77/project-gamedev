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
        private IInputStrategy _inputStrategy;

        public int Health { get; private set; } = 5;
        public int MaxHealth { get; private set; } = 5;d
        public bool IsHitting { get; private set; } = false;
        public bool isDead { get; private set; } = false;

        private float deathTimer { get; set; }
        
        public Vector2 Velocity;
        public Vector2 Position { get; set; }
        public float Scale { get; private set; } = 2f;

        private float delayTimer = 3f;

        public MovementHandler movementHandler;
        private readonly AnimationManager animationManager;
        public FightingHitboxHandler fightingHitboxHandler;

        private float hitCooldownTimer = 0f;
        private float hitCooldownDuration = 2f;

        private float flickerTimer = 0f;
        private float flickerDuration = 2f;
        private float flickerInterval = 0.1f;
        private bool isVisible = true;

        public event Action OnDeath;
        public Rectangle Bounds => movementHandler.Bounds;

        public Player(IInputStrategy inputStrategy, Vector2 startPosition, AnimationManager animationManager)
        {
            this._inputStrategy = inputStrategy;
            Position = startPosition;
            movementHandler = new MovementHandler(_inputStrategy);
            this.animationManager = animationManager;
            fightingHitboxHandler = new FightingHitboxHandler(animationManager, movementHandler);
        }

        public void Update(GameTime gameTime, CollisionManager collisionManager, List<IEntity> entities)
        {
            Vector2 inputDirection = _inputStrategy.GetMovementInput();
            if (!isDead)
            {
                movementHandler.HandleMovement(gameTime, inputDirection);
                Position = movementHandler.Position;
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

            animationManager.Update(gameTime, inputDirection);
            fightingHitboxHandler.GetSwordHitbox();

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


            Vector2 resolvedPosition = collisionManager.CheckCollision(
                movementHandler.Position,
                movementHandler.Position + movementHandler.Velocity,
                movementHandler.Bounds.Height,
                movementHandler.Bounds.Width
            );
            movementHandler.AdjustPosition(resolvedPosition - movementHandler.Position);

            collisionManager.ResolvePlayerCollisions(this, entities);
            animationManager.Update(gameTime, inputDirection);
        }
        //private void HandleInput(GameTime gameTime)
        //{
        //    Vector2 inputDirection = _inputStrategy.GetMovementInput();
        //    float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    if (_inputStrategy.IsActionPressed("fight"))
        //    {
        //        // stop movement while swinging
        //        currentAnimation = animations["fighting"];
        //        Velocity = Vector2.Zero; 
        //        Acceleration = Vector2.Zero;
        //        IsHitting = true;
        //    }
        //    else if (inputDirection != Vector2.Zero)
        //    {
        //        IsHitting = false;
        //        inputDirection.Normalize();
        //        Acceleration = inputDirection * accelerationRate;

        //        // update velocity with acceleration
        //        Velocity += Acceleration * deltaTime;

        //        //ensure velocity is not faster than maxspeed
        //        if (Velocity.Length() > maxSpeed)
        //        {
        //            Velocity.Normalize();
        //            Velocity *= maxSpeed;
        //        }
        //        currentAnimation = animations["running"];
        //        _currentFlipEffect = inputDirection.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        //    }
        //    else
        //    {
        //        if (Velocity.Length() > 0.1f)
        //        {
        //            // deceleration
        //            Velocity -= Velocity * decelerationRate * deltaTime;
        //        }
        //        else
        //        {
        //            Velocity = Vector2.Zero;
        //            currentAnimation = animations["idle"];
        //        }
        //    }

        //    Position += Velocity * deltaTime;
        //}

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
            animationManager.PlayAnimation("deathAnimation");      
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch, movementHandler.Position, Scale, isVisible);
        }

        public void DrawBounds(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            spriteBatch.Draw(debugTexture, Bounds, Color.Blue * 0.5f);
            spriteBatch.Draw(debugTexture, fightingHitboxHandler.GetSwordHitbox(), Color.Red * 0.5f);
        }
    }
}
