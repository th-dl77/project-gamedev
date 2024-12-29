using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevProject.Collisions
{
    public class CollisionLoader
    {
        private readonly CollisionManager _collisionManager;
        private readonly int _tileSize;

        public CollisionLoader(CollisionManager collisionManager, int tileSize)
        {
            _collisionManager = collisionManager;
            _tileSize = tileSize;
        }

        public void LoadCollidables(string[,] tileMap)
        {
            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    string tile = tileMap[x, y];
                    if (tile == "1" || tile == "4" || tile == "2")
                    {
                        Rectangle tileBoundingBox = new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize);
                        _collisionManager.AddCollidable(new CollidableObject(tileBoundingBox, true));
                    }
                }
            }
        }

        public void UpdateEnemyCollidables(List<IEntity> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    _collisionManager.UpdateCollidable(enemy.CollidableObject, enemy.GetBounds());
                }
            }
        }
    }
}
