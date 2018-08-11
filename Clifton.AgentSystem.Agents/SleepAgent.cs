﻿using System.Threading;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    public class SleepAgent : Agent
    {
        protected int msSleep;

        public SleepAgent(string context, string dataType, string responseDataType, int msSleep) : base(context, dataType, responseDataType)
        {
            this.msSleep = msSleep;
        }

        public override void Call(IProcessor processor, dynamic data)
        {
            Thread.Sleep(msSleep);
            data.Context = ResponseContext ?? data.Context;
            data.Type = ResponseDataType;
            processor.QueueData(data);
        }
    }
}
