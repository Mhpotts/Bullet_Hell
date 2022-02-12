using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Patterns
{
    public class StraightLinePattern : MovementPattern
    {
        private Vector2 velocity;

        public StraightLinePattern(Vector2 origin, Vector2 target, float speed)
        {
            Vector2 velocity = target - origin;
            velocity.Normalize();
            this.velocity = new Vector2(velocity.X * speed, velocity.Y * speed);
        }

        public override void Move(Entity entity, GameTime gameTime)
        {
            Vector2 gameVelocity = new Vector2(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            entity.setPosition(entity.getPosition() + gameVelocity);
        }
    }
}
