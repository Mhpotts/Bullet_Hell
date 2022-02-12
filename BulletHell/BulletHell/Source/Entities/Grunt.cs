using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using BulletHell.Source.Commands;
using BulletHell.Source.Patterns;

namespace BulletHell.Source.Entities
{
    public class Grunt : Entity
    {

        public event EventHandler gruntHasDied;

        public Grunt(int health, int damage, Vector2 position, Vector2 hitBoxSize, MovementPattern pattern, Texture2D texture, Vector2 imageDimensions) :
             base(health, damage, position, hitBoxSize, pattern, texture, imageDimensions)
        {

        }

        public override void kill()
        {
            if (this.alive == true)
            {
                base.kill();
                gruntHasDied.Invoke(this, new EventArgs());

            }
        }



    }
}
