using BulletHell.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BulletHell.Source.GameScenes
{
    public abstract class Scene
    {

        protected GraphicsDevice graphicsDevice;
        protected MainGame game;
        protected ContentManager manager;
        protected List<Component> components;
        protected Texture2D texture;
        public Scene(MainGame mainGame, GraphicsDevice graphicsDevice, ContentManager manager)
        {
            this.game = mainGame;
            this.graphicsDevice = graphicsDevice;
            this.manager = manager;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch _spriteBatch);
        public abstract void Update(GameTime gameTime);


    }
}
