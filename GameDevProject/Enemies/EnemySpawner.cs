using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using GameDevProject.Collisions;
using System.Runtime.CompilerServices;
using GameDevProject.Assets;


namespace GameDevProject.Enemies
{
    public class EnemySpawner
    {
        private EnemyFactory enemyFactory;
        private Random random = new Random();
        private int spawnAreaWidth;
        private int spawnAreaHeight;
        private CollisionLoader collisionLoader;
        private Vector2 playerPos;

        public EnemySpawner(ContentManager content, int spawnAreaWidth, int spawnAreaHeight, Vector2 playerPos, CollisionLoader collisionLoader, GameAssets gameAssets)
        {
            enemyFactory = new EnemyFactory(content, gameAssets);
            this.spawnAreaWidth = spawnAreaWidth;
            this.spawnAreaHeight = spawnAreaHeight;
            this.collisionLoader = collisionLoader;
            this.playerPos = playerPos;
        }
        public List<IEnemy> Spawn(int level)
        {
            switch (level)
            {
                case 1:
                    return SpawnLevel(10);
                case 2:
                    return SpawnLevel(20);
                case 3:
                    return SpawnLevel(30);
                default:
                    throw new Exception();
            }
        }
        private List<IEnemy> SpawnLevel(int enemyCount)
        {
            List<IEnemy> entities = new List<IEnemy>();

            for (int i = 0; i < enemyCount; i++)
            {
                entities.Add(enemyFactory.CreateSkeletonEnemy(GetRandomSpawnPosition(playerPos, 200)));
                entities.Add(enemyFactory.CreateGolemEnemy(GetRandomSpawnPosition(playerPos, 400), new List<Vector2>() { new Vector2(100, 200), new Vector2(200, 100), new Vector2(240, 300) }));
            }
            for (int i = 0; i < enemyCount / 2; i++)
            {
                entities.Add(enemyFactory.CreateSlimeEnemy(GetRandomSpawnPosition(playerPos, 200)));
                entities.Add(enemyFactory.CreateBatEnemy(GetRandomSpawnPosition(playerPos, 400)));
            }
            //collisionLoader.LoadEnemyCollidables(entities);
            return entities;
        }
        public Vector2 GetRandomSpawnPosition(Vector2 playerPosition, float minDistance)
        {
            int maxAttempts = 5;
            Vector2 spawnPosition;
            for (int i = 0; i < maxAttempts; i++)
            {
                float x = random.Next(0, spawnAreaWidth);
                float y = random.Next(0, spawnAreaHeight);

                spawnPosition = new Vector2(x, y);
                if (Vector2.Distance(spawnPosition, playerPosition) >= minDistance)
                {
                    return spawnPosition;
                }
            }
            // if no valid spawn is found; return middle of the map
            return new Vector2(1000, 1000);
        }
    }
}
