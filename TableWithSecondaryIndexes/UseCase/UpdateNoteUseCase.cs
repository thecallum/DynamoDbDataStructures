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
    public class UpdateNoteUseCase : IUpdateNoteUseCase
    {
        private NotesDbGateway _notesGateway;

        public UpdateNoteUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<Note> Execute(Guid noteId, Guid accountId, Note newNote)
        {
            var response = await _notesGateway.UpdateNote(noteId, accountId, newNote).ConfigureAwait(false);
            // returns null if not found

            return response;
        }
    }
}
