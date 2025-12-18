using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;

namespace TrustIssues.States
{
    public class VictoryState : State
    {
        public VictoryState(Game1 game, ContentManager content) : base(game, content) { }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Gold);
        }

        public override void LoadContent()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                game.ChangeState(new MenuState(game, content));
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
