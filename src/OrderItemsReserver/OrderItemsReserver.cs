using System;
using System.IO;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace OrderItemsReserver
{
    public class OrderItemsReserver
    {
        [FunctionName("OrderItemsReserver")]
        public static void Run(
                                [ServiceBusTrigger("orderitems", AutoCompleteMessages = true, Connection = "ServiceBusConnection")] ServiceBusReceivedMessage serviceBusMessage,
                                [Blob("orderitems/{rand-guid}.json", FileAccess.Write, Connection = "OrdersStorageConnection")] Stream outputBlob,
                                ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {serviceBusMessage}");
            log.LogInformation($"EnqueuedTimeUtc={serviceBusMessage.EnqueuedTime.UtcDateTime}");
            log.LogInformation($"DeliveryCount={serviceBusMessage.DeliveryCount}");
            log.LogInformation($"MessageId={serviceBusMessage.MessageId}");

            try
            {
                outputBlob.Write(serviceBusMessage.Body);
            }
            catch (Exception ex)
            {
                log.LogError($"Exception: {ex.Message}");
                throw;
            }
        }
    }
}
