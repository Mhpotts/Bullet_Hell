using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletHell.Source.Menus
{
    public class SideMenuItemHighScore : SideMenuItem
    {
        public SideMenuItemHighScore(SpriteFont font, string value, Vector2 position) :
            base(position)
        {
            this.title = "High Score: ";
            this.font = font;
            this.value = value;
        }
    }
}
