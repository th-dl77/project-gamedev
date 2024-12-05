using GameDevProject.PlayerFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Collisions
{
    public class CollisionManager
    {
        private readonly ICollisionHandler _collisionHandler;
        private readonly List<CollidableObject> _collidableObjects;

        public CollisionManager(ICollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
            _collidableObjects = new List<CollidableObject>();
        }

        public void AddCollidableObject(CollidableObject collidableObject)
        {
            _collidableObjects.Add(collidableObject);
        }

        // Check if the player is colliding with any objects
        public void CheckCollisions(Player player)
        {
            foreach (var collidable in _collidableObjects)
            {
                if (collidable.IsCollidable && _collisionHandler.CheckCollision(player.Bounds, collidable.Bounds))
                {
                    HandleCollision(player, collidable);
                }
            }
        }

        // Handle collision when the player hits an object
        private void HandleCollision(Player player, CollidableObject collidable)
        {
            // Simple example: stop player movement on collision
            // In a more complex game, you could push the player back, or handle other behaviors
            player.Velocity = Vector2.Zero; // Stop player movement on collision
        }

        public List<CollidableObject> GetCollidableObjects()
        {
            return _collidableObjects;
        }
        public Vector2 ResolveCollisions(Player player, Vector2 proposedPosition)
        {
            var playerBounds = new Rectangle((int)proposedPosition.X, (int)proposedPosition.Y, player.Bounds.Width, player.Bounds.Height);
            foreach (var collidable in _collidableObjects)
            {
                if (collidable.IsCollidable && _collisionHandler.CheckCollision(playerBounds,collidable.Bounds))
                {
                    return ResolveCollision(player.Bounds, collidable.Bounds, player.Position);
                }
            }
            return proposedPosition;
        }

        private Vector2 ResolveCollision(Rectangle playerBounds, Rectangle collidableBounds, Vector2 currentPosition)
        {
            // Check for overlap
            var overlapX = Math.Min(playerBounds.Right, collidableBounds.Right) - Math.Max(playerBounds.Left, collidableBounds.Left);
            var overlapY = Math.Min(playerBounds.Bottom, collidableBounds.Bottom) - Math.Max(playerBounds.Top, collidableBounds.Top);

            if (overlapX > 0 && overlapY > 0)
            {
                // Resolve based on smaller overlap
                if (Math.Abs(overlapX) < Math.Abs(overlapY))
                {
                    if (playerBounds.Center.X < collidableBounds.Center.X)
                        return new Vector2(collidableBounds.Left - playerBounds.Width, currentPosition.Y); // Push left
                    else
                        return new Vector2(collidableBounds.Right, currentPosition.Y); // Push right
                }
                else
                {
                    if (playerBounds.Center.Y < collidableBounds.Center.Y)
                        return new Vector2(currentPosition.X, collidableBounds.Top - playerBounds.Height); // Push up
                    else
                        return new Vector2(currentPosition.X, collidableBounds.Bottom); // Push down
                }
            }

            return currentPosition; // No collision, allow movement
        }



        public void DrawCollidables(SpriteBatch spriteBatch)
        {
            foreach (var collidable in _collidableObjects)
            {
                collidable.Draw(spriteBatch);
            }
        }
    }

}
