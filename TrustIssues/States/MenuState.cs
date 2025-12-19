using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace TrustIssues.States
{
    public class MenuState : State
    {
        public MenuState(Game1 game, ContentManager content) : base(game, content) { }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Red);
            // spriteBatch.Begin();
            // spriteBatch.DrawString(...);
            // spriteBatch.End();
        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                game.ChangeState(new GameState(game, content)); 
            }
        }
    }
}
