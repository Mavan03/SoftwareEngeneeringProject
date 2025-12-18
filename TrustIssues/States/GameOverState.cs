using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TrustIssues.States
{
    public class GameOverState : State
    {
        public GameOverState(Game1 game, ContentManager content) : base(game, content)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Black);
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                game.ChangeState(new MenuState(game, content));
            }
        }

    }
}
