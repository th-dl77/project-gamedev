﻿using GameDevProject.Assets;
using GameDevProject.GameStates;

namespace GameDevProject.Entities
{
    public class PlayerDeathHandler
    {
        private readonly GameStateManager gameStateManager;
        private readonly GameResetHandler gameResetHandler;

        private GameAssets gameAssets;
        public PlayerDeathHandler(GameStateManager gameStateManager, GameResetHandler gameResetHandler, GameAssets gameAssets)
        {
            this.gameStateManager = gameStateManager;
            this.gameResetHandler = gameResetHandler;
            this.gameAssets = gameAssets;
        }
        public void HandleDeath(Player player)
        {
            player.OnDeath += () => OnDeath();
        }
        private void OnDeath()
        {
            gameStateManager.ChangeGameState(new GameOverState(gameAssets, gameStateManager, gameResetHandler));
        }
    }
}