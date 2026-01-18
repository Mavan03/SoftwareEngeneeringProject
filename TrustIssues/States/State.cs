using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TrustIssues.States
{
    public abstract class State
    {
        protected Game1 game;
        protected ContentManager content;

        public State(Game1 game, ContentManager content)
        {
            this.game = game;
            this.content = content;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}