using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Patterns
{
    public class HoverPattern : MovementPattern
    {
        private float height;
        private float speed;
        private float counter;
        public HoverPattern(float height)
        {
            this.height = height;
            this.speed = height;
            this.counter = 0;
        }
        public override void Move(Entity entity, GameTime gameTime)
        {
            Vector2 velocity = new Vector2(0, speed);
            if (counter >= height)
            {
                speed = -speed;
                counter = 0;
            }
            counter++;
            Vector2 gameVelocity = new Vector2(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            entity.setPosition(entity.getPosition() + gameVelocity);
        }
    }
}
