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


namespace GameDevProject
{
    internal class Movement
    {
        private Vector2 movementVector = new Vector2(300,640);
        public Vector2 CheckHorizontal()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (movementVector.X < 550)
                {
                    return new Vector2(movementVector.X += 5, movementVector.Y);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (movementVector.X > 0)
                {
                    return new Vector2(movementVector.X -= 5, movementVector.Y);
                }
            }
            return new Vector2(movementVector.X, movementVector.Y);
        }
        public Vector2 CheckVertical()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (movementVector.Y > 0)
                {
                    return new Vector2(movementVector.X, movementVector.Y -= 3);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (movementVector.Y < 760)
                {
                    return new Vector2(movementVector.X, movementVector.Y += 3);
                }
            }
            return new Vector2(movementVector.X, movementVector.Y);
        }
    }
}
