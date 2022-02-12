using BulletHell.Source.Controllers;
using BulletHell.Source.Entities;
using BulletHell.Source.Misc;
using BulletHell.Source.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Factories
{
    public class MissileLauncher : BulletSpawner
    {

        private TimeSpan missileFired;
        private TimeSpan missileCooldown;
        private float missileSpeed;

        public MissileLauncher(Texture2D missileTexture, Vector2 origin, Entity target, MovementPattern pattern, Entity owner)
            : base(missileTexture, origin, target, pattern, owner)
        {
            if (owner is Player)
            {
                missileCooldown = (TimeSpan)GameSettings.Instance.getSetting("playerMissileCooldown");
            } else
            {
                missileCooldown = (TimeSpan)GameSettings.Instance.getSetting("missileCooldown");
            }
            missileSpeed = 750f;
        }

        public MissileLauncher(Texture2D missileTexture, Vector2 origin, Entity target, MovementPattern pattern)
            : base(missileTexture, origin, target, pattern)
        {
            missileCooldown = (TimeSpan)GameSettings.Instance.getSetting("missileCooldown");
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
                    position = owner.getPosition();
                }
                if ((gameTime.TotalGameTime - missileFired) > missileCooldown)
                {
                    int damage = (int)GameSettings.Instance.getSetting("missileDamage");
                    Vector2 missileTarget;
                    if (this.target != null) //enemy shooting at player
                    {
                        missileTarget = this.target.getPosition();
                    }
                    else if (owner is Player)//player shooting
                    {
                        missileTarget = new Vector2(position.X, 1);
                    } else
                    {
                        missileTarget = new Vector2(position.X, -1);
                    }
                    StraightLinePattern straightLine = new StraightLinePattern(position, missileTarget, missileSpeed);
                    Missile missile = new Missile(damage, position, new Vector2(20, 20), straightLine, bulletTexture, new Vector2(20, 20));
                    if (owner is Player)
                    {
                        EntityController.Instance.addPlayerBullet(missile);
                    }
                    else
                    {
                        EntityController.Instance.addEntity(missile);
                    }
                    missileFired = gameTime.TotalGameTime;
                }
            }
            else
            {
                this.kill();
            }
        }
    }
}
