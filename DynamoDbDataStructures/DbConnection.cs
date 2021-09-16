using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDbDataStructures
{
    public class DbConnection
    {
        public AmazonDynamoDBClient Client { get; set; }
        public IDynamoDBContext Context { get; set; }
    }
}
