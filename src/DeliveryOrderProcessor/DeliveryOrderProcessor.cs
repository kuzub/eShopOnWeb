using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DeliveryOrderProcessor;

public static class DeliveryOrderProcessor
{
    [FunctionName("DeliveryOrderProcessor")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        [CosmosDB(
                databaseName: "deliverydepartment",
                containerName: "orders",
                Connection = "CosmosDBConnection",
                CreateIfNotExists = true,
                PartitionKey = "/buyerId")] IAsyncCollector<Order> ordersOut,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var order = JsonConvert.DeserializeObject<Order>(requestBody);

        await ordersOut.AddAsync(order);

        return new OkObjectResult($"Data added to Cosmos DB");
    }
}
