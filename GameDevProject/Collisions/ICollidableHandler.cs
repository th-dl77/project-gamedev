using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Collisions
{
    public interface ICollidableHandler
    {
        Rectangle GetBoundingBox();
        bool IsSolid();
    }


}
