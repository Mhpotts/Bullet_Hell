using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Source.Patterns;

namespace BulletHell.Source
{
    public abstract class Entity
    {
        protected int health;
        protected int damage;
        protected Vector2 position;
        protected Vector2 dimensions;
        protected Rectangle hitBox;
        protected MovementPattern movePattern;
        protected Texture2D texture;
        protected SoundEffect soundEffect;
        protected bool alive;

        public Entity(int health, int damage, Vector2 position, Vector2 hitBoxSize, MovementPattern movePattern, Texture2D texture, Vector2 imageDimensions)
        {
            this.health = health;
            this.damage = damage;
            this.position = position;
            this.dimensions = imageDimensions;
            this.hitBox = new Rectangle((int)position.X, (int)position.Y, (int)hitBoxSize.X, (int)hitBoxSize.Y);
            this.texture = texture;
            this.movePattern = movePattern;
            this.texture = texture;
            this.alive = true;
        }

        public virtual void kill()
        {
            this.alive = false;
        }

        public bool isAlive()
        {
            return this.alive;
        }

        public virtual void update(GameTime gameTime)
        {
            if (alive)
            {
                if (movePattern != null) movePattern.Move(this, gameTime);
                hitBox.X = (int)position.X;
                hitBox.Y = (int)position.Y;
            }
        }

        public virtual void draw(SpriteBatch _spriteBatch)
        {
            if (texture != null && alive)
            {
                _spriteBatch.Draw(
               this.texture,
               new Rectangle((int)(position.X), (int)(position.Y), (int)dimensions.X, (int)dimensions.Y),
               null,
               Color.White,
               0.0f,
               new Vector2(this.texture.Width / 2, this.texture.Height / 2),
               SpriteEffects.None
               , 0
                );
            }
        }

        public Rectangle getHitBox()
        {
            return hitBox;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getDimensions()
        {
            return dimensions;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        public virtual void hit(Entity entity)
        {
            health -= entity.damage;
            if (health <= 0)
            {
                kill();
            }
        }
    }
}
