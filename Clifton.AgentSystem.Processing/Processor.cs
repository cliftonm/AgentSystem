using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Processing
{
    public class Processor : IProcessor
    {
        protected ConcurrentBag<IAgent> agentPool = new ConcurrentBag<IAgent>();
        protected ConcurrentQueue<dynamic> dataPool = new ConcurrentQueue<dynamic>();
        protected Semaphore dataSemaphore = new Semaphore(0, int.MaxValue);

        public void RegisterAgent(IAgent agent)
        {
            agentPool.Add(agent);
        }

        public void QueueData(dynamic data)
        {
            dataPool.Enqueue(data);
            dataSemaphore.Release();
        }

        public void StartSynchronousProcessing()
        {
            StartProcessing(ProcessSynchronously);
        }

        public void StartAsynchronousProcessing()
        {
            StartProcessing(ProcessAsynchronously);
        }

        protected void StartProcessing(Action processor)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    dataSemaphore.WaitOne();
                    processor();
                }
            });
        }

        protected void ProcessSynchronously()
        {
            dataPool.TryDequeue(out dynamic data);
            var agents = agentPool.Where(a => (a.Context == data.Context || a.Context == Constants.AnyContext) && (a.DataType == data.Type || a.DataType == Constants.AnyDataType)).ToList();
            Log(agents, data);
            agents.ForEach(agent => agent.Call(this, data));
        }

        protected void ProcessAsynchronously()
        {
            dataPool.TryDequeue(out dynamic data);
            var agents = agentPool.Where(a => (a.Context == data.Context || a.Context == Constants.AnyContext) && a.DataType == data.Type).ToList();
            Log(agents, data);
            agents.ForEach(agent => { Task.Run(() => agent.Call(this, (object)data)); });
        }

        protected void Log(IEnumerable<IAgent> agents, dynamic data)
        {
            agents.ForEach(agent => Console.WriteLine("Invoking " + agent.GetType().Name + " : " + data.Context + "." + data.Type));
        }
    }
}
