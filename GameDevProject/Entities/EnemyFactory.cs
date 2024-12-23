using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameDevProject.Entities
{
    public class EnemyFactory
    {
        private Texture2D skeletonTexture;
        private Texture2D golemRun;
        private Texture2D golemAttack;
        private Texture2D golemDeath;

        public EnemyFactory(ContentManager content)
        {
            skeletonTexture = content.Load<Texture2D>("Skeleton enemy");
            golemRun = content.Load<Texture2D>("Golem_Run");
            golemAttack = content.Load<Texture2D>("Golem_AttackA");
            golemDeath = content.Load<Texture2D>("Golem_DeathB");
        }
        public Enemy CreateEnemy(string enemyType, Vector2 Position)
        {
            switch (enemyType)
            {
                case "skeleton":
                    return CreateSkeletonEnemy(Position);
                case "golem":
                    return CreateGolemEnemy(Position);
                default:
                    throw new System.ArgumentException($"Unknown enemy type: {enemyType}");
            }
        }
        public Enemy CreateSkeletonEnemy(Vector2 position)
        {
            Dictionary<string, Animation> skeletonMeleeAnimations = new Dictionary<string, Animation>()
                    {
                        {"idle", new Animation(new SpriteSheet(skeletonTexture,64,64,192), new int[] {0,1,2,3 },0.2f) },
                        { "walk", new Animation(new SpriteSheet(skeletonTexture,64,64,128), new int[] {0,1,2,3,4,5,6,7,8,9,10,11 },0.2f)},
                        { "fight", new Animation(new SpriteSheet(skeletonTexture,64,64), new int[] { 0,1,2,3,4,5,6,7,8,9,10,11,12},0.2f)},
                        { "death", new Animation(new SpriteSheet(skeletonTexture,64,64,64), new int[] { 0,1,2,3,4,5,6,7,8,9,10,11},0.2f,false)}
                    };
            return new EnemyMelee(skeletonMeleeAnimations, position, 25f);
        }
        public Enemy CreateGolemEnemy(Vector2 position)
        {
            Dictionary<string, Animation> golemMeleeAnimations = new Dictionary<string, Animation>()
            {
                { "walk", new Animation(new SpriteSheet(golemRun,64,64), new int[] { 0,1,2,3},0.3f) },
                { "fight", new Animation(new SpriteSheet(golemAttack,64,64),new int[] { 0,1,2,3,4,5,6,7,8,9,10,11},0.2f)},
                { "death", new Animation(new SpriteSheet(golemDeath,64,64), new int[] { 0,1,2,3,4,5,6,7,8},0.2f,false)}
            };
            List<Vector2> patrolPoints = new List<Vector2>();
            patrolPoints.Add(new Vector2(100, 100));
            patrolPoints.Add(new Vector2(250, 100));
            patrolPoints.Add(new Vector2(450, 125));
            patrolPoints.Add(new Vector2(700, 100));
            return new GolemEnemy(golemMeleeAnimations, position, 40f, patrolPoints);
        }
    }
}
