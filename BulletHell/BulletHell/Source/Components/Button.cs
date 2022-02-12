using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Components
{
    public class Button : Component
    {
        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previousMouse;

        private Texture2D texture;


        public event EventHandler Click;

        public bool Clicked {
            get;
            private
            set;
        }

        public Color color { get; set; }
        public Vector2 position { get; set; }

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;
            this.color = Color.Black;
        }

        public void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }


        public override void Draw(SpriteBatch _spriteBatch)
        {
            var color = Color.White;

            if (isHovering)
            {
                color = Color.Gray;
            }

            _spriteBatch.Draw(texture, rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (rectangle.Y + (rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                _spriteBatch.DrawString(font, Text, new Vector2(x, y), this.color);
            } 
        }

        public override void Update(GameTime gametime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var rect = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);
            isHovering = false;

            if (rect.Intersects(rectangle))
            {
                isHovering = true;
                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        
    }
}
