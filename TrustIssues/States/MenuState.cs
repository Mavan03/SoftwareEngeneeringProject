using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Controls;

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

            int centerX = (game.GraphicsDevice.Viewport.Width / 2) - ((int)(playTexture.Width * 3f) / 2);

            var playButton = new Button(playTexture, font, "", new Vector2(centerX, 150), 3f);
            playButton.Click += (s, e) => game.ChangeState(new GameState(game, content, 0));

            var levelsButton = new Button(levelsTexture, font, "", new Vector2(centerX, 250), 3f);
            levelsButton.Click += (s, e) => game.ChangeState(new LevelSelectState(game, content));

            _components = new List<Button>() { playButton, levelsButton };
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