using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Source.Menus
{
    public abstract class SideMenuItem
    {
        protected SpriteFont font;
        protected string title;
        protected string value;
        protected Vector2 position;

        public SideMenuItem(Vector2 position)
        {
            this.position = position;
        }
        public void Update(string value)
        {
            this.value = value;
        }

        public void Draw(SpriteBatch _spriteBatch) 
        {
            _spriteBatch.DrawString(font, title + value, position, Color.White);
        }


        public string getValue()
        {
            return value;
        }

    }
}
