using BulletHell.Source.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Entities.PowerUps
{
    public abstract class PowerUp : Entity
    {
        public PowerUp(Vector2 position, Vector2 hitBoxSize, Texture2D powerUpTexture, Vector2 imageDimensions) 
            : base (1, 0, position, hitBoxSize, new HoverPattern(15), powerUpTexture, imageDimensions)
        {

        }

        public abstract override void hit(Entity entity);
    }
}
