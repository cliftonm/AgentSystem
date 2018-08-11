using System.Collections.Generic;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    public abstract class Agent : IAgent
    {
        public string Context { get; protected set; }
        public string DataType { get; protected set; }
        public string ResponseContext { get; set; }
        public string ResponseDataType { get; set; }
        public dynamic ContextData { get; set; }

        public Agent(string context, string dataType, string responseDataType)
        {
            Context = context;
            DataType = dataType;
            ResponseDataType = responseDataType;
        }

        public abstract void Call(IProcessor processor, dynamic data);

        public void SetContextAndType(dynamic data, dynamic resp, bool useAgentContextAndType)
        {
            if (useAgentContextAndType || !((IDictionary<string, object>)resp).ContainsKey("Context"))
            {
                resp.Context = ResponseContext ?? Context;
            }

            if (useAgentContextAndType || !((IDictionary<string, object>)resp).ContainsKey("Type"))
            {
                resp.Type = ResponseDataType;
            }
        }
    }
}
