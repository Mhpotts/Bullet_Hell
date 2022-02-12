using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using BulletHell.Source.Patterns;

namespace BulletHell.Source
{
    public abstract class BulletSpawner : Entity
    {
        protected Texture2D bulletTexture;
        protected Entity owner;
        protected float speed;
        protected Entity target;
        
        public BulletSpawner(Texture2D bulletTexture, Vector2 origin, Entity target, MovementPattern pattern) 
            : base (0, 0, origin, new Vector2(), pattern, null, new Vector2())
        {
            this.bulletTexture = bulletTexture;
            this.owner = null;
            this.target = target;
        }
        public BulletSpawner(Texture2D bulletTexture, Vector2 origin, Entity target, MovementPattern pattern, Entity owner)
            : base (0, 0, origin, new Vector2(), pattern, null, new Vector2())
        {
            this.position = origin;
            this.bulletTexture = bulletTexture;
            this.owner = owner;
            this.target = target;
        }

        public override void hit(Entity entity)
        {
            // cannot be hit or hit other things
        }

        public abstract override void update(GameTime gameTime);

    }
}
