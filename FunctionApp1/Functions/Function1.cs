using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequestMessage req,
            [Inject(typeof(IDo))] IDo myDo,
            TraceWriter log)
        {
            string name = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "name", StringComparison.Ordinal) == 0).Value;
            dynamic data = await req.Content.ReadAsAsync<object>();

            name = name ?? data?.name;

            string message = myDo.GetNameMessage(name);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json")
            };
        }
    }
}
