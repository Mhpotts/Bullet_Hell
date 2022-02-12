using BulletHell.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.GameScenes
{
    public class ChangeDifficulty : Scene
    {
        private Button normalButton;
        private Button insaneButton;
        private Texture2D buttonTextureSelected;
        private Texture2D buttonTexture;
        private SpriteFont buttonFont;

        public ChangeDifficulty(MainGame game, GraphicsDevice graphicsDevice, ContentManager manager) :
            base(game, graphicsDevice, manager)
        {
            texture = manager.Load<Texture2D>("difficulty_background");

            buttonTextureSelected = manager.Load<Texture2D>("button_texture_selected");
            buttonTexture = manager.Load<Texture2D>("button_texture");
            buttonFont = manager.Load<SpriteFont>("Fonts/UIText");

            normalButton = new Button(buttonTextureSelected, buttonFont)
            {
                position = new Vector2((graphicsDevice.PresentationParameters.BackBufferWidth / 2) - (100), graphicsDevice.PresentationParameters.BackBufferHeight / 3),
                Text = "Normal",
            };

           

            insaneButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2((graphicsDevice.PresentationParameters.BackBufferWidth / 2) - (100), (graphicsDevice.PresentationParameters.BackBufferHeight / 3) + 150),
                Text = "Insane",
            };


            var title = new Text(manager.Load<SpriteFont>("Fonts/UIText_Title"))
            {
                position = new Vector2(300, 50),
                Message = "Difficulty Level"
            };

            normalButton.Click += NormalButton_Click;
            insaneButton.Click += InsaneButton_Click;

            this.components = new List<Component>();
            this.components.Add(normalButton);
            this.components.Add(insaneButton);
            this.components.Add(title);
        }

        private void NormalButton_Click(object sender, EventArgs e)
        {
            normalButton.setTexture(buttonTextureSelected);
            insaneButton.setTexture(buttonTexture);
        }

        private void InsaneButton_Click(object sender, EventArgs e)
        {
            normalButton.setTexture(buttonTexture);
            insaneButton.setTexture(buttonTextureSelected);
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
            foreach (Component component in components)
            {
                component.Update(gameTime);
            }

            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Escape))
            {
                this.game.changeScene(new Options(this.game, graphicsDevice, manager));
            }
        }
    }
}
