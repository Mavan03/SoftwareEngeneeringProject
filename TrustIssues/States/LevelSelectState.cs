using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Controls;

namespace TrustIssues.States
{
    public class LevelSelectState : State
    {
        private List<Button> _components;
        public LevelSelectState(Game1 game, ContentManager content) : base(game, content) { }

        public override void LoadContent()
        {
            var font = content.Load<SpriteFont>("Menu/Font");
            var backTexture = content.Load<Texture2D>("Menu/Back");
            _components = new List<Button>();

            for (int i = 0; i < 3; i++)
            {
                int index = i;
                var tex = content.Load<Texture2D>("Menu/0" + (i + 1));
                var btn = new Button(tex, font, "", new Vector2(150 + (i * 150), 200), 3f);
                btn.Click += (s, e) => game.ChangeState(new GameState(game, content, index));
                _components.Add(btn);
            }

            var backBtn = new Button(backTexture, font, "", new Vector2(50, 50), 3f);
            backBtn.Click += (s, e) => game.ChangeState(new MenuState(game, content));
            _components.Add(backBtn);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            foreach (var c in _components) c.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime) { foreach (var c in _components) c.Update(gameTime); }
    }
}