using GameDevProject.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Entities
{
    public class EnemyMelee : Enemy
    {
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
                Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                _currentAnimation.SetDirection(direction);
                _currentAnimation = _animations["walk"];
            }
            else
            {
                _currentAnimation = _animations["idle"];
            }
            _currentAnimation.Update(gameTime);
        }
    }
}