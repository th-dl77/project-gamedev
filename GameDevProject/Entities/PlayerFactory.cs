using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using GameDevProject.Input;

namespace GameDevProject.Entities
{
    public class PlayerFactory
    {
        public Player CreatePlayer(Texture2D spriteSheetTexture, Vector2 initPos)
        {
            IInputStrategy inputStrategy = new KeyboardInputStrategy();

            Animation runningAnimation = new Animation(new SpriteSheet(spriteSheetTexture, 56, 56, 112), new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation idleAnimation = new Animation(new SpriteSheet(spriteSheetTexture, 56, 56), new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation fightingAnimation = new Animation(new SpriteSheet(spriteSheetTexture, 56, 56, 56), new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 0.08f);
            Animation damageAnimation = new Animation(new SpriteSheet(spriteSheetTexture, 56, 56, 280), new int[] { 0, 1, 2, 3 }, 0.2f, true);
            Animation deathAnimation = new Animation(new SpriteSheet(spriteSheetTexture, 56, 56, 336), new int[] { 0, 1, 2, 3, 4, 5, 6, 7,8,9,10,11 }, 0.2f, false);

            Dictionary<string, Animation> animations = new Dictionary<string, Animation>
            {
                {"idle", idleAnimation },
                {"running",runningAnimation },
                {"fighting",fightingAnimation },
                { "damageAnimation",damageAnimation},
                { "deathAnimation", deathAnimation}
            };
            AnimationManager animationManager = new AnimationManager(animations);
            return new Player(inputStrategy, animations, initPos, animationManager);
        }
    }
}
