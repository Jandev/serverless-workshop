using System.Collections.Generic;
using System.Threading.Tasks;
using BFYOC.Api.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BFYOC.Api
{
    public static class Products
    {
        private static IList<Product> productCollection { get; set; } = new List<Product>
        {
            new Product
            {
                Id = 1,
                Flavor = "Rainbow Road",
                PricePerScoop = 3.99m
            }
        };

        [FunctionName("Products")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{id}")] HttpRequest req,
            int id,
            ILogger log)
        {
            if (id != 1)
            {
                log.LogInformation($"The specified identifier `{id}` can not be found.");
                return new NotFoundResult();
            }

            return new OkObjectResult(productCollection[0]);
        }
    }
}
