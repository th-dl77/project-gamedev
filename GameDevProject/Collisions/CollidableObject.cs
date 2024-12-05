using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Collisions
{
    public class CollidableObject
    {
        public Rectangle Bounds { get; set; }
        public bool IsCollidable { get; set; }

        public CollidableObject(Rectangle bounds, bool isCollidable = true)
        {
            Bounds = bounds;
            IsCollidable = isCollidable;
        }
    }

}
