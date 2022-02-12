using BulletHell.Source.Components;
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
    public class ChangeKeyBinds : Scene
    {
        private Stopwatch stopwatch = new Stopwatch();
        private Button wasdButton;
        private Button arrowButton;
        private Texture2D wasdButtonTexture;
        private Texture2D wasdButtonTextureSelected;
        private Texture2D arrowButtonTexture;
        private Texture2D arrowButtonTextureSelected;

        public ChangeKeyBinds(MainGame game, GraphicsDevice graphicsDevice, ContentManager manager) :
            base(game, graphicsDevice, manager)
        {
            stopwatch.Start();

            texture = manager.Load<Texture2D>("keybinds_background");

            wasdButtonTexture = manager.Load<Texture2D>("keybind_wasd_background");
            wasdButtonTextureSelected = manager.Load<Texture2D>("keybind_wasd_selected_background");
            arrowButtonTextureSelected = manager.Load<Texture2D>("keybind_arrows_selected_background");

            var buttonFont = manager.Load<SpriteFont>("Fonts/UIText");

            wasdButton = new Button(wasdButtonTextureSelected, buttonFont)
            {
                position = new Vector2(100, 250),
            };
            

            arrowButtonTexture = manager.Load<Texture2D>("keybind_arrows_background");
            arrowButton = new Button(arrowButtonTexture, buttonFont)
            {
                position = new Vector2(500, 250),
            };

            
            var title = new Text(manager.Load<SpriteFont>("Fonts/UIText_Title"))
            {
                position = new Vector2(265, 50),
                Message = "Current Key Binds"
            };

            wasdButton.Click += WASDButton_Click;
            arrowButton.Click += ArrowButton_Click;

            this.components = new List<Component>();
            this.components.Add(wasdButton);
            this.components.Add(arrowButton);
            this.components.Add(title);
        }
        private void WASDButton_Click(object sender, EventArgs e)
        {
            wasdButton.setTexture(wasdButtonTextureSelected);
            arrowButton.setTexture(arrowButtonTexture);
        }

        private void ArrowButton_Click(object sender, EventArgs e)
        {
            wasdButton.setTexture(wasdButtonTexture);
            arrowButton.setTexture(arrowButtonTextureSelected);
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

            wasdButton.color = Color.Red;
        }
    }
}
