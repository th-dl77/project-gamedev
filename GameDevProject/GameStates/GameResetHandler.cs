using GameDevProject.Collisions;
using GameDevProject.Entities;
using GameDevProject.Map;
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
        private List<IEntity> enemies;
        public GameResetHandler(PlayerFactory playerFactory, IMapLoader mapLoader, CollisionManager collisionManager, List<IEntity> enemies)
        {
            this.playerFactory = playerFactory;
            this.mapLoader = mapLoader;
            this.collisionManager = collisionManager;
            this.enemies = enemies;
        }
        public Player ResetGame()
        {
            enemies.Clear();
            collisionManager
        }
    }
}
