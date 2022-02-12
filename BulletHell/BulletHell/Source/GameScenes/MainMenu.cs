using BulletHell.Source.Components;
using BulletHell.Source.GameScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BulletHell.Source.GameStates
{
    public class MainMenu : Scene
    {
        public MainMenu(MainGame game, GraphicsDevice graphicsDevice, ContentManager manager)
            : base(game, graphicsDevice, manager)
        {
            texture = manager.Load<Texture2D>("mainmenu_background");
            var buttonTexture = manager.Load<Texture2D>("button_texture");
            var buttonFont = manager.Load<SpriteFont>("Fonts/UIText");

            var title = new Text(manager.Load<SpriteFont>("Fonts/UIText"))
            {
                position = new Vector2(275, 70),
                Message = "WELCOME TO DABOYS GAME"
            };

            
            var startGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2((graphicsDevice.PresentationParameters.BackBufferWidth / 2) - (100), (graphicsDevice.PresentationParameters.BackBufferHeight / 3) - 50),
                Text = "Start Game",
            };

            var optionsButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2((graphicsDevice.PresentationParameters.BackBufferWidth / 2) - (100), (graphicsDevice.PresentationParameters.BackBufferHeight / 3) + 80),
                Text = "Options",
            };

            

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2((graphicsDevice.PresentationParameters.BackBufferWidth / 2) - (100), (graphicsDevice.PresentationParameters.BackBufferHeight / 3) + 210),
                Text = "Quit Game",
            };

            startGameButton.Click += StartGameButton_Click;
            optionsButton.Click += OptionsButton_Click;
            quitGameButton.Click += QuitGameButton_Click;


            this.components = new List<Component>();
            this.components.Add(startGameButton);
            this.components.Add(optionsButton);
            this.components.Add(quitGameButton);
            this.components.Add(title);
        }



        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Quit game pressed");
            this.game.Exit();
            //throw new NotImplementedException();
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Options button pressed");
            this.game.changeScene(new Options(game, graphicsDevice, manager));

        }
        private void StartGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Start game pressed");
            this.game.changeScene(new GameController(game, graphicsDevice, manager));
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
