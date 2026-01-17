using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TrustIssues.Controls;
using TrustIssues.Core;

namespace TrustIssues.States
{
    public class GameOverState : State
    {
        private int _levelToRestart;
        private List<Button> _components;
        private Texture2D _background; 
        public GameOverState(Game1 game, ContentManager content, int levelIndex) : base(game, content)
        {
            _levelToRestart = levelIndex;
        }

        public override void LoadContent()
        {
            // 1. We moeten nu ook het Font laden, want de nieuwe Button heeft dat nodig
            _background = content.Load<Texture2D>("Menu/Gray");
            var font = content.Load<SpriteFont>("Menu/Font");

            var restartTexture = content.Load<Texture2D>("Menu/Restart");
            var closeTexture = content.Load<Texture2D>("Menu/Close");

            // 2. Knoppen aanmaken met de NIEUWE manier:
            // Parameters: (Texture, Font, "Tekst", Positie, Schaal)
            // We laten de tekst leeg ("") omdat er al een plaatje op de knop staat.
            var restartButton = new Button(restartTexture, font, "", new Vector2(250, 200), 3f);
            restartButton.Click += RestartButton_Click;

            var menuButton = new Button(closeTexture, font, "", new Vector2(450, 200), 3f);
            menuButton.Click += MenuButton_Click;

            _components = new List<Button>() { restartButton, menuButton };
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            // Herstarten op hetzelfde level
            game.ChangeState(new GameState(game, content, _levelToRestart));
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            // Terug naar hoofdmenu
            game.ChangeState(new MenuState(game, content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Teken achtergrond
            int w = game.GraphicsDevice.Viewport.Width;
            int h = game.GraphicsDevice.Viewport.Height;
            spriteBatch.Draw(_background, new Rectangle(0, 0, w, h), Color.White);

            // Optioneel: Teken "GAME OVER" tekst
            // var font = content.Load<SpriteFont>("Font");
            // spriteBatch.DrawString(font, "GAME OVER", new Vector2(350, 100), Color.Red);

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