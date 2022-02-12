using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletHell.Source.Menus
{
    public class SideMenuItemScore : SideMenuItem
    {
        public SideMenuItemScore(SpriteFont font, string value, Vector2 position) :
            base(position)
        {
            this.title = "Score: ";
            this.font = font;
            this.value = value;
        }
    }
}
