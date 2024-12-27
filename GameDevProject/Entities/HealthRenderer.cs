using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Entities
{
    public class HealthRenderer
    {
        private Texture2D heartTextureFull;
        private Texture2D heartTextureEmpty;
        private Vector2 startPos;
        private int spacing;

        public HealthRenderer(Texture2D heartTextureFull, Texture2D heartTextureEmpty, Vector2 startPos, int spacing = 30)
        {
            this.heartTextureFull = heartTextureFull;
            this.heartTextureEmpty = heartTextureEmpty;
            this.startPos = startPos;
            this.spacing = spacing;
        }
        public void Draw(SpriteBatch spriteBatch, int currentHealth, int maxHealth)
        {
            for (int i = 0; i < maxHealth; i++)
            {
                if (i < currentHealth)
                {
                    spriteBatch.Draw(heartTextureFull, startPos + new Vector2(i * spacing, 0), Color.White);
                }
                else
                {
                    spriteBatch.Draw(heartTextureFull, startPos + new Vector2(i * spacing, 0), Color.White);
                }
            }
        }
    }
}
