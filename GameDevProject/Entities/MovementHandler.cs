using Microsoft.Xna.Framework;
using GameDevProject.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.Entities
{
    public class MovementHandler
    {
        public Vector2 Position { get; set; } = new Vector2(800, 800);
        public Vector2 Velocity { get; set; }
        public Rectangle Bounds { get; private set; }
        public Vector2 Acceleration { get; private set; }
        private int spriteWidth = 56;
        private int spriteHeight = 56;
        private float scale = 2f;

        private AnimationManager animationManager;
        private readonly IInputStrategy _inputStrategy;

        private float Speed { get; set; } = 10f;
        private float maxSpeed = 4f;
        private float accelerationRate = 50f;
        private float decelerationRate = 20f;
        public MovementHandler(IInputStrategy inputStrategy, AnimationManager animationManager)
        {
            _inputStrategy = inputStrategy;
            this.animationManager = animationManager;
        }

        public void HandleMovement(GameTime gameTime)
        {
            Vector2 inputDirection = _inputStrategy.GetMovementInput();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (inputDirection != Vector2.Zero)
            {
                inputDirection.Normalize();
                Acceleration = inputDirection * accelerationRate;

                // update velocity with acceleration
                Velocity += Acceleration * deltaTime;
                if (inputDirection.X < 0 )
                {
                    animationManager.PlayAnimation("running");
                    animationManager.FlipAnimation(true);
                }
                else if (inputDirection.X > 0)
                {
                    animationManager.PlayAnimation("running");
                    animationManager.FlipAnimation(false);
                }

                //ensure velocity is not faster than maxspeed
                if (Velocity.LengthSquared() > maxSpeed * maxSpeed)
                {
                    Velocity = Vector2.Normalize(Velocity) * maxSpeed;
                }
            }
            else
            {
                if (Velocity.Length() > 0.1f)
                {
                    // deceleration
                    Velocity -= Velocity * decelerationRate * deltaTime;
                }
                else
                {
                    animationManager.PlayAnimation("idle");
                    Velocity = Vector2.Zero;
                }
            }
            Position += Velocity * deltaTime;
            UpdateBounds();
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

        public void AdjustPosition(Vector2 positionAdjustment)
        {
            Position += positionAdjustment;
        }
    }
}
