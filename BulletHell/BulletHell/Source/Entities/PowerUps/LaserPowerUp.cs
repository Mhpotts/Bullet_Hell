using BulletHell.Source.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Entities.PowerUps
{
    public class LaserPowerUp : PowerUp
    {
        private Texture2D laserTexture;

        public LaserPowerUp(Texture2D laserTexture, Vector2 position, Vector2 hitBoxSize, Texture2D powerUpTexture, Vector2 imageDimensions) 
            : base(position, hitBoxSize, powerUpTexture, imageDimensions)
        {
            this.laserTexture = laserTexture;
        }

        public override void hit(Entity entity)
        {
            if (entity is Player)
            {
                Player player = (Player)entity;
                LaserGun laserGun = new LaserGun(laserTexture, player.getPosition(), null, null, player);
                player.setGun(laserGun);
                this.kill();
            }
        }
    }
}
