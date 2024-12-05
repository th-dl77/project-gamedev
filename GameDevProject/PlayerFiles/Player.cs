﻿using Microsoft.Xna.Framework.Input;
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

namespace GameDevProject.PlayerFiles
{
    public class Player
    {
        private Animation runningAnimation;
        private Animation idleAnimation;
        private Animation fightingAnimation;
        private Animation currentAnimation;

        public Vector2 Velocity;
        public Vector2 Position { get; private set; }

        public Rectangle Bounds { get; private set; }

        private float speed;
        private bool isFacingLeft = false;

        public Player(Animation runningAnimation, Animation idleAnimation, Animation fightingAnimation, Vector2 startPosition, float speed = 200f)
        {
            this.runningAnimation = runningAnimation;
            this.idleAnimation = idleAnimation;
            this.fightingAnimation = fightingAnimation;
            Position = startPosition;
            this.speed = speed;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 56, 56);
            Velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
            currentAnimation.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            Velocity = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                if (Position.Y > 0)
                {
                    Velocity.Y -= 1;
                }

            }
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                if (Position.Y < 800)
                {
                    Velocity.Y += 1;
                }
            }
            if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))
            {
                if (Position.X > 0)
                {
                    Velocity.X -= 1;
                }
                isFacingLeft = true;

            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                if (Position.X < 800)
                {
                    Velocity.X += 1;
                }
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
            spriteBatch.Draw(currentAnimation.SpriteSheet.Texture, Position, currentAnimation.GetCurrentFrame(), Color.White, 0f, new Vector2(0, 0), 2f, flip, 0f);
        }
    }
}
