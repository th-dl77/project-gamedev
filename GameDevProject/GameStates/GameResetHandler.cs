using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Map;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameDevProject.Assets;
using GameDevProject.Enemies;

namespace GameDevProject.GameStates
{
    public class GameResetHandler
    {
        private PlayerFactory playerFactory;
        private GameStateManager gameStateManager;
        private IMapLoader mapLoader;
        private CollisionManager collisionManager;
        private CollisionLoader collisionLoader;
        private List<IEnemy> enemies;
        private EnemySpawner enemySpawner;
        private Game1 game;

        private GameAssets gameAssets;
        public GameResetHandler(Game1 game, GameAssets gameAssets, IMapLoader mapLoader, CollisionManager collisionManager, CollisionLoader collisionLoader, PlayerFactory playerFactory, GameStateManager gameStateManager, EnemySpawner enemySpawner, List<IEnemy> entities)
        {
            this.playerFactory = playerFactory;
            this.mapLoader = mapLoader;
            this.gameStateManager = gameStateManager;
            this.collisionManager = collisionManager;
            this.collisionLoader = collisionLoader;
            this.game = game;
            this.enemies = entities;
            this.gameAssets = gameAssets;
            this.enemySpawner = enemySpawner;
        }
        public Player ResetGame()
        {
            game.entities.Clear();
            collisionManager.collidables.Clear();

            string[,] tileMap = mapLoader.Load("Content/Tilemap.txt");
            collisionLoader.LoadCollidables(tileMap);
            Player player = playerFactory.CreatePlayer(gameAssets.GetTexture("player"), new Vector2(800, 800));
            player.OnDeath += () => gameStateManager.ChangeGameState(new GameOverState(gameAssets, gameStateManager, this));
            return player;
        }
    }
}
