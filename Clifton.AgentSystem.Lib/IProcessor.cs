using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clifton.AgentSystem.Lib
{
    public interface IProcessor
    {
        void QueueData(dynamic data);
    }
}
