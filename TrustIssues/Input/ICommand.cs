using TrustIssues.Entities;

namespace TrustIssues.Input
{
    public interface ICommand
    {
        void Execute(Player player);
    }
}