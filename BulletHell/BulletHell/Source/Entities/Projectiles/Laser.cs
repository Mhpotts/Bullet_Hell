using BulletHell.Source.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Entities.Projectiles
{
    public class Laser : Projectile
    {
        public Laser(int damage, Vector2 position, Vector2 hitBoxSize, MovementPattern pattern, Texture2D texture, Vector2 imageDimensions)
            : base(damage, position, hitBoxSize, pattern, texture, imageDimensions)
        {

        }
    }
}
