﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Entities
{
    public static class PlayerFactory
    {
        public static Player CreatePlayer(Texture2D spriteSheetTexture, Camera camera, Vector2 initPos)
        {
            SpriteSheet runningAnimationSheet = new SpriteSheet(spriteSheetTexture, 56, 56, 112);
            SpriteSheet idleAnimationSheet = new SpriteSheet(spriteSheetTexture, 56, 56);
            SpriteSheet fightingAnimationSheet = new SpriteSheet(spriteSheetTexture, 56, 56, 56);

            Animation runningAnimation = new Animation(runningAnimationSheet, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation idleAnimation = new Animation(idleAnimationSheet, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation fightingAnimation = new Animation(fightingAnimationSheet, new int[] { 0, 1, 2, 3, 4, 5,6,7 }, 0.2f);

            return new Player(runningAnimation, idleAnimation, fightingAnimation, initPos, camera);
        }
    }
}
