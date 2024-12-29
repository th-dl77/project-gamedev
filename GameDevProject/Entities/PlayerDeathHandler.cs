using GameDevProject.GameStates;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Entities
{
    public class PlayerDeathHandler
    {
        private readonly GameStateManager gameStateManager;
        private readonly GameResetHandler gameResetHandler;
        public PlayerDeathHandler(GameStateManager gameStateManager, GameResetHandler gameResetHandler)
        {
            this.gameStateManager = gameStateManager;
            this.gameResetHandler = gameResetHandler;
        }
        public void HandleDeath(Player player, Texture2D buttonTexture, SpriteFont font, Texture2D deathScreenBackground)
        {
            player.OnDeath += () => OnDeath(buttonTexture, font, deathScreenBackground);
        }
        private void OnDeath(Texture2D buttonTexture, SpriteFont font, Texture2D deathScreenBackground)
        {
            gameStateManager.ChangeGameState(new GameOverState(buttonTexture, font, deathScreenBackground, gameStateManager, gameResetHandler));
        }
    }
}
