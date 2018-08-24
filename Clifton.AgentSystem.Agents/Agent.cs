using System.Collections.Generic;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    public abstract class Agent : IAgent
    {
        /// <summary>
        /// Used to extract stringified names of dynamic keys.
        /// </summary>
        private static dynamic ddata = new object();

        public const string RESPONSE_CONTEXT_KEY = nameof(ddata.ResponseContext);
        public const string RESPONSE_TYPE_KEY = nameof(ddata.ResponseType);

        /// <summary>
        /// The agent's default context.
        /// </summary>
        public string Context { get; protected set; }

        /// <summary>
        /// The agent's input data type.
        /// </summary>
        public string DataType { get; protected set; }

        /// <summary>
        /// The agent's default response context.
        /// </summary>
        public string ResponseContext { get; protected set; }

        /// <summary>
        /// The agent's default response data type.
        /// </summary>
        public string ResponseDataType { get; protected set; }

        /// <summary>
        /// Essentially an ExpandoObject of key-value pair constants that this agent always uses.
        /// </summary>
        public dynamic ContextData { get; protected set; }

        /// <summary>
        /// Instantiate the agent with the required input context and data type and response data type.
        /// The response context is optional and if not specified the agent will publish its response
        /// using the input context.
        /// </summary>
        public Agent(string context, string dataType, string responseDataType, string responseContext = null)
        {
            Context = context;
            DataType = dataType;
            ResponseDataType = responseDataType;
            ResponseContext = responseContext;
        }

        /// <summary>
        /// The sub-class implements this method to process the data.
        /// </summary>
        public abstract void Call(IProcessor processor, dynamic data);

        /// <summary>
        /// Logic for setting the context and type of the response data.
        /// 1. If the data itself contains the key ResponseContext, use that key's value.
        /// </summary>
        public void SetContextAndType(dynamic data, dynamic resp)
        {
            var dict = (IDictionary<string, object>)data;

            // Use the value of ResponseContext if the data itself specifies a response context.
            if (dict.ContainsKey(nameof(data.ResponseContext)))
            {
                resp.Context = data.ResponseContext;
            }
            else
            {
                // Use the response context if provided, otherwise use the agent's context defined at instantiation.
                resp.Context = ResponseContext ?? Context;
            }

            // Similarly, if the data itself defines a response type, use it.
            if (dict.ContainsKey(nameof(data.ResponseType)))
            {
                resp.Type = data.ResponseType;
            }
            else
            {
                // Otheriwse use the response type defined at instantiation.
                resp.Type = ResponseDataType;
            }
        }
    }
}
