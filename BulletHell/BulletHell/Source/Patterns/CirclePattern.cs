using BulletHell.Source;
using BulletHell.Source.Commands;
using BulletHell.Source.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Patterns
{
    class CirclePattern : MovementPattern
    {
        double counter = 0;
        double magnitude = 1;
        public CirclePattern(double magnitude)
        {
            this.magnitude = magnitude;
        }
        public override void Move(Entity entity, GameTime gameTime)
        {
            Vector2 velocity = new Vector2((float)(magnitude * Math.Sin(counter)), (float)(magnitude * Math.Cos(counter)));
            counter = (counter + .1) % (2 * Math.PI);

            Vector2 gameVelocity = new Vector2(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            entity.setPosition(entity.getPosition() + gameVelocity);
        }
    }
}
