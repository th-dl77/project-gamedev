using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Map;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevProject.Assets;

namespace GameDevProject.GameStates
{
    public class GameResetHandler
    {
        private PlayerFactory playerFactory;
        private GameStateManager gameStateManager;
        private IMapLoader mapLoader;
        private CollisionManager collisionManager;
        private CollisionLoader collisionLoader;
        private List<IEntity> enemies;
        private EnemySpawner enemySpawner;
        private Game1 game;

        private GameAssets gameAssets;
        public GameResetHandler(Game1 game, GameAssets gameAssets, IMapLoader mapLoader, CollisionManager collisionManager, CollisionLoader collisionLoader, PlayerFactory playerFactory, EnemyFactory enemyFactory, GameStateManager gameStateManager, EnemySpawner enemySpawner, List<IEntity> entities)
        {
            this.playerFactory = playerFactory;
            this.mapLoader = mapLoader;
            this.gameStateManager = gameStateManager;
            this.collisionManager = collisionManager;
            this.collisionLoader = collisionLoader;
            this.game = game;
            this.enemies = entities;
            this.enemySpawner = enemySpawner;
        }
        public Player ResetGame()
        {
            game.entities.Clear();
            collisionManager.collidables.Clear();
            enemies.AddRange(enemySpawner.Spawn(1));

            string[,] tileMap = mapLoader.Load("Content/Tilemap.txt");
            collisionLoader.LoadCollidables(tileMap);
            Player player = playerFactory.CreatePlayer(gameAssets.PlayerTexture, new Vector2(800, 800));
            player.OnDeath += () => gameStateManager.ChangeGameState(new GameOverState(gameAssets.ButtonTexture, gameAssets.Font, gameAssets.DeathScreenBackground, gameStateManager, this));
            return player;
        }
    }
}
