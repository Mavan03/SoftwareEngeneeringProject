using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TrustIssues.States
{
    public class MenuState : State
    {
        public MenuState(Game1 game, ContentManager content) : base(game, content) { }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Red);
        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
        
        }
    }
}
