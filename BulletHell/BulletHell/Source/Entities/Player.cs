using BulletHell.Source.Controllers;
using BulletHell.Source.Factories;
using BulletHell.Source.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Entities
{
    public class Player : Entity
    {
        private int MAXHEALTH;
        public event EventHandler playerHasDied;
        private TimeSpan respawnTime;
        private TimeSpan timeAlive;
        private BulletSpawner launcher;
        private Boundary playerBounds;

        public Player(Vector2 position, Vector2 imageDimensions, Boundary bounds, Texture2D texture, Texture2D missileTexture)
            : base((int)GameSettings.Instance.getSetting("playerHealth"), 1, position, (Vector2)GameSettings.Instance.getSetting("playerHBSize"), null, texture, imageDimensions)
        {
            this.playerBounds = bounds;
            this.MAXHEALTH = health;
            this.launcher = new MissileLauncher(missileTexture, position, null, null, this);
            

        }

        public void setGun(BulletSpawner gun)
        {
            this.launcher = gun;
        }

        public override void hit(Entity entity)
        {
            if ((timeAlive) > (TimeSpan)GameSettings.Instance.getSetting("playerRespawnImmunityCooldown"))
            {
                base.hit(entity);
            }
        }

        public override void kill()
        {
            base.kill();
            playerHasDied.Invoke(this, new EventArgs());
        }

        public void setImmunity()
        {
            respawnTime = TimeSpan.FromSeconds(-1);
        }

        public void respawn(Vector2 initPosition)
        {
            position = initPosition;
            alive = true;
            health = MAXHEALTH;
            respawnTime = TimeSpan.FromSeconds(-1);
        }

        public override void update(GameTime gameTime)
        {
            if (respawnTime < TimeSpan.FromSeconds(0)) respawnTime = gameTime.TotalGameTime;
            timeAlive = gameTime.TotalGameTime - respawnTime;

            KeyboardState kstate = Keyboard.GetState();
            float speed;
            if (kstate.IsKeyDown(Keys.X))
                speed = (float)GameSettings.Instance.getSetting("playerSpeedLow");
            else
                speed = (float)GameSettings.Instance.getSetting("playerSpeedHigh");

            float X = position.X;
            float Y = position.Y;

            if (kstate.IsKeyDown((Keys)GameSettings.Instance.getSetting("playerUp")))
            {
                Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown((Keys)GameSettings.Instance.getSetting("playerDown")))
            {
                Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }

            if (kstate.IsKeyDown((Keys)GameSettings.Instance.getSetting("playerLeft")))
            {
                X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown((Keys)GameSettings.Instance.getSetting("playerRight")))
            {
                X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (playerBounds.inBounds(X, Y))
            {
                position.Y = Y;
                position.X = X;
                hitBox.X = (int)position.X;
                hitBox.Y = (int)position.Y;
            }

            if (kstate.IsKeyDown((Keys)GameSettings.Instance.getSetting("playerFire")))
            {
                launcher.update(gameTime);
            }
        }
    }
}
