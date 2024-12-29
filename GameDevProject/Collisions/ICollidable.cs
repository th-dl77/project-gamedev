using Microsoft.Xna.Framework;

namespace GameDevProject.Collisions
{
    public interface ICollidable
    {
        public Rectangle Bounds { get; set; }
        Rectangle GetBoundingBox();
        bool IsSolid();
    }


}
