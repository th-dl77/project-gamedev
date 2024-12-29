using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevProject.Collisions
{
    public class CollisionManager
    {
        public List<ICollidable> collidables = new List<ICollidable>();
        public void AddCollidable(ICollidable collidable)
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

        public void UpdateCollidable(ICollidable collidable, Rectangle newBounds)
        {
            collidable.Bounds = newBounds;
        }
    }
}
