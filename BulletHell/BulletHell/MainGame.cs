using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using BulletHell.Source.GameScenes;
using BulletHell.Source.GameStates;
using Microsoft.Xna.Framework.Media;

namespace BulletHell
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
      
        private int clientBoundsHeight = 600;
        private int clientBoundsWidth = 593;

        private Scene currentScene;
        private Scene nextScene;

        private Song song;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void changeScene(Scene scene)
        {
            nextScene = scene;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Initialize()
        {
            
            IsMouseVisible = true;
            GraphicsDevice.PresentationParameters.BackBufferHeight = clientBoundsHeight;
            GraphicsDevice.PresentationParameters.BackBufferWidth = Convert.ToInt32(clientBoundsWidth * 1.5);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.song = this.Content.Load<Song>("Strut_Your_Stuff");
            MediaPlayer.Volume = 10f;
            MediaPlayer.Play(song);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            currentScene = new MainMenu(this, _graphics.GraphicsDevice, Content);
        }


        protected override void Update(GameTime gameTime)
        {
            if (nextScene != null)
            {
                currentScene = nextScene;
                nextScene = null;
            }
            currentScene.Update(gameTime);
            base.Update(gameTime);
        }

    
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentScene.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
    }
}

