using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevProject.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Entities
{
    public abstract class Enemy : IEntity
    {
        protected Animation _currentAnimation;
        protected Dictionary<string, Animation> _animations;
        public Vector2 Position { get; protected set; }
        public float Speed { get; protected set; }
        public Rectangle Bounds => new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            (int)_currentAnimation._spriteSheet.FrameWidth,
            (int)_currentAnimation._spriteSheet.FrameHeight
        );
        public abstract void Update(GameTime gameTime, CollisionManager collisionManager);

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentAnimation.Draw(spriteBatch, Position);
        }
    }
}
