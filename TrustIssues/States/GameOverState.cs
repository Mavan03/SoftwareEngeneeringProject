using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TrustIssues.States
{
    public class GameOverState : State
    {
        private int _levelToRestart;

        // Optioneel: laad een font als je dat hebt, anders tekenen we geen tekst
        // private SpriteFont _font; 

        public GameOverState(Game1 game, ContentManager content, int levelIndex) : base(game, content)
        {
            _levelToRestart = levelIndex;
        }

        public override void LoadContent()
        {
            // _font = content.Load<SpriteFont>("File"); 
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Black);

            // Als je een font hebt, kun je hier instructies tekenen:
            // spriteBatch.Begin();
            // spriteBatch.DrawString(_font, "GAME OVER", new Vector2(300, 200), Color.Red);
            // spriteBatch.DrawString(_font, "Press R to Restart Level", new Vector2(300, 250), Color.White);
            // spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            var mouse = Mouse.GetState();

            // OPTIE 1: HERSTARTEN (Druk op R of Linkermuisknop)
            if (keyboard.IsKeyDown(Keys.R) || mouse.LeftButton == ButtonState.Pressed)
            {
                // Hier gebruiken we de variabele _levelToRestart!
                game.ChangeState(new GameState(game, content, _levelToRestart));
            }

            // OPTIE 2: HOOFDMENU (Druk op M of Escape)
            if (keyboard.IsKeyDown(Keys.M) || keyboard.IsKeyDown(Keys.Escape))
            {
                game.ChangeState(new MenuState(game, content));
            }
        }
    }
}