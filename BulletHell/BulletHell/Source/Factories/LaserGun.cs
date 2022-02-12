using BulletHell.Source.Controllers;
using BulletHell.Source.Entities;
using BulletHell.Source.Entities.Projectiles;
using BulletHell.Source.Misc;
using BulletHell.Source.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Factories
{
    public class LaserGun : BulletSpawner
    {
        public LaserGun(Texture2D laserTexture, Vector2 origin, Entity target, MovementPattern pattern, Entity owner)
            : base (laserTexture, origin, target, pattern, owner)
        {

        }

        public override void update(GameTime gameTime)
        {
            if (owner == null || owner.isAlive())
            {
                if (movePattern != null)
                {
                    movePattern.Move(this, gameTime);
                } 
                else if (owner != null)
                {
                    position = new Vector2(owner.getPosition().X, owner.getPosition().Y - 75);
                } 
                int damage = (int)GameSettings.Instance.getSetting("laserDamage");
                Vector2 laserTarget;
                if (this.target != null)
                {
                    laserTarget = this.target.getPosition();
                }
                else if (owner is Player)
                {
                    laserTarget = new Vector2(position.X, 1);
                } else
                {
                    laserTarget = new Vector2(position.X, -1);
                }
                StraightLinePattern straightLine = new StraightLinePattern(position, laserTarget, 5000);
                Laser laser = new Laser(damage, position, new Vector2(25, 100), straightLine, bulletTexture, new Vector2(25, 100));
                if (owner is Player)
                {
                    EntityController.Instance.addPlayerBullet(laser);
                } else
                {
                    EntityController.Instance.addEntity(laser);
                }

            }
        }
    }
}
