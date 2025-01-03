using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Map;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameDevProject.Assets;
using GameDevProject.Enemies;

namespace GameDevProject.GameStates
{
    public class GameResetHandler : IResetHandler
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
            ClearEntities();
            ClearCollidables();
            LoadMapCollidables();
            Player player = CreatePlayer();
            player.OnDeath += () => gameStateManager.ChangeGameState(new GameOverState(gameAssets, gameStateManager, this));
            return player;
        }
        private void ClearEntities()
        {
            game.entities.Clear();
        }
        private void ClearCollidables()
        {
            collisionManager.Collidables.Clear();
        }

        private void LoadMapCollidables()
        {
            string[,] tileMap = mapLoader.Load("Content/Tilemap.txt");
            collisionLoader.LoadCollidables(tileMap);
        }
        private Player CreatePlayer()
        {
            return playerFactory.CreatePlayer(gameAssets.GetTexture("player"), new Vector2(800, 800));
        }
    }
}
