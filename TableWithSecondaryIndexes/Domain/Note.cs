using System;
using System.Collections.Generic;
using System.Text;

namespace TableWithSecondaryIndexes.Domain
{
    public class Note
    {
        public string Title { get; set; }

        public string Contents { get; set; }

        public DateTime Created { get; set; }
    }
}
