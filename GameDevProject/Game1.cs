using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GameDevProject.PlayerFiles;
using GameDevProject.Collisions;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player;

        private Camera camera;

        private Texture2D spriteSheetTexture;

        private CollisionManager collisionManager;

        private Texture2D _debugTexture;

        private TiledMap tiledmap;
        private TiledMapRenderer tiledMapRenderer;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            tiledmap = Content.Load<TiledMap>("map");
            collisionManager = new CollisionManager(new RectangleCollisionHandler());
            LoadCollidableObjectsFromTiledMap(tiledmap);

            camera = new Camera(GraphicsDevice.Viewport);  
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the tiledmap and playerchar
            spriteSheetTexture = Content.Load<Texture2D>("char_red_1");

            tiledMapRenderer = new TiledMapRenderer(GraphicsDevice);

            tiledMapRenderer.LoadMap(tiledmap);

            _debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            _debugTexture.SetData(new[] { Color.White });

            //animations
            SpriteSheet spriteSheetRunning = new SpriteSheet(spriteSheetTexture, 56, 56, 112);
            SpriteSheet spriteSheetIdle = new SpriteSheet(spriteSheetTexture, 56, 56);
            SpriteSheet spriteSheetFighting = new SpriteSheet(spriteSheetTexture, 56, 56, 56);
            Animation idle = new Animation(spriteSheetIdle, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation running = new Animation(spriteSheetRunning, new int[] { 0, 1, 2, 3, 4, 5 }, 0.2f);
            Animation fighting = new Animation(spriteSheetFighting, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 },0.2f);

            player = new Player(running, idle, fighting, new Vector2(200,200), 100f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            collisionManager.CheckCollisions(player);
            player.Update(gameTime, collisionManager);
            camera.Update(player.Position,800,800);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //clear screen
            GraphicsDevice.Clear(Color.Gray);

            //draw the map
            //_spriteBatch.Begin(transformMatrix: camera.Transform);
            _spriteBatch.Begin();
            //tiledMapRenderer.Draw(camera.Transform);
            tiledMapRenderer.Draw();
            collisionManager.DrawCollidables(_spriteBatch);
            _spriteBatch.End();

            //draw the player
            _spriteBatch.Begin();
            player.DrawBounds(_spriteBatch, _debugTexture);
            player.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void LoadCollidableObjectsFromTiledMap(TiledMap map)
        {
            // Retrieve the object layer named "Collision"
            var objectLayer = map.GetLayer<TiledMapObjectLayer>("Collisions");

            if (objectLayer != null)
            {
                foreach (var obj in objectLayer.Objects)
                {
                    // Check if the object has the "collisions" property set to "true"
                    if (obj.Properties.TryGetValue("collisions", out var collisionProperty) &&
                        collisionProperty == "true")
                    {
                        // Add the object as a collidable rectangle
                        var bounds = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, (int)obj.Size.Width, (int)obj.Size.Height);
                        collisionManager.AddCollidableObject(new CollidableObject(bounds,GraphicsDevice));
                    }
                }
            }
        }


    }
}
