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
        void Update(GameTime gameTime, CollisionManager collisionManager);
        void Draw(SpriteBatch spriteBatch);
    }
}
