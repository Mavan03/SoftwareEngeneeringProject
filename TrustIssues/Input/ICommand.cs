using TrustIssues.Entities;

namespace TrustIssues.Input
{
    //Interface seg
    public interface ICommand
    {
        void Execute(Player player);
    }
}