using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Entities
{
    public class GolemEnemy : EnemyMelee
    {
        private SpriteEffects flip = SpriteEffects.None;
        protected float swingTimer = 0f;
        protected const float swingDuration = 1f;
        public GolemEnemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed) : base(animations, startPosition, speed)
        {
        }
        public override void Update(GameTime gameTime, Player player)
        {
            if (IsAlive)
            {
                if (!isHitting)
                {
                    Vector2 playerPosition = player.Position;
                    Vector2 direction = playerPosition - Position;
                    if (direction.Length() > 0)
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
                }
                this.CheckRange(player, gameTime);
            }
            _currentAnimation.Update(gameTime);
        }
    }
}
