using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace GameDevProject
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public Viewport viewport;
        public Vector2 position;
        public float zoom;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            zoom = 1.25f;
            position = Vector2.Zero;
        }

        public void Update(Vector2 playerPosition, int mapWidth, int mapHeight)
        {
            //camera in the center of the player
            position = playerPosition - new Vector2(viewport.Width / 2, viewport.Height / 2) / zoom;

            //clamp camera so it doesnt go past border
            position.X = MathHelper.Clamp(position.X, 0, mapWidth - viewport.Width / zoom);
            position.Y = MathHelper.Clamp(position.Y, 0, mapHeight - viewport.Height / zoom);

            Transform = Matrix.CreateTranslation(new Vector3(-position, 0)) *
                        Matrix.CreateRotationZ(0) *
                        Matrix.CreateScale(zoom);
        }
    }
}
