﻿using System.Collections.Generic;
using GameDevProject.Animations;
using GameDevProject.Collisions;
using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Enemies
{
    public abstract class Enemy : IEnemy
    {
        public bool IsAlive { get; set; } = true;
        public ICollidable CollidableObject { get; private set; }
        public bool IsVisible { get; set; } = true;

        protected bool isHitting;
        protected Animation _currentAnimation;
        protected Dictionary<string, Animation> _animations;
        public Vector2 Position { get; protected set; }
        public float Speed { get; protected set; }
        public Rectangle Bounds => GetBounds();
        public virtual void Update(GameTime gameTime, Player player)
        {
            CollidableObject.Bounds = GetBounds();
        }

        public Enemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed)
        {
            _animations = animations;
            _currentAnimation = _animations["walk"];
            Position = startPosition;
            Speed = speed;
            CollidableObject = new CollidableObject(GetBounds(), false);
        }

        public virtual Rectangle GetBounds()
        {
            return new Rectangle(
                (int)Position.X + 45,
                (int)Position.Y + 45,
                _currentAnimation._spriteSheet.FrameWidth-20,
                _currentAnimation._spriteSheet.FrameHeight-20
            );
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void CheckRange(Player player, GameTime gameTime);
        public abstract void Die(GameTime gameTime);
    }
}
