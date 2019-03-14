using System.IO;
using BFYOC.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BFYOC.Api
{
    public static class AddProduct
    {
        [FunctionName("AddProduct")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            [CosmosDB(
                "%repositoryName%",
                "products",
                ConnectionStringSetting = "developerdayRepositoryConnectionString",
                CreateIfNotExists = true)]
            out dynamic createdProduct,
            ILogger log)
        {
            var content = new StreamReader(req.Body).ReadToEndAsync().Result;
            var product = JsonConvert.DeserializeObject<Product>(content);

            log.LogInformation($"Creating product `{product.Id}`");
            createdProduct = product;

            return new CreatedResult("products", product.Id);
        }
    }
}