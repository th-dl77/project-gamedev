using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.GameStates
{
    public class StartMenuState : IGameState
    {
        private Texture2D buttonTexture;
        private SpriteFont font;
        private Texture2D backgroundTexture;
        public StartMenuState(Texture2D buttonTexture, SpriteFont font, Texture2D backgroundTexture)
        {
            this.buttonTexture = buttonTexture;
            this.font = font;
            this.backgroundTexture = backgroundTexture;
        }
        public void Update(Game1 game, GameTime gameTime)
        {

        }
        public void Draw(Game1 game, GameTime gameTime)
        {

        }
    }
}
