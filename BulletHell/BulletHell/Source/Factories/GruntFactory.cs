using BulletHell.Source;
using BulletHell.Source.Commands;
using BulletHell.Source.Entities;
using BulletHell.Source.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Factories
{
    public abstract class GruntFactory
    {
        protected Texture2D gruntTexture;
        protected Boundary bounds;

        public GruntFactory(ContentManager manager, string texture, Boundary bounds)
        {
            this.gruntTexture = manager.Load<Texture2D>(texture);
            this.bounds = bounds;
        }

        public abstract Grunt spawnGrunt(Vector2 origin, MovementPattern movementPattern);

    }
}
