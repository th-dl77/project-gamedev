using GameDevProject.Enemies;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevProject.Collisions
{
    public class CollisionLoader
    {
        private readonly CollisionManager collisionManager;
        private readonly int tileSize;

        public CollisionLoader(CollisionManager collisionManager, int tileSize)
        {
            this.collisionManager = collisionManager;
            this.tileSize = tileSize;
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
                        Rectangle tileBoundingBox = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                        collisionManager.AddCollidable(new CollidableObject(tileBoundingBox, true));
                    }
                }
            }
        }

        public void UpdateEnemyCollidables(List<IEnemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    collisionManager.UpdateCollidable(enemy.CollidableObject, enemy.GetBounds());
                }
            }
        }
    }
}
