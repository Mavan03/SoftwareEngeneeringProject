using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TrustIssues.Controls;
using TrustIssues.Core;

namespace TrustIssues.States
{
    public class LevelSelectState : State
    {
        private List<Button> _components;

        public LevelSelectState(Game1 game, ContentManager content) : base(game, content)
        {
        }

        public override void LoadContent()
        {
            var font = content.Load<SpriteFont>("Menu/Font");
            var backTexture = content.Load<Texture2D>("Menu/Back");

            _components = new List<Button>();

            int startX = 150;
            int gap = 150;

            for (int i = 0; i < 3; i++)
            {
                int levelNum = i + 1; // 1, 2, 3

                // HIER IS DE TRUC:
                // We maken de bestandsnaam dynamisch.
                // "0" + levelNum zorgt voor "01", "02", "03".
                // Pas dit aan als je bestanden anders heten (bijv "Menu/01").
                string textureName = "Menu/0" + levelNum;

                Texture2D levelTexture = content.Load<Texture2D>(textureName);

                Vector2 pos = new Vector2(startX + (i * gap), 200);

                // MAAK DE KNOP:
                // We geven nu een LEGE string ("") mee als tekst, 
                // want het nummer staat al op je plaatje!
                var levelBtn = new Button(levelTexture, font, "", pos, 3f);

                int index = i;
                levelBtn.Click += (s, e) => game.ChangeState(new GameState(game, content, index));

                _components.Add(levelBtn);
            }

            // De Terug knop
            // (Zorg dat je ook een 'Back' plaatje hebt, of gebruik tijdelijk iets anders)
            var backBtn = new Button(backTexture, font, "", new Vector2(50, 50), 3f);
            backBtn.Click += (s, e) => game.ChangeState(new MenuState(game, content));

            _components.Add(backBtn);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (var component in _components) component.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components) component.Update(gameTime);
        }
    }
}