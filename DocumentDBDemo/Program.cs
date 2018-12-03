using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace DocumentDBDemo
{
    class Program
    {
        private const string EndpointUrl = "https://documentdb3.documents.azure.com:443/";
        private const string AuthorizationKey = "wbiB1ppk2XCEF3NBV7tcXYMHqgD8uVgif8JkaFjy08Wfw7maFTaAncBA9ZtdkakFTqpSFgfHDDMLjps59gR6iQ==";

        static void Main(string[] args)
        {
            try
            {
                CreateDocumentClient().Wait();
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }

            Console.ReadKey();
        }

        private static async Task CreateDocumentClient()
        {
            // Create a new instance of the DocumentClient
            using (var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey))
            {
                GetDatabases(client);
                //await CreateDatabase(client);
            }
        }

        private async static Task CreateDatabase(DocumentClient client)
        {
            Console.WriteLine();
            Console.WriteLine("******** Create Database *******");

            var databaseDefinition = new Database { Id = "mynewdb" };
            var result = await client.CreateDatabaseAsync(databaseDefinition);
            var database = result.Resource;

            Console.WriteLine(" Database Id: {0}; Rid: {1}", database.Id, database.ResourceId);
            Console.WriteLine("******** Database Created *******");
        }

        private static void GetDatabases(DocumentClient client)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("******** Get Databases List ********");

            var databases = client.CreateDatabaseQuery().ToList();

            foreach (var database in databases)
            {
                Console.WriteLine(" Database Id: {0}; Rid: {1}", database.Id, database.ResourceId);
            }

            Console.WriteLine();
            Console.WriteLine("Total databases: {0}", databases.Count);
        }
    }
}
