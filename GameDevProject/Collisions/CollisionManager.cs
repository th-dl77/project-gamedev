using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDevProject.Collisions
{
    public class CollisionManager
    {
        private Player player;

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        private List<CollidableObject> collidableObjects;

        public CollisionManager()
        {
            this.collidableObjects = new List<CollidableObject>();
        }
        public Vector2 ResolveCollisions(Vector2 proposedPosition)
        {
            // predict position of proposedposition, if it collides with something then return original playerpos, nullifying the movement
            if (WillCollideWithObjects(proposedPosition))
            {
                return player.Position;
            }

            return proposedPosition;
        }

        //method for prediction
        private bool WillCollideWithObjects(Vector2 proposedPosition)
        {
            Rectangle playerBounds = new Rectangle((int)proposedPosition.X, (int)proposedPosition.Y, player.Bounds.Width, player.Bounds.Height);

            foreach (var collidableObject in collidableObjects)
            {
                if (playerBounds.Intersects(collidableObject.Bounds))
                {
                    return true;
                }
            }
            return false;
        }
        public void DrawCollidables(SpriteBatch spriteBatch)
        {
            foreach (var collidable in collidableObjects)
            {
                collidable.Draw(spriteBatch);
            }
        }

        public void AddCollidableObjects(List<CollidableObject> collidableObjects)
        {
            this.collidableObjects = collidableObjects;
        }
    }
}
