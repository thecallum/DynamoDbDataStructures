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
