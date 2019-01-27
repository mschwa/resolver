using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Resolver.Case.Api.Snapshot;

namespace Resolver.Case.Api.Services
{
    public class StorageRepository : IStorageRepository
    {
        public async Task<Models.Case> GetCaseFromStorageQueueAsync()
        {
            //Retrieve storage account from connection string
            CloudStorageAccount storageAccount = CaseBuilder.CreateStorageAccountFromConnectionString();

            // Create the queue client
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            CloudQueue queue = queueClient.GetQueueReference("case");
            await queue.FetchAttributesAsync();

            var cases = new List<Models.Case>();

            foreach (var message in await queue.PeekMessagesAsync(queue.ApproximateMessageCount.Value))
            {
                // Display message.
                Console.WriteLine(message.AsString);

                var myCase = JsonConvert.DeserializeObject<Models.Case>(message.AsString);
                cases.Add(myCase);
            }

            return new Models.Case();
        }

        public async Task<CaseSnapshot> GetCaseSnapshotFromStorageTableAsync(string caseId)
        {
            //Retrieve storage account from connection string
            CloudStorageAccount storageAccount = CaseBuilder.CreateStorageAccountFromConnectionString();

            // Create the queue client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to a queue
            CloudTable table = tableClient.GetTableReference("CaseSnapshot");

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<CaseSnapshot>("2019", caseId);

            // Execute the retrieve operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            return ((CaseSnapshot)retrievedResult.Result);
        }

        public async Task<bool> SubmitCaseSnapshotToStorageTableAsync(CaseSnapshot caseSnapshot)
        {
            //Retrieve storage account from connection string
            CloudStorageAccount storageAccount = CaseBuilder.CreateStorageAccountFromConnectionString();

            // Create the queue client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to a queue
            CloudTable table= tableClient.GetTableReference("CaseSnapshot");

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(caseSnapshot);

            // Execute the insert operation.
            var result = await table.ExecuteAsync(insertOperation);

            var success = (result.HttpStatusCode == 200 || result.HttpStatusCode == 201);

            return Task.FromResult(success).GetAwaiter().GetResult();
        }

        public async Task<bool> ReplaceCaseSnapshotToStorageTableAsync(CaseSnapshot caseSnapshot)
        {
            //Retrieve storage account from connection string
            CloudStorageAccount storageAccount = CaseBuilder.CreateStorageAccountFromConnectionString();

            // Create the queue client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to a queue
            CloudTable table = tableClient.GetTableReference("CaseSnapshot");

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Replace(caseSnapshot);

            // Execute the insert operation.
            var result = await table.ExecuteAsync(insertOperation);

            var success = (result.HttpStatusCode == 200 || result.HttpStatusCode == 201);

            return Task.FromResult(success).GetAwaiter().GetResult();
        }


    }
}
