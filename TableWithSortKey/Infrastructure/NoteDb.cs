using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TableWithSortKey.Infrastructure
{
    [DynamoDBTable("Notes", LowerCamelCaseProperties = true)]
    public class NoteDb
    {
        [DynamoDBHashKey]
        public Guid AccountId { get; set; }

        [DynamoDBRangeKey]
        public Guid NoteId { get; set; }

        [DynamoDBProperty]
        public string Title { get; set; }

        [DynamoDBProperty]
        public string Contents { get; set; }

        [DynamoDBProperty]
        public DateTime Created { get; set; }
    }
}
