using GameDevProject.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Entities
{
    public class EnemyMelee : Enemy
    {
        private SpriteEffects flip = SpriteEffects.None;
        public EnemyMelee(Dictionary<string, Animation> animations, Vector2 startPosition, float speed) : base(animations, startPosition, speed)
        {
        }
        public override void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            Vector2 playerPosition = collisionManager.Player.Position;
            Vector2 direction = playerPosition - Position;
            if (direction.Length() >0)
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
            _currentAnimation.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _currentAnimation.Draw(spriteBatch, Position, flip);
        }
    }
}