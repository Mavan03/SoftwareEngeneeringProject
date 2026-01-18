using Microsoft.Xna.Framework;
using TrustIssues.Entities;

namespace TrustIssues.Input
{
    public class MoveCommand : ICommand
    {
        private Vector2 _velocity;
        public MoveCommand(Vector2 velocity) { _velocity = velocity; }
        public void Execute(Player player) => player.Move(_velocity);
    }
}
