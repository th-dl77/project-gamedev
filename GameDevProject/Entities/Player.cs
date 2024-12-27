using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDevProject.Collisions;
using GameDevProject.Input;
using GameDevProject.GameStates;
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
        public Vector2 Position { get; private set; }
        private float Speed { get; set; } = 100f;
        public float Scale { get; private set; } = 2f;

        private float delayTimer = 3f;

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

        public void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            if (!isDead)
            {
                HandleInput(gameTime);
                delayTimer = 3f;
            }
            else
            {
               delayTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
               if (delayTimer<=0)
                {
                    OnDeath?.Invoke();
                }
            }
            Vector2 proposedPosition = Position + Velocity * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 resolvedPosition = collisionManager.CheckCollision(Position,proposedPosition,Bounds.Height, Bounds.Width);
            Position = resolvedPosition;
            currentAnimation.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            Vector2 inputVelocity = _inputStrategy.GetMovementInput();
            Speed = 100f;
            if (_inputStrategy.IsActionPressed("fight"))
            {
                currentAnimation = animations["fighting"];
                Speed = 0f;
                IsHitting = true;
            }
            else if (inputVelocity != Vector2.Zero)
            {
                IsHitting = false;
                inputVelocity.Normalize(); //used to fix diagonal movement, otherwise way too fast
                currentAnimation = animations["running"];
                if (inputVelocity.X < 0)
                {
                    _currentFlipEffect = SpriteEffects.FlipHorizontally; // Player is moving left
                }
                else if (inputVelocity.X > 0)
                {
                    _currentFlipEffect = SpriteEffects.None; // Player is moving right
                }
            }
            else
            {
                currentAnimation = animations["idle"];
                IsHitting = false;
            }
            Velocity = inputVelocity;
        }
        public void TakeHit(int dmgAmount, GameTime gameTime)
        {
            currentAnimation = animations["damageAnimation"];
            Health -= dmgAmount;
            if (Health < 0)
            {
                Die(gameTime);
            }
        }

        public void Die(GameTime gameTime)
        {
            isDead = true;
            currentAnimation = animations["deathAnimation"];         
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch, Position, _currentFlipEffect);
        }

        public void DrawBounds(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            spriteBatch.Draw(debugTexture, Bounds, Color.Blue * 0.5f);
        }

        public Rectangle GetSwordHitbox()
        {
            int swordHeight = 50;
            int swordWidth = 20;

            Rectangle swordHitbox = new Rectangle();

            if (_currentFlipEffect == SpriteEffects.FlipHorizontally)
            {
                swordHitbox = new Rectangle((int)Position.X - swordWidth, (int)Position.Y, swordWidth, swordHeight);
            }
            else
            {
                swordHitbox = new Rectangle((int)Position.X + Bounds.Width, (int)Position.Y, swordWidth, swordHeight);
            }

            return swordHitbox;
        }
    }
}
