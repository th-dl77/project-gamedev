using GameDevProject.Assets;
using GameDevProject.Enemies;
using GameDevProject.PlayerClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;


namespace GameDevProject.GameStates
{
    public class PlayingState : IGameState
    {
        public GameAssets gameAssets;
        public EnemySpawner enemySpawner;
        private int currentLevel;
        public PlayingState(Game1 game, GameAssets gameAssets)
        {
            camera = game._camera;
            currentLevel = 0;
            this.gameAssets = gameAssets;
            enemySpawner = new EnemySpawner(game.Content, 1600, 1600, game.player.Position, gameAssets);
        }

        private Camera camera;
        public void Update(Game1 game, GameTime gameTime)
        {
            if (game.gameStateManager.AllEnemiesDead(game.entities))
            {
                currentLevel++;
                if (currentLevel == 4)
                {
                    game.gameStateManager.ChangeGameState(new VictoryState(gameAssets, gameAssets.GetTexture("mainMenuBackground"), game.gameStateManager, game.gameResetHandler));
                    currentLevel = 0;
                }
                else
                {
                    game.entities = enemySpawner.Spawn(currentLevel);
                    //game.collisionLoader.LoadEnemyCollidables(game.entities);
                }
            }
            game.player.Update(gameTime, game._collisionManager, game.entities);
            foreach (var entity in game.entities)
            {
                entity.Update(gameTime, game.player);
            }
            if (game.player.IsHitting)
            {
                HandlePlayerHitting(game, gameTime);
            }
            game.collisionLoader.UpdateEnemyCollidables(game.entities);
            game._camera.Update(game.player.Position, 1600, 1600);
        }

        private void HandlePlayerHitting(Game1 game, GameTime gameTime)
        {
            Rectangle swordHitbox = game.player.fightingHitboxHandler.GetSwordHitbox();

            foreach (var entity in game.entities)
            {
                if (swordHitbox.Intersects(entity.GetBounds()))
                {
                    entity.Die(gameTime);
                }
            }
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            SpriteBatch _spriteBatch = game._spriteBatch;
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.Transform);
            DrawTileMap(game);
            DrawEnemies(game);
            game.player.Draw(_spriteBatch);
            //debug
            //game.player.DrawBounds(_spriteBatch, gameAssets.GetTexture("debug"));
            //game._collisionManager.DrawCollidables(_spriteBatch, gameAssets.GetTexture("debug"));
            _spriteBatch.End();
            _spriteBatch.Begin();
            DrawUI(game);
            _spriteBatch.End();
        }
        private void DrawTileMap(Game1 game)
        {
            SpriteBatch spriteBatch = game._spriteBatch;
            for (int y = 0; y < game.tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < game.tileMap.GetLength(0); x++)
                {
                    string tileValue = game.tileMap[x, y];
                    int tileIndex = int.Parse(tileValue);

                    if (tileIndex >= 0 && tileIndex < game.tiles.Length)
                    {
                       spriteBatch.Draw(game.tiles[tileIndex], new Rectangle(x * Game1.TILE_SIZE, y * Game1.TILE_SIZE, Game1.TILE_SIZE, Game1.TILE_SIZE), Color.White);
                    }
                }
            }
        }
        private void DrawEnemies(Game1 game)
        {
            SpriteBatch spriteBatch = game._spriteBatch;
            foreach (var entity in game.entities)
            {
                entity.Draw(spriteBatch);
            }
        }
        private void DrawUI(Game1 game)
        {
            SpriteBatch spriteBatch = game._spriteBatch;
            game.healthRenderer.Draw(spriteBatch, game.player.Health, game.player.MaxHealth);
            spriteBatch.DrawString(gameAssets.GetFont("font"), $"Current level : {currentLevel}", new Vector2(10, 10), Color.Red, 0f, new Vector2(0, 0), 0.4f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(gameAssets.GetFont("font"), $"Enemies left: {game.entities.Count(e => e.IsAlive)}", new Vector2(10, 50), Color.Red, 0f, new Vector2(0, 0), 0.4f, SpriteEffects.None, 0f);
        }
    }
}
