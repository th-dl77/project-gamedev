using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using GameDevProject.Input;

namespace GameDevProject.Entities
{
    public static class PlayerFactory
    {
        public static Player CreatePlayer(IInputStrategy inputStrategy, Texture2D spriteSheetTexture, Vector2 initPos)
        {
            SpriteSheet runningAnimationSheet = new SpriteSheet(spriteSheetTexture, 56, 56, 112);
            SpriteSheet idleAnimationSheet = new SpriteSheet(spriteSheetTexture, 56, 56);
            SpriteSheet fightingAnimationSheet = new SpriteSheet(spriteSheetTexture, 56, 56, 56);
            SpriteSheet deathAnimationSheet = new SpriteSheet(spriteSheetTexture, 56, 56, 392);

            Animation runningAnimation = new Animation(runningAnimationSheet, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation idleAnimation = new Animation(idleAnimationSheet, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation fightingAnimation = new Animation(fightingAnimationSheet, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 0.08f);
            Animation deathAnimation = new Animation(deathAnimationSheet, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 0.2f, false);
            Animation dead = new Animation(deathAnimationSheet, new int[] { 0, 1, 2, 3 }, 0.2f, false);

            Dictionary<string, Animation> animations = new Dictionary<string, Animation>
            {
                {"idle", idleAnimation },
                {"running",runningAnimation },
                {"fighting",fightingAnimation },
                { "deathAnimation", deathAnimation},
                {"dead", dead }
            };


            return new Player(inputStrategy, animations, initPos);
        }
    }
}
