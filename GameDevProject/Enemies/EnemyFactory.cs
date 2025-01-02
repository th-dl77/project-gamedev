﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using GameDevProject.Assets;
using GameDevProject.Animations;
using GameDevProject.Entities;
using System;

namespace GameDevProject.Enemies
{
    public class EnemyFactory
    {
        private Texture2D skeletonTexture;
        private Texture2D golemRun;
        private Texture2D golemAttack;
        private Texture2D golemDeath;
        private Texture2D slimeDeath;
        private Texture2D slimeRun;
        private Texture2D slimeFight;
        private Texture2D batFight;
        private Texture2D batFly;
        private GameAssets gameAssets;
        private Texture2D batDeath;

        public EnemyFactory(ContentManager content, GameAssets gameAssets)
        {
            skeletonTexture = content.Load<Texture2D>("Skeleton enemy");
            golemRun = content.Load<Texture2D>("Golem_Run");
            golemAttack = content.Load<Texture2D>("Golem_AttackA");
            golemDeath = content.Load<Texture2D>("Golem_DeathB");
            slimeDeath = content.Load<Texture2D>("Slime_Spiked_Death");
            slimeRun = content.Load<Texture2D>("Slime_Spiked_Run");
            slimeFight = content.Load<Texture2D>("Slime_Spiked_Ability");
            batFight = content.Load<Texture2D>("Bat_Attack");
            batFly = content.Load<Texture2D>("Bat_Fly");
            batDeath = content.Load<Texture2D>("Bat_Death");
            this.gameAssets = gameAssets;
        }
        public Enemy CreateSkeletonEnemy(Vector2 position)
        {
            Dictionary<string, Animation> skeletonMeleeAnimations = new Dictionary<string, Animation>()
                    {
                        {"idle", new Animation(new SpriteSheet(skeletonTexture,64,64,192), new int[] {0,1,2,3 },0.2f) },
                        { "walk", new Animation(new SpriteSheet(skeletonTexture,64,64,128), new int[] {0,1,2,3,4,5,6,7,8,9,10,11 },0.2f)},
                        { "fight", new Animation(new SpriteSheet(skeletonTexture,64,64), new int[] { 0,1,2,3,4,5,6,7,8,9,10,11,12},0.2f)},
                        { "death", new Animation(new SpriteSheet(skeletonTexture,64,64,64), new int[] { 0,1,2,3,4,5,6,7,8,9,10,11,12},0.2f,false)}
                    };
            return new SkeletonEnemy(skeletonMeleeAnimations, position, 25f, gameAssets);
        }
        public Enemy CreateGolemEnemy(Vector2 position)
        {
            Dictionary<string, Animation> golemMeleeAnimations = new Dictionary<string, Animation>()
            {
                { "walk", new Animation(new SpriteSheet(golemRun,64,64), new int[] { 0,1,2,3},0.3f) },
                { "fight", new Animation(new SpriteSheet(golemAttack,64,64),new int[] { 0,1,2,3,4,5,6,7,8,9,10,11},0.2f)},
                { "death", new Animation(new SpriteSheet(golemDeath,64,64), new int[] { 0,1,2,3,4,5,6,7,8},0.2f,false)}
            };
            List<Vector2> patrolPoints = GenerateRandomPatrolPoints(4, 0, 800, 0, 800);
            return new GolemEnemy(golemMeleeAnimations, position, 40f, patrolPoints);
        }
        public Enemy CreateSlimeEnemy(Vector2 position)
        {
            Dictionary<string, Animation> slimeEnemyAnimations = new Dictionary<string, Animation>()
            {
                { "walk", new Animation(new SpriteSheet(slimeRun,64,64), new int[] { 0,1,2,3},0.2f) },
                { "death", new Animation(new SpriteSheet(slimeDeath,64,64), new int[] { 0,1,2,3,4},0.2f,false)},
                { "fight", new Animation(new SpriteSheet(slimeFight,64,64), new int[] { 0,1,2,3},0.2f)}
            };
            List<Vector2> patrolPoints = GenerateRandomPatrolPoints(4, 400, 800, 200, 800);
            return new SlimeEnemy(slimeEnemyAnimations, position, 40f, patrolPoints);
        }
        public Enemy CreateBatEnemy(Vector2 position)
        {
            Dictionary<string, Animation> batEnemyAnimations = new Dictionary<string, Animation>()
            {
                { "walk", new Animation(new SpriteSheet(batFly,64,64), new int[] { 0,1,2,3},0.2f) },
                { "death", new Animation(new SpriteSheet(batDeath,64,64), new int[] { 0,1,2,3,4,5,6,7,8,9,10,11},0.2f,false)},
                { "fight", new Animation(new SpriteSheet(batFight,64,64), new int[] { 0,1,2,3,4,5},0.2f)}
            };
            List<Vector2> patrolPoints = GenerateRandomPatrolPoints(4,0,1600,0,1600);
            return new BatEnemy(batEnemyAnimations, position, 150f, patrolPoints);
        }

        public List<Vector2> GenerateRandomPatrolPoints(int patrolPointCount, int minX, int maxX, int minY, int maxY)
        {
            List<Vector2> patrolPoints = new List<Vector2>();
            Random random = new Random();

            for (int i = 0; i < patrolPointCount; i++)
            {
                float x = random.Next(minX, maxX);
                float y = random.Next(minY, maxY);
                patrolPoints.Add(new Vector2(x, y));
            }

            return patrolPoints;
        }
    }
}
