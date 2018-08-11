using System.Net.Http;
using System.Drawing;

using Clifton.AgentSystem.Lib;

namespace Clifton.AgentSystem.Agents
{
    /// <summary>
    /// Adds the Image property to the existing data.
    /// </summary>
    public class HttpGetImageAgent : Agent
    {
        public HttpGetImageAgent(string context, string dataType, string responseDataType) : base(context, dataType, responseDataType)
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
                        var image = Image.FromStream(stream);
                        data.Image = image;
                        data.Context = ResponseContext ?? data.Context;
                        data.Type = ResponseDataType;
                        processor.QueueData(data);
                    }
                }
            }
        }
    }
}
