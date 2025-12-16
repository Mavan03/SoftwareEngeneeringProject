using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustIssues.Entities;

namespace TrustIssues.Input
{
    public interface ICommand
    {
        void Execute(Player player);
    }
}
