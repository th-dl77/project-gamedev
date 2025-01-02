using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDevProject.Assets;

namespace GameDevProject.Entities
{
    public class HealthRenderer
    {
        private GameAssets gameAssets;
        private Vector2 startPos;
        private int spacing;

        public HealthRenderer(GameAssets gameAssets, Vector2 startPos, int spacing = 40)
        {
            this.gameAssets = gameAssets;
            this.startPos = startPos;
            this.spacing = spacing;
        }
        public void Draw(SpriteBatch spriteBatch, int currentHealth, int maxHealth)
        {
            for (int i = 0; i < maxHealth; i++)
            {
                if (i < currentHealth)
                {
                    spriteBatch.Draw(gameAssets.GetTexture("heartFull"), startPos + new Vector2(i * spacing, 0), Color.White);
                }
                else
                {
                    spriteBatch.Draw(gameAssets.GetTexture("heartEmpty"), startPos + new Vector2(i * spacing, 0), Color.White);
                }
            }
        }
    }
}
