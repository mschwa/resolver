using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Resolver.Case.Api.Models;
using Resolver.Case.Api.Snapshot;

namespace Resolver.Case.Api.Services
{
    public class CaseBuilder
    {
        private readonly IMapper _mapper;
        private readonly IStorageRepository _storageRepository;
        
        public CaseBuilder(IStorageRepository storageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        /// <summary>
        /// Create a queue for the sample application to process messages in. 
        /// </summary>
        /// <returns>A CloudQueue object</returns>
        public async Task<CloudQueue> CreateQueueAsync(string queueName)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString();

            // Create a queue client for interacting with the queue service
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            Console.WriteLine("1. Create a queue for the demo");

            CloudQueue queue = queueClient.GetQueueReference(queueName);
            try
            {
                await queue.CreateIfNotExistsAsync();
            }
            catch
            {
                Console.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator.  ess the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return queue;
        }

        /// <summary>
        /// Validate the connection string information in app.config and throws an exception if it looks like 
        /// the user hasn't updated this to valid values. 
        /// </summary>
        /// <param name="storageConnectionString">The storage connection string</param>
        /// <returns>CloudStorageAccount object</returns>
        public static CloudStorageAccount CreateStorageAccountFromConnectionString()
        {
            string storageConnectionString =
                "DefaultEndpointsProtocol=https;AccountName=resolver;AccountKey=pZM4DWJRctnU0OLujNaNMwZvd9VB9nVvmhZ/Epjh760/tXaeZtfbKLgot6OWMOyYCE+79Hvtp1PsZL34x3x32Q==";

            CloudStorageAccount storageAccount;

            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.{e.Message}");
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        public static void WriteException(Exception ex)
        {
            Console.WriteLine("Exception thrown. {0}, msg = {1}", ex.Source, ex.Message);
        }

        public void BuilCase(string xCase)
        {
            // Find the event the Queue for the first time the case appeared as a subject in the queue (Event = "AddCase", Subject = xCase.caseId)

            // Read the events, and do the following in paralell until the end of queue...

            // - Check to see what type of event it is - 
            //      - Case  Create
            //      -       Update
            //      -       Entity Assignment/Removal (currently just Claimant / Insurer)
            //      - Claimant  Update
            //      - Insurer   Update


        }

        public void FindFirstEvent()
        {
        }

        public async Task<bool> ProcessCreateCase(string caseId, Models.Case xCase)
        {
            var snapShot = new CaseSnapshot(caseId, "2019");
            snapShot = _mapper.Map(xCase, snapShot);
            
            return await _storageRepository.SubmitCaseSnapshotToStorageTableAsync(snapShot);
        }

        public async Task<bool> ProcessUpdateCase(string caseId, Models.Case xCase)
        {
            var snapShot = new CaseSnapshot(caseId, "2019");
            snapShot = _mapper.Map(xCase, snapShot);
            
            return await _storageRepository.SubmitCaseSnapshotToStorageTableAsync(snapShot);
        }
        
        public async Task<bool> ProcessClaimantUpdate(string caseId, Claimant claimant)
        {
            var snapshot = await _storageRepository.GetCaseSnapshotFromStorageTableAsync(caseId);
            snapshot.Claimant = claimant; // TODO: Map updates

            return await _storageRepository.ReplaceCaseSnapshotToStorageTableAsync(snapshot);
        }

        public async Task<bool> PrcessClaimantAssignment(string caseId, Claimant claimant)
        {
            var snapshot = await _storageRepository.GetCaseSnapshotFromStorageTableAsync(caseId);
            snapshot.Claimant = claimant;

            return await _storageRepository.ReplaceCaseSnapshotToStorageTableAsync(snapshot);
        }

        public async Task<bool> ProcessClaimantRemoval(string caseId)
        {
            var snapshot = await _storageRepository.GetCaseSnapshotFromStorageTableAsync(caseId);
            snapshot.Claimant = null;

            return await _storageRepository.ReplaceCaseSnapshotToStorageTableAsync(snapshot);
        }

        public async Task<bool> ProcessInsurerUpdate(string caseId, Insurer insurer)
        {
            var snapshot = await _storageRepository.GetCaseSnapshotFromStorageTableAsync(caseId);
            snapshot.Insurer = insurer; // TODO: Map updates

            return await _storageRepository.ReplaceCaseSnapshotToStorageTableAsync(snapshot);
        }

        public async Task<bool> PrcessInsurerAssignment(string caseId, Insurer insurer)
        {
            var snapshot = await _storageRepository.GetCaseSnapshotFromStorageTableAsync(caseId);
            snapshot.Insurer = insurer;

            return await _storageRepository.ReplaceCaseSnapshotToStorageTableAsync(snapshot);
        }

        public async Task<bool> ProcessInsurerRemoval(string caseId)
        {
            var snapshot = await _storageRepository.GetCaseSnapshotFromStorageTableAsync(caseId);
            snapshot.Insurer = null;

            return await _storageRepository.ReplaceCaseSnapshotToStorageTableAsync(snapshot);
        }

        
    }

    public class EventData
    {
        public string Id { get; set; }

        public string Subject { get; set; }

        public Models.Case Data { get; set; }

        public string EventType { get; set; }

        public DateTime EventTime { get; set; }

        public static EventData FromJson(string data)
        {
            return JsonConvert.DeserializeObject<EventData>(data);
        }

        //{
        //    "id": "b7e9b8b7-306d-4ea9-9ac2-34ba54a4162e",
        //    "subject": "b7e9b8b7-306d-4ea9-9ac2-34ba54a4162e",
        //    "data": {
        //        "Id": "b7e9b8b7-306d-4ea9-9ac2-34ba54a4162e",
        //        "DisplayName": "Kid in an Auto Accident"
        //    },
        //    "eventType": "AddCase",
        //    "eventTime": "2018-12-31T19:51:48.437228Z",
        //    "dataVersion": "1.0",
        //    "metadataVersion": "1",
        //    "topic": "/subscriptions/ed95ccfe-fe8f-4032-a293-cdde1c151afe/resourceGroups/Resolver.POC/providers/Microsoft.EventGrid/topics/resolver-case"
        //}
    }
}
