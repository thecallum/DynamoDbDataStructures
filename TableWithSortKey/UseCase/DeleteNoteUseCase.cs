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
    public class DeleteNoteUseCase : IDeleteNoteUseCase
    {
        private NotesDbGateway _notesGateway;

        public DeleteNoteUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<Note> Execute(Guid id)
        {
            var response = await _notesGateway.DeleteNote(id).ConfigureAwait(false);
            // returns null if not found

            return response;
        }
    }
}
