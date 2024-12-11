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
        public override void Update(GameTime gameTime, Player player)
        {
            Vector2 direction = player.Position - Position;
            if (direction.Length() >0)
            {
                direction.Normalize();
            }
            Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentAnimation = _animations["walk"];
            _currentAnimation.Update(gameTime);

        }
    }
}