using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TrustIssues.Entities;

namespace TrustIssues.Input
{
    public class InputHandler
    {
        private ICommand _moveLeft, _moveRight, _jump;

        public InputHandler()
        {
            _moveLeft = new MoveCommand(new Vector2(-1, 0));
            _moveRight = new MoveCommand(new Vector2(1, 0));
            _jump = new JumpCommand();
        }

        public void Update(Player player)
        {
            var state = Keyboard.GetState();

            // Links 
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
                _moveLeft.Execute(player);

            // Rechts
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
                _moveRight.Execute(player);

            // Springen 
            if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Z))
                _jump.Execute(player);
        }
    }
}