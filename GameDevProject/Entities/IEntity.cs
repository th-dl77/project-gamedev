using GameDevProject.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Entities
{
    public interface IEntity
    {
        Vector2 Position { get; }
        Rectangle Bounds { get; }
        public bool IsAlive { get; set; }
        void Update(GameTime gameTime, Player player);
        void Draw(SpriteBatch spriteBatch);
        void Die(GameTime gameTime);
    }
}
