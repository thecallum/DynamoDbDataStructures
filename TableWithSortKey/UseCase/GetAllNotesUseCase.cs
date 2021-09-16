using Amazon.DynamoDBv2.DataModel;
using TableWithSortKey.Domain;
using TableWithSortKey.Gateway;
using TableWithSortKey.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TableWithSortKey.UseCase
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
