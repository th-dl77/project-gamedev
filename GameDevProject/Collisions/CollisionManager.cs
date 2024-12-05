using GameDevProject.PlayerFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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

    }

}
