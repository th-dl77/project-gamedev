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
    public class Player
    {
        private Animation animation; 
        private Vector2 position;
        private float speed;

        public Player(Animation animation, Vector2 startPosition, float speed = 200f)
        {
            this.animation = animation;
            this.position = startPosition;
            this.speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            animation.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W)) // Move up
                position.Y -= speed * deltaTime;
            if (state.IsKeyDown(Keys.S)|| state.IsKeyDown(Keys.Down)) // Move down
                position.Y += speed * deltaTime;
            if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left)) // Move left
                position.X -= speed * deltaTime;
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) // Move right
                position.X += speed * deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation.SpriteSheet.Texture,position,animation.GetCurrentFrame(),Color.White);
        }
    }
}
