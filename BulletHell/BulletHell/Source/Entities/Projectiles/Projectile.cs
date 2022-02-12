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
using BulletHell.Source.Patterns;

namespace BulletHell.Source
{
    public abstract class Projectile : Entity
    {
        public Projectile(int damage, Vector2 position, Vector2 hitBoxSize, MovementPattern pattern, Texture2D texture, Vector2 imageDimensions)
            : base(1, damage, position, hitBoxSize, pattern, texture, imageDimensions)
        {
           
        }
       

    }
}
