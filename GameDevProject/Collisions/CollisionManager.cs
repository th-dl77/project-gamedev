using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
                    Vector2 pushDirection = position - proposedPosition;
                    pushDirection.Normalize();

                    proposedPosition += pushDirection * 5f;
                    return proposedPosition;
                }
            }
            return proposedPosition;
        }

        public void UpdateCollidable(ICollidable collidable, Rectangle newBounds)
        {
            collidable.Bounds = newBounds;
        }

        public void DrawCollidables(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            foreach (var collidable in collidables)
            {
                spriteBatch.Draw(debugTexture, collidable.GetBoundingBox(), Color.Green*0.5f);
            }
        }

        public void ResolvePlayerCollisions(Player player, List<IEntity> entities)
        {
            foreach (IEntity entity in entities)
            {
                if (player.Bounds.Intersects(entity.GetBounds()))
                {
                    ResolveCollision(player, entity);
                }
            }
        }

        private void ResolveCollision(Player player, IEntity enemy)
        {
            if (!enemy.IsAlive)
            {
                return;
            }
            Vector2 pushDirection = player.Position - enemy.Position;
            pushDirection.Normalize();
            player.Velocity = pushDirection * (player.Velocity.Length() / 2);
            player.Position += pushDirection * 5f;
        }

    }
}
