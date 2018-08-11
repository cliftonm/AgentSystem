using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Dynamic;
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

        public void QueueData(dynamic data)
        {
            dataPool.Enqueue(data);
            dataSemaphore.Release();
        }

        protected void ProcessSynchronously()
        {
            dataPool.TryDequeue(out dynamic data);
            var agents = agentPool.Where(a => a.Context == data.Context && a.DataType == data.Type).ToList();
            Log(agents, data);
            agents.ForEach(agent => agent.Call(this, data));
        }

        protected void ProcessAsynchronously()
        {
            dataPool.TryDequeue(out dynamic data);
            var agents = agentPool.Where(a => a.Context == data.Context && a.DataType == data.Type).ToList();
            Log(agents, data);
            agents.ForEach(agent => { Task.Run(() => agent.Call(this, (object)data)); });
        }

        protected void Log(IEnumerable<IAgent> agents, dynamic data)
        {
            agents.ForEach(agent => Console.WriteLine("Invoking " + agent.GetType().Name + " : " + data.Context + "." + data.Type));
        }

        // This works as an Evaluate!
        // Map = "var today=new Date(); ({date: today.getFullYear()+'-'+('0' + (today.getMonth()+1)).slice(-2) +'-'+('0' + today.getDate()).slice(-2)});",

        public dynamic CreateExpando(Dictionary<string, dynamic> collection)
        {
            var obj = (IDictionary<string, object>)new ExpandoObject();
            collection.ForEach(kvp => obj[kvp.Key] = kvp.Value);

            return obj;
        }
    }
}
