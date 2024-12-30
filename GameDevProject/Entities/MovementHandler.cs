using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static System.Formats.Asn1.AsnWriter;
using System.Transactions;

namespace GameDevProject.Entities
{
    public class MovementHandler
    {
        public Vector2 Position { get; private set; }
        public Rectangle Bounds { get; private set; }
        private int spriteWidth;
        private int spriteHeight;
        private float scale;
        public MovementHandler(int spriteWidth, int spriteHeight, float scale)
        {
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.scale = scale;
        }
        public void Update(Vector2 startPos, int spriteWidth, int spriteHeight, float scale)
        {

        }
        private void Move(Vector2 direction, float deltaTime)
        {

        }

        private void Decelerate(float deltaTime)
        {

        }

        private void UpdateBounds()
        {
            Bounds = new Rectangle(
                (int)Position.X + 25,
                (int)Position.Y + 45,
                (int)((spriteWidth - 25) * scale),
                (int)((spriteHeight - 20) * scale)
);
        }
    }
}
