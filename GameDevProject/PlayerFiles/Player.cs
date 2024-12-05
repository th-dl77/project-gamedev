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

        private float Speed { get; } = 100f;

        private Camera _camera;
        public float Scale { get; private set; } = 2f;
        private bool isFacingLeft = false;
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

        public Player(Animation runningAnimation, Animation idleAnimation, Animation fightingAnimation, Vector2 startPosition, Camera _camera)
        {
            this.runningAnimation = runningAnimation;
            this.idleAnimation = idleAnimation;
            this.fightingAnimation = fightingAnimation;
            Position = startPosition;
            this._camera = _camera;
        }

        public void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            HandleInput(gameTime);
            Vector2 proposedPosition = Position + Velocity * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 resolvedPosition = collisionManager.ResolveCollisions(this, proposedPosition);
            Position = resolvedPosition;
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
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
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
                velocity.Normalize(); //used to fix diagonal movement, otherwise way too fast
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
            Velocity = velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //used to flip the animation when walking left
            SpriteEffects flip;
            if (isFacingLeft)
                flip = SpriteEffects.FlipHorizontally;
            else
                flip = SpriteEffects.None;

            spriteBatch.Draw(
                currentAnimation.SpriteSheet.Texture,
                Position,
                currentAnimation.GetCurrentFrame(),
                Color.White,
                0f,
                new Vector2(0, 0),
                Scale,
                flip,
                0f
            );
        }

        public void DrawBounds(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            spriteBatch.Draw(debugTexture, Bounds, Color.Blue * 0.5f);
        }
    }
}
