using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    public class RenderingManager
    {
        private readonly TiledMapRenderer _tiledMapRenderer;
        private readonly Camera _camera;

        public RenderingManager(GraphicsDevice graphicsDevice, TiledMap tiledMap, Camera camera)
        {
            _tiledMapRenderer = new TiledMapRenderer(graphicsDevice);
            _tiledMapRenderer.LoadMap(tiledMap);
            _camera = camera;
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            _tiledMapRenderer.Draw(_camera.Transform);
        }
    }
}
