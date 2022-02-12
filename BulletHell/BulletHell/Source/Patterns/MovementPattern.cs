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
    public abstract class MovementPattern
    {
        public abstract void Move(Entity entity, GameTime gameTime);

        public MovementPattern getCopy()
        {
            return (MovementPattern)this.MemberwiseClone();

        }
    }
}
