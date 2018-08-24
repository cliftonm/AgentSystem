using System;
using System.Collections.Generic;
using System.Linq;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    // An array of equality expressions and maps.
    // For example:
    // match: hdurl !== undefined && hdurl != ""   map: {url: data.hdurl}
    // match: true                                 map: {url: data.url}        (this is the "default" case)
    // The first match that evaluates to true is used.
    // Matches are parsed in the order they are defined.
    // Matches don't execute things, they map data to a new data type and optionally a new context,
    // because it is data that drives the flow, not function calls.
    // So, the map must include dataType and responseDataType and optionally include context and responseContext.
    // If context and/or responseContext are left out, the context for the agent is used.
    public class MatchAgent : Agent
    {
        protected List<(Func<dynamic, dynamic, bool> condition, Func<dynamic, dynamic, dynamic> map)> matches =
            new List<(Func<dynamic, dynamic, bool>, Func<dynamic, dynamic, dynamic>)>();

        public MatchAgent(string context, string dataType, string responseDataType) : base(context, dataType, responseDataType)
        {
        }

        public MatchAgent Add(Func<dynamic, dynamic, bool> condition, Func<dynamic, dynamic, dynamic> map)
        {
            matches.Add((condition, map));

            return this;
        }

        public override void Call(IProcessor processor, dynamic data)
        {
            var match = matches.FirstOrDefault(t => t.condition(ContextData, data));

            if (!match.Equals((null, null)))
            {
                dynamic resp = match.map(ContextData, data);
                SetContextAndType(data, resp);
                processor.QueueData(resp);
            }
        }
    }
}
