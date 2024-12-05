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
using GameDevProject.Collisions;

namespace GameDevProject.PlayerFiles
{
    public class Player
    {
        private Animation runningAnimation;
        private Animation idleAnimation;
        private Animation fightingAnimation;
        private Animation currentAnimation;

        public int SpriteHeight { get; private set; } = 56;
        public int SpriteWidth { get; private set; } = 56;

        public Vector2 Velocity;
        public Vector2 Position { get; private set; }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)Position.X + 25,
                    (int)Position.Y+45, 
                    (int)((SpriteWidth -25) * Scale),                    
                    (int)((SpriteHeight - 20)* Scale)                    
                );
            }
        }


        public float Scale { get; private set; } = 2f; 
        private float speed;
        private bool isFacingLeft = false;

        public Player(Animation runningAnimation, Animation idleAnimation, Animation fightingAnimation, Vector2 startPosition, float speed = 200f)
        {
            this.runningAnimation = runningAnimation;
            this.idleAnimation = idleAnimation;
            this.fightingAnimation = fightingAnimation;
            Position = startPosition;
            this.speed = speed;
            Velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            HandleInput(gameTime);
            var proposedPosition = Position + Velocity;
            var resolvedPosition = collisionManager.ResolveCollisions(this, proposedPosition);
            Position = resolvedPosition;
            currentAnimation.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            Vector2 velocity = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                Velocity.Y -= 1;

            }
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                Velocity.Y += 1;
            }
            if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))
            {
                Velocity.X -= 1;
                isFacingLeft = true;

            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                Velocity.X += 1;
                isFacingLeft = false;
            }


            //if velocity is not zero: apply the running animation
            if (Velocity != Vector2.Zero)
            {
                Velocity.Normalize(); //used to fix diagonal movement, otherwise way too fast
                currentAnimation = runningAnimation;
            }
            else if (state.IsKeyDown(Keys.Space))
            {
                currentAnimation = fightingAnimation;
            }
            else
            {
                currentAnimation = idleAnimation;
            }
            Position += Velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            spriteBatch.Draw(
                currentAnimation.SpriteSheet.Texture,
                Position, 
                currentAnimation.GetCurrentFrame(), 
                Color.White, 
                0f, 
                new Vector2(0, 0), 
                Scale, 
                flip, 
                0f);
        }

        public void DrawBounds(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            spriteBatch.Draw(debugTexture, Bounds, Color.Blue * 0.5f);
        }
    }
}
