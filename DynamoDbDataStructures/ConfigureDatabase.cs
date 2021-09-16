using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDbDataStructures
{
    public class ConfigureDatabase
    {
        private readonly DbConnection _dbConnection;

        public IDynamoDBContext Context
        {
            get {  return _dbConnection.Context; }
        }

        public ConfigureDatabase()
        {
            _dbConnection = ConnectToDatabase();

        }

        public async Task SetupDatabase()
        {
            await DeleteDatabaseAsync();
            await CreateDatabaseAsync();
        }

        private async Task DeleteDatabaseAsync()
        {
            try
            {
                await _dbConnection.Client.DeleteTableAsync("Notes").ConfigureAwait(false);
            }
            catch (Exception)
            {

            }
        }

        private async Task CreateDatabaseAsync()
        {
            string tableName = "Notes";

            var request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                    {
                        new AttributeDefinition
                        {
                            AttributeName = "id",
                            AttributeType = "S"
                        }
                    },
                KeySchema = new List<KeySchemaElement>()
                  {
                    new KeySchemaElement
                    {
                      AttributeName = "id",
                      KeyType = "HASH"  //Partition key
                    }
                  },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 2,
                    WriteCapacityUnits = 2
                }
            };

            await _dbConnection.Client.CreateTableAsync(request).ConfigureAwait(false);
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
