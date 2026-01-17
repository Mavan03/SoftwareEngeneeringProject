using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TrustIssues.Controls;
using TrustIssues.Core;

namespace TrustIssues.States
{
    public class MenuState : State
    {
        private List<Button> _components;
        private Texture2D _background;
        public MenuState(Game1 game, ContentManager content) : base(game, content) { }

        public override void LoadContent()
        {
            _background = content.Load<Texture2D>("Menu/Green");

            var font = content.Load<SpriteFont>("Menu/Font");
            var playTexture = content.Load<Texture2D>("Menu/Play");
            var levelsTexture = content.Load<Texture2D>("Menu/Levels");

            // Scherm breedte ophalen om te centreren
            int screenW = game.GraphicsDevice.Viewport.Width;
            int screenH = game.GraphicsDevice.Viewport.Height;

            // Knoppen zijn 21 breed * 3 schaal = 63 pixels breed
            int btnWidth = (int)(playTexture.Width * 3f);
            int centerX = (screenW / 2) - (btnWidth / 2);

            // Play Knop (Met lege tekst "" omdat het plaatje zelf al tekst heeft)
            var playButton = new Button(playTexture, font, "", new Vector2(centerX, 150), 3f);
            playButton.Click += (s, e) => game.ChangeState(new GameState(game, content, 0));

            // Levels Knop
            var levelsButton = new Button(levelsTexture, font, "", new Vector2(centerX, 250), 3f);
            levelsButton.Click += (s, e) => game.ChangeState(new LevelSelectState(game, content));

            _components = new List<Button>() { playButton, levelsButton };
        }
        private void PlayButton_Click(object sender, EventArgs e)
        {
            // Start direct Level 1 (Index 0)
            game.ChangeState(new GameState(game, content, 0));
        }

        private void LevelsButton_Click(object sender, EventArgs e)
        {
            // Ga naar het level kies scherm
            game.ChangeState(new LevelSelectState(game, content));
        }
        // In MenuState.Draw, LevelSelectState.Draw, etc:
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // 3. TEKEN DE ACHTERGROND EERST (Over het hele scherm)
            int w = game.GraphicsDevice.Viewport.Width;
            int h = game.GraphicsDevice.Viewport.Height;
            spriteBatch.Draw(_background, new Rectangle(0, 0, w, h), Color.White);

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
