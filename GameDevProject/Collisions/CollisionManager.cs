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

        public Vector2 CheckCollision(Vector2 position, Vector2 proposedPosition, int boundsHeight, int boundsWidth)
        {
            Rectangle newPlayerbounds = new Rectangle((int)proposedPosition.X, (int)proposedPosition.Y, boundsWidth, boundsHeight);

            foreach (var collidable in collidables)
            {
                if (newPlayerbounds.Intersects(collidable.GetBoundingBox()))
                {
                    return position;
                }
            }
            return proposedPosition;
        }
    }
}
