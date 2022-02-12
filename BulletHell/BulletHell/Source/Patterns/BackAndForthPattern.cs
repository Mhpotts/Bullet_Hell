using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Patterns
{
    public class BackAndForthPattern : MovementPattern
    {
        private float radius;
        private float speed;
        private float wobble;
        private float counter;
        private float wobbleCounter;
        public BackAndForthPattern(float radius, float speed, float wobble)
        {
            this.radius = radius;
            this.speed = radius;
            this.wobble = wobble;
            this.counter = radius / 2;
            this.wobbleCounter = 0;
        }

        public override void Move(Entity entity, GameTime gameTime)
        {
            Vector2 velocity = new Vector2(speed, wobble * (float)Math.Sin(wobbleCounter));
            if (counter >= radius)
            {
                speed = -speed;
                counter = 0;
            }
            counter++;
            wobbleCounter = wobbleCounter + (float)(0.1 % 2*Math.PI);

            Vector2 gameVelocity = new Vector2(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            entity.setPosition(entity.getPosition() + gameVelocity);
        }
    }
}
