using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Patterns
{
    public class SpiralPattern : MovementPattern
    {
        private double counter = 0;
        private double radius;
        private double forwardMomentum;
        private bool inverted = false;
        public SpiralPattern(double radius, double forwardMomentum)
        {
            this.radius = radius;
            this.forwardMomentum = forwardMomentum;
        }

        public override void Move(Entity entity, GameTime gameTime)
        {
            Vector2 velocity;
            if (inverted)
            {
                velocity = new Vector2((float)(radius * Math.Cos(counter)), (float)(radius * Math.Sin(counter) + forwardMomentum));
            }
            else
            {
                velocity = new Vector2((float)(radius * Math.Sin(counter)), (float)(radius * Math.Cos(counter) + forwardMomentum));
            }
            
            counter = (counter + .01) % (2 * Math.PI);

            Vector2 gameVelocity = new Vector2(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            entity.setPosition(entity.getPosition() + gameVelocity);
        }

        public SpiralPattern invert()
        {
            SpiralPattern invertSpiral = (SpiralPattern)this.MemberwiseClone();
            invertSpiral.inverted = true;
            return invertSpiral;
        }
    }
}
