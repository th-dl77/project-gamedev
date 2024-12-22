using Microsoft.Xna.Framework;

namespace GameDevProject.Collisions
{
    public class CollidableObject : ICollidableHandler
    {
        private readonly Rectangle _boundingBox;
        private readonly bool _isSolid;
        public CollidableObject(Rectangle boundingBox, bool isSolid)
        {
            _boundingBox = boundingBox;
            _isSolid = isSolid;
        }
        public Rectangle GetBoundingBox()
        {
            return _boundingBox;
        }
        public bool IsSolid()
        {
            return _isSolid;
        }
    }

}
