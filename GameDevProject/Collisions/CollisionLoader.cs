using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Collisions
{
    public static class CollisionLoader
    {
        public static List<CollidableObject> LoadCollidableObjectsFromTiledMap(TiledMap map, GraphicsDevice graphicsDevice)
        {
            List<CollidableObject> collidableObjects = new List<CollidableObject>();
            // Retrieve the object layer named "Collision"
            var objectLayer = map.GetLayer<TiledMapObjectLayer>("Collisions");

            if (objectLayer != null)
            {
                foreach (var obj in objectLayer.Objects)
                {
                    // Check if the object has the "collisions" property set to "true"
                    if (obj.Properties.TryGetValue("collisions", out var collisionProperty) &&
                        collisionProperty == "true")
                    {
                        // Add the object as a collidable rectangle
                        Rectangle bounds = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, (int)obj.Size.Width, (int)obj.Size.Height);
                        collidableObjects.Add(new CollidableObject(bounds, graphicsDevice));
                    }
                }
            }
            return collidableObjects;
        }
    }
}
