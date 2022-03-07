using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Cosmos;

namespace PrisninjaDatabaseAPI
{
    public class PrisninjaDB
    {
        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];

        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "PrisninjaDatabase";
        private string containerId = "ItemContainer";

        public async Task DoEverything()
        {
            try
            {
                Console.WriteLine("Beginning operations...\n");
                await GetStartedDemoAsync();

            }
            catch (CosmosException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
        }

        public async Task GetStartedDemoAsync()
        {
            
            // Create a new instance of the Cosmos Client
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
           
            await this.CreateDatabaseAsync();
            await this.CreateContainerAsync();
            await this.AddToItemContainer();
            //await this.AddItemsToContainerAsync();
            //await this.QueryItemsAsync();
            //await this.ReplaceFamilyItemAsync();
            //await this.DeleteFamilyItemAsync();
            //await this.DeleteDatabaseAndCleanupAsync();
        }

        private async Task CreateDatabaseAsync()
        {

            // Create a new database
            
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            
            Console.WriteLine("Created Database: {0}\n", this.database.Id);
        }

        // </CreateDatabaseAsync>

        // <CreateContainerAsync>
        /// <summary>
        /// Create the container if it does not exist. 
        /// Specifiy "/LastName" as the partition key since we're storing family information, to ensure good distribution of requests and storage.
        /// </summary>
        /// <returns></returns>
        private async Task CreateContainerAsync()
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/EAN");
            Console.WriteLine("Created Container: {0}\n", this.container.Id);
        }



        public async Task AddToItemContainer()
        {
            Console.WriteLine("dhudfhsu");
            Item item1 = new Item
            {
                Id = "23",
                EAN = "3123",
                Name = "Skinke",
                Weight = 12,
                Price = 32
            };
            //try
            //{
                
            //    // Read the item to see if it exists.  
            //    ItemResponse<Item> itemToAddResponse = await this.container.ReadItemAsync<Item>(item1.EAN, new PartitionKey(item1.EAN));
            //    Console.WriteLine("Item in database with id: {0} already exists\n", itemToAddResponse.Resource.EAN);
            //}
            //catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            //{
                // Create an item in the container representing the item to add. Note we provide the value of the partition key for this item, which is the item Name
                ItemResponse<Item> itemToAddResponse = await this.container.CreateItemAsync<Item>(item1, new PartitionKey(item1.EAN));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", itemToAddResponse.Resource.EAN, itemToAddResponse.RequestCharge);
            //}
        }

        // <DeleteDatabaseAndCleanupAsync>
        /// <summary>
        /// Delete the database and dispose of the Cosmos Client instance
        /// </summary>
        private async Task DeleteDatabaseAndCleanupAsync()
        {
            DatabaseResponse databaseResourceResponse = await this.database.DeleteAsync();
            // Also valid: await this.cosmosClient.Databases["FamilyDatabase"].DeleteAsync();

            Console.WriteLine("Deleted Database: {0}\n", this.databaseId);

            //Dispose of CosmosClient
            this.cosmosClient.Dispose();
        }
    }
}
