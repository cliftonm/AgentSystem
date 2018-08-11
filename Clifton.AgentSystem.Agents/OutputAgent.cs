using System;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    public class OutputAgent : Agent
    {
        protected Action<dynamic> action;

        public OutputAgent(string context, string dataType, string responseDataType, Action<dynamic> action) : base(context, dataType, responseDataType)
        {
            this.action = action;
        }

        public override void Call(IProcessor processor, dynamic data)
        {
            action(data);
            data.Context = ResponseContext ?? data.Context;
            data.Type = ResponseDataType;
            processor.QueueData(data);
        }
    }
}
