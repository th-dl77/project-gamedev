using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Animations
{
    public class Animation : IAnimation
    {
        private int[] frames;
        private float frameTime;
        private float timer;
        private int currentFrame;
        public bool IsLooping { get; set; }
        public SpriteSheet SpriteSheet { get; set; }

        public Animation(SpriteSheet spriteSheet, int[] frames, float frameTime, bool isLooping = true)
        {
            this.SpriteSheet = spriteSheet; 
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
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            SpriteSheet.DrawFrame(spriteBatch, frames[currentFrame], position);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects flip)
        {
            SpriteSheet.DrawFrame(spriteBatch, frames[currentFrame], position, flip);
        }
    }
}
