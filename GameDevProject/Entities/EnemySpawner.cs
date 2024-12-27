using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace GameDevProject.Entities
{
    public class EnemySpawner
    {
        private EnemyFactory enemyFactory;

        public EnemySpawner(ContentManager content)
        {
            enemyFactory = new EnemyFactory(content);
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

            for (int i = 0; i < 1400; i += 100)
            {
                entities.Add(enemyFactory.CreateSkeletonEnemy(new Vector2(100 + i, 200 + i)));
            }
            //entities.Add(enemyFactory.CreateEnemy("golem", new Vector2(100, 100)));
            entities.Add(enemyFactory.CreateSlimeEnemy(new Vector2(100, 200)));
            entities.Add(enemyFactory.CreateBatEnemy(new Vector2(80, 200)));
            entities.Add(enemyFactory.CreateGolemEnemy(new Vector2(80,200),new List<Vector2>() { new Vector2(100,200), new Vector2(200,100), new Vector2(240,300)}));
            return entities;
        }
    }
}
