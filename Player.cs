using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;


namespace GameDevProject
{
    internal class Player : Sprite
    {
        private float Speed = 200f;
        public Player(Rectangle rectangle, Vector2 position) : base(rectangle, position)
        {
        }
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Position.X <550)
            {
                Position.X += Speed * deltaTime;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && Position.X > 0)
            {
                Position.X -= Speed * deltaTime;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Position.Y > 0)
            {
                Position.Y -= Speed * deltaTime;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Position.Y < 760)
            {
                Position.Y += Speed * deltaTime;
            }

        }
    }
}
