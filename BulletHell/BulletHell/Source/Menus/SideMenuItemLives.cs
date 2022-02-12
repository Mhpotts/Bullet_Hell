using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletHell.Source.Menus
{
    public class SideMenuItemLives : SideMenuItem
    {
        public SideMenuItemLives(SpriteFont font, string value, Vector2 position) :
            base(position)
        {
            this.title = "Lives left: ";
            this.font = font;
            this.value = value;
        }
    }
}
