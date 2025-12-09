using Microsoft.Xna.Framework.Input;
using SoftwareEngeneeringProject.Commands;
using SoftwareEngeneeringProject.Interfaces;
using System.Collections.Generic;

namespace SoftwareEngeneeringProject.Input
{
    public class InputReader
    {
        //toetesen koppelen met commando
        public List<ICommand> ReadInput()
        {
            var commands = new List<ICommand>();
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Q))
            {
                commands.Add(new MoveLeftCommand());
            }
            if (state.IsKeyDown(Keys.D))
            {
                commands.Add(new MoveRightCommand());
            }
            if (state.IsKeyDown(Keys.Space))
            {
                commands.Add(new JumpCommand());
            }
            if (state.IsKeyDown(Keys.LeftControl))
            {
                commands.Add(new CrouchCommand());
            }

            return commands;
        }
    }
}
