using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletHell.Source.Menus
{
    public class SideMenuItemDifficulty : SideMenuItem
    {
        public SideMenuItemDifficulty(SpriteFont font, string value, Vector2 position) :
            base(position)
        {
            this.title = "Difficulty: ";
            this.font = font;
            this.value = value;
           
        }
    }
}
