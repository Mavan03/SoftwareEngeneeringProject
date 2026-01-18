using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Controls;

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
            _background = content.Load<Texture2D>("Menu/Gray");
            var font = content.Load<SpriteFont>("Menu/Font");
            var restartBtn = new Button(content.Load<Texture2D>("Menu/Restart"), font, "", new Vector2(250, 200), 3f);
            restartBtn.Click += (s, e) => game.ChangeState(new GameState(game, content, _levelToRestart));

            var menuBtn = new Button(content.Load<Texture2D>("Menu/Close"), font, "", new Vector2(450, 200), 3f);
            menuBtn.Click += (s, e) => game.ChangeState(new MenuState(game, content));

            _components = new List<Button>() { restartBtn, menuBtn };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_background, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
            foreach (var c in _components) c.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime) { foreach (var c in _components) c.Update(gameTime); }
    }
}