using GameDevProject.Assets;
using GameDevProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameDevProject.GameStates
{
    public class PlayingState : IGameState
    {
        public GameAssets gameAssets;
        public PlayingState(Game1 game, GameAssets gameAssets)
        {
            camera = game._camera;
            this.gameAssets = gameAssets;
        }

        private Camera camera;
        public void Update(Game1 game, GameTime gameTime)
        {

            game.player.Update(gameTime, game._collisionManager);
            foreach (var entity in game.entities)
            {
                entity.Update(gameTime, game.player);
            }
            game._camera.Update(game.player.Position, 1600, 1600);
            if (game.player.IsHitting)
            {
                Rectangle swordHitbox = game.player.GetSwordHitbox();

                foreach (var entity in game.entities)
                {
                    if (swordHitbox.Intersects(entity.Bounds))
                    {
                        entity.Die(gameTime);
                    }
                }
            }
            if (game.gameStateManager.AllEnemiesDead(game.entities))
            {
                game.gameStateManager.ChangeGameState(new VictoryState(gameAssets, game.mainMenuBackground, game.gameStateManager, game.gameResetHandler));
            }
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            SpriteBatch _spriteBatch = game._spriteBatch;
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.Transform);

            for (int y = 0; y < game.tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < game.tileMap.GetLength(0); x++)
                {
                    string tileValue = game.tileMap[x, y];
                    int tileIndex = int.Parse(tileValue);

                    if (tileIndex >= 0 && tileIndex < game.tiles.Length)
                    {
                        _spriteBatch.Draw(game.tiles[tileIndex], new Rectangle(x * Game1.TILE_SIZE, y * Game1.TILE_SIZE, Game1.TILE_SIZE, Game1.TILE_SIZE), Color.White);
                    }
                }
            }
            foreach (var entity in game.entities)
            {
                entity.Draw(_spriteBatch);
            }
            game.player.Draw(_spriteBatch);
            _spriteBatch.End();
            _spriteBatch.Begin();
            game.healthRenderer.Draw(_spriteBatch, game.player.Health, game.player.MaxHealth);
            _spriteBatch.End();
        }
    }
}
