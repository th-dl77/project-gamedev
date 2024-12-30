﻿using GameDevProject.Assets;
using GameDevProject.Entities;
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
            enemySpawner = new EnemySpawner(game.Content, 1600, 1600, game.player.Position, game.collisionLoader, gameAssets);
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
            game._camera.Update(game.player.Position, 1600, 1600);
            if (game.player.IsHitting)
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
            game.collisionLoader.UpdateEnemyCollidables(game.entities);
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
            //debug
            //game.player.DrawBounds(_spriteBatch, gameAssets.GetTexture("debug"));
            //game._collisionManager.DrawCollidables(_spriteBatch, gameAssets.GetTexture("debug"));
            _spriteBatch.End();
            _spriteBatch.Begin();
            game.healthRenderer.Draw(_spriteBatch, game.player.Health, game.player.MaxHealth);
            _spriteBatch.DrawString(gameAssets.GetFont("font"), $"Current level : {currentLevel}", new Vector2(10, 10), Color.Red,0f,new Vector2(0,0),0.4f,SpriteEffects.None,0f);
            _spriteBatch.DrawString(gameAssets.GetFont("font"), $"Enemies left: {game.entities.Count(e => e.IsAlive)}", new Vector2(10, 50), Color.Red, 0f, new Vector2(0, 0), 0.4f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
    }
}
