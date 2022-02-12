using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletHell.Source.Menus
{
    public class SideMenuBuilder
    {
        private SideMenu sidemenu;
        private int x = 0;
        private int y = 0;

        public SideMenuBuilder createMenu(Texture2D background, Vector2 position)
        {
            sidemenu = new SideMenu(background);
            x = (int) position.X;
            y = (int) position.Y;

            return this;
        }

        public SideMenuBuilder buildLives(SpriteFont font, string value)
        {
            SideMenuItemLives lives = new SideMenuItemLives(font, value, new Vector2(x, y));
            sidemenu.addMenuItem("lives", lives);
            changePosition();
            return this;
        }

        public SideMenuBuilder buildHighScore(SpriteFont font, string value)
        {
            SideMenuItemHighScore highscore = new SideMenuItemHighScore(font, value, new Vector2(x, y));
            sidemenu.addMenuItem("highscore", highscore);
            changePosition();
            return this;
        }

        public SideMenuBuilder buildScore(SpriteFont font, string value)
        {
            SideMenuItemScore score = new SideMenuItemScore(font, value, new Vector2(x, y));
            sidemenu.addMenuItem("score", score);
            changePosition();
            return this;
        }

        public SideMenuBuilder buildDifficulty(SpriteFont font, string value)
        {
            SideMenuItemDifficulty difficulty = new SideMenuItemDifficulty(font, value, new Vector2(x, y));
            sidemenu.addMenuItem("difficulty", difficulty);
            changePosition();
            return this;
        }

        public SideMenuBuilder buildPower(SpriteFont font, string value)
        {
            SideMenuItemPower power = new SideMenuItemPower(font, value, new Vector2(x, y));
            sidemenu.addMenuItem("power", power);
            changePosition();
            return this;
        }

        public SideMenu getProduct()
        {
            return sidemenu;
        }

        private void changePosition()
        {
            y = (y + 75);
        }
    }
}
