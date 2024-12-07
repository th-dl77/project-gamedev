using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Entities
{
    public class Animation
    {
        public SpriteSheet _spriteSheet;

        private int[] frames;
        private float frameTime;
        private float timer;
        private int currentFrame;
        public bool IsLooping { get; set; }

        public Animation(SpriteSheet spriteSheet, int[] frames, float frameTime, bool isLooping = true)
        {
            _spriteSheet = spriteSheet;
            this.frames = frames;
            this.frameTime = frameTime;
            IsLooping = isLooping;
            timer = 0f;
            currentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= frameTime)
            {
                timer -= frameTime;
                currentFrame++;

                if (currentFrame >= frames.Length)
                {
                    currentFrame = IsLooping ? 0 : frames.Length - 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects flip = SpriteEffects.None)
        {
            _spriteSheet.DrawFrame(spriteBatch, frames[currentFrame], position, flip);
        }
    }
}
