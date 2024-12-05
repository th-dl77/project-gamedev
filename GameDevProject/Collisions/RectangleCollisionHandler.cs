using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Collisions
{
    public class RectangleCollisionHandler : ICollisionHandler
    {
        public bool CheckCollision(Rectangle playerBounds, Rectangle objectBounds)
        {
            return playerBounds.Intersects(objectBounds);
        }
    }

}
