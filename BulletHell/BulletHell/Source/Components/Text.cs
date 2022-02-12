using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Components
{
    public class Text : Component
    {
        private SpriteFont font;
        public Vector2 position { get; set; }
        public string Message { get; set; }

        public Text(SpriteFont font)
        {
            this.font = font;
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(font, Message, position, Color.White);
        }

        public override void Update(GameTime gametime)
        {
            //throw new NotImplementedException();
        }
    }
}
