using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Runtime.Intrinsics;


namespace GameDevProject.Entities
{
    public class EnemySpawner
    {
        private EnemyFactory enemyFactory;
        private Random random = new Random();
        private int spawnAreaWidth;
        private int spawnAreaHeight;
        private Vector2 playerPos;

        public EnemySpawner(ContentManager content, int spawnAreaWidth, int spawnAreaHeight, Vector2 playerPos)
        {
            enemyFactory = new EnemyFactory(content);
            this.spawnAreaWidth = spawnAreaWidth;
            this.spawnAreaHeight = spawnAreaHeight;
            this.playerPos = playerPos;
        }
        public List<IEntity> Spawn(int level)
        {
            switch (level)
            {
                case 1:
                    return SpawnLevelOne();
                    break;
                default:
                    throw new System.Exception();
                    break;
            }
        }
        private List<IEntity> SpawnLevelOne()
        {
            List<IEntity> entities = new List<IEntity>();

            for (int i = 0; i < 50; i ++)
            {
                entities.Add(enemyFactory.CreateSkeletonEnemy(GetRandomSpawnPosition(playerPos, 200)));
            }
            //entities.Add(enemyFactory.CreateEnemy("golem", new Vector2(100, 100)));
            entities.Add(enemyFactory.CreateSlimeEnemy(GetRandomSpawnPosition(playerPos, 300)));
            entities.Add(enemyFactory.CreateBatEnemy(GetRandomSpawnPosition(playerPos, 200)));
            entities.Add(enemyFactory.CreateGolemEnemy(GetRandomSpawnPosition(playerPos, 400), new List<Vector2>() { new Vector2(100,200), new Vector2(200,100), new Vector2(240,300)}));
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
                if (Vector2.Distance(spawnPosition,playerPosition)>= minDistance)
                {
                    return spawnPosition;
                }
            }
            // if no valid spawn is found; return middle of the map
            return new Vector2(800, 800);
        }
    }
}
