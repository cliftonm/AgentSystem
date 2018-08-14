using System.Configuration;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    public class AppConfigAgent : Agent
    {
        public AppConfigAgent(string context, string dataType, string responseDataType) : base(context, dataType, responseDataType)
        {
        }

        public override void Call(IProcessor processor, dynamic data)
        {
            data.Value = ConfigurationManager.AppSettings.Get(data.Key);
            data.Context = ResponseContext ?? data.Context;
            data.Type = ResponseDataType;
            processor.QueueData(data);
        }
    }
}
