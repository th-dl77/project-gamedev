using Microsoft.Xna.Framework;

namespace GameDevProject.Collisions
{
    public class CollidableObject : ICollidable
    {
        public Rectangle Bounds { get; set; }
        private readonly bool isSolid;
        public CollidableObject(Rectangle boundingBox, bool isSolid)
        {
            Bounds = boundingBox;
            this.isSolid = isSolid;
        }
        public Rectangle GetBoundingBox()
        {
            return Bounds;
        }
        public bool IsSolid()
        {
            return isSolid;
        }
    }

}
