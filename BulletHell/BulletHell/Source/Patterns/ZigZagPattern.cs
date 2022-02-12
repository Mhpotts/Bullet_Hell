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
    public class ZigZagPattern : MovementPattern
    {
        private float speed;
        private Boundary bounds;
        private bool outOfBounds = false;
        private bool goRight;

        public ZigZagPattern(float speed, Boundary bounds, bool goRight)
        {
            this.speed = speed;
            this.bounds = bounds;
            this.goRight = goRight;
        }

        public override void Move(Entity entity, GameTime gameTime)
        {
            int xdir;
            if (goRight) xdir = 1;
            else xdir = -1;

            Vector2 velocity = new Vector2(xdir * speed * 4, speed);

            if (bounds.inBounds(entity.getPosition()))
            {
                outOfBounds = false;
            }

            if (!outOfBounds && !bounds.inBounds(entity.getPosition()))
            {
                outOfBounds = true;
                velocity.X = -velocity.X;
                goRight = !goRight;
            }

            Vector2 gameVelocity = new Vector2(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            entity.setPosition(entity.getPosition() + gameVelocity);
        }
    }
}
