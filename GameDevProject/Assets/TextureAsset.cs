using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Assets
{
    public class TextureAsset
    {
        public Texture2D Texture { get; private set; }

        public TextureAsset(Texture2D texture)
        {
            Texture = texture;
        }
    }
}
