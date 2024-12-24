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

namespace GameDevProject.GameStates
{
    public class GameResetHandler
    {
        private PlayerFactory playerFactory;
        private Texture2D buttonTexture;
        private SpriteFont font;
        private GameStateManager gameStateManager;
        private Texture2D deathScreenBackground;
        private IMapLoader mapLoader;
        private CollisionManager collisionManager;
        private CollisionLoader collisionLoader;
        private Texture2D playerTexture;
        private List<IEntity> enemies;
        private Game1 game;
        public GameResetHandler(Game1 game, Texture2D playerTexture, IMapLoader mapLoader, CollisionManager collisionManager, CollisionLoader collisionLoader, PlayerFactory playerFactory, EnemyFactory enemyFactory, Texture2D buttonTexture, SpriteFont font, Texture2D deathScreenBackground, GameStateManager gameStateManager)
        {
            this.playerFactory = playerFactory;
            this.playerTexture = playerTexture;
            this.mapLoader = mapLoader;
            this.buttonTexture = buttonTexture;
            this.font = font;
            this.deathScreenBackground = deathScreenBackground;
            this.gameStateManager = gameStateManager;
            this.collisionManager = collisionManager;
            this.collisionLoader = collisionLoader;
            this.game = game;
        }
        public Player ResetGame()
        {
            game.entities.Clear();
            collisionManager.collidables.Clear();
            game.entities = game.enemySpawner.Spawn(1);

            string[,] tileMap = mapLoader.Load("Content/Tilemap.txt");
            collisionLoader.LoadCollidables(tileMap);
            Player player = playerFactory.CreatePlayer(playerTexture, new Vector2(200, 200));
            player.OnDeath += () => gameStateManager.ChangeGameState(new GameOverState(buttonTexture, font, deathScreenBackground, gameStateManager, this));
            return player;
        }
    }
}
