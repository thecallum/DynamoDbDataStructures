using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTable.Infrastructure
{
    public class SetupDatabase
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly IDynamoDBContext _context;

        public SetupDatabase(AmazonDynamoDBClient client, IDynamoDBContext context)
        {
            _client = client;
            _context = context;
        }

        public async Task SetupTables()
        {
            await DeleteDatabaseAsync();
            await CreateDatabaseAsync();
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

            await _client.CreateTableAsync(request).ConfigureAwait(false);
        }

        private async Task DeleteDatabaseAsync()
        {
            try
            {
                await _client.DeleteTableAsync("Notes").ConfigureAwait(false);
            }
            catch (Exception)
            {

            }
        }
    }
}
