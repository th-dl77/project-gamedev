using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Input
{
    public class KeyboardInputStrategy : IInputStrategy
    {
        public Vector2 GetMovementInput()
        {
            Vector2 velocity = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up)) velocity.Y -= 1;
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down)) velocity.Y += 1;
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) velocity.X -= 1;
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) velocity.X += 1;
            return velocity;
        }

        public bool IsActionPressed(string action)
        {
            if (action == "fight")
                return Keyboard.GetState().IsKeyDown(Keys.Space);

            return false;
        }
    }
}
