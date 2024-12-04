using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        private Viewport viewport;
        private Vector2 position;
        private float zoom;
        private float rotation;

        public Camera(Viewport viewport)
        {
            viewport = viewport;
            zoom = 2.5f; 
            rotation = 0f;
            position = Vector2.Zero;
        }

        //method to follow the player
        public void Update(Vector2 playerPosition, int mapWidth, int mapHeight)
        {
            //center the camera on player, taking into account the zoom
            position = playerPosition - new Vector2(viewport.Width / 2, viewport.Height / 2) / zoom;

            // make it so the camera cant go further than the map itself
            position.X = MathHelper.Clamp(position.X, 0, mapWidth - viewport.Width / zoom);
            position.Y = MathHelper.Clamp(position.Y, 0, mapHeight - viewport.Height / zoom);

            // transform for the camera
            Transform = Matrix.CreateTranslation(new Vector3(-position, 0)) *
                        Matrix.CreateRotationZ(rotation) *
                        Matrix.CreateScale(zoom);
        }
    }
}
