using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    /// <summary>
    /// Terminal agent.  Message is not re-queued.
    /// </summary>
    public class ConsoleLogAgent : Agent
    {
        public ConsoleLogAgent(string context, string dataType, string responseDataType) : base(context, dataType, responseDataType)
        {
        }

        public override void Call(IProcessor processor, dynamic data)
        {
            if (data is ExpandoObject)
            {
                ((IDictionary<string, object>)data).Where(kvp => kvp.Key != "Context" && kvp.Key != "Type").ForEach(kvp => Console.WriteLine(kvp.Key + " = " + kvp.Value.ToString()));
            }

            //data.Context = ResponseContext ?? data.Context;
            //data.Type = ResponseDataType;
            //processor.QueueData(data);
        }
    }
}
