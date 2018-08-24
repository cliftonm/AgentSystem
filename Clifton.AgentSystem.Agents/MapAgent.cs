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

        public MapAgent(string context, string dataType, string responseDataType) : base(context, dataType, responseDataType)
        {
        }

        public override void Call(IProcessor processor, dynamic data)
        {
            var resp = Map == null ? ((Func<dynamic, dynamic, bool>)data.Map)(ContextData, data) : Map(ContextData, data);
            SetContextAndType(data, resp);
            processor.QueueData(resp);
        }
    }
}
