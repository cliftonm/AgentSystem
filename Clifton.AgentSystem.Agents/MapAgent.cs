using System;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    /// <summary>
    /// Executes a script mapping, using either the Map specified in the data or the script in the Map property.
    /// </summary>
    public class MapAgent : Agent
    {
        public Func<dynamic, dynamic, dynamic> Map { get; set; }

        protected bool useAgentContextAndType;

        public MapAgent(string context, string dataType, string responseDataType, bool useAgentContextAndType = true) : base(context, dataType, responseDataType)
        {
            this.useAgentContextAndType = useAgentContextAndType;
        }

        public override void Call(IProcessor processor, dynamic data)
        {
            var resp = Map == null ? ((Func<dynamic, dynamic, bool>)data.Map)(ContextData, data) : Map(ContextData, data);
            SetContextAndType(data, resp, useAgentContextAndType);
            processor.QueueData(resp);
        }
    }
}
