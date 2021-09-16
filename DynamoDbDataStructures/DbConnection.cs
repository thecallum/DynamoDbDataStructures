using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamoDbDataStructures
{
    public class DbConnection
    {
        public AmazonDynamoDBClient Client { get; set; }
        public IDynamoDBContext Context { get; set; }

    }
}
