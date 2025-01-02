using GameDevProject.Assets;
using GameDevProject.GameStates;
using System.Threading.Tasks;

namespace GameDevProject.Entities
{
    public class PlayerDeathHandler
    {
        private readonly GameStateManager gameStateManager;
        private readonly GameResetHandler gameResetHandler;
        private Player player;

        private GameAssets gameAssets;
        public PlayerDeathHandler(GameStateManager gameStateManager, GameResetHandler gameResetHandler, GameAssets gameAssets, Player player)
        {
            this.gameStateManager = gameStateManager;
            this.gameResetHandler = gameResetHandler;
            this.gameAssets = gameAssets;
            this.player = player;
        }
        public void HandleDeath()
        {
            player.OnDeath += () => OnDeath();
        }
        private async Task OnDeath()
        {
            await Task.Delay(3000);
            gameStateManager.ChangeGameState(new GameOverState(gameAssets, gameStateManager, gameResetHandler));
        }
    }
}
