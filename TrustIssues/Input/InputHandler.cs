using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TrustIssues.Input;
using TrustIssues.Entities;

namespace TrustIssues.Input
{
    public class InputHandler
    {
        private ICommand moveLeft;
        private ICommand moveRight;
        private ICommand jump;


        public InputHandler()
        {
            //snelheid
            moveLeft = new MoveCommand(new Vector2(-5, 0));
            moveRight = new MoveCommand(new Vector2(5, 0));
            jump = new JumpCommand();
        }
        public void Update(Player player)
        {
            var state = Keyboard.GetState();

            if(state.IsKeyDown(Keys.A)|| state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
            {
                moveLeft.Execute(player);
            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                moveRight.Execute(player);
            }
            if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Z))
            {
                jump.Execute(player);
            }
        }
    }
}
