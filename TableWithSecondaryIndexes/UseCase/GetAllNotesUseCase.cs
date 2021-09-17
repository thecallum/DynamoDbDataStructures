using Amazon.DynamoDBv2.DataModel;
using TableWithSecondaryIndexes.Domain;
using TableWithSecondaryIndexes.Gateway;
using TableWithSecondaryIndexes.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TableWithSecondaryIndexes.UseCase
{
    public class GetAllNotesUseCase : IGetAllNotesUseCase
    {
        private NotesDbGateway _notesGateway;

        public GetAllNotesUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<IEnumerable<Note>> Execute(Guid accountId)
        {
            var response = await _notesGateway.GetAllNotes(accountId).ConfigureAwait(false);

            return response;
        }
    }
}
