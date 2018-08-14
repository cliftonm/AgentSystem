using System.Collections.Generic;
using System.Dynamic;

namespace Clifton.AgentSystem.Lib
{
    /// <summary>
    /// Supports a fluent style for creating types in the Agent System.
    /// Ex: var hello = AgentType.New("App", "LogMessage").KeyValue("Message", "Agent System Running").Get();
    /// </summary>
    public class AgentType
    {
        protected Dictionary<string, dynamic> fields = new Dictionary<string, dynamic>();

        protected AgentType()
        {
        }

        public static AgentType New()
        {
            return new AgentType();
        }

        public static AgentType New(string context, string type)
        {
            return new AgentType().Context(context).Type(type);
        }

        public static dynamic CreateExpando(Dictionary<string, dynamic> collection)
        {
            var obj = (IDictionary<string, object>)new ExpandoObject();
            collection.ForEach(kvp => obj[kvp.Key] = kvp.Value);

            return obj;
        }

        public AgentType Context(string context)
        {
            fields.Add(Constants.Context, context);

            return this;
        }

        public AgentType Type(string type)
        {
            fields.Add(Constants.Type, type);

            return this;
        }

        public AgentType KeyValue(string key, string value)
        {
            fields.Add(key, value);

            return this;
        }

        public dynamic Get()
        {
            var exp = CreateExpando(fields);

            return exp;
        }
    }
}
