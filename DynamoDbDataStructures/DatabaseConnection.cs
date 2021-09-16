using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDbDataStructures
{
    public class DatabaseConnection
    {
        public readonly AmazonDynamoDBClient Client;
        public readonly IDynamoDBContext Context;

        public DatabaseConnection()
        {
            var dbConnection = ConnectToDatabase();

            Client = dbConnection.Client;
            Context = dbConnection.Context;
        } 
       
        private static DbConnection ConnectToDatabase()
        {
            var clientConfig = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000" };
            var client = new AmazonDynamoDBClient(clientConfig);

            var context = new DynamoDBContext(client);

            return new DbConnection
            {
                Client = client,
                Context = context
            };
        }
    }
}
