﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDevProject.Collisions;
using GameDevProject.Input;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace GameDevProject.Entities
{
    public class Player
    {
        private IInputStrategy _inputStrategy;
        public bool IsHitting { get; private set; } = false;
        public bool IsDead => healthManager.IsDead;

        public int Health => healthManager.Health;

        public int MaxHealth => healthManager.MaxHealth;

        private float deathTimer { get; set; }
        
        public Vector2 Velocity;
        public Vector2 Position { get; set; }
        public float Scale { get; private set; } = 2f;

        private float delayTimer = 3f;

        public MovementHandler movementHandler;
        private readonly AnimationManager animationManager;
        public FightingHitboxHandler fightingHitboxHandler;
        public HealthManager healthManager;

        public PlayerDeathHandler DeathHandler { get; private set; }

        private float hitCooldownTimer = 0f;
        private float hitCooldownDuration = 2f;

        private float flickerTimer = 0f;
        private float flickerDuration = 2f;
        private float flickerInterval = 0.1f;
        private bool isVisible = true;

        public event Action OnDeath;
        public Rectangle Bounds => movementHandler.Bounds;

        public Player(IInputStrategy inputStrategy, Vector2 startPosition, AnimationManager animationManager, HealthManager healthManager)
        {
            this._inputStrategy = inputStrategy;
            Position = startPosition;
            movementHandler = new MovementHandler(_inputStrategy);
            this.animationManager = animationManager;
            fightingHitboxHandler = new FightingHitboxHandler(animationManager, movementHandler);
            this.healthManager = healthManager;

            this.healthManager.OnDeath += HandleDeath;
        }
        
        public void Update(GameTime gameTime, CollisionManager collisionManager, List<IEntity> entities)
        {
            Vector2 inputDirection = _inputStrategy.GetMovementInput();
            Position = movementHandler.Position;
            animationManager.Update(gameTime, _inputStrategy);
            if (_inputStrategy.IsActionPressed("fight"))
            {
                IsHitting = true;
                fightingHitboxHandler.GetSwordHitbox();
            }
            else
                IsHitting = false;

            movementHandler.HandleMovement(gameTime, inputDirection, IsHitting);

            Vector2 resolvedPosition = collisionManager.CheckCollision(
                movementHandler.Position,
                movementHandler.Position + movementHandler.Velocity,
                movementHandler.Bounds.Height,
                movementHandler.Bounds.Width
            );
            movementHandler.AdjustPosition(resolvedPosition - movementHandler.Position);

            collisionManager.ResolvePlayerCollisions(this, entities);
        }

        public void TakeHit(int dmgAmount, GameTime gameTime)
        {
            healthManager.TakeDamage(dmgAmount);
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

        private void HandleDeath()
        {
            healthManager.IsDead = true;
            animationManager.PlayAnimation("deathAnimation");
            OnDeath?.Invoke();
        }
    }
}
