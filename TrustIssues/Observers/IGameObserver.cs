using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustIssues.Observers
{
    public interface IGameObserver
    {
        void OnNotify(string eventName);
    }
}
