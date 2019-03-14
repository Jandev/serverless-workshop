using BFYOC.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace BFYOC.Api
{
    public static class Products
    {
        [FunctionName("Products")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{id}")]
            HttpRequest req,
            int id,
            [CosmosDB(
                "%repositoryName%",
                "products",
                ConnectionStringSetting = "developerdayRepositoryConnectionString",
                Id = "{id}")]
            Product product,
            ILogger log)
        {
            if (product == null)
            {
                log.LogInformation("The product is not found.");
                return new NotFoundResult();
            }

            return new OkObjectResult(product);
        }
    }
}