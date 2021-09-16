using Amazon.DynamoDBv2.DataModel;
using SimpleTable.Domain;
using SimpleTable.Gateway;
using SimpleTable.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTable.UseCase
{
    public class GetAllNotesUseCase : IGetAllNotesUseCase
    {
        private NotesDbGateway _notesGateway;

        public GetAllNotesUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<IEnumerable<Note>> Execute()
        {
            var response = await _notesGateway.GetAllNotes().ConfigureAwait(false);

            return response;
        }
    }
}
