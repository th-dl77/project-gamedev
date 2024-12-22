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
        private SpriteEffects flip = SpriteEffects.None;

        protected bool isHitting;
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
        public abstract void Update(GameTime gameTime, Player player);
        
        public Enemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed)
        {
            _animations = animations;
            _currentAnimation = _animations["idle"];
            Position = startPosition;
            Speed = speed;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void CheckRange(Player player, GameTime gameTime);
    }
}
