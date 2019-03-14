using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BFYOC.Api
{
    public static class IcecreamOrder
    {
        [FunctionName("IcecreamOrder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var content = new StreamReader(req.Body).ReadToEndAsync().Result;
            var icecreamOrder = JsonConvert.DeserializeObject<ViewModels.IcecreamOrder>(content);

            string topicEndpoint = Environment.GetEnvironmentVariable("icecreamOrdersTopicEndpoint", EnvironmentVariableTarget.Process);
            string topicKey = Environment.GetEnvironmentVariable("icecreamOrdersTopicKey", EnvironmentVariableTarget.Process);

            string topicHostname = new Uri(topicEndpoint).Host;
            TopicCredentials topicCredentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(topicCredentials);

            var @event = new EventGridEvent(
                id: Guid.NewGuid().ToString("N"),
                subject: "BFYOC/stores/serverlessWorkshop/orders",
                dataVersion: "2.0",
                eventType: nameof(IcecreamOrder),
                data: icecreamOrder,
                eventTime: DateTime.UtcNow
                );

            await client.PublishEventsAsync(topicHostname, new List<EventGridEvent>{ @event });

            return new OkObjectResult(@event.Id);
        }
    }
}
