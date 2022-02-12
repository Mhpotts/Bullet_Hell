using BulletHell.Factories;
using BulletHell.Source.Entities;
using BulletHell.Source.Misc;
using BulletHell.Source.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Factories
{
    public class ScorpionFactory : GruntFactory
    {
        public ScorpionFactory(ContentManager manager, Boundary bounds) : base(manager, "GreenAlien", bounds)
        {

        }

        public override Grunt spawnGrunt(Vector2 origin, MovementPattern movementPattern)
        {
            int health = (int)GameSettings.Instance.getSetting("scorpionHealth");
            int damage = (int)GameSettings.Instance.getSetting("scorpionDamage");
            Vector2 size = (Vector2)GameSettings.Instance.getSetting("scorpionHBSize");
            Vector2 imageSize = (Vector2)GameSettings.Instance.getSetting("scorpionImageSize");
            return new Grunt(health, damage, origin, size, movementPattern, gruntTexture, imageSize);
        }

    }
}
