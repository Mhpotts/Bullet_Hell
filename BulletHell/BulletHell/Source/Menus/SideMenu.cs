using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Source.Menus
{
    public class SideMenu
    {
        
        private Texture2D background;
        //private List<SideMenuItem> items;
        private Dictionary<string, SideMenuItem> items;

        public SideMenu(Texture2D background)
        {
            this.background = background;
            this.items = new Dictionary<string, SideMenuItem>();
        }

       

        public void Update(string type, string value)
        {
            items[type].Update(value);
        }

        public void Draw(SpriteBatch _spritebatch)
        {
            foreach (KeyValuePair<string, SideMenuItem> entry in items)
            {
                entry.Value.Draw(_spritebatch);
            }
            
        }

        public void addMenuItem(string type, SideMenuItem item)
        {
            items[type] = item;
        }
        
    }
}
