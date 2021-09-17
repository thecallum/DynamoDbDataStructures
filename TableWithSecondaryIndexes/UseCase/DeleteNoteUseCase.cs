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
    public class DeleteNoteUseCase : IDeleteNoteUseCase
    {
        private NotesDbGateway _notesGateway;

        public DeleteNoteUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<Note> Execute(Guid noteId, Guid accountId)
        {
            var response = await _notesGateway.DeleteNote(noteId, accountId).ConfigureAwait(false);
            // returns null if not found

            return response;
        }
    }
}
