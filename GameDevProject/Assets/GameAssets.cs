using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Assets
{
    public class GameAssets
    {
        public Texture2D ButtonTexture { get; }
        public SpriteFont Font { get; }
        public Texture2D DeathScreenBackground { get; }
        public Texture2D PlayerTexture { get; }

        public GameAssets(Texture2D buttonTexture, SpriteFont font, Texture2D deathScreenBackground, Texture2D playerTexture)
        {
            ButtonTexture = buttonTexture;
            Font = font;
            DeathScreenBackground = deathScreenBackground;
            PlayerTexture = playerTexture;
        }
    }
}
