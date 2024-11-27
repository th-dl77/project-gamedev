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
        private Animation runningAnimation;
        private Animation idleAnimation;
        private Animation currentAnimation;
        private Vector2 position;
        private float speed;
        private bool isFacingLeft = false;

        public Player(Animation runningAnimation, Animation idleAnimation, Vector2 startPosition, float speed = 200f)
        {
            this.runningAnimation = runningAnimation;
            this.idleAnimation = idleAnimation;
            this.position = startPosition;
            this.speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            currentAnimation.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            Vector2 velocity = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 1;
            }
            if (state.IsKeyDown(Keys.S)|| state.IsKeyDown(Keys.Down))
            {
                velocity.Y += 1;
            }
            if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1;
                isFacingLeft = true;
            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                velocity.X += 1;
                isFacingLeft = false;
            }

            //if velocity is not zero: apply the running animation
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize(); //used to fix diagional movement
                currentAnimation = runningAnimation;
            }
            else
            {
                currentAnimation = idleAnimation;
            }
            position += velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //used to flip the animation when walking left
            SpriteEffects flip;
            if (isFacingLeft)
                flip = SpriteEffects.FlipHorizontally;
            else
                flip = SpriteEffects.None;

            //draw player on screen
            spriteBatch.Draw(currentAnimation.SpriteSheet.Texture, position, currentAnimation.GetCurrentFrame(), Color.White, 0f, new Vector2(0, 0), 2f, flip, 0f);
        }
    }
}
