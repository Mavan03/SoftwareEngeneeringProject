using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TrustIssues.Controls;

namespace TrustIssues.States
{
    public class VictoryState : State
    {
        private List<Button> _components;
        private Texture2D _background;

        public VictoryState(Game1 game, ContentManager content) : base(game, content)
        {
        }

        public override void LoadContent()
        {
            // Laad een feestelijke achtergrond (bijv. Green of Yellow)
            _background = content.Load<Texture2D>("Menu/Yellow");
            var font = content.Load<SpriteFont>("Menu/Font");

            // We gebruiken de "Close" knop om naar het menu te gaan
            // Of als je een "Home" plaatje hebt, gebruik die.
            var menuTexture = content.Load<Texture2D>("Menu/Close");

            int screenW = game.GraphicsDevice.Viewport.Width;
            int screenH = game.GraphicsDevice.Viewport.Height;

            // Knop centreren
            int btnWidth = (int)(menuTexture.Width * 3f);
            int centerX = (screenW / 2) - (btnWidth / 2);

            var menuButton = new Button(menuTexture, font, "", new Vector2(centerX, 250), 3f);
            menuButton.Click += MenuButton_Click;

            _components = new List<Button>() { menuButton };
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Achtergrond
            int w = game.GraphicsDevice.Viewport.Width;
            int h = game.GraphicsDevice.Viewport.Height;
            spriteBatch.Draw(_background, new Rectangle(0, 0, w, h), Color.White);

            // Tekst: "VICTORY!" of "YOU WIN!"
            // We laden het font hier even lokaal om te tekenen (of maak het een variabele)
            var font = content.Load<SpriteFont>("Menu/Font");
            string text = "YOU WIN!";
            Vector2 size = font.MeasureString(text);

            // Tekst centreren boven de knop
            Vector2 textPos = new Vector2((w / 2) - (size.X / 2), 150);
            spriteBatch.DrawString(font, text, textPos, Color.Gold);

            foreach (var component in _components)
                component.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}