using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Entities
{
    public class EnemyFactory
    {
        private SpriteSheet enemyMeleeSpriteSheetIdle;
        private SpriteSheet enemyMeleeSpriteSheetWalk;
        private SpriteSheet enemyMeleeSpriteSheetFight;

        public EnemyFactory(ContentManager content)
        {
            enemyMeleeSpriteSheetIdle = new SpriteSheet(content.Load<Texture2D>("Skeleton enemy"),64,64,192);
            enemyMeleeSpriteSheetWalk = new SpriteSheet(content.Load<Texture2D>("Skeleton enemy"), 64, 64,128);
            enemyMeleeSpriteSheetFight = new SpriteSheet(content.Load<Texture2D>("Skeleton enemy"), 64, 64);
        }
        public Enemy CreateEnemy(Vector2 Position)
        {
            Dictionary<string, Animation> enemyMeleeAnimations = new Dictionary<string, Animation>()
            {
                {"idle", new Animation(enemyMeleeSpriteSheetIdle, new int[] {0,1,2,3 },0.2f) },
                { "walk", new Animation(enemyMeleeSpriteSheetWalk, new int[] {0,1,2,3,4,5,6,7,8,9,10,11 },0.2f)},
                { "fight", new Animation(enemyMeleeSpriteSheetFight, new int[] { 0,1,2,3,4,5,6,7,8,9,10,11,12},0.1f)}
            };
            return new EnemyMelee(enemyMeleeAnimations, Position, 25f);
        }
    }
}
