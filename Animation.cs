using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    public class Animation
    {
        public SpriteSheet SpriteSheet { get; }

        private int[] frames;
        private float frameTime;
        private float timer;
        private int currentFrame;
        public bool IsLooping { get; set; }

        public Animation(SpriteSheet spriteSheet, int[] frames, float frameTime, bool isLooping = true)
        {
            this.SpriteSheet = spriteSheet;
            this.frames = frames;
            this.frameTime = frameTime;
            this.IsLooping = isLooping;
            this.timer = 0f;
            this.currentFrame = 0;
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

        public Rectangle GetCurrentFrame()
        {
            return SpriteSheet.GetFrame(frames[currentFrame]);
        }
    }
}
