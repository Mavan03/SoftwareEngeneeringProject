using Microsoft.Xna.Framework;
using TrustIssues.Entities;

namespace TrustIssues.Input
{
    internal class MoveCommand : ICommand
    {
        private Vector2 velocity;

        public MoveCommand(Vector2 velocity)
        {
            this.velocity = velocity;
        }
        public void Execute(Player player)
        {
            player.Move(velocity);
        }
    }
}
