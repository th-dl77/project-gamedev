using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameDevProject.Assets;
using GameDevProject.Animations;
using GameDevProject.Entities;

namespace GameDevProject.Enemies
{
    public class BatEnemy : PatrollingEnemy
    {
        public BatEnemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed, List<Vector2> patrolPoints) : base(animations, startPosition, speed, patrolPoints)
        {
        }
        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime, player);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch, Position, flip);
        }

    }
}
