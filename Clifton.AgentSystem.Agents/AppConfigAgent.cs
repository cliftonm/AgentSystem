using System.Configuration;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    public class AppSettingAgent : Agent
    {
        public AppSettingAgent(string context, string dataType, string responseDataType) : base(context, dataType, responseDataType)
        {
        }

        public override void Call(IProcessor processor, dynamic data)
        {
            string setting = ConfigurationManager.AppSettings.Get(data.AppSetting);
            var resp = AgentType.New().KeyValue("Value", setting).Get();
            SetContextAndType(data, resp);
            processor.QueueData(resp);
        }
    }
}
