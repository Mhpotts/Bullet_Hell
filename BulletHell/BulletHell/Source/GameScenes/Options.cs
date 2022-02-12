using BulletHell.Source.Components;
using BulletHell.Source.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BulletHell.Source.GameScenes
{
    public class Options : Scene
    {
        private Stopwatch stopwatch = new Stopwatch();
      
     
        public Options(MainGame game, GraphicsDevice graphicsDevice, ContentManager manager) :
            base(game, graphicsDevice, manager)
        {

            stopwatch.Start();

            texture = manager.Load<Texture2D>("options_background");

            var buttonTexture = manager.Load<Texture2D>("button_texture");
            var buttonFont = manager.Load<SpriteFont>("Fonts/UIText");

            var hotKeysButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2((graphicsDevice.PresentationParameters.BackBufferWidth / 2) - (100), graphicsDevice.PresentationParameters.BackBufferHeight / 3),
                Text = "Key Binds",
            };

           

            var difficultyButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2((graphicsDevice.PresentationParameters.BackBufferWidth / 2) - (100), (graphicsDevice.PresentationParameters.BackBufferHeight / 3) + 150),
                Text = "Difficulty",
            };

            
            var title = new Text(manager.Load<SpriteFont>("Fonts/UIText_Title"))
            {
                position = new Vector2(310, 50),
                Message = "Options Menu"
            };

            hotKeysButton.Click += HotKeysButton_Click;
            difficultyButton.Click += DifficultyButton_Click;

            this.components = new List<Component>();
            this.components.Add(hotKeysButton);
            this.components.Add(difficultyButton);
            this.components.Add(title);
        }

        private void HotKeysButton_Click(object sender, EventArgs e)
        {
            this.game.changeScene(new ChangeKeyBinds(this.game, graphicsDevice, manager));
        }

        private void DifficultyButton_Click(object sender, EventArgs e)
        {
            this.game.changeScene(new ChangeDifficulty(this.game, graphicsDevice, manager));
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, new Vector2(0, 0), Color.White);
            foreach (Component component in components)
            {
                component.Draw(_spriteBatch);
            }
            _spriteBatch.End();

        }

        public override void Update(GameTime gameTime)
        {
            foreach(Component component in components)
            {
                component.Update(gameTime);
            }

            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Escape))
            {
                stopwatch.Stop();
                long elapsedTime = stopwatch.ElapsedMilliseconds;

                if (elapsedTime > 200)
                {
                    stopwatch.Stop();
                    this.game.changeScene(new MainMenu(this.game, graphicsDevice, manager));
                }
                else
                {
                    stopwatch.Start();
                }

            }

        }
    }
}
