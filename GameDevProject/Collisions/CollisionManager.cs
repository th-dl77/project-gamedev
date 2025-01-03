﻿using GameDevProject.Enemies;
using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDevProject.Collisions
{
    public class CollisionManager
    {
        public List<ICollidable> Collidables { get; set; } = new List<ICollidable>();
        public void AddCollidable(ICollidable collidable)
        {
            Collidables.Add(collidable);
        }

        public Vector2 CheckCollision(Vector2 position, Vector2 proposedPosition, int boundsHeight, int boundsWidth)
        {
            Rectangle newPlayerBounds = new Rectangle((int)proposedPosition.X, (int)proposedPosition.Y, boundsWidth, boundsHeight);

            foreach (var collidable in Collidables)
            {
                if (newPlayerBounds.Intersects(collidable.GetBoundingBox()))
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
            foreach (var collidable in Collidables)
            {
                spriteBatch.Draw(debugTexture, collidable.GetBoundingBox(), Color.Green * 0.5f);
            }
        }

        public void ResolvePlayerCollisions(Player player, List<IEnemy> entities)
        {
            foreach (IEnemy entity in entities)
            {
                if (entity is BatEnemy)
                {
                    return;
                }
                if (player.movementHandler.Bounds.Intersects(entity.GetBounds()))
                {
                    ResolveCollision(player, entity);
                }
            }
        }

        private void ResolveCollision(Player player, IEnemy enemy)
        {
            if (!enemy.IsAlive)
            {
                return;
            }
            Vector2 pushDirection = player.movementHandler.Position - enemy.Position;
            pushDirection.Normalize();
            player.movementHandler.Velocity = pushDirection * (player.movementHandler.Velocity.Length() / 2);
            player.movementHandler.Position += pushDirection * 5f;
        }
    }
}
