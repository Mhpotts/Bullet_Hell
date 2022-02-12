using BulletHell.Source.Components;
using BulletHell.Source.GameScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BulletHell.Source.GameStates
{
    public class WinGame : Scene
    {
        public WinGame(MainGame game, GraphicsDevice graphicsDevice, ContentManager manager) :
            base(game, graphicsDevice, manager)
        {
            var buttonTexture = manager.Load<Texture2D>("button_texture");
            var buttonFont = manager.Load<SpriteFont>("Fonts/UIText");

            texture = manager.Load<Texture2D>("win_background");


            var restartGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2((graphicsDevice.PresentationParameters.BackBufferWidth / 2) - (100), graphicsDevice.PresentationParameters.BackBufferHeight / 3),
                Text = "Restart",
            };

            restartGameButton.Click += RestartGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2((graphicsDevice.PresentationParameters.BackBufferWidth / 2) - (100), (graphicsDevice.PresentationParameters.BackBufferHeight / 3) + 150),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            var title = new Text(manager.Load<SpriteFont>("Fonts/UIText_Title"))
            {
                position = new Vector2(338, 50),
                Message = "You won!!!!"
            };

            this.components = new List<Component>();
            this.components.Add(restartGameButton);
            this.components.Add(quitGameButton);
            this.components.Add(title);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            this.game.Exit();
        }

        private void RestartGameButton_Click(object sender, EventArgs e)
        {
            this.game.changeScene(new GameController(this.game, graphicsDevice, manager));
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, new Vector2(0, 0), Color.White);

            foreach (var component in components)
            {
                component.Draw(_spriteBatch);
            }
            _spriteBatch.End();

        }


        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.Update(gameTime);
            }

        }
    }
}

