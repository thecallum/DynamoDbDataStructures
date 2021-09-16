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
    public class GetNoteByIdUseCase : IGetNoteByIdUseCase
    {
        private NotesDbGateway _notesGateway;

        public GetNoteByIdUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<Note> Execute(Guid id)
        {
            var response = await _notesGateway.GetNoteById(id).ConfigureAwait(false);
            if (response == null) return null;

            return response;
        }
    }
}
