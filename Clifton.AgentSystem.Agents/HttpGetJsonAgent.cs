using System.Dynamic;
using System.IO;
using System.Net.Http;

using Newtonsoft.Json;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    public class HttpGetJsonAgent : Agent
    {
        public HttpGetJsonAgent(string context, string dataType, string responseDataType) : base(context, dataType, responseDataType)
        {
        }

        public async override void Call(IProcessor processor, dynamic data)
        {
            HttpClient client = new HttpClient();
            using (var response = await client.GetAsync(data.url, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            // https://blogs.msdn.microsoft.com/henrikn/2012/02/17/httpclient-downloading-to-a-local-file/
                            var str = await streamReader.ReadToEndAsync();
                            dynamic resp = JsonConvert.DeserializeObject<ExpandoObject>(str);
                            resp.Context = ResponseContext ?? data.Context;
                            resp.Type = ResponseDataType;
                            processor.QueueData(resp);
                        }
                    }
                }
            }
        }
    }
}
