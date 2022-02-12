using BulletHell.Source.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Entities.PowerUps
{
    public class Nuke : PowerUp
    {
        public Nuke(Vector2 position, Vector2 hitBoxSize, Texture2D powerUpTexture, Vector2 imageDimensions)
            : base(position, hitBoxSize, powerUpTexture, imageDimensions)
        {

        }

        public override void hit(Entity entity)
        {
            if (entity is Player)
            {
                EntityController.Instance.clearNPC();
                this.kill();
            }
        }
    }
}
