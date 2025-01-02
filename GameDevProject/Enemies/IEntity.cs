using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using GameDevProject.Collisions;
using GameDevProject.Entities;

namespace GameDevProject.Enemies
{
    public interface IEnemy
    {
        Vector2 Position { get; }
        ICollidable CollidableObject { get; }
        Rectangle GetBounds();
        public bool IsAlive { get; set; }
        void Update(GameTime gameTime, Player player);
        void Draw(SpriteBatch spriteBatch);
        void Die(GameTime gameTime);
    }
}
