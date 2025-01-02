using GameDevProject.Animations;
using GameDevProject.Assets;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameDevProject.Enemies
{
    public class SkeletonEnemy : EnemyMelee, IEnemy
    {
        public SkeletonEnemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed, GameAssets gameAssets = null) : base(animations, startPosition, speed)
        {
            this.gameAssets = gameAssets;
        }
    }
}
