using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Components
{
    public abstract class Component
    {
        public abstract void Update(GameTime gametime);


        public abstract void Draw(SpriteBatch _spriteBatch);
        
    }
}
