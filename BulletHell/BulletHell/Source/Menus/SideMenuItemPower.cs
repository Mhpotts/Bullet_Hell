using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletHell.Source.Menus
{
    public class SideMenuItemPower : SideMenuItem
    {
        public SideMenuItemPower(SpriteFont font, string value, Vector2 position) :
            base(position)
        {
            this.title = "Power: ";
            this.font = font;
            this.value = value;
        }
    }
}
