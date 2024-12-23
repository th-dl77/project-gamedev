﻿using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Map;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameStates
{
    public class GameResetHandler
    {
        private PlayerFactory playerFactory;
        private IMapLoader mapLoader;
        private CollisionManager collisionManager;
        private CollisionLoader collisionLoader;
        private Texture2D playerTexture;
        private List<IEntity> enemies;
        private EnemyFactory enemyFactory;
        public GameResetHandler(Texture2D playerTexture, IMapLoader mapLoader, CollisionManager collisionManager, List<IEntity> enemies, CollisionLoader collisionLoader, PlayerFactory playerFactory, EnemyFactory enemyFactory)
        {
            this.playerFactory = playerFactory;
            this.playerTexture = playerTexture;
            this.mapLoader = mapLoader;
            this.collisionManager = collisionManager;
            this.collisionLoader = collisionLoader;
            this.enemyFactory = enemyFactory;
            this.enemies = enemies;
        }
        public Player ResetGame()
        {
            enemies.Clear();
            collisionManager.collidables.Clear();
            for (int i = 0; i < 1000; i += 100)
            {
                enemies.Add(enemyFactory.CreateEnemy(new Vector2(300 + i, 400 + i)));
            }

            string[,] tileMap = mapLoader.Load("Content/Tilemap.txt");
            collisionLoader.LoadCollidables(tileMap);
            Player player = playerFactory.CreatePlayer(playerTexture, new Vector2(200, 200));
            return player;
        }
    }
}