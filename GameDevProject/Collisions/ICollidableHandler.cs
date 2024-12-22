using Microsoft.Xna.Framework;

namespace GameDevProject.Collisions
{
    public interface ICollidableHandler
    {
        Rectangle GetBoundingBox();
        bool IsSolid();
    }


}
