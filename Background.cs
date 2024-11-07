using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject
{
    internal class Background : Sprite
    {
        public Background(Rectangle rectangle, Vector2 position) : base(rectangle, position)
        {
        }
        public override void LoadContent(ContentManager content)
        {
            content.Load<Texture2D>("skybackground");
        }
    }
}
