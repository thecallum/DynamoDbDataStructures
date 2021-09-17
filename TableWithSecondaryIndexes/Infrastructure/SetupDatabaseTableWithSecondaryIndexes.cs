using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TableWithSecondaryIndexes.Infrastructure
{
    public class SetupDatabaseTableWithSecondaryIndexes
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly IDynamoDBContext _context;

        public SetupDatabaseTableWithSecondaryIndexes(AmazonDynamoDBClient client, IDynamoDBContext context)
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
                            AttributeName = "accountId",
                            AttributeType = "S"
                        },
                        new AttributeDefinition
                        {
                            AttributeName = "noteId",
                            AttributeType = "S"
                        },
                        new AttributeDefinition
                        {
                            AttributeName = "created",
                            AttributeType = "S"
                        }
                    },
             
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 2,
                    WriteCapacityUnits = 2
                }
            };

            request.KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement() { AttributeName = "accountId", KeyType = "HASH" },  //Partition key
                new KeySchemaElement() { AttributeName = "noteId", KeyType = "RANGE" }  //Sort key
            };

 

            request.LocalSecondaryIndexes = CreateLocalSecondaryIndexes();

            await _client.CreateTableAsync(request).ConfigureAwait(false);
        }

        private List<LocalSecondaryIndex> CreateLocalSecondaryIndexes()
        {
            return new List<LocalSecondaryIndex>()
            {
                CreateCreatedIndexLocalSecondaryIndex()
            };
        }

        private LocalSecondaryIndex CreateCreatedIndexLocalSecondaryIndex()
        {
            var createdIndexSchema = new List<KeySchemaElement>
            {
                new KeySchemaElement() { AttributeName = "accountId", KeyType = "HASH" },  //Partition key
                new KeySchemaElement() { AttributeName = "created", KeyType = "RANGE" }  //Sort key
            };

            return new LocalSecondaryIndex
            {
                IndexName = "CreatedIndex",
                KeySchema = createdIndexSchema,
                Projection = new Projection { ProjectionType = ProjectionType.ALL }
            };
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
