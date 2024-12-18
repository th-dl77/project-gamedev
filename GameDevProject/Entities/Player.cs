using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using GameDevProject.Collisions;
using GameDevProject.Input;

namespace GameDevProject.Entities
{
    public class Player : IEntity
    {

        private readonly Dictionary<string, Animation> animations;
        //private Animation runningAnimation;
        //private Animation idleAnimation;
        //private Animation fightingAnimation;
        private Animation currentAnimation;

        private SpriteEffects _currentFlipEffect = SpriteEffects.None;

        private IInputStrategy _inputStrategy;

        public int SpriteHeight { get; private set; } = 56;
        public int SpriteWidth { get; private set; } = 56;

        public Vector2 Velocity;
        public Vector2 Position { get; private set; }

        private float Speed { get; } = 100f;

        private Camera _camera;
        public float Scale { get; private set; } = 2f;
        private bool isFacingLeft = false;
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)Position.X + 25,
                    (int)Position.Y + 45,
                    (int)((SpriteWidth - 25) * Scale),
                    (int)((SpriteHeight - 20) * Scale)
                );
            }
        }

        public Player(IInputStrategy inputStrategy, Dictionary<string, Animation> animations, Vector2 startPosition, Camera _camera)
        {
            this._inputStrategy = inputStrategy;
            this.animations = animations;
            currentAnimation = animations["idle"];
            Position = startPosition;
            this._camera = _camera;
        }

        public void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            HandleInput(gameTime);
            collisionManager.Player = this;
            Vector2 proposedPosition = Position + Velocity * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 resolvedPosition = collisionManager.ResolveCollisions(proposedPosition);
            Position = resolvedPosition;
            currentAnimation.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            Vector2 inputVelocity = _inputStrategy.GetMovementInput();

            if (_inputStrategy.IsActionPressed("fight"))
            {
                currentAnimation = animations["fighting"];
            }
            else if (inputVelocity != Vector2.Zero)
            {
                inputVelocity.Normalize(); //used to fix diagonal movement, otherwise way too fast
                currentAnimation = animations["running"];
                if (inputVelocity.X < 0)
                {
                    _currentFlipEffect = SpriteEffects.FlipHorizontally; // Player is moving left
                }
                else if (inputVelocity.X > 0)
                {
                    _currentFlipEffect = SpriteEffects.None; // Player is moving right
                }
            }
            else
            {
                currentAnimation = animations["idle"];
            }

            Velocity = inputVelocity;
        }
        public void TakeHit(Enemy enemy)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch, Position, _currentFlipEffect);
        }

        public void DrawBounds(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            spriteBatch.Draw(debugTexture, Bounds, Color.Blue * 0.5f);
        }
    }
}
