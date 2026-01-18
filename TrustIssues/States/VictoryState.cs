using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Controls;

namespace TrustIssues.States
{
    public class VictoryState : State
    {
        private List<Button> _components;
        private Texture2D _background;

        public VictoryState(Game1 game, ContentManager content) : base(game, content) { }

        public override void LoadContent()
        {
            _background = content.Load<Texture2D>("Menu/Yellow");
            var font = content.Load<SpriteFont>("Menu/Font");
            int centerX = (game.GraphicsDevice.Viewport.Width / 2) - 50;

            var menuBtn = new Button(content.Load<Texture2D>("Menu/Close"), font, "", new Vector2(centerX, 250), 3f);
            menuBtn.Click += (s, e) => game.ChangeState(new MenuState(game, content));

            _components = new List<Button>() { menuBtn };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_background, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);

            string text = "YOU WIN!";
            var font = content.Load<SpriteFont>("Menu/Font");
            Vector2 size = font.MeasureString(text);
            spriteBatch.DrawString(font, text, new Vector2((game.GraphicsDevice.Viewport.Width / 2) - (size.X / 2), 150), Color.Gold);

            foreach (var c in _components) c.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime) { foreach (var c in _components) c.Update(gameTime); }
    }
}