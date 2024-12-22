using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDevProject.Collisions
{
    public class CollisionManager
    {
        private readonly List<ICollidableHandler> collidables = new List<ICollidableHandler>();
        public void AddCollidable(ICollidableHandler collidable)
        {
            collidables.Add(collidable);
        }

        public bool CheckCollision(Rectangle boundingBox)
        {
            foreach (var collidable in collidables)
            {
                if (collidable.IsSolid() && collidable.GetBoundingBox().Intersects(boundingBox))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
