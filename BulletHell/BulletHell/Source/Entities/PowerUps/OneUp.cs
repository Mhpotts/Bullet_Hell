using BulletHell.Source.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Entities.PowerUps
{
    public class OneUp : PowerUp
    {
        private GameController controller;
        public OneUp(GameController controller, Vector2 position, Vector2 hitBoxSize, Texture2D powerUpTexture, Vector2 imageDimensions)
            : base (position, hitBoxSize, powerUpTexture, imageDimensions)
        {
            this.controller = controller;
        }

        public override void hit(Entity entity)
        {
            if (entity is Player)
            {
                controller.lives++;
                this.kill();
            }
        }
    }
}
