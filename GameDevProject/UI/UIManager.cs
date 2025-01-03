﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameDevProject.UI
{
    public class UIManager
    {
        public Texture2D ButtonTexture { get; }
        public SpriteFont Font { get; }

        public UIManager(Texture2D buttonTexture, SpriteFont font)
        {
            this.ButtonTexture = buttonTexture;
            this.Font = font;
        }

        public bool IsButtonClicked(Rectangle buttonRect, MouseState mouseState)
        {
            return buttonRect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed;
        }

        public void DrawButton(SpriteBatch spriteBatch, Vector2 position, string text)
        {
            spriteBatch.Draw(ButtonTexture, position, new Rectangle(0, 0, 200, 200), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(Font, text, new Vector2(position.X + 95, position.Y + 115), Color.Black);
        }
    }

}
