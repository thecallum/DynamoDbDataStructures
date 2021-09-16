using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTable.Infrastructure
{
    [DynamoDBTable("Notes", LowerCamelCaseProperties = true)]
    public class NoteDb
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }

        [DynamoDBProperty]
        public string Title { get; set; }

        [DynamoDBProperty]
        public string Contents { get; set; }

        [DynamoDBProperty]
        public DateTime Created { get; set; }
    }
}
