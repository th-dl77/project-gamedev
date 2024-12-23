using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameDevProject.Entities
{
    public class EnemyFactory
    {
        private Texture2D enemyTexture;

        public EnemyFactory(ContentManager content)
        {
            enemyTexture = content.Load<Texture2D>("Skeleton enemy");
        }
        public Enemy CreateEnemy(string enemyType, Vector2 Position)
        {
            switch (enemyType)
            {
                case "skeleton":
                    return createSkeletonEnemy(Position);
                default:
                    throw new System.ArgumentException($"Unknown enemy type: {enemyType}");
            }
        }
        public Enemy createSkeletonEnemy(Vector2 position)
        {
            Dictionary<string, Animation> enemyMeleeAnimations = new Dictionary<string, Animation>()
                    {
                        {"idle", new Animation(new SpriteSheet(enemyTexture,64,64,192), new int[] {0,1,2,3 },0.2f) },
                        { "walk", new Animation(new SpriteSheet(enemyTexture,64,64,128), new int[] {0,1,2,3,4,5,6,7,8,9,10,11 },0.2f)},
                        { "fight", new Animation(new SpriteSheet(enemyTexture,64,64), new int[] { 0,1,2,3,4,5,6,7,8,9,10,11,12},0.2f)},
                        { "death", new Animation(new SpriteSheet(enemyTexture,64,64,64), new int[] { 0,1,2,3,4,5,6,7,8,9,10,11},0.2f,false)}
                    };
            return new EnemyMelee(enemyMeleeAnimations, position, 25f);
        }
    }
}
