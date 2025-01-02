using System.Collections.Generic;
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
        public Vector2 Position { get; protected set; }
        public float Speed { get; protected set; }
        public Rectangle Bounds => GetBounds();

        protected bool isHitting;
        protected Animation currentAnimation;
        protected Dictionary<string, Animation> animations;
        public virtual void Update(GameTime gameTime, Player player)
        {
            CollidableObject.Bounds = GetBounds();
        }

        public Enemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed)
        {
            this.animations = animations;
            currentAnimation = this.animations["walk"];
            Position = startPosition;
            Speed = speed;
            CollidableObject = new CollidableObject(GetBounds(), false);
        }

        public virtual Rectangle GetBounds()
        {
            return new Rectangle(
                (int)Position.X + 45,
                (int)Position.Y + 45,
                currentAnimation.SpriteSheet.FrameWidth-20,
                currentAnimation.SpriteSheet.FrameHeight-20
            );
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void CheckRange(Player player, GameTime gameTime);
        public abstract void Die(GameTime gameTime);
    }
}
