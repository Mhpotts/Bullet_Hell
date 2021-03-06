using BulletHell.Source.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Entities
{
    public class MidBoss : Entity
    {
        public event EventHandler midBossHasDied;

        public MidBoss(int health, int damage, Vector2 position, Vector2 hitBoxSize, MovementPattern pattern, Texture2D texture, Vector2 imageDimensions)
            : base(health, damage, position, hitBoxSize, pattern, texture, imageDimensions)
        {

        }

        public override void kill()
        {
            if (this.alive == true)
            {
                base.kill();
                midBossHasDied.Invoke(this, new EventArgs());

            }
        }
    }
}
