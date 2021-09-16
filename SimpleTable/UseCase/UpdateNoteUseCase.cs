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
    public class UpdateNoteUseCase : IUpdateNoteUseCase
    {
        private NotesDbGateway _notesGateway;

        public UpdateNoteUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<Note> Execute(Guid id, Note newNote)
        {
            var response = await _notesGateway.UpdateNote(id, newNote).ConfigureAwait(false);
            // returns null if not found

            return response;
        }
    }
}
